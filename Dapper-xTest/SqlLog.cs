using Castle.DynamicProxy;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Dapper_xTest
{
    public class SqlLog : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            PropertyInfo property = invocation.TargetType.GetProperty("Connection");
            DbConnection conn = (DbConnection)property.GetValue(invocation.InvocationTarget);
            MiniProfiler mp = MiniProfiler.Current;
            if (mp == null)
            {
                mp = MiniProfiler.StartNew($"{invocation.TargetType}-{invocation.Method.Name}");
            }
            conn = new StackExchange.Profiling.Data.ProfiledDbConnection(conn, MiniProfiler.Current);
            property.SetValue(invocation.InvocationTarget, conn);
            invocation.Proceed();
            mp.Stop();
            if (mp.Root!=null)
            {
                if (mp.Root.CustomTimings.ContainsKey("sql"))
                {
                    List<CustomTiming> list = mp.Root.CustomTimings["sql"];
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}  {mp.Name}】");
                    foreach (var item in list)
                    {
                        if (item.ExecuteType == "Open" || item.ExecuteType == "Close")
                        {
                            continue;
                        }
                        sb.AppendLine($"{(item.Errored ? "【ERROR】" : "")}CommandString:{item.CommandString}\r\nExecuteType:{item.ExecuteType}\r\nDurationMilliseconds:{item.DurationMilliseconds}ms");
                    }
                    Console.WriteLine(sb);
                }
            }
           

        }
    }

    public class SqlProfiler {
        public Guid Id { get; set; }
        public string CommandString { get; set; }
        public string ExecuteType { get; set; }
        public string StackTraceSnippet { get; set; }
        public double StartMilliseconds { get; set; }
        public double DurationMilliseconds { get; set; }
        public double FirstFetchDurationMilliseconds { get; set; }
        public bool Errored { get; set; }



    }
}
