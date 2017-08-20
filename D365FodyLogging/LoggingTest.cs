using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace D365FodyLogging
{
    public class LoggingTest : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            #region
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            #endregion

            MethodTimeLogger.Tracer = tracer;
            
            try
            {
                // What manually setting up method timing might look like
                ManualTimer(tracer);
     
                // What using IL weaving to inject logging looks like
                IlWeavngTimer();

                // Used in another class
                OtherStuff.DoWork();
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        [Time]
        private void IlWeavngTimer()
        {
            // Do something.
            Thread.Sleep(5000);
        }

        private void ManualTimer(ITracingService tracer)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            // Do something.
            Thread.Sleep(5000);

            // Stop timing.
            stopwatch.Stop();

            // Get the current method name
            var methodName = MethodBase.GetCurrentMethod().Name;

            // Write result.
            tracer.Trace($"{methodName} Method Time elapsed: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}