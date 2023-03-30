using MYInboxSystem.Mails.Categories;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenerateMail))]
[CanEditMultipleObjects]
public class GenerateMailEditor : Editor {

    public override void OnInspectorGUI() {

        serializedObject.DrawInspectorExcept("m_Script");

        if (Application.isPlaying) {

            if (GUILayout.Button("Generate Mail")) {

                GenerateMail();

            }

        }

    }

    public void GenerateMail() {

        MailCategoryEnum activeCategory = Camera.main.GetComponent<GenerateMail>().mailCategory;

        if (activeCategory == MailCategoryEnum.Social) {
            SocialExampleMail dae = new SocialExampleMail();
            dae.randomVariableInt = Random.Range(10, 1000);
            dae.Send();
        } else if (activeCategory == MailCategoryEnum.News) {
            NewsExampleMail dae = new NewsExampleMail();
            dae.randomVariableInt = Random.Range(10, 1000);
            dae.Send();
        } else if (activeCategory == MailCategoryEnum.Finance) {
            FinanceExampleMail dae = new FinanceExampleMail();
            dae.randomVariableInt = Random.Range(10, 1000);
            dae.Send();
        }

    }

}
