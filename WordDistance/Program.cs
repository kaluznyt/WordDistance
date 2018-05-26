using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordDistance
{
    class Program
    {
        static int Main(string[] args)
        {
            var commandDispatcher = new ConsoleCommandDispatcherWrapper();

            return commandDispatcher.RunCommands(args);
        }
    }
}
