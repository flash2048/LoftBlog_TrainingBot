using System.Collections.Generic;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    public class Cards : ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }

        private const string HeroCard = "hero";
        private const string ThumbnailCard = "thumbnail";
        private const string ReceiptCard = "receipt";
        private const string SigninCard = "sign-in";
        private const string AnimationCard = "animation";
        private const string VideoCard = "video";
        private const string AudioCard = "audio";
        public Activity Run(Activity activity)
        {
            if (activity?.Conversation != null)
            {
                activity.Attachments = new List<Attachment>();
                var command = activity.Text.Trim().ToLower();

                Attachment attachment;
                switch (command)
                {
                    case HeroCard:
                        attachment = GetHeroCard();
                        break;
                    case ThumbnailCard:
                        attachment = GetThumbnailCard();
                        break;
                    case ReceiptCard:
                        attachment = GetReceiptCard();
                        break;
                    case SigninCard:
                        attachment = GetSigninCard();
                        break;
                    case AnimationCard:
                        attachment = GetAnimationCard();
                        break;
                    case VideoCard:
                        attachment = GetVideoCard();
                        break;
                    case AudioCard:
                        attachment = GetAudioCard();
                        break;

                    default:
                        attachment = GetDefaultCard();
                        break;
                }

                activity.Attachments.Add(attachment);
            }
            return activity;
        }

        public Cards()
        {
            CommandsName = new List<string>() { "/cards" };
            Description = "Выподит типы карт";
        }

        private static Attachment GetDefaultCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Список доступных карт",
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack, "HeroCard", value: "Cards "+HeroCard),
                    new CardAction(ActionTypes.ImBack, "Thumbnail", value: "Cards "+ThumbnailCard),
                    new CardAction(ActionTypes.ImBack, "Receipt", value: "Cards "+ReceiptCard),
                    new CardAction(ActionTypes.ImBack, "Signin", value: "Cards "+SigninCard),
                    new CardAction(ActionTypes.ImBack, "Animation", value: "Cards "+AnimationCard),
                    new CardAction(ActionTypes.ImBack, "Video", value: "Cards "+VideoCard),
                    new CardAction(ActionTypes.ImBack, "Audio", value: "Cards "+AudioCard),
                }
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Заголовок Hero Card",
                Subtitle = "Содержит одно большое изображение, одну или несколько кнопок и текст.",
                Text = "Текст для Hero Card",
                Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Открыть ссылку", value: "https://docs.microsoft.com/bot-framework") }
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard()
        {
            var heroCard = new ThumbnailCard
            {
                Title = "Заголовок Thumbnail Card",
                Subtitle = "Содержит одно небольшое изображение, одну или несколько кнопок и текст.",
                Text = "Текст для Thumbnail Card",
                Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Открыть ссылку", value: "https://docs.microsoft.com/bot-framework") }
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetReceiptCard()
        {
            var receiptCard = new ReceiptCard
            {
                Title = "Заголовок Receipt Card",
                Facts = new List<Fact> { new Fact("Номер заказа", "1234"), new Fact("Метод оплаты", "VISA 5555-****") },
                Items = new List<ReceiptItem>
                {
                    new ReceiptItem("Обмен данными", price: "$ 38.45", quantity: "368", image: new CardImage(url: "https://github.com/amido/azure-vector-icons/raw/master/renders/traffic-manager.png")),
                    new ReceiptItem("Служба приложений", price: "$ 45.00", quantity: "720", image: new CardImage(url: "https://github.com/amido/azure-vector-icons/raw/master/renders/cloud-service.png")),
                },
                Tax = "$ 7.50",
                Total = "$ 90.95",
                Buttons = new List<CardAction>
                {
                    new CardAction(
                        ActionTypes.OpenUrl,
                        "Больше информации",
                        "https://account.windowsazure.com/content/6.10.1.38-.8225.160809-1618/aux-pre/images/offer-icon-freetrial.png",
                        "https://azure.microsoft.com/en-us/pricing/")
                }
            };

            return receiptCard.ToAttachment();
        }

        private static Attachment GetSigninCard()
        {
            var signinCard = new SigninCard
            {
                Text = "Заголовок Sign-in Card",
                Buttons = new List<CardAction> { new CardAction(ActionTypes.Signin, "Войти", value: "https://login.microsoftonline.com/") }
            };

            return signinCard.ToAttachment();
        }

        private static Attachment GetAnimationCard()
        {
            var animationCard = new AnimationCard
            {
                Title = "Заголовок Animation Card",
                Subtitle = "Может воспроизводить анимированные GIF или короткие видеоролики",
                Image = new ThumbnailUrl
                {
                    Url = "https://docs.microsoft.com/en-us/bot-framework/media/how-it-works/architecture-resize.png"
                },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "http://i.giphy.com/Ki55RUbOV5njy.gif"
                    }
                }
            };

            return animationCard.ToAttachment();
        }

        private static Attachment GetVideoCard()
        {
            var videoCard = new VideoCard
            {
                Title = "Заголовок Video Card",
                Subtitle = "Может воспроизводить видео",
                Text = "Текст для Video Card",
                Image = new ThumbnailUrl
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Big_buck_bunny_poster_big.jpg/220px-Big_buck_bunny_poster_big.jpg"
                },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4"
                    }
                },
                Buttons = new List<CardAction>
                {
                    new CardAction()
                    {
                        Title = "Открыть ссылку",
                        Type = ActionTypes.OpenUrl,
                        Value = "https://peach.blender.org/"
                    }
                }
            };

            return videoCard.ToAttachment();
        }

        private static Attachment GetAudioCard()
        {
            var audioCard = new AudioCard
            {
                Title = "Заголовок Audio Card",
                Subtitle = "может воспроизводить аудиофайл",
                Text = "Текст для Audio Card",
                Image = new ThumbnailUrl
                {
                    Url = "https://upload.wikimedia.org/wikipedia/en/3/3c/SW_-_Empire_Strikes_Back.jpg"
                },
                Media = new List<MediaUrl>
                {
                    new MediaUrl()
                    {
                        Url = "http://www.wavlist.com/movies/004/father.wav"
                    }
                },
                Buttons = new List<CardAction>
                {
                    new CardAction()
                    {
                        Title = "Открыть ссылку",
                        Type = ActionTypes.OpenUrl,
                        Value = "https://en.wikipedia.org/wiki/The_Empire_Strikes_Back"
                    }
                }
            };

            return audioCard.ToAttachment();
        }
    }
}