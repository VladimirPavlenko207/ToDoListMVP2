using System.Windows.Forms;

namespace MessageServices
{
    /// <summary>
    /// Представляет службу вывода сообщений.
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// Выводит сообщение с ошибкой.
        /// </summary>
        /// <param name="error"></param>
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Выводит вопросительное сообщение с вариантами ответов Ок и Cancel.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>если ответ Ок возвращает bool.true иначе bool.false.</returns>
        public bool ShowOkCancelMessage(string message)
        {
            return MessageBox.Show(message, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }

        /// <summary>
        /// Выводит простое сообщение.
        /// </summary>
        /// <param name="message"></param>
        public void ShowSimpleMessage(string message)
        {
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
