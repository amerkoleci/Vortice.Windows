using System;
using SharpGen.Runtime;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Serilog;
using Vortice.Direct2D1;
using Vortice.Mathematics;

namespace HelloComposition
{
    public sealed class CompositionDemoVisual : CompositionVisual
    {
        private const string RectangleBrushName = "RectangleBrush";
        private const int _windowMargin = 20;

        public CompositionDemoVisual([DisallowNull] CompositionGraphicsDevice device) : base(device, 40, 40)
        {
            Position.ConstraintAgents += PositionConstraintAgent;
            
            // Enable this when https://github.com/SharpGenTools/SharpGenTools/pull/161 yields some input
            // Position.AnimationFactories += PositionAnimationFactory;
            
            Device.Schedule(ApplyImpulse);
        }

        private void PositionAnimationFactory(ref CompositionVisualPosition.AnimationFactoryArgs args)
        {
            args.Transition = AnimationImpulseInterpolator.CreateTransition(
                Device.AnimationTransitionFactory,
                args.TargetValue,
                1.0
            );
        }

        private void PositionConstraintAgent(ref CompositionVisualPosition.VariableConstraintAgentArgs args)
        {
            var size = Device.MainWindow.ClientSize;

            ref var point = ref args.Value;
            if (point.X < 0)
                point.X = 0;
            if (point.Y < 0)
                point.Y = 0;
            var windowWidth = size.Width - Width;
            var windowHeight = size.Height - Height;
            if (point.X >= windowWidth)
                point.X = windowWidth;
            if (point.Y >= windowHeight)
                point.Y = windowHeight;
        }

        private void ApplyImpulse()
        {
            const double pi = Math.PI;
            var (width, height) = Device.MainWindow.ClientSize;

            if (width >= 10 && height >= 10)
            {
                var r = NextDouble(20, 200);

                var phiExclusionRanges = new List<(double, double)>();

                var (currentX, currentY) = Position.TargetPoint;
                if (currentY < _windowMargin)
                    phiExclusionRanges.Add((pi / 6, 5 * pi / 6));
                if (currentX < _windowMargin)
                    phiExclusionRanges.Add((2 * pi / 3, 4 * pi / 3));
                if (currentY >= height - Height - _windowMargin)
                    phiExclusionRanges.Add((7 * pi / 6, 11 * pi / 6));
                if (currentX >= width - Width - _windowMargin)
                {
                    phiExclusionRanges.Add((0, pi / 3));
                    phiExclusionRanges.Add((5 * pi / 3, 2 * pi));
                }

                if (phiExclusionRanges.Count != 0)
                    Log.Verbose(
                        "[Demo] Impulse restricted angles: {Angles} for [{X};{Y}]",
                        phiExclusionRanges,
                        currentX, currentY
                    );

                uint attempts = 0;
                bool retryRandom;
                double phi;
                do
                {
                    ++attempts;
                    phi = NextDouble(0, 2 * pi);
                    retryRandom = false;

                    foreach (var (from, to) in phiExclusionRanges)
                    {
                        if (phi >= from && phi <= to)
                            retryRandom = true;
                    }
                } while (retryRandom && attempts <= 5);

                if (!retryRandom)
                {
                    var (x, y) = (r * Math.Cos(phi), r * Math.Sin(phi));
                    Position.AnimateToRelativeOffset(new PointF((float)x, (float)y));
                }
            }

            Device.Schedule(TimeSpan.FromMilliseconds(NextDouble(500, 1500)), ApplyImpulse);
        }

        public double NextDouble(double minValue, double maxValue)
        {
            return Device.Random.NextDouble() * (maxValue - minValue) + minValue;
        }

        protected override void DrawImpl(ID2D1DeviceContext context, IReadOnlyDictionary<string, ComObject> resources,
            Point offset)
        {
            if (!resources.TryGetValue(RectangleBrushName, out var brushObject))
            {
                ResourcesMissing = true;
                return;
            }

            if (!(brushObject is ID2D1Brush brush))
            {
                return;
            }

            context.DrawRectangle(new RectangleF(offset.X, offset.Y, 40, 40), brush);
        }

        public override void CreateResources(IDictionary<string, ComObject> resources, ID2D1DeviceContext context)
        {
            Log.Verbose("[Demo] CreateResources [{Id}]", RuntimeHelpers.GetHashCode(this));
            var clearColor = new Color4(0.8f, 0.8f, 1f, 1.0f);
            resources[RectangleBrushName] = context.CreateSolidColorBrush(clearColor);
        }
    }
}
