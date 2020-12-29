using UnityEditor;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{

    [CustomEditor(typeof(TabWindowView))]
    public class TabWindowViewEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {

            var script = target as TabWindowView;

            base.OnInspectorGUI();
            
            EditorGUILayout.Space(10);
            
            foreach (var tab in script.tabButtons)
            {
                if (GUILayout.Button(tab.name))
                {
                    if (tab.canvasGroup)
                    {
                        script.Display(tab);
                    }
                }
            }

        }
    }
    
}