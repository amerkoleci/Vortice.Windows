using System;
using System.Runtime.InteropServices;
using Serilog;
using Vortice.Animation;
using Vortice.DirectComposition;
using Vortice.Mathematics;

namespace HelloComposition
{
    public sealed partial class CompositionVisualPosition
    {
        /// <summary>
        /// Delegate which verifies the proposed variable value and modifies it as necessary. If the constraint still can't be satisfied - aborts the operation.
        /// </summary>
        /// <param name="args">Event args with variable value</param>
        /// <returns>true if the point now satisfies the constraint, false means operation abort</returns>
        public delegate void VariableConstraintAgent(ref VariableConstraintAgentArgs args);

        public ref struct VariableConstraintAgentArgs
        {
            private readonly Span<PointF> _value;

            internal VariableConstraintAgentArgs(ref PointF value)
            {
                _value = MemoryMarshal.CreateSpan(ref value, 1);
                Cancel = false;
            }

            public ref PointF Value => ref _value[0];
            public bool Cancel;
        }

        public event VariableConstraintAgent ConstraintAgents;

        private void InvokeConstraintAgents(ref VariableConstraintAgentArgs args)
        {
            var agents = ConstraintAgents?.GetInvocationList();
            if (agents is null || agents.Length == 0) return;

            foreach (var agentDelegate in agents)
            {
                if (agentDelegate is VariableConstraintAgent agent)
                    agent(ref args);

                if (args.Cancel)
                    break;
            }
        }

        private bool TryApplyValue(PointF value)
        {
            if (_visual is null || _graphicsDevice is null || _x is null || _y is null)
                return false;
            
            var args = new VariableConstraintAgentArgs(ref value);
            InvokeConstraintAgents(ref args);

            var (newX, newY) = args.Value;
            
            if (args.Cancel)
            {
                Log.Verbose("[Visual] ConstraintAgents [{X};{Y}] -> Cancel", newX, newY);
                return false;
            }

            if (!(AnimationCommit is null))
            {
                Log.Warning("[Visual] AnimationCommit was not committed when creating next commit request");
                AnimationCommit.Dispose();
                AnimationCommit = null;
            }

            AnimationCommit = new CompositionVisualPositionAnimationCommit(this)
            {
                XComposition = new IDCompositionAnimation(_graphicsDevice.CompositionDevice),
                YComposition = new IDCompositionAnimation(_graphicsDevice.CompositionDevice),
                Storyboard = new IUIAnimationStoryboard2(_graphicsDevice.AnimationManager)
            };

            using var xTransition = CreateAnimation(_targetPoint.X, newX);
            AnimationCommit.Storyboard.AddTransition(_x, xTransition);
            _targetPoint.X = newX;

            using var yTransition = CreateAnimation(_targetPoint.Y, newY);
            AnimationCommit.Storyboard.AddTransition(_y, yTransition);
            _targetPoint.Y = newY;

            _visual.OnPositionAnimationInvalidated();
            
            return true;
        }
    }
}
