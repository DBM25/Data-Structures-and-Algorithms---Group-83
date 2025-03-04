using System;

namespace WebServiceManagementSystem
{
    public static class ExecutionTimeAnalysis
    {
        public static TimeSpan MeasureExecutionTime(Action action)
        {
            DateTime startTime = DateTime.Now;
            action();
            DateTime endTime = DateTime.Now;
            return endTime - startTime;
        }
    }
}
