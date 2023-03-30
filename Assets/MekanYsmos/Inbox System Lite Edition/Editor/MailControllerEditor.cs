using MYInboxSystem.Mails;
using UnityEditor;

[CustomEditor(typeof(MailController))]
public class MailControllerEditor : Editor {

    public override void OnInspectorGUI() {

        MailController script = (MailController) target;

        serializedObject.DrawInspectorExcept("m_Script");

    }

}
