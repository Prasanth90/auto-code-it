using System.Collections.Generic;
using System.Linq;
using Atmel.Studio.Services;
using CodeWizard.DataModel;
using CodeWizard.PluginManager;
using Company.AvrCodeGenerator.Utils;
using Company.AvrCodeGenerator.ViewModel.CodeWizardViewModel;

namespace Company.AvrCodeGenerator.Actions
{
    public class AsfModuleAdder : ICodeGenerationAction
    {
        private readonly ProjectData _projectData;
        private readonly CodeWizardViewModel _codeWizardViewModel;
        private const string ContentId = "Atmel.ASF";
        private readonly List<string> _moduleIdList = new List<string>();
        private string Status;

        public AsfModuleAdder(ProjectData projectData, CodeWizardViewModel codeWizardViewModel)
        {
            _projectData = projectData;
            _codeWizardViewModel = codeWizardViewModel;
            var plugins = PluginManager.GeneralPlugins;
            var enabledModules = GetEnabledModules();
            foreach (var codeWizardPlugin in plugins)
            {
                _moduleIdList.AddRange(codeWizardPlugin.CodeGenerator().GetAsfModuleIds(enabledModules));
            }
        }


        public string StatusMessage()
        {
           return "Adding Asf Modules";
        }

        public void Run()
        {
            var project = ProjectUtilities.GetProject(_projectData.Name);
            var service = ATServiceProvider.AsfService;
            if (service != null)
            {
                string templateId = GetUserBoardTemplate(_projectData.Device);
                service.AddAsfUserBoardTemplate(project, templateId, ContentId, "3.11.0");
                foreach (var moduleId in _moduleIdList)
                {
                    if(!string.IsNullOrEmpty(moduleId))
                    service.AddModule(project,ContentId,moduleId,"","3.11.0");
                }
            }
        }


        private string GetUserBoardTemplate(string device)
        {
            if (device.ToLower().EndsWith("u"))
            {
                return "common.applications.user_application.user_board.xmegaau";
            }
            var count = device.Count();
            return string.Format("common.applications.user_application.user_board.xmega{0}", device.ToLower()[count-2]);
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
