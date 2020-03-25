using System;
using Serilog;
using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;

namespace HelloComposition
{
    internal class Program
    {
        static void Main()
        {
            var validation = false;
#if DEBUG
            Configuration.EnableObjectTracking = true;
            validation = true;
#endif
            
            Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Debug().CreateLogger();

            using (var app = new CompositionGraphicsDevice(validation))
            {
                for (var i = 0; i < 50; i++)
                {
                    app.AddVisual(new CompositionDemoVisual(app));                    
                }
                
                app.Run();
            }

#if DEBUG
            Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
        }
    }
}
