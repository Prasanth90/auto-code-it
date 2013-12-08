using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeWizard.DataModel;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Company.AvrCodeGenerator.Actions
{
    public class ProjectCreator : ICodeGenerationAction
    {
        private readonly ProjectData _projectData;
        private EnvDTE.DTE _dte;
        public ProjectCreator(ProjectData projectData)
        {
            _projectData = projectData;
            _dte = Package.GetGlobalService(typeof (EnvDTE.DTE)) as EnvDTE.DTE;
        }



        public string StatusMessage()
        {
           return "Creating Project";
        }

        public void Run()
        {
            CreateProject("AvrGcc.AutomationConfiguration", () => CreateProjectWithSolution(Globals.AvrGccTemplateFileName, Globals.AvrGccProjectLanguageName), _projectData.Device);
        }

        private void CreateProject(string automationConfigName, Func<string> creator, string device)
        {
            dynamic automationConfig = _dte.GetObject(automationConfigName);

            automationConfig.NewProjectDeviceName = device;

            creator();
        }


        /// <summary>
        /// create a project and either add it to existing solution or create a new solution
        /// </summary>
        /// <param name="projectTemplateFileName"></param>
        /// <param name="projectLanguageName"></param>
        /// <returns></returns>
        private string CreateProjectWithSolution(string projectTemplateFileName, string projectLanguageName)
        {
            // Solution.GetProjectTemplate takes name of the template as parameter but not the name of the zip file.
            var templatePath = ((Solution2)_dte.Solution).GetProjectTemplate(projectTemplateFileName, projectLanguageName);
            var projectPath = _projectData.Path;
            const bool suppressUi = true;

            object[] contextParameters = {
                                            Globals.vsWizardNewProject , _projectData.Name, projectPath , "", false,
                                             "TestSolution", suppressUi
                                         };

            _dte.LaunchWizard(templatePath, ref contextParameters);

            //Save solution
            //The IVsSolution api is used instead of _dte.Solution.SaveAs
            //When _dte.Solution.SaveAs is used,entire path of project from root drive gets saved and causes problems when project is moved to a different location
            //TODO: Consider using AvrSolution class instead
            var sol = Package.GetGlobalService(typeof(IVsSolution)) as IVsSolution;
            if (sol != null) sol.SaveSolutionElement((uint)__VSSLNSAVEOPTIONS.SLNSAVEOPT_SaveIfDirty, null, 0);

            return "";
        }
    }
}
