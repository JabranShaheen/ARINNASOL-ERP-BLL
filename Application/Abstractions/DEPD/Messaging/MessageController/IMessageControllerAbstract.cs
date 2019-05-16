using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.BusinessLogic.Messaging
{
    public interface IMessagingControllerAbstract
    {
        bool SendMessage(object dataToSend, string to, Type type, string jobCardNum = null);
        MessageAbstract ReceiveMessage();
    }
}
