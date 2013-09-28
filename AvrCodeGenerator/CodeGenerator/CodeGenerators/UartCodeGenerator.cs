using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGenerator.Constants;
using DataModel;
using DataModel.UsarModel;

namespace CodeGenerator.CodeGenerators
{
    public class UartCodeGenerator : CodeGeneratorBase
    {
        public UartCodeGenerator(McuModel mcuModel, FilesContentStore filesContentStore) : base(mcuModel,filesContentStore)
        {

        }

        public override CodeBlock GetCode()
        {
            var codeBlock = new CodeBlock("USART");
            var codeGenerationInfos = new List<CodeGenerationInfo>();
            foreach (var usartModel in McuModel.UsartModels)
            {
                var codegenerationinfo = new CodeGenerationInfo(usartModel.UsartName);
                string hashDefineContents = GetUsartDefineTemplate();
                var replacemntDict = GetReplacementDict_UsartDefines(usartModel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref hashDefineContents);
                codegenerationinfo.HashDefineBlock.Append(hashDefineContents);

                string usartInitContents = GetUsartInitTemplate();
                replacemntDict = GetReplacementDict_UsartInit(usartModel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref usartInitContents);
                codegenerationinfo.CodeBlock.Append(usartInitContents);

                string interuptHandlerContents = GetInteruptHandlerCode(usartModel);
                codegenerationinfo.InteruptHandlerBlock.Append(interuptHandlerContents);

                codegenerationinfo.FunctionCallsBlock.Append(GetFunctionCallsBlock(usartModel));
                codegenerationinfo.FunctionDeclarationBlock.Append(GetFunctionDeclarationBlock(usartModel));

                codeGenerationInfos.Add(codegenerationinfo);

                

                //string demoAppContents = GetselectedDemoAppTemplate(usartModel);
                //demoAppContents = demoAppContents.Replace(UsartConstants.UsartSerialInit, usartInitContents);
                //demoAppContents = demoAppContents.Replace(UsartConstants.UsartName, usartModel.UsartName);
                //codegenerationinfo.DemoAppBlock.Append(demoAppContents);
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }

        private string GetselectedDemoAppTemplate(UsartModel usartModel)
        {
            return string.Empty;
        }

        private string GetFunctionCallsBlock(UsartModel usartModel)
        {
            return string.Format("{0}_init();", usartModel.UsartName);
        }

        private string GetFunctionDeclarationBlock(UsartModel usartModel)
        {
            return string.Format("void {0}_init(void);", usartModel.UsartName);
        }

        private string GetUsartInitTemplate()
        {
            return FilesContentStore[FileNames.UsartSerialInit];
        }

        private string GetUsartDefineTemplate()
        {
            return FilesContentStore[FileNames.UsartSerialDefines];
        }

        private Dictionary<string, string> GetReplacementDict_UsartDefines(UsartModel usartModel)
        {
            var usartSettings = usartModel.UsartSettings;
            var replacementDict = new Dictionary<string, string>()
                {
                    {UsartConstants.UsartName, usartModel.UsartName},
                    {UsartConstants.BaudRate, usartSettings.SelectedBaudRate},
                    {UsartConstants.CharLength, usartSettings.SelectedDataBitLength},
                    {UsartConstants.Parity, usartSettings.SelectedParityMode},
                    {UsartConstants.StopBit, usartSettings.IsTwoStopBits.ToString().ToLower()}
                };
            return replacementDict;
        }

        private Dictionary<string, string> GetReplacementDict_UsartInit(UsartModel usartModel)
        {
            var replacementDict = new Dictionary<string, string>()
                                {
                                    {UsartConstants.InteruptInit, GetInteruptInitCode(usartModel)},
                                    {UsartConstants.UsartName, usartModel.UsartName},
                                };
            return replacementDict;
        }

        private string GetInteruptInitCode(UsartModel usartModel)
        {
            var interuptCode = new StringBuilder();
            string template = GetInteruptInitTemplate();
            string code;
            if (usartModel.UsartSettings.RxCompleteIntEnabled)
            {
                code = template.Replace(UsartConstants.InteruptType, "rx")
                               .Replace(UsartConstants.UsartName, usartModel.UsartName)
                               .Replace(UsartConstants.InteruptLevel, usartModel.UsartSettings.SelectedRxInteruptLevel);
                interuptCode.Append(code);
                interuptCode.AppendLine();
            }
            if (usartModel.UsartSettings.TxCompleteIntEnabled)
            {
                code = template.Replace(UsartConstants.InteruptType, "tx")
               .Replace(UsartConstants.UsartName, usartModel.UsartName)
               .Replace(UsartConstants.InteruptLevel, usartModel.UsartSettings.SelectedTxInteruptLevel);
                interuptCode.Append(code);
                interuptCode.AppendLine();
            }
            if (usartModel.UsartSettings.DataReceivedIntEnabled)
            {
                code = template.Replace(UsartConstants.InteruptType, "dre")
               .Replace(UsartConstants.UsartName, usartModel.UsartName)
               .Replace(UsartConstants.InteruptLevel, usartModel.UsartSettings.SelectedDreInteruptLevel);
                interuptCode.Append(code);
                interuptCode.AppendLine();
            }
            return interuptCode.ToString();
        }

        private string GetInteruptInitTemplate()
        {
            return FilesContentStore[FileNames.UsartInteruptInit];
        }

        private string GetInteruptHandlerTemplate()
        {
            return FilesContentStore[FileNames.UsartInteruptHandler];
        }

        private string GetInteruptHandlerCode(UsartModel usartModel)
        {
            var interuptHandlerCode = new StringBuilder();
            string template = GetInteruptHandlerTemplate();
            string code;
            if (usartModel.UsartSettings.RxCompleteIntEnabled)
            {
                code = template.Replace(UsartConstants.InteruptType, "RXC")
                               .Replace(UsartConstants.UsartName, usartModel.UsartName);
                interuptHandlerCode.Append(code);
                interuptHandlerCode.AppendLine();
            }
            if (usartModel.UsartSettings.TxCompleteIntEnabled)
            {
                code = template.Replace(UsartConstants.InteruptType, "TXC")
                               .Replace(UsartConstants.UsartName, usartModel.UsartName);
                interuptHandlerCode.Append(code);
                interuptHandlerCode.AppendLine();
            }
            if (usartModel.UsartSettings.DataReceivedIntEnabled)
            {
                code = template.Replace(UsartConstants.InteruptType, "DRE")
                               .Replace(UsartConstants.UsartName, usartModel.UsartName);
                interuptHandlerCode.Append(code);
                interuptHandlerCode.AppendLine();
            }
            return interuptHandlerCode.ToString();
        }
    }
}
