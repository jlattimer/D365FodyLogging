using Microsoft.Xrm.Sdk;
using System.Reflection;

internal static class MethodTimeLogger
{
    public static ITracingService Tracer;

    public static void Log(MethodBase methodBase, long milliseconds)
    {
        Tracer.Trace($"{methodBase.Name} Method Time elapsed: {milliseconds}ms");
    }
}