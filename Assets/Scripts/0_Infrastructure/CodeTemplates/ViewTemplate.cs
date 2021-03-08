namespace ProjectBlue.CodeGenerator
{

    public class ViewTemplate : CodeTemplateBase
    {

        public override string FolderPath => "Presentation/View/";
        public override string FileName => $"{className}View.cs";
        
        public ViewTemplate(string nameSpaceName, string className) : base(nameSpaceName, className){}

        protected override string Template =>  @"

using UnityEngine;
using #NAME_SPACE#.Domain.Model;

namespace #NAME_SPACE#.Presentation.View
{

    public class #CLASS_NAME#View : MonoBehaviour, I#CLASS_NAME#View 
    {

    }
}
";
        
    }
    
}