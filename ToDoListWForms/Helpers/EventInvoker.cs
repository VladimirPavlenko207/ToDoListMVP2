using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Models.Requests;

namespace ToDoListWForms.Helpers
{
    public static class EventInvoker
    {
        internal static Func<Task<bool>> InvokeAddNewCategoryClick(Func<CategoryRequestModel, Task<bool>> addNewCategoryClick, CategoryRequestModel currentCategory)
        {
            return new Func<Task<bool>>(() => addNewCategoryClick?.Invoke(currentCategory));
        }

        internal static Func<Task<bool>> InvokeEditCategoryClick(Func<CategoryRequestModel, Task<bool>> editCategoryClick, CategoryRequestModel currentCategory)
        {
            return new Func<Task<bool>>(() => editCategoryClick?.Invoke(currentCategory));
        }

        internal static Func<Task<bool>> InvokeAddNewTagClick(Func<TagRequestModel, Task<bool>> addNewTagClick, TagRequestModel currentTag)
        {
            return new Func<Task<bool>>(() => addNewTagClick?.Invoke(currentTag));
        }

        internal static Func<Task<bool>> InvokeEditTagClick(Func<TagRequestModel, Task<bool>> editTagClick, TagRequestModel currentTag)
        {
            return new Func<Task<bool>>(() => editTagClick?.Invoke(currentTag));
        }

        internal static Func<Task<bool>> InvokeAddNewTaskClick(Func<TaskRequestModel, Task<bool>> addNewTaskClick, TaskRequestModel newTask)
        {
            return new Func<Task<bool>>(() => addNewTaskClick?.Invoke(newTask));
        }

        internal static Func<Task<bool>> InvokeEditTaskClick(Func<TaskRequestModel, Task<bool>> editTaskClick, TaskRequestModel newTask)
        {
            return new Func<Task<bool>>(() => editTaskClick?.Invoke(newTask));
        }
    }
}
