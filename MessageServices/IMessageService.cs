using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServices
{
    /// <summary>
    /// Задает спобоб вывода сообщений.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Вывод простого сообщения.
        /// </summary>
        /// <param name="message"></param>
        void ShowSimpleMessage(string message);
        /// <summary>
        /// Вывод сообщения с ответами Ок и Cancel.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool ShowOkCancelMessage(string message);
        /// <summary>
        /// Вывод ошибки.
        /// </summary>
        /// <param name="error"></param>
        void ShowError(string error);
    }
}
