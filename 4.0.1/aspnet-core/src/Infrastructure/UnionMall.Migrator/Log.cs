using System;
using Castle.Core.Logging;
using Abp.Dependency;
using Abp.Timing;

namespace UnionMall.Migrator
{
    public class Log : ITransientDependency
    {
       // public ILogger Logger { get; set; }

        public Log()
        {
           // Logger = NullLogger.Instance;
        }

        public void Write(string text)
        {

        }
    }
}
