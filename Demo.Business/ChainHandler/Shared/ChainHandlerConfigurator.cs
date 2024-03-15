using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Demo.Business.ChainHandler.Shared
{
    /// <summary>
    /// <see cref="https://stackoverflow.com/questions/55476378/how-to-inject-the-dependency-of-the-next-handler-in-a-chain-of-responsibility"/>
    /// </summary>
    internal static class ChainHandlerConfigurator
    {
        public interface IChainHandlerConfigurator<T>
        {
            IChainHandlerConfigurator<T> Add<TImplementation>() where TImplementation : T;
            void Configure();
        }

        public static IChainHandlerConfigurator<T> Chain<T>(this IServiceCollection services, object? key) where T : class
        {
            return new ChainConfiguratorImpl<T>(services, key);
        }

        private class ChainConfiguratorImpl<T>(IServiceCollection services, object? key) : IChainHandlerConfigurator<T> where T : class
        {
            private List<Type> types = new List<Type>();
            private Type interfaceType = typeof(T);

            public IChainHandlerConfigurator<T> Add<TImplementation>() where TImplementation : T
            {
                var type = typeof(TImplementation);

                types.Add(type);

                return this;
            }

            public void Configure()
            {
                if (types.Count == 0)
                    throw new InvalidOperationException($"No implementation defined for {interfaceType.Name}");

                foreach (var type in types)
                {
                    ConfigureType(type);
                }
            }

            private void ConfigureType(Type currentType)
            {
                // gets the next type, as that will be injected in the current type
                var nextType = types.SkipWhile(x => x != currentType).SkipWhile(x => x == currentType).FirstOrDefault();

                // Makes a parameter expression, that is the IServiceProvider x 
                var parameter = Expression.Parameter(typeof(IServiceProvider), "x");

                // get constructor with highest number of parameters. Ideally, there should be only 1 constructor, but better be safe.
                var ctor = currentType.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();

                // for each parameter in the constructor
                var ctorParameters = ctor.GetParameters().Select(p =>
                {
                    // check if it implements the interface. That's how we find which parameter to inject the next handler.
                    if (interfaceType.IsAssignableFrom(p.ParameterType))
                    {
                        if (nextType is null)
                        {
                            // if there's no next type, current type is the last in the chain, so it just receives null
                            return Expression.Constant(null, interfaceType);
                        }
                        else
                        {
                            // if there is, then we call IServiceProvider.GetRequiredService to resolve next type for us
                            return Expression.Call(typeof(ServiceProviderKeyedServiceExtensions), "GetRequiredKeyedService", [nextType], parameter, Expression.Constant(key));
                        }
                    }

                    // this is a parameter we don't care about, so we just ask GetRequiredService to resolve it for us 
                    return (Expression)Expression.Call(typeof(ServiceProviderServiceExtensions), "GetRequiredService", [p.ParameterType], parameter);
                });

                // cool, we have all of our constructors parameters set, so we build a "new" expression to invoke it.
                var body = Expression.New(ctor, ctorParameters.ToArray());

                // if current type is the first in our list, then we register it by the interface, otherwise by the concrete type
                var first = types[0] == currentType;
                var resolveType = first ? interfaceType : currentType;
                var expressionType = Expression.GetFuncType(typeof(IServiceProvider), resolveType);

                // finally, we can build our expression
                var expression = Expression.Lambda(expressionType, body, parameter);

                // compile it
                var compiledExpression = (Func<IServiceProvider, object>)expression.Compile();
                // and register it in the services collection as transient
                services.AddKeyedTransient(resolveType, key, (IServiceProvider provider, object? key) => { return compiledExpression.Invoke(provider); });



                //var nextType = types.SkipWhile(x => x != currentType).SkipWhile(x => x == currentType).FirstOrDefault();
                //services.AddKeyedTransient(currentType, key, (IServiceProvider provider, object? key) => { return provider.GetRequiredKeyedService(nextType, key); });
            }
        }
    }
}