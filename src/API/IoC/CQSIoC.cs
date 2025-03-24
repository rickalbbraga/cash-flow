using System.Reflection;
using Domain.Interfaces.CQS;

namespace API.IoC
{
    public static class CQSIoC
    {
        public static void AddCQSConfigurations(this IServiceCollection serviceCollection)
        {
            var types = new Type[]
            {
            typeof(ICommandHandlerWithTResult<,>),
            typeof(ICommandHandlerWithoutTResult<>),
            typeof(IQueryHandlerWithTResult<,>),
            typeof(IQueryHandlerWithTResultList<,>)  
            };

            var layers = Assembly.GetExecutingAssembly().FullName?.Replace("API", "Application");

            var handlers = Assembly
                                .Load(layers!)
                                .GetTypes()
                                .Where(t => 
                                    t.GetInterfaces()
                                     .ToList()
                                     .Exists(i => 
                                        i.IsGenericType && 
                                        types.Contains(i.GetGenericTypeDefinition())));

            foreach (var handler in handlers)
            {
                serviceCollection.AddScoped(handler.GetInterfaces()[0], handler);
            }
        }
    }
}