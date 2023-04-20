using Castle.DynamicProxy;
using Core.CrossCuttingConcerns;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Entities.Exceptions;
using Core.Helpers;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly ISeriLogService _logger;
        private readonly IHttpAccessorHelper _httpAccessorHelper;

        public ExceptionLogAspect()
        {
            _logger = ServiceTool.ServiceProvider.GetService<ISeriLogService>();
            _httpAccessorHelper = ServiceTool.ServiceProvider.GetService<IHttpAccessorHelper>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            if (e.GetType() != typeof(ValidationException) && e.GetType() != typeof(NotFoundException))
            {
                LogDetailWithException logDetailWithException = GetLogDetail(invocation, e);

                _logger.Error(logDetailWithException, e, LogType.Aspect);
            }
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation, System.Exception e)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i]?.GetType().Name
                });
            }

            return new LogDetailWithException
            {
                LogType = LogType.Aspect.ToString(),
                Time = DateTime.Now,
                UserId = _httpAccessorHelper?.GetUserId(),
                MethodName = invocation.Method?.Name,
                ClassName = invocation.Method?.DeclaringType?.FullName,
                LogParameters = logParameters,
                ExceptionMessage = e.Message
            };
        }
    }
}