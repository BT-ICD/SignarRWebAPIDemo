using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.HubFilter
{
    public class CustomHubFilter: IHubFilter
    {
        public CustomHubFilter()
        {

        }
        public async  ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
        {
            try
            {
                return await next(invocationContext);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext,
        //                                                  Func<HubInvocationContext,
        //                                                  ValueTask<object?>> next)
        //{
        //    //var request = invocationContext.HubMethodArguments[0] as QuoteRequest;

        //    try
        //    {
        //        //if (request != null)
        //        //    Console.WriteLine($"{invocationContext.Context.ConnectionId} has selected {request.NewSymbol}.");

        //        return await next(invocationContext);
        //    }
        //    catch (Exception exc)
        //    {
        //        //if (request != null)
        //        //    Console.WriteLine($"Error switching symbol to '{request.NewSymbol}': {exc}");

        //        throw;
        //    }
        //}
        // Optional method
        //public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        //{
        //    return next(context);
        //}

        // Optional method
        //public Task OnDisconnectedAsync(
        //    HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
        //{
        //    return next(context, exception);
        //}
    }
}
