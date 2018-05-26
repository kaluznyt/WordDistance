using System;
using System.Collections.Generic;
using ManyConsole;

namespace WordDistance
{
    public class ConsoleCommandDispatcherWrapper
    {
        public IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }

        public int RunCommands(string[] args)
        {
            var commands = GetCommands();
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}