using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.AvrCodeGenerator.Actions;

namespace Company.AvrCodeGenerator.CodeComposeSteps
{
    public class ComposeSteps
    {
        public ComposeSteps()
        {
            
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
            catch (Exception)
            {
                throw;
                //TODO: Log error somewhere
            }

        }

        private List<ICodeGenerationAction> GetCodeComposeSteps()
        {
            List<ICodeGenerationAction> actions = new List<ICodeGenerationAction>();
            actions.Add(new ProjectCreator());
            actions.Add(new AsfModuleAdder());
            actions.Add(new FileUpdater());
            return actions;
        }
    }
}
