namespace ProjectBlue.CodeGenerator
{
    public abstract class CodeTemplateBase
    {

        protected string nameSpaceName;
        protected string className;

        public CodeTemplateBase(string nameSpaceName, string className)
        {
            this.nameSpaceName = nameSpaceName;
            this.className = className;
        }

        public string GetCode()
        {
            return Replace();
        }
        
        public abstract string FolderPath { get; }
        public abstract string FileName { get; }
        
        protected abstract string Template { get; }
        
        private string Replace()
        {
            return Template
                .Replace("#NAME_SPACE#", nameSpaceName)
                .Replace("#CLASS_NAME#", className);
        }
    }
    
    
}