using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeWizard.DataModel;
using Company.AvrCodeGenerator.Actions;
using Company.AvrCodeGenerator.ViewModel.CodeWizardViewModel;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Company.AvrCodeGenerator.CodeComposeSteps
{
    public class ComposeSteps
    {
        private readonly ProjectData _projectData;
        private readonly CodeWizardViewModel _codeWizardViewModel;
        private string _statusMessage = string.Empty;
        private uint _total;
        private uint _current;

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
                _total = (uint) steps.Count();
                _current = 0;
                 System.Threading.Tasks.Task.Factory.StartNew(ShowProgress);
                foreach (var codeGenerationAction in steps)
                {
                    _statusMessage = codeGenerationAction.StatusMessage();
                    ++_current;
                    codeGenerationAction.Run();
                }
            }
            catch (Exception e)
            {
                _current = _total;
                //TODO: Log error somewhere
            }

        }

        public void RunMainContents()
        {
            try
            {

                IEnumerable<ICodeGenerationAction> steps = GetCodeComposeStepsMainFile();
                _total = (uint)steps.Count();
                _current = 0;
                System.Threading.Tasks.Task.Factory.StartNew(ShowProgress);
                foreach (var codeGenerationAction in steps)
                {
                    _statusMessage = codeGenerationAction.StatusMessage();
                    ++_current;
                    codeGenerationAction.Run();
                }
            }
            catch (Exception e)
            {
                _current = _total;
                //TODO: Log error somewhere
            }

        }

        private void ShowProgress()
        {
            IVsStatusbar statusBar =(IVsStatusbar)Package.GetGlobalService(typeof(SVsStatusbar));
            uint cookie = 0;
            // Initialize the progress bar.
            statusBar.Progress(ref cookie, 1, "", 0, 0);

            while(_current<_total)
            {
                // Display incremental progress.
                statusBar.Progress(ref cookie, 1, _statusMessage, _current, _total);
                System.Threading.Thread.Sleep(10);
            }

            // Clear the progress bar.
            statusBar.Progress(ref cookie, 0, "", 0, 0);
        }

        private List<ICodeGenerationAction> GetCodeComposeSteps()
        {
            List<ICodeGenerationAction> actions = new List<ICodeGenerationAction>();
            actions.Add(new ProjectCreator(_projectData));
            actions.Add(new AsfModuleAdder(_projectData,_codeWizardViewModel));
            actions.Add(new FileUpdater(_projectData, _codeWizardViewModel));
            return actions;
        }

        private List<ICodeGenerationAction> GetCodeComposeStepsMainFile()
        {
            List<ICodeGenerationAction> actions = new List<ICodeGenerationAction>();
            actions.Add(new MainFileContentCreator(_projectData, _codeWizardViewModel));
            return actions;
        }
    }
}
