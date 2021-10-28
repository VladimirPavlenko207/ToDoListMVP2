using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;

namespace ToDoListPresenter
{
    public interface IView
    {
        CategoryResponseModel CurrentCategory { get; set; }
        TagResponseModel CurrentTag { get; set; }
        TaskResponseModel CurrentTask { get; set; }
        void SetTags(string[] tagNames);
        void SetCategories(string[] categoryNames);
        void SetTasks(List<TaskResponseModel> tasks);

        event Action TotalLoading;

        event Action<string> CurrentTagChenged;
        event Action<string> CurrentCategoryChenged;
        event Action<int> CurrentTaskChenged;

        event Func<TagRequestModel, Task<bool>> AddNewTagClick;
        event Func<CategoryRequestModel, Task<bool>> AddNewCategoryClick;
        event Func<TaskRequestModel, Task<bool>> AddNewTaskClick;

        event Func<TagRequestModel, Task<bool>> EditTagClick;
        event Func<CategoryRequestModel, Task<bool>> EditCategoryClick;
        event Func<TaskRequestModel, Task<bool>> EditTaskClick;

        event Func<TagResponseModel, Task<bool>> RemoveTagClick;
        event Func<CategoryResponseModel, Task<bool>> RemoveCategoryClick;
        event Func<TaskResponseModel, Task<bool>> RemoveTaskClick;
    }
}
