using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;

namespace ToDoList.BL
{
    public interface ITopManager
    {
        Task<bool> AddCategoryAsync(string url, CategoryRequestModel cat);
        Task<bool> AddTagAsync(string url, TagRequestModel tag);
        Task<bool> AddTaskAsync(string url, TaskRequestModel task);
        Task<bool> EditCategoryAsync(string url, CategoryRequestModel cat);
        Task<bool> EditTagAsync(string url, TagRequestModel tag);
        Task<bool> EditTaskAsync(string url, TaskRequestModel task);
        Task<List<CategoryResponseModel>> LoadCategoriesAsync(string url, List<CategoryResponseModel> list);
        Task<List<TagResponseModel>> LoadTagsAsync(string url, List<TagResponseModel> list);
        Task<List<TaskResponseModel>> LoadTasksAsync(string url, List<TaskResponseModel> list);
        Task<bool> RemoveCategoryAsync(string url, int id);
        Task<bool> RemoveTagAsync(string url, int id);
        Task<bool> RemoveTaskAsync(string url, int id);
    }
}
