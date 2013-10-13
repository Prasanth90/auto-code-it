using System.Text;
using CodeWizard.DataModel.ICodeWizardPlugin;

namespace CodeWizard.Plugins.CodeGeneration
{
    public abstract class CodeGeneratorBase : ICodeGenerator
    {

        protected FilesContentStore FilesContentStore { get; set; }

        protected CodeGeneratorBase( FilesContentStore filesContentStore)
        {
            
            FilesContentStore = filesContentStore;
        }

        public abstract CodeBlock GetCode();
        

        protected StringBuilder GetCommentSection(string message)
        {
            var commentsection = new StringBuilder();
            commentsection.AppendLine();
            commentsection.Append("/**");
            commentsection.AppendLine();
            commentsection.Append("* \\brief " + message);
            commentsection.AppendLine();
            commentsection.Append("*/");
            commentsection.AppendLine();
            return commentsection;
        }
    }

    

   
}
