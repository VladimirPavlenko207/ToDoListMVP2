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
    /// <summary>
    /// Главный менеджер по управлению менеджерами задач, меток и категорий.
    /// </summary>
    public class TopManager : ITopManager
    {
        private readonly IManager _tagManager;
        private readonly IManager _categoryManager;
        private readonly IManager _taskManager;

        /// <summary>
        /// Конструктор главного менеджера, принимает ссылку на менеджеры меток, категорий и задач.
        /// </summary>
        /// <param name="tagManager"></param>
        /// <param name="categoryManager"></param>
        /// <param name="taskManager"></param>
        public TopManager(IManager tagManager, IManager categoryManager, IManager taskManager)
        {
            _tagManager = tagManager;
            _categoryManager = categoryManager;
            _taskManager = taskManager;
        }

        #region Tags
        /// <summary>
        /// Асинхронный метод загрузки списка меток.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<TagResponseModel>> LoadTagsAsync(string url, List<TagResponseModel> list)
        {
            return await Task.Run(() => _tagManager.Load(url, list));
        }

        /// <summary>
        /// Асинхронный метод добавления метки.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<bool> AddTagAsync(string url, TagRequestModel tag)
        {
            return await Task.Run(() => _tagManager.Add(url, tag));
        }

        /// <summary>
        /// Асинхронный метод редактирования метки.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<bool> EditTagAsync(string url, TagRequestModel tag)
        {
            return await Task.Run(() => _tagManager.Edit(url, tag));
        }

        /// <summary>
        /// Асинхронный метод удаления метки.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTagAsync(string url, int id)
        {
            return await Task.Run(() => _tagManager.Remove(url, id));
        }
        #endregion

        #region Categories
        /// <summary>
        /// Асинхронный метод загрузки списка категорий.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<CategoryResponseModel>> LoadCategoriesAsync(string url, List<CategoryResponseModel> list)
        {
            return await Task.Run(() => _categoryManager.Load(url, list));
        }

        /// <summary>
        /// Асинхронный метод добавления категории.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        public async Task<bool> AddCategoryAsync(string url, CategoryRequestModel cat)
        {
            return await Task.Run(() => _categoryManager.Add(url, cat));
        }

        /// <summary>
        /// Асинхронный метод редактирования категории.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        public async Task<bool> EditCategoryAsync(string url, CategoryRequestModel cat)
        {
            return await Task.Run(() => _categoryManager.Edit(url, cat));
        }

        /// <summary>
        /// Асинхронный метод удаления категории.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveCategoryAsync(string url, int id)
        {
            return await Task.Run(() => _categoryManager.Remove(url, id));
        }
        #endregion

        #region Tasks
        /// <summary>
        /// Асинхронный метод загрузки списка задач.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<TaskResponseModel>> LoadTasksAsync(string url, List<TaskResponseModel> list)
        {
            return await Task.Run(() => _taskManager.Load(url, list));
        }

        /// <summary>
        /// Асинхронный метод добавления задачи.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> AddTaskAsync(string url, TaskRequestModel task)
        {
            return await Task.Run(() => _taskManager.Add(url, task));
        }

        /// <summary>
        /// Асинхронный метод редактирования задачи.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> EditTaskAsync(string url, TaskRequestModel task)
        {
            return await Task.Run(() => _taskManager.Edit(url, task));
        }

        /// <summary>
        /// Асинхронный метод удаления задачи.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTaskAsync(string url, int id)
        {
            return await Task.Run(() => _taskManager.Remove(url, id));
        }
        #endregion
    }
}
