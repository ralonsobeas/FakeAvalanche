using MYInboxSystem.Mails.Categories;

namespace MYInboxSystem.Mails {
    public interface IMail {

        int ID { get; set; }

        MailCategoryEnum InboxCategory { get; set; }

        string PartialTitle { get; set; }
        string PartialText { get; set; }
        string Title { get; set; }
        string Text { get; set; }

        void CreateStructure();

        void Send();

    }
}