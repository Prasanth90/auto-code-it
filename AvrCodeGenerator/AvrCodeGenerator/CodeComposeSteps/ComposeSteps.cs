using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeWizard.DataModel;
using Company.AvrCodeGenerator.Actions;
using Company.AvrCodeGenerator.ViewModel.CodeWizardViewModel;

namespace Company.AvrCodeGenerator.CodeComposeSteps
{
    public class ComposeSteps
    {
        private readonly ProjectData _projectData;
        private readonly CodeWizardViewModel _codeWizardViewModel;

        public ComposeSteps(ProjectData projectData, CodeWizardViewModel codeWizardViewModel)
        {
            _projectData = projectData;
            _codeWizardViewModel = codeWizardViewModel;
        }

        public void Run()
        {
            try
            {
                IEnumerable<ICodeGenerationAction> steps = GetCodeComposeSteps();
                foreach (var codeGenerationAction in steps)
                {
                    codeGenerationAction.Run();
                }
            }
            catch (Exception e)
            {
                //TODO: Log error somewhere
            }

        }

        private List<ICodeGenerationAction> GetCodeComposeSteps()
        {
            List<ICodeGenerationAction> actions = new List<ICodeGenerationAction>();
            actions.Add(new ProjectCreator(_projectData));
            actions.Add(new AsfModuleAdder(_projectData,_codeWizardViewModel));
            actions.Add(new FileUpdater(_projectData, _codeWizardViewModel));
            return actions;
        }
    }
}
