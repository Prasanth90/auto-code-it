using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGenerator.Properties;
using DataModel;
using DataModel.PortModel;

namespace CodeGenerator.CodeGenerators
{
    public class PortCodeGenerator : CodeGeneratorBase
    {
        public PortCodeGenerator(McuModel mcuModel, FilesContentStore filesContentStore) : base(mcuModel, filesContentStore) { }

        public override CodeBlock GetCode()
        {
            var codeBlock = new CodeBlock("IO Ports");
            var codeGenerationInfos = new List<CodeGenerationInfo>();
            foreach (Port port in this.McuModel.IOPortModel.Ports)
            {
                var codegenerationinfo = new CodeGenerationInfo(port.PortName);
                codegenerationinfo.CodeBlock.Append(string.Format("void {0}_init()", port.PortName));
                codegenerationinfo.CodeBlock.AppendLine();
                codegenerationinfo.CodeBlock.Append("{");
                codegenerationinfo.CodeBlock.AppendLine();
                foreach (Pin pin in port.Pins.Where(pin => pin.HasUserConfigured))
                {
                    codegenerationinfo.CodeBlock.Append(GetPinConfigCodeBlock(port, pin));
                    codegenerationinfo.CodeBlock.AppendLine();
                    codegenerationinfo.HashDefineBlock.Append(GetPinConfigHashDefine(port, pin));
                    codegenerationinfo.HashDefineBlock.AppendLine();
                    
                }
                codegenerationinfo.CodeBlock.Append("}");
                codegenerationinfo.CodeBlock.AppendLine();
                codegenerationinfo.FunctionCallsBlock.Append(GetFunctionCallsBlock(port));
                codegenerationinfo.FunctionDeclarationBlock.Append(GetFunctionDeclarationBlock(port));
                codeGenerationInfos.Add(codegenerationinfo);
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }

        private StringBuilder GetPinConfigCodeBlock(Port port, Pin pin)
        {
            var codeblock = new StringBuilder();
           // codeblock.Append(GetCommentSection(string.Format("Configures Pin {0} of {1}",pin.PinNumber,port.PortName)));
            string pinInitContents = GetPinInitTemplate();
            var replacemntDict = GetReplacementDict_Init(pin,port);
            Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref pinInitContents);
            codeblock.Append(pinInitContents);



            //if (pin.SelectedDirection != null)
            //{
            //    codeblock.Append(Resources.PinDirectionCode.Replace(Constants.Constants.PinName,pin.PinName).Replace(Constants.Constants.PinDirection,pin.SelectedDirection));
            //    codeblock.AppendLine();
            //}

            //if (pin.SelectedOutputValue != null)
            //{
            //    codeblock.Append(Resources.PinOutputValueCode.Replace(Constants.Constants.PinName, pin.PinName).Replace(Constants.Constants.PinOutputValue, pin.SelectedOutputValue));
            //    codeblock.AppendLine();
            //}

            //if (pin.SelectedInputSenseMode != null)
            //{
            //    codeblock.Append(Resources.PinInputSenseModeCode.Replace(Constants.Constants.PinName, pin.PinName).Replace(Constants.Constants.PinSenseMode, pin.SelectedInputSenseMode));
            //    codeblock.AppendLine();
            //}
            //var outputconfigCode = GetOutputConfigCode(pin);
            //if (!string.IsNullOrEmpty(outputconfigCode))
            //{
            //    codeblock.Append(Resources.PinOutputPullConfigCode.Replace(Constants.Constants.PinName, pin.PinName).Replace(Constants.Constants.PinOutputMode,outputconfigCode));
            //    codeblock.AppendLine();
            //}
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
            return CodeGeneratorUtils.CodeGeneratorUtils.GetHashDefine(string.Format("{0}_{1}",port.PortName, pin.PinName), string.Format("IOPORT_CREATE_PIN({0},{1})", port.PortName ,pin.PinNumber));
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
