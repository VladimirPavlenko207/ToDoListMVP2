using MessageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoList.BL;
using ToDoList.BL.Managers;
using ToDoListPresenter;

namespace ToDoListWForms
{
    static class Program
    {
        private const string Url = "http://localhost:16808/api/";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ITopManager manager = new TopManager(new TagManager(), new CategoryManager(), new TaskManager());
            MainForm view = new();
            IMessageService messageService = new MessageService();
            _ = new MainPresenter(view, manager, messageService, Url);

            Application.Run(view);
        }
    }
}
