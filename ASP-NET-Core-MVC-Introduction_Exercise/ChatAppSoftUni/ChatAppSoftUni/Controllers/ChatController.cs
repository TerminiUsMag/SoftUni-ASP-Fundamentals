using ChatAppSoftUni.Core.Contracts;
using ChatAppSoftUni.Core.Models.Message;
using ChatAppSoftUni.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSoftUni.Controllers
{
    public class ChatController : Controller
    {
        //private static List<KeyValuePair<string, string>> s_messages = new List<KeyValuePair<string, string>>();
        private readonly IMessageService ms;
        public ChatController(IMessageService messageService)
        {
            ms = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            var messages = await ms.AllMessages();
            if (messages.Count < 1)
            {
                return View(new ChatViewModel());
            }
            var chatModel = new ChatViewModel()
            {
                Messages = messages.Select(m => new MessageViewModel() { Sender = m.Sender, MessageText = m.MessageText }).ToList()
            };
            return View(chatModel);
        }

        public async Task<IActionResult> Show(ChatViewModel chat)
        {
            var messages = await ms.AllMessages();
            if (messages.Count < 1)
            {
                chat.Messages = new List<MessageViewModel>();
                return View(chat);
            }
            chat.Messages = messages.Select(m => new MessageViewModel() { Sender = m.Sender, MessageText = m.MessageText }).ToList();
            return View(chat);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            if (chat.CurrentMessage.MessageText is null)
            {
                chat.CurrentMessage.MessageText = "Empty messages aren't allowed";
                return RedirectToAction("Show", chat);
            }

            else if (chat.CurrentMessage.Sender is null)
            {
                chat.CurrentMessage.Sender = "Please Specify the sender";
                return View("Show", chat);
            }

            ms.AddMessage(chat.CurrentMessage);
            return RedirectToAction("Show");

            //messages.Add(new KeyValuePair<string, string>
            //    (newMessage.Sender, newMessage.MessageText));
        }

        public IActionResult Delete(int id)
        {
            ms.DeleteByID(id);
            return RedirectToAction("Show");
        }
    }
}
