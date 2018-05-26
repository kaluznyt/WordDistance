using Autofac;

namespace WordDistance
{
    class Program
    {
        static int Main(string[] args)
        {
            RegisterIoC();

            var commandDispatcher = new ConsoleCommandDispatcherWrapper();

            return commandDispatcher.RunCommands(args);
        }

        private static void RegisterIoC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HammingWordDistanceCalculator>().As<IWordDistanceCalculator>();
            builder.RegisterType<ShortestEditPathLister>().As<IWordPathLister>();

            builder.Build();
        }
    }
}
