using Castle.DynamicProxy;
using Core.CrossCuttingConcerns;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Helpers;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly ISeriLogService _logger;
        private readonly IHttpAccessorHelper _httpAccessorHelper;

        public LogAspect()
        {
            _httpAccessorHelper = ServiceTool.ServiceProvider.GetService<IHttpAccessorHelper>();
            _logger = ServiceTool.ServiceProvider.GetService<ISeriLogService>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _logger.Info(GetLogDetail(invocation), LogType.Aspect);
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            return new LogDetail
            {
                LogType = LogType.Aspect.ToString(),
                Time = DateTime.Now,
                UserId = _httpAccessorHelper?.GetUserId(),
                MethodName = invocation.Method?.Name,
                ClassName = invocation.Method?.DeclaringType?.FullName,
                LogParameters = logParameters
            };
        }
    }
}