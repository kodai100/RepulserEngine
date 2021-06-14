namespace ProjectBlue.CodeGenerator
{
    public abstract class CodeTemplateBase
    {
        private string nameSpaceName;
        private string folderPath; // ex. "Domain/Model/"
        protected string className;

        public CodeTemplateBase(string nameSpaceName, string folderPath, string className)
        {
            this.nameSpaceName = nameSpaceName;
            this.folderPath = folderPath;
            this.className = className;
        }

        public string GetCode()
        {
            return Replace();
        }

        public string FolderPath => folderPath;
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
