using System.Collections.Generic;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.SPI;
using CodeWizard.Plugins.Constants;

namespace CodeWizard.Plugins.CodeGeneration.CodeGenerators
{
    public class SpiCodeGenerator : CodeGeneratorBase
    {
        private readonly SpiModel _spiModel;

        public SpiCodeGenerator(SpiModel spiModel, FilesContentStore filesContentStore)
            : base( filesContentStore)
        {
            _spiModel = spiModel;
        }

        public override CodeBlock GetCode()
        {
            var codeBlock = new CodeBlock("SPI");
            var codeGenerationInfos = new List<CodeGenerationInfo>();
            foreach (var spiModel in _spiModel.Spis)
            {
                var codegenerationinfo = new CodeGenerationInfo(spiModel.SpiName);
                string hashDefineContents = GetSpiDefineTemplate();
                var replacemntDict = GetReplacementDict_SPIDefines(spiModel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref hashDefineContents);
                codegenerationinfo.HashDefineBlock.Append(hashDefineContents);

                string spiInitContents = GetSpiInitTemplate();
                replacemntDict = GetReplacementDict_SpiInit(spiModel);
                Utils.Utils.PerformReplacementInFileContents(replacemntDict, ref spiInitContents);
                codegenerationinfo.SourceCodeBlock.Append(spiInitContents);

                string interuptHandlerContents = GetInteruptHandlerCode(spiModel);
                codegenerationinfo.InteruptHandlerBlock.Append(interuptHandlerContents);

                codegenerationinfo.FunctionCallsBlock.Append(GetFunctionCallsBlock(spiModel));
                codegenerationinfo.FunctionDeclarationBlock.Append(GetFunctionDeclarationBlock(spiModel));
                codeGenerationInfos.Add(codegenerationinfo);
            }
            codeBlock.CodeGenerationInfos = codeGenerationInfos;
            return codeBlock;
        }

        private string GetFunctionDeclarationBlock(Spi spiModel)
        {
            return string.Format("void {0}_init(void);", spiModel.SpiName);   
        }

        private string GetFunctionCallsBlock(Spi spiModel)
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

        private Dictionary<string, string> GetReplacementDict_SPIDefines(Spi spiModel)
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

        private Dictionary<string, string> GetReplacementDict_SpiInit(Spi spiModel)
        {
            return GetReplacementDict_SPIDefines(spiModel);
        }

        private string GetInteruptHandlerCode(Spi spiModel)
        {
            return string.Empty;
        }
    }
}
