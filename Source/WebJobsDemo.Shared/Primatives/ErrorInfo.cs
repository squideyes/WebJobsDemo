using System;

namespace WebJobsDemo.Shared
{
    public class ErrorInfo<T> where T : class
    {
        public T Message { get; set; }
        public Exception Error { get; set; }
    }
}
