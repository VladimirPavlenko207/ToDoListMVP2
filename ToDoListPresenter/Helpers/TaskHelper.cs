using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;

namespace ToDoListPresenter.Helpers
{
    internal static class TaskHelper
    {
        private static List<TaskResponseModel> ByTag(List<TaskResponseModel> tasks, string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) return tasks;
            return tasks.Where(task => task.Tags.Any(tag => tag.Name == tagName)).ToList();
        }

        private static List<TaskResponseModel> ByCategory(List<TaskResponseModel> tasks, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) return tasks;
            return tasks.Where(task => task.Category.Name == categoryName).ToList();
        }

        internal static List<TaskResponseModel> FilterByTagAndCategory(List<TaskResponseModel> tasks, string tagName, string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName) && string.IsNullOrEmpty(tagName)) return tasks;
            if (string.IsNullOrEmpty(categoryName)) return ByTag(tasks, tagName);
            if (string.IsNullOrEmpty(tagName)) return ByCategory(tasks, categoryName);
            return ByCategory(ByTag(tasks, tagName), categoryName);
        }

        internal static bool IsTaskFieldsCorrect(TaskRequestModel task)
        {
            if (task.Description.Trim().Length == 0) return false;
            if (task.CategoryName.Trim().Length == 0) return false;
            if (task.Priority < 0) return false;
            return true;
        }
    }
}
