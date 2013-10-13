using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CodeWizard.Plugins.CodeGeneration
{

    /// <summary>
    /// This class represents the step that creates the raw project file contents
    /// which will undergo transformation at various steps.
    /// </summary>
    public class CreateRawInput
    {
        private readonly FilesContentStore _filesContentsStore;

        public CreateRawInput(FilesContentStore filesContentsStore)
        {
            _filesContentsStore = filesContentsStore;
        }

        /// <summary>
        /// Performs code generation task
        /// </summary>
        public  void LoadResourceFile()
        {
            _filesContentsStore.Clear();

            // Add the file contents to the project files contents store
            foreach (var fileName in FileNames.List)
            {
                // Get content for the specified file
                var fileContent = ReadFileContent(fileName);

                // Add the file content to file store
                _filesContentsStore.Add(fileName, fileContent);
            }
        }




        /// <summary>
        /// Returns the content of the specified file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string ReadFileContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CodeWizard.Plugins.Resources.Templates." + fileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            return string.Empty;
        }

    }

    public class FilesContentStore : Dictionary<string,string>
    {
        
    }

}
