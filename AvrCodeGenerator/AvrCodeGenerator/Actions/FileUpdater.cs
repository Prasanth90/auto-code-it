using System.Collections.Generic;
using System.IO;
using CodeWizard.DataModel;
using CodeWizard.Plugins.CodeGeneration;
using Company.AvrCodeGenerator.Utils;
using Company.AvrCodeGenerator.ViewModel.CodeWizardViewModel;

namespace Company.AvrCodeGenerator.Actions
{
    public class FileUpdater : ICodeGenerationAction
    {
        private readonly ProjectData _projectData;
        private readonly CodeWizardViewModel _codeWizardViewModel;

        public FileUpdater(ProjectData projectData, CodeWizardViewModel codeWizardViewModel)
        {
            _projectData = projectData;
            _codeWizardViewModel = codeWizardViewModel;
        }

        public void Run()
        {
            var codegen = new CodeGenerator();
            var enabledModules = GetEnabledModules();
            string generatedCode = codegen.GetGeneratedCode(enabledModules);
            _codeWizardViewModel.Code = generatedCode;
            var project = ProjectUtilities.GetProject(_projectData.Name);
            var projectDir = Path.GetDirectoryName(project.FullName);
            var mainFilePath = Path.Combine(projectDir, "src\\main.c");
            if (File.Exists(mainFilePath))
            {
                using (var streamWriter = new StreamWriter(mainFilePath))
                {
                    streamWriter.Write(generatedCode);
                }
            }
        }

        private List<string> GetEnabledModules()
        {
            var enabledModules = new List<string>();
            if (_codeWizardViewModel.McuPeripheralsViewModel != null && _codeWizardViewModel.McuPeripheralsViewModel.PeripheralViewModels != null)
            {
                var phViewModel = _codeWizardViewModel.McuPeripheralsViewModel.PeripheralViewModels;
                foreach (var peripheralViewModel in phViewModel)
                {
                    foreach (var childrenPeripheral in peripheralViewModel.ChildrenPeripherals)
                    {
                        if (childrenPeripheral.IsModuleEnabled)
                        {
                            enabledModules.Add(childrenPeripheral.Name);
                        }
                    }
                }
            }
            return enabledModules;
        }
    }
}
