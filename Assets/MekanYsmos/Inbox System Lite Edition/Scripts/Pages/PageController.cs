using MYInboxSystem.Mails;
using MYInboxSystem.Mails.Categories;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace MYInboxSystem.Pages {
    public class PageController : MonoBehaviour {

        public GameObject buttonsContainer;
        public GameObject pagesContainer;
        [Space(10)]
        public GameObject buttonModel;
        public GameObject pageModel;

        public static List<Page> pages = new List<Page>();

        private GameObject activePage;

        private MailController mailController;

        void Start() {

            mailController = Camera.main.GetComponent<MailController>();

            foreach (MailCategoryEnum item in System.Enum.GetValues(typeof(MailCategoryEnum))) {
                Page page = new Page();
                page.mailCategory = item;
                pages.Add(page);

                string pageName = ObjectNames.NicifyVariableName(page.mailCategory.ToString());

                GameObject button = Instantiate(buttonModel.gameObject, buttonsContainer.transform);
                button.name = pageName + " - Button";
                button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = pageName;
                page.button = button;
                button.transform.SetSiblingIndex(pages.IndexOf(page));

                GameObject pg = Instantiate(pageModel.gameObject, pagesContainer.transform);
                pg.name = pageName + " - Page";
                page.page = pg;
                page.itemContainer = RecursiveFindChild(pg.transform, "Mail List").gameObject;
                page.noResults = RecursiveFindChild(pg.transform, "No results").gameObject;
                pg.transform.SetSiblingIndex(pages.IndexOf(page));

                page.button.GetComponent<Button>().onClick.AddListener(delegate { OpenPage(page.page); });
            }

            CloseAllPages();
            mailController.UpdateMailNumbers();
            OpenPage(pages[0].page);

        }

        public void OpenPage(GameObject page) {

            if (activePage != null) {
                CloseActivePage();
            }

            page.SetActive(true);
            activePage = page;
            if(pages.Find(x => x.page == page) != null) OpenFirstEmailCategory(pages.Find(x => x.page == page));

        }

        void CloseActivePage() {

            activePage.SetActive(false);

        }

        void CloseAllPages() {

            for (int i = 0; i < pages.Count; i++) {
                if (pages[i].itemContainer.transform.childCount == 1 && pages[i].noResults == pages[i].itemContainer.transform.GetChild(0)) {
                    pages[i].noResults.SetActive(true);
                }
                pages[i].page.SetActive(false);
            }

        }

        void OpenFirstEmailCategory(Page page) {

            if (page.itemContainer.transform.childCount > 1) {
                page.itemContainer.transform.GetChild(1).GetComponent<Button>().onClick.Invoke();
                page.noResults.SetActive(false);
            } else if (page.itemContainer.transform.childCount == 1) {
                page.noResults.SetActive(true);
                mailController.emailSelected.SetActive(false);
            }

        }

        public static Transform RecursiveFindChild(Transform parent, string childName) {
            Transform child = null;
            for (int i = 0; i < parent.childCount; i++) {
                child = parent.GetChild(i);
                if (child.name == childName) {
                    break;
                } else {
                    child = RecursiveFindChild(child, childName);
                    if (child != null) {
                        break;
                    }
                }
            }

            return child;
        }

    }
}