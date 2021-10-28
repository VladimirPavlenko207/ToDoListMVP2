using System.Windows.Forms;

namespace MessageServices
{
    public class MessageService : IMessageService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ShowOkCancelMessage(string message)
        {
            return MessageBox.Show(message, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }

        public void ShowSimpleMessage(string message)
        {
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
