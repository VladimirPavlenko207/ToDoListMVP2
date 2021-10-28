using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServices
{
    public interface IMessageService
    {
        void ShowSimpleMessage(string message);
        bool ShowOkCancelMessage(string message);
        void ShowError(string error);
    }
}
