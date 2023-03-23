using MYInboxSystem.Mails.Categories;
using UnityEngine;

namespace MYInboxSystem.Pages {
    [System.Serializable]
    public class Page {

        public MailCategoryEnum mailCategory;

        public GameObject button;
        public GameObject page;
        public GameObject itemContainer;
        public GameObject noResults;

    }
}
