using FileLibrary;
using System;

namespace FileLibraryCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            try
            {
                XmlFileReader xmlFileReader =
                new XmlFileReader(@"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain",
                "ContentXML");

                var te = xmlFileReader.Read();
                Console.WriteLine(te);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
    }
}
