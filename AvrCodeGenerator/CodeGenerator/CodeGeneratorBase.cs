using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;

namespace CodeGenerator
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
