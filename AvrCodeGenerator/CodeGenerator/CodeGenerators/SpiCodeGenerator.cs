using System.Collections.Generic;
using CodeGenerator.Constants;
using DataModel;
using DataModel.SPI;

namespace CodeGenerator.CodeGenerators
{
    public class SpiCodeGenerator : CodeGeneratorBase
    {
        public SpiCodeGenerator(McuModel mcuModel, FilesContentStore filesContentStore)
            : base(mcuModel, filesContentStore)
        {

        }

        public override CodeBlock GetCode()
        {
            var codeBlock = new CodeBlock("SPI");
            var codeGenerationInfos = new List<CodeGenerationInfo>();
            foreach (var spiModel in McuModel.SpiModels)
            {
                var codegenerationinfo = new CodeGenerationInfo(spiModel.SpiName);
                string hashDefineContents = GetSpiDefineTemplate();
                var replacemntDict = GetReplacementDict_SPIDefines(spiModel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref hashDefineContents);
                codegenerationinfo.HashDefineBlock.Append(hashDefineContents);

                string spiInitContents = GetSpiInitTemplate();
                replacemntDict = GetReplacementDict_SpiInit(spiModel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref spiInitContents);
                codegenerationinfo.CodeBlock.Append(spiInitContents);

                string interuptHandlerContents = GetInteruptHandlerCode(spiModel);
                codegenerationinfo.InteruptHandlerBlock.Append(interuptHandlerContents);

                codegenerationinfo.FunctionCallsBlock.Append(GetFunctionCallsBlock(spiModel));
                codegenerationinfo.FunctionDeclarationBlock.Append(GetFunctionDeclarationBlock(spiModel));
                codeGenerationInfos.Add(codegenerationinfo);
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }

        private string GetFunctionDeclarationBlock(SpiModel spiModel)
        {
            return string.Format("void {0}_init(void);", spiModel.SpiName);   
        }

        private string GetFunctionCallsBlock(SpiModel spiModel)
        {
            return string.Format("{0}_init();", spiModel.SpiName);   
        }


        private string GetSpiInitTemplate()
        {
            return FilesContentStore[FileNames.SpiInit];
        }

        private string GetSpiDefineTemplate()
        {
            return FilesContentStore[FileNames.SpiDefine];
        }

        private Dictionary<string, string> GetReplacementDict_SPIDefines(SpiModel spiModel)
        {
            var spiSettings = spiModel.SpiSettings;
            var replacementDict = new Dictionary<string, string>()
                {
                    {SpiConstants.SpiName, spiModel.SpiName},
                    {SpiConstants.BaudRate, spiSettings.BaudRate},
                    {SpiConstants.CsPin, spiSettings.CsPin},
                    {SpiConstants.CsPort, spiSettings.CsPort},
                    {SpiConstants.Mode, spiSettings.SpiMode},
                    {SpiConstants.SpiPort,GetSpiPort(spiModel.SpiName)}
                };
            return replacementDict;
        }

        private string GetSpiPort(string spiName)
        {
            var length = spiName.Length;
            if(length>0)
            return string.Format("PORT{0}", spiName[spiName.Length - 1]);
            return string.Empty;
        }

        private Dictionary<string, string> GetReplacementDict_SpiInit(SpiModel spiModel)
        {
            return GetReplacementDict_SPIDefines(spiModel);
        }

        private string GetInteruptHandlerCode(SpiModel spiModel)
        {
            return string.Empty;
        }
    }
}
