using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Managers;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;

namespace ToDoList.BL
{
    public class TopManager : ITopManager
    {
        private readonly IManager _tagManager;
        private readonly IManager _categoryManager;
        private readonly IManager _taskManager;

        public TopManager(IManager tagManager, IManager categoryManager, IManager taskManager)
        {
            _tagManager = tagManager;
            _categoryManager = categoryManager;
            _taskManager = taskManager;
        }

        #region Tags
        public async Task<List<TagResponseModel>> LoadTagsAsync(string url, List<TagResponseModel> list)
        {
            return await Task.Run(() => _tagManager.Load(url, list));
        }

        public async Task<bool> AddTagAsync(string url, TagRequestModel tag)
        {
            return await Task.Run(() => _tagManager.Add(url, tag));
        }

        public async Task<bool> EditTagAsync(string url, TagRequestModel tag)
        {
            return await Task.Run(() => _tagManager.Edit(url, tag));
        }

        public async Task<bool> RemoveTagAsync(string url, int id)
        {
            return await Task.Run(() => _tagManager.Remove(url, id));
        }
        #endregion

        #region Categories
        public async Task<List<CategoryResponseModel>> LoadCategoriesAsync(string url, List<CategoryResponseModel> list)
        {
            return await Task.Run(() => _categoryManager.Load(url, list));
        }

        public async Task<bool> AddCategoryAsync(string url, CategoryRequestModel tag)
        {
            return await Task.Run(() => _categoryManager.Add(url, tag));
        }

        public async Task<bool> EditCategoryAsync(string url, CategoryRequestModel tag)
        {
            return await Task.Run(() => _categoryManager.Edit(url, tag));
        }

        public async Task<bool> RemoveCategoryAsync(string url, int id)
        {
            return await Task.Run(() => _categoryManager.Remove(url, id));
        }
        #endregion

        #region Tasks
        public async Task<List<TaskResponseModel>> LoadTasksAsync(string url, List<TaskResponseModel> list)
        {
            return await Task.Run(() => _taskManager.Load(url, list));
        }

        public async Task<bool> AddTaskAsync(string url, TaskRequestModel tag)
        {
            return await Task.Run(() => _taskManager.Add(url, tag));
        }

        public async Task<bool> EditTaskAsync(string url, TaskRequestModel tag)
        {
            return await Task.Run(() => _taskManager.Edit(url, tag));
        }

        public async Task<bool> RemoveTaskAsync(string url, int id)
        {
            return await Task.Run(() => _taskManager.Remove(url, id));
        }
        #endregion
    }
}
