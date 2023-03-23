using UnityEngine;

namespace MYInboxSystem.Mails {
    public class MailSender {

        public void Send(IMail iMailCategory) {

            MailController mailController = Camera.main.GetComponent<MailController>();

            if (iMailCategory != null) {
                iMailCategory.CreateStructure();
                mailController.MailList.Add(iMailCategory);
                mailController.PostMail(iMailCategory, true);
            } else {
                Debug.Log("iMailCategory is null");
            }

        }

    }
}