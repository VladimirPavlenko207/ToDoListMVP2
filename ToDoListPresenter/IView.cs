using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;

namespace ToDoListPresenter
{
    /// <summary>
    /// Представляет оконный интерфейс приложения.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Текущая категория.
        /// </summary>
        CategoryResponseModel CurrentCategory { get; set; }
        /// <summary>
        /// Текущая метка.
        /// </summary>
        TagResponseModel CurrentTag { get; set; }
        /// <summary>
        /// Текущая задача.
        /// </summary>
        TaskResponseModel CurrentTask { get; set; }
        /// <summary>
        /// Метод, устанавливающий метки полученные в виде массива названий меток.
        /// </summary>
        /// <param name="tagNames"></param>
        void SetTags(string[] tagNames);
        /// <summary>
        /// Метод, устанавливающий категории полученные в виде массива названий категорий.
        /// </summary>
        /// <param name="categoryNames"></param>
        void SetCategories(string[] categoryNames);
        /// <summary>
        /// Метод, устанавливающий задачи полученные в виде списка TaskResponseModel.
        /// </summary>
        /// <param name="tasks"></param>
        void SetTasks(List<TaskResponseModel> tasks);
        /// <summary>
        /// Метод завершения приложения.
        /// </summary>
        void Exit();

        /// <summary>
        /// Событие, при котором загружаются все модели.
        /// </summary>
        event Action TotalLoading;

        /// <summary>
        /// Событие, при котором текущая метка изменилась.
        /// </summary>
        event Action<string> CurrentTagChenged;
        /// <summary>
        /// Событие, при котором текущая категория изменилась.
        /// </summary>
        event Action<string> CurrentCategoryChenged;
        /// <summary>
        /// Событие, при котором текущая задача изменилась.
        /// </summary>
        event Action<int> CurrentTaskChenged;

        /// <summary>
        /// Событие, при котором добавляется новая метка и сохраняется в базе данных.
        /// </summary>
        event Func<TagRequestModel, Task<bool>> AddNewTagClick;
        /// <summary>
        /// Событие, при котором добавляется новая категория и сохраняется в базе данных.
        /// </summary>
        event Func<CategoryRequestModel, Task<bool>> AddNewCategoryClick;
        /// <summary>
        /// Событие, при котором добавляется новая задача и сохраняется в базе данных.
        /// </summary>
        event Func<TaskRequestModel, Task<bool>> AddNewTaskClick;

        /// <summary>
        /// Событие, при котором редактируется данная метка и сохраняется в базе данных.
        /// </summary>
        event Func<TagRequestModel, Task<bool>> EditTagClick;
        /// <summary>
        /// Событие, при котором редактируется данная категория и сохраняется в базе данных.
        /// </summary>
        event Func<CategoryRequestModel, Task<bool>> EditCategoryClick;
        /// <summary>
        /// Событие, при котором редактируется данная задача и сохраняется в базе данных.
        /// </summary>
        event Func<TaskRequestModel, Task<bool>> EditTaskClick;

        /// <summary>
        /// Событие, при котором удаляется данная метка и изменения сохраняются в базе данных.
        /// </summary>
        event Func<TagResponseModel, Task<bool>> RemoveTagClick;
        /// <summary>
        /// Событие, при котором удаляется данная категория и изменения сохраняются в базе данных.
        /// </summary>
        event Func<CategoryResponseModel, Task<bool>> RemoveCategoryClick;
        /// <summary>
        /// Событие, при котором удаляется данная задача и изменения сохраняются в базе данных.
        /// </summary>
        event Func<TaskResponseModel, Task<bool>> RemoveTaskClick;
    }
}
