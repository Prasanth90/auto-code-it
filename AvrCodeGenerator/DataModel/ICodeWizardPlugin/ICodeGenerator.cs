using System.Collections.Generic;

namespace CodeWizard.DataModel.ICodeWizardPlugin
{
    public  interface ICodeGenerator
    {
        CodeBlock GetCode(List<string> enabledModules);

        List<string> GetAsfModuleIds(List<string> enabledModules);
    }
}
