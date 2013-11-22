using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.Timer;
using CodeWizard.Plugins.Constants;

namespace CodeWizard.Plugins.CodeGeneration.CodeGenerators
{
    public class TimerCodeGenerator : CodeGeneratorBase
    {
        private readonly TimerModel _timerModel;
        private StringBuilder _funcDeclarationBlock = new StringBuilder();
        private StringBuilder _interruptHandlerBlock = new StringBuilder();

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
                _funcDeclarationBlock = new StringBuilder();
                var codegenerationinfo = new CodeGenerationInfo(timer.TimerName);
                codeGenerationInfos.Add(codegenerationinfo);

                var timerInitContnets = GetTimerSourceContents(timer);
                codegenerationinfo.SourceCodeBlock.Append(timerInitContnets);

                codegenerationinfo.FunctionCallsBlock.Append(timer.TimerName+ "_init();");
                _funcDeclarationBlock.Append("void " + timer.TimerName + "_init();");

                codegenerationinfo.FunctionDeclarationBlock.Append(_funcDeclarationBlock);

                UpdateInteruptHandlerContents(timer.TimerSettings.OverFlowInterupt,"overflow");
                codegenerationinfo.InteruptHandlerBlock.Append(_interruptHandlerBlock);
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }

        private string GetTimerSourceContents(Timer timer)
        {
            var replacemntDict = GetReplacementDict_timerInit(timer);
            var template = FilesContentStore[FileNames.TimerInit];
            Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref template);
            return template;
        }

        private Dictionary<string, string> GetReplacementDict_timerInit(Timer timer)
        {
            var replacementDict = new Dictionary<string, string>()
                                {
                                    {TimerConstants.TimerName, timer.TimerName},
                                    {TimerConstants.TimerPeriod,timer.TimerSettings.PeriodValue },
                                    {TimerConstants.SelectedClockSource,timer.TimerSettings.TimerClockSource },
                                    {TimerConstants.SelectedWaveFormMode,timer.TimerSettings.TimerMode },
                                    {TimerConstants.SelectedModeInit, GetTimerModeInit(timer) },
                                    {TimerConstants.TimerChannelsInitFunCall,GetTimerChInitFuncCall(timer) },
                                    {TimerConstants.TimerChannelsInitFunDefine,GetTimerChInitFuncDefines(timer) },
                                };
            return replacementDict;
        }

        private string GetTimerModeInit(Timer timer)
        {
            var template = FilesContentStore[FileNames.TimerModeInitFile];
            template = template.Replace(TimerConstants.TimerName, timer.TimerName);
            template = template.Replace(TimerConstants.SelectedEventAcction, timer.TimerSettings.SelectedEventAction);
            template = template.Replace(TimerConstants.SelectedEventSource, timer.TimerSettings.SelectedEventSource);
            return template;
        }

        private string GetTimerChInitFuncCall(Timer timer)
        {
            StringBuilder code = new StringBuilder();
            var template =  FilesContentStore[FileNames.TimerChannelInitFuncCall];
            template = template.Replace(TimerConstants.TimerName, timer.TimerName);
            if (timer.TimerSettings.CCAChannel.IsEnabled)
            {
                var contents = template.Replace(TimerConstants.SelectedTimerChannel, timer.TimerSettings.CCAChannel.Name); 
                code.Append(contents);
                _funcDeclarationBlock.Append("void " + contents);
                _funcDeclarationBlock.AppendLine();
                code.AppendLine();
            }
            if (timer.TimerSettings.CCBChannel.IsEnabled)
            {
                var contents = template.Replace(TimerConstants.SelectedTimerChannel, timer.TimerSettings.CCBChannel.Name);
                code.Append(contents);
                _funcDeclarationBlock.Append("void " + contents);
                _funcDeclarationBlock.AppendLine();
                code.AppendLine();
            }
            if (timer.TimerSettings.CCCChannel.IsEnabled)
            {
                var contents = template.Replace(TimerConstants.SelectedTimerChannel, timer.TimerSettings.CCDChannel.Name);
                code.Append(contents);
                _funcDeclarationBlock.Append("void " + contents);
                _funcDeclarationBlock.AppendLine();
                code.AppendLine();
            }
            if (timer.TimerSettings.CCDChannel.IsEnabled)
            {
                var contents = template.Replace(TimerConstants.SelectedTimerChannel, timer.TimerSettings.CCDChannel.Name);
                code.Append(contents);
                _funcDeclarationBlock.Append("void " + contents);
                _funcDeclarationBlock.AppendLine();
                code.AppendLine();
            }
            return code.ToString();

        }

        private string GetTimerChInitFuncDefines(Timer timer)
        {
            StringBuilder code = new StringBuilder();
            var template = FilesContentStore[FileNames.TimerChannelInit];
            template = template.Replace(TimerConstants.TimerName, timer.TimerName);
            if (timer.TimerSettings.CCAChannel.IsEnabled)
            {
                string contents = template;
                var replacemntDict = GetReplacementDict_timerChInit(timer, timer.TimerSettings.CCAChannel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref contents);
                code.Append(contents);
                code.AppendLine();
            }
            if (timer.TimerSettings.CCBChannel.IsEnabled)
            {
                string contents = template;
                var replacemntDict = GetReplacementDict_timerChInit(timer, timer.TimerSettings.CCBChannel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref contents);
                code.Append(contents);
                code.AppendLine();
            }
            if (timer.TimerSettings.CCCChannel.IsEnabled)
            {
                string contents = template;
                var replacemntDict = GetReplacementDict_timerChInit(timer, timer.TimerSettings.CCCChannel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref contents);
                code.Append(contents);
                code.AppendLine();
            }
            if (timer.TimerSettings.CCDChannel.IsEnabled)
            {
                string contents = template;
                var replacemntDict = GetReplacementDict_timerChInit(timer, timer.TimerSettings.CCDChannel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref contents);
                code.Append(contents);
                code.AppendLine();
            }
            return code.ToString();
        }

        private Dictionary<string, string> GetReplacementDict_timerChInit(Timer timer, TimerChannel timerChannel)
        {
            var replacementDict = new Dictionary<string, string>()
                                {
                                    {TimerConstants.TimerName, timer.TimerName},
                                    {TimerConstants.SelectedTimerChannel,timerChannel.Name },
                                    {TimerConstants.ChannelPeriod, timerChannel.ChannelValue },
                                    {TimerConstants.SelectedTimerChannelEnable,string.Format("{0}_{1}EN_bm",timer.TimerName,timerChannel.Name) },
                                    {TimerConstants.TimerChannelInterrupt,GetTimerChInterruptInit(timer,timerChannel) },
                                };
            return replacementDict;
        }

        private string GetTimerChInterruptInit(Timer timer,TimerChannel timerChannel)
        {
            string template = string.Empty;
            if (timerChannel.ChannelInterupt.IsEnabled)
            {
               template = FilesContentStore[FileNames.TimerChannelInteruppt];
               template = template.Replace(TimerConstants.TimerName, timer.TimerName);
               template = template.Replace(TimerConstants.InterruptName, timerChannel.ChannelInterupt.Name);
               template = template.Replace(TimerConstants.InteruptLevel, timerChannel.ChannelInterupt.Level);
               template = template.Replace(TimerConstants.CallBackFunctionName,"callBack");
               UpdateInteruptHandlerContents(timerChannel.ChannelInterupt,"callBack");
            }
            return template;
        }

        private void UpdateInteruptHandlerContents(TimerInterupt timerInterupt,string callBackName)
        {
            if (timerInterupt.IsEnabled)
            {
                string template = FilesContentStore[FileNames.TimerInteruptHandler];
                template = template.Replace(TimerConstants.CallBackFunctionName, callBackName);
                _interruptHandlerBlock.Append(template);
            }  
        }
    }
}
