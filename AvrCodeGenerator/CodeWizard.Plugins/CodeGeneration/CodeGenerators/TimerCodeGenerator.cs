using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.Timer;

namespace CodeWizard.Plugins.CodeGeneration.CodeGenerators
{
    public class TimerCodeGenerator : CodeGeneratorBase
    {
        private readonly TimerModel _timerModel;

        public TimerCodeGenerator( TimerModel timerModel , FilesContentStore filesContentStore) : base(filesContentStore)
        {
            _timerModel = timerModel;
        }

        public override CodeBlock GetCode()
        {
            var codeBlock = new CodeBlock("TIMER");
            var codeGenerationInfos = new List<CodeGenerationInfo>();
            foreach (Timer timer in _timerModel.Timers)
            {
                var codegenerationinfo = new CodeGenerationInfo(timer.TimerName);
                codeGenerationInfos.Add(codegenerationinfo);
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }
    }
}
