using ChatAppSoftUni.Core.Contracts;
using ChatAppSoftUni.Core.Models.Message;
using ChatAppSoftUni.Infrastructure.Data.Models;
using LibraryManagerConsole.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSoftUni.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository repo;
        public MessageService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddMessage(MessageViewModel message)
        {
            await repo.AddAsync(new Message()
            {
                MessageText = message.MessageText,
                Sender = message.Sender
            });
            await repo.SaveChangesAsync();
        }

        public async Task<IList<MessageViewModel>> AllMessages()
        {
            return await repo.All<Message>().Select(m => new MessageViewModel()
            {
                MessageText = m.MessageText,
                Sender = m.Sender
            }).ToListAsync();

        }

        public async void DeleteByID(object id)
        {
            if (id is not null)
            {
                await repo.DeleteAsync<Message>(id);
                await repo.SaveChangesAsync();
                return;
            }
            var messageToDelete = repo.All<Message>().Where()
        }
    }
}
