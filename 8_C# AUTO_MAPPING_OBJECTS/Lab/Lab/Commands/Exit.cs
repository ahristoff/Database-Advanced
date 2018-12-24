using System;

namespace Forum.App.Commands
{
    public class Exit : Contracts.ICommand
    {       
        public string Execute(params string[] arguments)
        {
            Environment.Exit(0); 
            return string.Empty;
        }
    }
}
