using MYInboxSystem.Pages;
using UnityEditor;

[CustomEditor(typeof(PageController))]
public class PageControllerEditor : Editor {

    public override void OnInspectorGUI() {

        serializedObject.DrawInspectorExcept("m_Script");

    }

}
