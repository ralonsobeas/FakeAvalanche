using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using MYInboxSystem.Mails.Categories;
using MYInboxSystem.Pages;
using System.Globalization;

namespace MYInboxSystem.Mails {

    public class MailController : MonoBehaviour {

        [Space(10)]
        public GameObject emailSelected;
        public GameObject mailGO;

        public List<IMail> MailList { get; set; }

        void Awake() {

            /// Uncomment this lines if you wish that US culture is applied. It affects language, date format, etc.
            //CultureInfo ci = new CultureInfo("en-US");
            //CultureInfo.CurrentCulture = ci;
            //CultureInfo.CurrentUICulture = ci;

            MailList = new List<IMail>();

        }

        public void PostMail(IMail mail, bool updateSingleCategory = true) {

            foreach (var page in PageController.pages) {
                if (page.mailCategory == mail.InboxCategory) {
                    GameObject newMail = Instantiate(mailGO, page.itemContainer.transform);
                    newMail.transform.SetSiblingIndex(0);
                    newMail.name = mail.PartialTitle.Split(' ')[0] + " Mail " + mail.ID;
                    ///Change the line below to show the date you want to. Or comment it.
                    newMail.transform.Find("Date").GetComponent<TextMeshProUGUI>().text = new System.DateTime(2022, Random.Range(1, 12), Random.Range(1, 28)).ToString("MMMM dd");
                    newMail.transform.Find("Partial Title").GetComponent<TextMeshProUGUI>().text = mail.PartialTitle;
                    newMail.transform.Find("Partial Text").GetComponent<TextMeshProUGUI>().text = mail.PartialText;

                    newMail.GetComponent<Button>().onClick.AddListener(delegate { AssignInfoToSelectedMail(mail); });
                    break;
                }
            }

            if (updateSingleCategory) {
                UpdateMailNumbers(mail.InboxCategory);
            }

        }

        void AssignInfoToSelectedMail(IMail mail) {

            emailSelected.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = mail.Title;
            emailSelected.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = mail.Text;
            emailSelected.SetActive(true);

        }

        public void UpdateMailNumbers() {

            foreach (var page in PageController.pages) {
                if (page.button.transform.Find("Number") != null) {
                    int number = MailList.Where(x => x.InboxCategory == page.mailCategory).ToList().Count;
                    page.button.transform.Find("Number").GetComponent<TextMeshProUGUI>().text = "" + (number == 0 ? "" : number.ToString());
                }
            }

        }

        void UpdateMailNumbers(MailCategoryEnum inboxCategory) {

            foreach (var page in PageController.pages) {
                if (page.mailCategory == inboxCategory) {
                    int number = MailList.Where(x => x.InboxCategory == page.mailCategory).ToList().Count;
                    page.button.transform.Find("Number").GetComponent<TextMeshProUGUI>().text = "" + (number == 0 ? "" : number.ToString());
                    if (number == 0) {
                        page.noResults.SetActive(true);
                    } else {
                        page.noResults.SetActive(false);
                    }
                    break;
                }
            }

        }

    }
}