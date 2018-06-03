using FileLibrary;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using static FileLibraryCLI.CLISystem;

namespace FileLibraryCLI
{
    public class Program
    {                        
        static void Main(string[] args)
        {            
            CLISystem cliSystem = new CLISystem();
            cliSystem.ShowMainMenu();            
        }
    }
}
