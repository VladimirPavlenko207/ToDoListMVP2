using MessageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;
using ToDoListPresenter.Helpers;

namespace ToDoListPresenter
{
    /// <summary>
    /// Главный презентер, взаимодействующий с IView с помощью ITopManager и IMessageService.
    /// </summary>
    public class MainPresenter
    {
        private readonly IView view;
        private readonly ITopManager manager;
        private readonly IMessageService messageService;
        private readonly string url;

        private List<TaskResponseModel> Tasks { get; set; }
        private List<TagResponseModel> Tags { get; set; }
        private List<CategoryResponseModel> Categories { get; set; }

        /// <summary>
        /// Конструктор, принимающий ссылки на IView, ITopManager, IMessageService, адрес url.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="manager"></param>
        /// <param name="messageService"></param>
        /// <param name="url"></param>
        public MainPresenter(IView view, ITopManager manager, IMessageService messageService, string url)
        {
            this.manager = manager;
            this.view = view;
            this.messageService = messageService;
            this.url = url;

            view.AddNewCategoryClick += View_AddNewCategoryClick;
            view.AddNewTagClick += View_AddNewTagClick;
            view.AddNewTaskClick += View_AddNewTaskClick;

            view.CurrentCategoryChenged += View_CurrentCategoryChenged;
            view.CurrentTagChenged += View_CurrentTagChenged;
            view.CurrentTaskChenged += View_CurrentTaskChenged;

            view.TotalLoading += View_TotalLoading;

            view.EditCategoryClick += View_EditCategoryClick;
            view.EditTagClick += View_EditTagClick;
            view.EditTaskClick += View_EditTaskClick;

            view.RemoveCategoryClick += View_RemoveCategoryClick;
            view.RemoveTagClick += View_RemoveTagClick;
            view.RemoveTaskClick += View_RemoveTaskClick;
        }

        private async Task<bool> View_RemoveTaskClick(TaskResponseModel task)
        {
            if (task is null) return false;

            bool result = messageService.ShowOkCancelMessage($"Вы действительно хотите удалить задачу \"{task.Description}\"?");
            if (!result) return false;

            result = await manager.RemoveTaskAsync(url, task.Id);
            if (!result)
            {
                messageService.ShowError($"Не удалось удалить задачу \"{task.Description}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Задача \"{task.Description}\" успешно удалена.");
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_RemoveTagClick(TagResponseModel tag)
        {
            if (tag is null) return false;

            bool result = messageService.ShowOkCancelMessage($"Вы действительно хотите удалить метку \"{tag.Name}\"?");
            if (!result) return false;

            result = await manager.RemoveTagAsync(url, tag.Id);
            if (!result)
            {
                messageService.ShowError($"Не удалось удалить метку \"{tag.Name}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Метка \"{tag.Name}\" успешно удалена.");
            Tags = await manager.LoadTagsAsync(url, Tags);
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            view.SetTags(Tags?.Select(t => t.Name).ToArray());
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_RemoveCategoryClick(CategoryResponseModel category)
        {
            if (category is null) return false;

            bool result = messageService.ShowOkCancelMessage($"Вы действительно хотите удалить категорию \"{category.Name}\"?");
            if (!result) return false;

            result = await manager.RemoveCategoryAsync(url, category.Id);
            if (!result)
            {
                messageService.ShowError($"Не удалось удалить категорию \"{category.Name}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Категория \"{category.Name}\" успешно удалена.");
            Categories = await manager.LoadCategoriesAsync(url, Categories);
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            view.SetCategories(Categories?.Select(c => c.Name).ToArray());
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_EditTaskClick(TaskRequestModel task)
        {
            bool result = messageService.
               ShowOkCancelMessage($"Вы действительно хотите сохранить изменения в задаче \"Id = {task.Id}\"?");
            if (!result) return false;

            if (!TaskHelper.IsTaskFieldsCorrect(task))
            {
                messageService.ShowError("Не все поля задачи заполнены!");
                return false;
            }

            result = await manager.EditTaskAsync(url, task);
            if (!result)
            {
                messageService.ShowError($"Не удалось сохранить изменения в задаче \" id = {task.Id}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Изменения в задаче \"id = {task.Id}\" успешно сохранены.");
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_EditTagClick(TagRequestModel tag)
        {
            bool result = messageService.
               ShowOkCancelMessage($"Вы действительно хотите изменить метку \"{tag.Name}\" на \"{tag.NewName}\"?");
            if (!result) return false;

            result = await manager.EditTagAsync(url, tag);
            if (!result)
            {
                messageService.ShowError($"Не удалось изменить метку \"{tag.Name}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Метка \"{tag.Name}\" успешно изменена на \"{tag.NewName}\".");
            Tags = await manager.LoadTagsAsync(url, Tags);
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            view.SetTags(Tags?.Select(t => t.Name).ToArray());
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_EditCategoryClick(CategoryRequestModel category)
        {
            bool result = messageService.
                ShowOkCancelMessage($"Вы действительно хотите изменить категорию \"{category.Name}\" на \"{category.NewName}\"?");
            if (!result) return false;

            result = await manager.EditCategoryAsync(url, category);
            if (!result)
            {
                messageService.ShowError($"Не удалось изменить категорию \"{category.Name}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Категория \"{category.Name}\" успешно изменена на \"{category.NewName}\".");
            Categories = await manager.LoadCategoriesAsync(url, Categories);
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            view.SetCategories(Categories?.Select(c => c.Name).ToArray());
            ShowFilteredTasks();
            return true;
        }

        private async void View_TotalLoading()
        {
            bool isConnected = false;
            while (!isConnected)
            {
                Tags = await manager.LoadTagsAsync(url, Tags);
                isConnected = Tags is not null;
                if (!isConnected)
                {
                    var finish = messageService.ShowOkCancelMessage("Возможно нет соединения с сервером. Завершить работу?");
                    if (finish) view.Exit();
                }
            }
           
            Categories = await manager.LoadCategoriesAsync(url, Categories);
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            view.SetTags(Tags?.Select(t => t.Name).ToArray());
            view.SetCategories(Categories?.Select(c => c.Name).ToArray());
            ShowFilteredTasks();
        }

        private void View_CurrentTaskChenged(int taskId)
        {
            var task = Tasks?.FirstOrDefault(t => t.Id == taskId);
            view.CurrentTask = task is not null ? task : new();
        }

        private void View_CurrentTagChenged(string tagName)
        {
            var tn = tagName.Trim();
            var tag = Tags?.FirstOrDefault(t => t.Name == tn);
            view.CurrentTag = tag is not null ? tag : new() { Name = string.Empty };
            ShowFilteredTasks();
        }

        private void View_CurrentCategoryChenged(string categoryName)
        {
            var cn = categoryName.Trim();
            var category = Categories?.FirstOrDefault(c => c.Name == cn);
            view.CurrentCategory = category is not null ? category : new() { Name = string.Empty };
            ShowFilteredTasks();
        }

        private async Task<bool> View_AddNewTaskClick(TaskRequestModel task)
        {
            bool result = messageService.ShowOkCancelMessage($"Вы действительно хотите добавить задачу \"{task.Description}\"?");
            if (!result) return false;

            if (!TaskHelper.IsTaskFieldsCorrect(task))
            {
                messageService.ShowError("Не все поля задачи заполнены!");
                return false;
            }

            result = await manager.AddTaskAsync(url, task);
            if (!result)
            {
                messageService.ShowError($"Не удалось добавить задачу \"{task.Description}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Задача \"{task.Description}\" успешно добавлена.");
            Tasks = await manager.LoadTasksAsync(url, Tasks);
            view.SetTasks(Tasks);
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_AddNewTagClick(TagRequestModel tag)
        {
            bool result = messageService.ShowOkCancelMessage($"Вы действительно хотите добавить метку \"{tag.Name}\"?");
            if (!result) return false;

            result = await manager.AddTagAsync(url, tag);
            if (!result)
            {
                messageService.ShowError($"Не удалось добавить метку \"{tag.Name}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Метка \"{tag.Name}\" успешно добавлена.");
            Tags = await manager.LoadTagsAsync(url, Tags);
            view.SetTags(Tags?.Select(t => t.Name).ToArray());
            ShowFilteredTasks();
            return true;
        }

        private async Task<bool> View_AddNewCategoryClick(CategoryRequestModel category)
        {
            bool result = messageService.ShowOkCancelMessage($"Вы действительно хотите добавить категорию \"{category.Name}\"?");
            if (!result) return false;

            result = await manager.AddCategoryAsync(url, category);
            if (!result)
            {
                messageService.ShowError($"Не удалось добавить категорию \"{category.Name}\".");
                return false;
            }
            messageService.ShowSimpleMessage($"Категория \"{category.Name}\" успешно добавлена.");
            Categories = await manager.LoadCategoriesAsync(url, Categories);
            view.SetCategories(Categories?.Select(c => c.Name).ToArray());
            ShowFilteredTasks();
            return true;
        }

        private void ShowFilteredTasks()
        {
            var vctn = view.CurrentTag?.Name.Trim();
            var vccn = view.CurrentCategory?.Name.Trim();
            view.SetTasks(TaskHelper.FilterByTagAndCategory(Tasks, vctn, vccn));
        }
    }
}
