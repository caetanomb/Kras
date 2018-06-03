using FileLibrary;
using FileLibrary.Domain.Exception;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileLibraryCLI
{
    public class CLISystem
    {
        public enum FilesTypes
        {
            Text,
            XML,
            JSON
        }

        public enum FileData
        {
            Encrypted,
            Decrypted
        }

        public enum ReaderType
        {
            TextFile,
            TextFileEncrypt,
            TextFileEncryptRoleBased,
            TextFileRoleBased,

            XmlFile,
            XmlFileEncrypt,
            XmlFileEncryptRoleBased,
            XmlFileRoleBased,

            JsonFile,
            JsonFileEncrypt,
            JsonFileEncryptRoleBased,
            JsonFileRoleBased
        }

        private const string TextFile = "Content.txt";
        private const string TextFileEncrypt = "EncryptedContent.txt";
        private const string TextFileEncryptRoleBased = "EncryptedContentRoleBasedEmployee.txt";
        private const string TextFileRoleBased = "ContentRoleBasedVisitor.txt";

        private const string XmlFile = "ContentXML.xml";
        private const string XmlFileEncrypt = "EncryptedContentXML.xml";
        private const string XmlFileEncryptRoleBased = "EncryptedContentXMLRoleBasedEmployee.xml";
        private const string XmlFileRoleBased = "ContentXMLRoleBasedVisitor.xml";

        private const string JsonFile = "ContentJson.json";
        private const string JsonFileEncrypt = "EncryptedContentJson.json";
        private const string JsonFileEncryptRoleBased = "EncryptedContentJsonRoleBasedEmployee.json";
        private const string JsonFileRoleBase = "ContentJsonRoleBasedVisitor.json";

        private Mock<IUserAuthorizationService> _mockUserAuthorizationService;
        private Mock<IFileRoleValidationService> _mockFileRoleValidationService;

        private const string basePath = @"E:\Waes\Kras\Src\FileLibrary\FileLibraryCLI\Files";
        private const string textFileDirectory = "Text";
        private const string xmlFileDirectory = "Xml";
        private const string jsonFileDirectory = "Json";        

        //Menus
        public void ShowMainMenu()
        {
            IFileReader reader;
            string role;
            string resultRead;

            while (true)
            {
                Console.WriteLine("\n\n\t *** Kras Assessment *** \n");
                Console.WriteLine("\t Developer: Caetano Marques Bruno \n");
                Console.WriteLine("\t Platform: Microsoft C# .Net Core \n");
                Console.WriteLine("\t Internal files: \n");
                Console.WriteLine(PrintAvailableFiles());
                Console.WriteLine("Which file type do you want to read?");
                Console.WriteLine("  A - Text");
                Console.WriteLine("  B - XML");
                Console.WriteLine("  C - JSON");
                Console.WriteLine(" -------------------------------------------------------------");
                Console.WriteLine("3 - Clear");
                Console.WriteLine("4 - Exit");

                role = string.Empty;
                resultRead = string.Empty;
                string input = Console.ReadLine();

                switch (input)
                {
                    case "A":
                    case "a":
                        try
                        {
                            reader = SubMenuEncryptSystem(FilesTypes.Text);

                            ExecuteReaderAndPrint(reader);
                        }                        
                        catch (Exception ex)
                        {
                            Console.WriteLine(" -------------------------------------------------------------");
                            Console.WriteLine("Error: " + ex.Message);
                        }

                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                        break;
                    case "B":
                    case "b":
                        try
                        {
                            reader = SubMenuEncryptSystem(FilesTypes.XML);

                            ExecuteReaderAndPrint(reader);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(" -------------------------------------------------------------");
                            Console.WriteLine("Error: " + ex.Message);
                        }

                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                        break;
                    case "C":
                    case "c":
                        try
                        {
                            reader = SubMenuEncryptSystem(FilesTypes.JSON);

                            ExecuteReaderAndPrint(reader);
                        }
                        catch (Exception ex)
                        {
                            PrintExceptionError(ex);                            
                        }

                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        break;
                    case "4":
                        Environment.Exit(Environment.ExitCode);
                        continue;
                    default:
                        Console.WriteLine("Invalid option!");
                        continue;
                }
            }
        }

        private void ExecuteReaderAndPrint(IFileReader reader)
        {
            string role;
            string resultRead;
            try
            {
                if (reader is IFileReaderRoleBased)
                {
                    Console.WriteLine(" -------------------------------------------------------------");
                    Console.WriteLine("Enter the role");
                    role = Console.ReadLine();
                    resultRead = ((IFileReaderRoleBased)reader).Read(role);
                }
                else
                {
                    resultRead = reader.Read();
                }
                                                
                PrintResultRead(resultRead);
            }
            catch (FileSecurityException ex)
            {
                PrintExceptionError(ex);
                SubMenuRetryRole(reader);
            }
        }

        private void PrintExceptionError(Exception ex)
        {
            Console.WriteLine(" -------------------------------------------------------------");
            Console.WriteLine("Error: " + ex.Message);
        }

        private void SubMenuRetryRole(IFileReader reader)
        {
            while (true)
            {
                Console.WriteLine(" -------------------------------------------------------------");
                Console.WriteLine(" Do you want to try again with another role?");
                Console.WriteLine("  1 - Yes");
                Console.WriteLine("  2 - No");
                var tryAgain = Console.ReadLine();

                switch (tryAgain)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("Enter the role");
                            var role = Console.ReadLine();
                            string resultRead = ((IFileReaderRoleBased)reader).Read(role);

                            PrintResultRead(resultRead);
                            return;
                        }
                        catch (FileSecurityException ex)
                        {
                            PrintExceptionError(ex);
                            break;                            
                        }
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        continue;
                }
            }            
        }

        private string PrintAvailableFiles()
        {
            string[] foundDirectories = Directory.GetDirectories(basePath);
            string[] foundFiles;
            StringBuilder files = new StringBuilder();
            string path;

            foreach (string directory in foundDirectories)
            {                
                foundFiles = Directory.GetFiles(directory);
                foreach (var fileName in foundFiles)
                {
                    path = Path.Combine(directory, fileName);
                    files.AppendLine(path);
                }                
            }

            return files.ToString();
        }

        public IFileReader SubMenuEncryptSystem(FilesTypes fileType)
        {
            IFileReader reader;

            while (true)
            {
                Console.WriteLine("Do you want to use encryption system?");
                Console.WriteLine("  1 - Yes");
                Console.WriteLine("  2 - No");
                string inputEncryptSystem = Console.ReadLine();

                switch (inputEncryptSystem)
                {
                    case "1":
                        reader = UseRoleBaseSystem(fileType, FileData.Encrypted);

                        if (reader != null)
                            return reader;

                        if (fileType == FilesTypes.Text)
                            return GenerateReaderTextFileEncrypt();
                        else if (fileType == FilesTypes.XML)
                            return GenerateReaderXmlFileEncrypt();
                        else
                            return GenerateReaderJsonFileEncrypt();

                    case "2":
                        reader = UseRoleBaseSystem(fileType, FileData.Decrypted);

                        if (reader != null)
                            return reader;

                        if (fileType == FilesTypes.Text)
                            return GenerateReaderTextFile();
                        else if (fileType == FilesTypes.XML)
                            return GenerateReaderXmlFile();
                        else
                            return GenerateReaderJsonFile();
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }

        public IFileReader UseRoleBaseSystem(FilesTypes fileType, FileData fileData)
        {
            while (true)
            {
                Console.WriteLine("Do you want to use role based security system?");
                Console.WriteLine("  1 - Yes");
                Console.WriteLine("  2 - No");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        if (fileType == FilesTypes.Text)
                        {
                            if (fileData == FileData.Decrypted)
                                return Factory(ReaderType.TextFileRoleBased);
                            else
                                return Factory(ReaderType.TextFileEncryptRoleBased);
                        }
                        else if (fileType == FilesTypes.XML)
                        {
                            if (fileData == FileData.Decrypted)
                                return Factory(ReaderType.XmlFileRoleBased);
                            else
                                return Factory(ReaderType.XmlFileEncryptRoleBased);
                        }
                        else
                        {
                            if (fileData == FileData.Decrypted)
                                return Factory(ReaderType.JsonFileRoleBased);
                            else
                                return Factory(ReaderType.JsonFileEncryptRoleBased);
                        }
                    case "2":
                        if (fileType == FilesTypes.Text)
                        {
                            if (fileData == FileData.Decrypted)
                                return Factory(ReaderType.TextFile);
                            else
                                return Factory(ReaderType.TextFileEncrypt);
                        }
                        else if (fileType == FilesTypes.XML)
                        {
                            if (fileData == FileData.Decrypted)
                                return Factory(ReaderType.XmlFile);
                            else
                                return Factory(ReaderType.XmlFileEncrypt);
                        }
                        else
                        {
                            if (fileData == FileData.Decrypted)
                                return Factory(ReaderType.JsonFile);
                            else
                                return Factory(ReaderType.JsonFileEncrypt);
                        }                        
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }

        //Factory
        public IFileReader Factory(ReaderType readerType)
        {
            switch (readerType)
            {
                //Text
                case ReaderType.TextFile:
                    return GenerateReaderTextFile();

                case ReaderType.TextFileEncrypt:
                    return GenerateReaderTextFileEncrypt();                

                case ReaderType.TextFileRoleBased:
                    return GenerateReaderTextFileRoleBased();

                case ReaderType.TextFileEncryptRoleBased:
                    return GenerateReaderTextFileEncryptRoleBased();

                //XML
                case ReaderType.XmlFile:
                    return GenerateReaderXmlFile();

                case ReaderType.XmlFileEncrypt:
                    return GenerateReaderXmlFileEncrypt();                

                case ReaderType.XmlFileRoleBased:
                    return GenerateReaderXmlFileRoleBased();

                case ReaderType.XmlFileEncryptRoleBased:
                    return GenerateReaderXmlFileEncryptRoleBased();

                //Json
                case ReaderType.JsonFile:
                    return GenerateReaderJsonFile();

                case ReaderType.JsonFileEncrypt:
                    return GenerateReaderJsonFileEncrypt();                

                case ReaderType.JsonFileRoleBased:
                    return GenerateReaderJsonFileRoleBased();

                case ReaderType.JsonFileEncryptRoleBased:
                    return GenerateReaderJsonFileEncryptRoleBased();
                default:
                    return null;
            }
        }
        
        //Readers XML
        private IFileReader GenerateReaderXmlFileRoleBased()
        {
            string[] userRoles = new string[] { "Visitor" };

            ConfigureMocks(userRoles);

            return new XmlFileReaderRoleBased(Path.Combine(basePath, xmlFileDirectory), XmlFileRoleBased,
                _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
        }

        private IFileReader GenerateReaderXmlFileEncryptRoleBased()
        {            
            string[] userRoles = new string[] { "Employee" };

            ConfigureMocks(userRoles);

            IDecryptDataService decryptService = GenerateDecryptServiceReverseString();

            return new XmlFileReaderRoleBased(Path.Combine(basePath, xmlFileDirectory), XmlFileEncryptRoleBased,
                _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object, decryptService);
        }

        private IFileReader GenerateReaderXmlFileEncrypt()
        {            
            IDecryptDataService decryptService = GenerateDecryptServiceReverseString();

            return new XmlFileReader(Path.Combine(basePath, xmlFileDirectory),
                XmlFileEncrypt, decryptService);
        }
        
        private IFileReader GenerateReaderXmlFile()
        {            
            return new XmlFileReader(Path.Combine(basePath, xmlFileDirectory), XmlFile);
        }

        //Readers Text
        public IFileReader GenerateReaderTextFile()
        {                     
            return new TextFileReader(Path.Combine(basePath, textFileDirectory), TextFile);
        }

        public IFileReader GenerateReaderTextFileEncrypt()
        {            
            IDecryptDataService decryptService = GenerateDecryptServiceReverseString();

            return new TextFileReader(Path.Combine(basePath, textFileDirectory),
                TextFileEncrypt, decryptService);
        }

        public IFileReader GenerateReaderTextFileRoleBased()
        {
            string[] userRoles = new string[] { "Visitor" };

            ConfigureMocks(userRoles);

            return new TextFileReaderRoleBased(Path.Combine(basePath, textFileDirectory), TextFileRoleBased,
                _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
        }

        public IFileReader GenerateReaderTextFileEncryptRoleBased()
        {
            string[] userRoles = new string[] { "Employee" };

            ConfigureMocks(userRoles);

            IDecryptDataService decryptService = GenerateDecryptServiceReverseString();

            return new TextFileReaderRoleBased(Path.Combine(basePath, textFileDirectory), TextFileEncryptRoleBased,
                _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object, decryptService);
        }

        //Readers Json
        private IFileReader GenerateReaderJsonFile()
        {
            return new JsonFileReader(Path.Combine(basePath, jsonFileDirectory), JsonFile);
        }

        private IFileReader GenerateReaderJsonFileEncrypt()
        {
            IDecryptDataService decryptService = GenerateDecryptServiceBase64();

            return new JsonFileReader(Path.Combine(basePath, jsonFileDirectory),
                JsonFileEncrypt, decryptService);
        }

        private IFileReader GenerateReaderJsonFileRoleBased()
        {
            string[] userRoles = new string[] { "Visitor" };

            ConfigureMocks(userRoles);

            return new JsonFileReaderRoleBased(Path.Combine(basePath, jsonFileDirectory), JsonFileRoleBase,
                _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
        }

        private IFileReader GenerateReaderJsonFileEncryptRoleBased()
        {
            string[] userRoles = new string[] { "Employee" };

            ConfigureMocks(userRoles);

            IDecryptDataService decryptService = GenerateDecryptServiceBase64();

            return new JsonFileReaderRoleBased(Path.Combine(basePath, jsonFileDirectory), JsonFileEncryptRoleBased,
                _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object, decryptService);
        }
               
        //Mocks - Data Decrypt services
        private IDecryptDataService GenerateDecryptServiceReverseString()
        {
            //Mock DecryptService Service
            var mockDecryptService = new Mock<IDecryptDataService>();
            mockDecryptService
                .Setup(mock => mock.DecryptData(It.IsAny<string>()))
                .Returns((string param) =>
                {
                    StringBuilder stringBuilder =
                        new StringBuilder(param);

                    char[] array = stringBuilder.ToString().ToCharArray();
                    Array.Reverse(array);

                    return new string(array);
                });

            return mockDecryptService.Object;
        }

        private IDecryptDataService GenerateDecryptServiceBase64()
        {
            //Mock DecryptService Service
            var mockDecryptService = new Mock<IDecryptDataService>();
            mockDecryptService
                .Setup(mock => mock.DecryptData(It.IsAny<string>()))
                .Returns((string param) =>
                {
                    string content = Encoding.UTF8.GetString(Convert.FromBase64String(param));
                    string[] splittedContent = content.Split("\r\n");

                    content = string.Empty;
                    foreach (var item in splittedContent)
                    {
                        content += item.Trim();
                    }

                    return content;
                });

            return mockDecryptService.Object;
        }

        private void ConfigureMocks(string[] loggedUserRoles)
        {
            //Mock IUserAuthorizationService
            _mockUserAuthorizationService = new Mock<IUserAuthorizationService>();
            _mockUserAuthorizationService
                .Setup(mock => mock.AuthorizeUser(It.IsAny<string>()))
                .Returns((string requiredRole) =>
                {
                    if (!loggedUserRoles.Contains(requiredRole))
                        return false; //User does not have the role informed

                    return true;
                });

            //Mock IFileRoleValidationService
            _mockFileRoleValidationService = new Mock<IFileRoleValidationService>();
            _mockFileRoleValidationService
                .Setup(mock => mock.Validate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string fileRole, string informedUserRole) =>
                {
                    if (!string.Equals(fileRole.ToLower(), informedUserRole.ToLower()))
                        return false; //User informed role does not match with file role

                    return true;
                });
        }

        public void PrintResultRead(string resultRead)
        {
            Console.WriteLine(" -------------------------------------------------------------");
            Console.WriteLine("File content: ");
            Console.WriteLine(resultRead);
        }
    }
}
