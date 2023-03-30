using UnityEngine;

namespace MYInboxSystem.Mails.Categories {
    public class SocialExampleMail : IMail {

        public int ID { get; set; }

        public MailCategoryEnum InboxCategory { get; set; }

        public string PartialTitle { get; set; }
        public string PartialText { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int randomVariableInt;

        public void CreateStructure() {

            ID = Camera.main.GetComponent<MailController>().MailList.Count + 1;

            InboxCategory = MailCategoryEnum.Social;

            PartialTitle = "You received " + randomVariableInt.ToString() + " social coins";

            PartialText = "You received " + randomVariableInt.ToString() + " dollars today! Shadowy flight into the dangerous world of a man who does not exist. Michael Knight.";

            Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";

            Text += "You received " + randomVariableInt.ToString() + ", a shadowy flight into the dangerous world of a man who does not exist. Michael Knight, " +
                "a young loner on a crusade to champion the cause of the innocent, the helpless in a world of criminals who operate above the law. Hong Kong Phooey, number one super guy. " +
                "Hong Kong Phooey, quicker than the human eye.He’s got style, a groovy style, and a car that just won’t stop. When the going gets tough, he’s really rough, with a " +
                "Hong Kong Phooey chop(Hi-Ya!). Hong Kong Phooey, number one super guy. Hong Kong Phooey, quicker than the human eye.Hong Kong Phooey, he’s fan-riffic! This is my boss, Jonathan Hart, " +
                "a self - made millionaire, he’s quite a guy. This is Mrs H., she’s gorgeous, she’s one lady. \n\n" +
                "Who knows how to take care of herself.By the way, my name is Max.I take care of both of them, " +
                "which ain’t easy, ’cause when they met it was MURDER! Thundercats are on the move, Thundercats are loose.Feel the magic, hear the roar, Thundercats are loose.Thunder, thunder, thunder, " +
                "Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thundercats! One for all and all for one, Muskehounds " +
                "are always ready.One for all and all for one, helping everybody.One for all and all for one, it’s a pretty story.Sharing everything with fun, that’s the way to be.One for all and all for " +
                "one, Muskehounds are always ready.One for all and all for one, helping everybody.One for all and all for one, can sound pretty corny.If you’ve got a problem chum, think how it could be. " +
                "Donec vel velit pellentesque, luctus nisi in, vehicula mi. Suspendisse vel mi nibh.\n\n" +
                "Thundercats are on the move, Thundercats are loose.Feel the magic, hear the roar, Thundercats are loose.Thunder, thunder, thunder, " +
                "Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thundercats! One for all and all for one, Muskehounds " +
                "are always ready.One for all and all for one, helping everybody.One for all and all for one, it’s a pretty story.Sharing everything with fun, that’s the way to be.One for all and all for " +
                "one, Muskehounds are always ready.One for all and all for one, helping everybody.One for all and all for one, can sound pretty corny.If you’ve got a problem chum, think how it could be. " +
                "Donec vel velit pellentesque, luctus nisi in, vehicula mi. Suspendisse vel mi nibh.\n\n" +
                "Thundercats are on the move, Thundercats are loose.Feel the magic, hear the roar, Thundercats are loose.Thunder, thunder, thunder, " +
                "Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thundercats! One for all and all for one, Muskehounds " +
                "are always ready.One for all and all for one, helping everybody.One for all and all for one, it’s a pretty story.Sharing everything with fun, that’s the way to be.One for all and all for " +
                "one, Muskehounds are always ready.One for all and all for one, helping everybody.One for all and all for one, can sound pretty corny.If you’ve got a problem chum, think how it could be. " +
                "Donec vel velit pellentesque, luctus nisi in, vehicula mi. Suspendisse vel mi nibh.\n\n" +
                "Thundercats are on the move, Thundercats are loose.Feel the magic, hear the roar, Thundercats are loose.Thunder, thunder, thunder, " +
                "Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thunder, thunder, thunder, Thundercats! Thundercats! One for all and all for one, Muskehounds " +
                "are always ready.One for all and all for one, helping everybody.One for all and all for one, it’s a pretty story.Sharing everything with fun, that’s the way to be.One for all and all for " +
                "one, Muskehounds are always ready.One for all and all for one, helping everybody.One for all and all for one, can sound pretty corny.If you’ve got a problem chum, think how it could be. " +
                "Donec vel velit pellentesque, luctus nisi in, vehicula mi. Suspendisse vel mi nibh.\n\n";

        }

        public void Send() {

            new MailSender().Send(this);

        }

    }
}