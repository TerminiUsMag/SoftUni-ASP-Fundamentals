using ChatAppSoftUni.Core.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSoftUni.Core.Contracts
{
    public interface IMessageService
    {
        Task AddMessage(MessageViewModel message);

        Task<IList<MessageViewModel>> AllMessages();

        void DeleteByID(int id);
    }
}
