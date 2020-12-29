namespace ProjectBlue.CodeGenerator
{
    public abstract class CodeTemplateBase
    {
        public abstract string GetCode(string nameSpace, string className);
        public abstract string FolderPath { get; }
        
        protected static string Replace(string source, string nameSpace, string className)
        {
            return source
                .Replace("NAME_SPACE", nameSpace)
                .Replace("#CLASS_NAME", className);
        }
    }
    
    
}