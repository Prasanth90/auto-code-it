using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.CodeGeneratorUtils;
using CodeGenerator.CodeGenerators;
using CodeGenerator.Constants;
using DataModel;
using DataModel.UsarModel;

namespace CodeGenerator
{
    public class CodeGenerator
    {
        private readonly McuModel _mcuModel;
        private readonly FilesContentStore _filesContentStore = new FilesContentStore();
       
        public CodeGenerator(McuModel mcuModel )
        {
            _mcuModel = mcuModel;
             new CreateRawInput(_filesContentStore).LoadResourceFile();
        }

        public string GetGeneratedCode()
        {
            List<CodeBlock> generatedCodes = new List<CodeBlock>();
            List<ICodeGenerator> codeGenerators = new List<ICodeGenerator>
                                                {
                                                    new PortCodeGenerator(_mcuModel,_filesContentStore),
                                                    new UartCodeGenerator(_mcuModel,_filesContentStore),
                                                    new SpiCodeGenerator(_mcuModel,_filesContentStore)
                                                };
            foreach (var codeGenerator in codeGenerators)
            {
                generatedCodes.Add(codeGenerator.GetCode());
            }
            return GetCodeStream(generatedCodes);
        }

        private string GetCodeStream(IEnumerable<CodeBlock> generatedCodes)
        {
            string mainCodeStream = _filesContentStore[FileNames.MainFileName];
            var replacementDict = GetReplacementDict_MainFile(generatedCodes);
            Utils.Utils.PerformReplacementInFileContents(replacementDict, ref mainCodeStream);
            return mainCodeStream;
        }

        private Dictionary<string, string> GetReplacementDict_MainFile(IEnumerable<CodeBlock> codeBlocks )
        {
            StringBuilder hashDefineBlock = new StringBuilder();
            StringBuilder functionCalls = new StringBuilder();
            StringBuilder functionDefine = new StringBuilder();
            StringBuilder functionDeclaration = new StringBuilder();
            StringBuilder interuptHandler = new StringBuilder();

            foreach (CodeBlock codeBlock in codeBlocks)
            {
                foreach (var codeGenerationInfo in codeBlock.CodeGenerationInfos)
                {
                    AppendString(codeGenerationInfo.HashDefineBlock, hashDefineBlock);
                    AppendString(codeGenerationInfo.InteruptHandlerBlock, interuptHandler);
                    AppendString(codeGenerationInfo.CodeBlock, functionDefine);
                    AppendString(codeGenerationInfo.FunctionCallsBlock, functionCalls);
                    AppendString(codeGenerationInfo.FunctionDeclarationBlock, functionDeclaration);
                }
            }
            var replacementDict = new Dictionary<string, string>()
                {
                    {MainFileConstants.HashDefines, hashDefineBlock.ToString()},
                    {MainFileConstants.FunctionDefines, functionDefine.ToString()},
                    {MainFileConstants.FunctionDeclaration, functionDeclaration.ToString()},
                    {MainFileConstants.FunctionCalls, functionCalls.ToString()},
                    {MainFileConstants.InteruptHandler ,interuptHandler.ToString()}
                };
            return replacementDict;
        }

        private void AppendString(StringBuilder source, StringBuilder dest)
        {
            if (!string.IsNullOrEmpty(source.ToString()))
            {
                dest.Append(source);
                dest.AppendLine();
            }
        }

        public static List<string> GetGeneratedCodeStrings(List<CodeBlock> generatedCodeBlocks)
        {
            var codeContentList = new List<string>();
            foreach (var generatedCodeBlock in generatedCodeBlocks)
            {
                foreach (var codeGenerationInfo in generatedCodeBlock.CodeGenerationInfos)
                {
                    var builder = new StringBuilder();
                    builder.Append("Code Generated for" + codeGenerationInfo.PeripheralName);
                    builder.AppendLine();
                    builder.Append(codeGenerationInfo.HashDefineBlock);
                    builder.AppendLine();
                    builder.Append(codeGenerationInfo.CodeBlock);
                    builder.AppendLine();
                    builder.Append(codeGenerationInfo.InteruptHandlerBlock);
                    builder.AppendLine();
                    codeContentList.Add(builder.ToString());
                }
                
            }
            return codeContentList;
        }
    }
}
