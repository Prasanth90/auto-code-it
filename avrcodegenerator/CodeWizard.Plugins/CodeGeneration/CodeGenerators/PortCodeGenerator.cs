using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.PortModel;

namespace CodeWizard.Plugins.CodeGeneration.CodeGenerators
{
    public class PortCodeGenerator : CodeGeneratorBase
    {
        private readonly IOPortModel _ioPortModel;

        public PortCodeGenerator(IOPortModel ioPortModel, FilesContentStore filesContentStore) : base(filesContentStore)
        {
            _ioPortModel = ioPortModel;
        }

        public override CodeBlock GetCode(List<string> enabledModules)
        {
            var codeBlock = new CodeBlock("IO Ports");
            var codeGenerationInfos = new List<CodeGenerationInfo>();
            foreach (Port port in _ioPortModel.Ports)
            {
                if (enabledModules.Contains(port.PortName))
                {
                    var codegenerationinfo = new CodeGenerationInfo(port.PortName);
                    codegenerationinfo.SourceCodeBlock.Append(string.Format("void {0}_init()", port.PortName));
                    codegenerationinfo.SourceCodeBlock.AppendLine();
                    codegenerationinfo.SourceCodeBlock.Append("{");
                    codegenerationinfo.SourceCodeBlock.AppendLine();
                    foreach (Pin pin in port.Pins.Where(pin => pin.HasUserConfigured))
                    {
                        codegenerationinfo.SourceCodeBlock.Append(GetPinConfigCodeBlock(port, pin));
                        codegenerationinfo.SourceCodeBlock.AppendLine();
                        codegenerationinfo.HashDefineBlock.Append(GetPinConfigHashDefine(port, pin));
                        codegenerationinfo.HashDefineBlock.AppendLine();

                    }
                    codegenerationinfo.SourceCodeBlock.Append("}");
                    codegenerationinfo.SourceCodeBlock.AppendLine();
                    codegenerationinfo.FunctionCallsBlock.Append(GetFunctionCallsBlock(port));
                    codegenerationinfo.FunctionDeclarationBlock.Append(GetFunctionDeclarationBlock(port));
                    codeGenerationInfos.Add(codegenerationinfo);
                }
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }

        public override List<string> GetAsfModuleIds(List<string> enabledModules)
        {
            foreach (Port port in _ioPortModel.Ports)
            {
                if (enabledModules.Contains(port.PortName))
                {
                    return new List<string>()
                    {
                        "common.services.ioport",
                        "common.services.basic.gpio"
                    };
                }
            }
            return new List<string>();
        }

        private StringBuilder GetPinConfigCodeBlock(Port port, Pin pin)
        {
            var codeblock = new StringBuilder();
            string pinInitContents = GetPinInitTemplate();
            var replacemntDict = GetReplacementDict_Init(pin,port);
            Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref pinInitContents);
            codeblock.Append(pinInitContents);
            return codeblock;

        }

        private Dictionary<string, string> GetReplacementDict_Init(Pin pin,Port port)
        {
            var replacementDict = new Dictionary<string, string>()
                {
                    {Constants.Constants.PinName,string.Format("{0}_{1}",port.PortName, pin.PinName)},
                    {Constants.Constants.PinDirection, pin.SelectedDirection},
                    {Constants.Constants.PinOutputValue, pin.SelectedOutputValue},
                    {Constants.Constants.PinOutputMode, GetOutputConfigCode(pin)},
                    {Constants.Constants.PinSenseMode, pin.SelectedInputSenseMode},
                    {Constants.Constants.Portname,port.PortName}
                };
            return replacementDict;
        }

        private string GetPinInitTemplate()
        {
            return FilesContentStore[FileNames.PortInit];
        }

        private String GetPinConfigHashDefine(Port port, Pin pin)
        {
            return Utils.Utils.GetHashDefine(string.Format("{0}_{1}",port.PortName, pin.PinName), string.Format("IOPORT_CREATE_PIN({0},{1})", port.PortName ,pin.PinNumber));
        }

        private string GetOutputConfigCode(Pin pin)
        {
            bool ouputconfigured = pin.SelectedOutputPullConfig != null;
            var outputConfigCode = string.Format("{0}{1}{2}",
                                             ouputconfigured ? pin.SelectedOutputPullConfig + " | " : string.Empty,
                                             pin.IsInverted ? McuModel.PeripheralInfoProvider.GetInvertedPinMode() + " | ": string.Empty,
                                             pin.IsOutputSlRateLimited ? McuModel.PeripheralInfoProvider.GetReducedSlewRateMode() : string.Empty);
            if(outputConfigCode.EndsWith(" | "))
            {
                var index =  outputConfigCode.LastIndexOf(" | ",StringComparison.OrdinalIgnoreCase);
                outputConfigCode = outputConfigCode.Remove(index);
            }
            return outputConfigCode;
        }

        private string GetFunctionDeclarationBlock(Port port)
        {
            return string.Format("void {0}_init(void);", port.PortName);
        }

        private string GetFunctionCallsBlock(Port port)
        {
            return string.Format("{0}_init();", port.PortName);
        }
    }
}
