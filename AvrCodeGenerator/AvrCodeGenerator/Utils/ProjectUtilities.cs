using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Company.AvrCodeGenerator.Utils
{
    public class ProjectUtilities
    {
        public static Project GetProject(string projectName)
        {
            var dte = (DTE)Package.GetGlobalService(typeof(DTE));

            var projects = dte.Solution.Projects;

            return projects.Cast<Project>().FirstOrDefault(project => project.Name == projectName);
        }
    }
}
