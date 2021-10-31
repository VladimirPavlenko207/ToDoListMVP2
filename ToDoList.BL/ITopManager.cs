using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Models.Requests;
using ToDoList.BL.Models.Responses;

namespace ToDoList.BL
{
    /// <summary>
    /// Представляет главный менеджер по управлению менеджерами задач, меток и категорий.
    /// </summary>
    public interface ITopManager
    {
        /// <summary>
        /// Асинхронное добавление категории.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        Task<bool> AddCategoryAsync(string url, CategoryRequestModel cat);
        /// <summary>
        /// Асинхронное добавление метки.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<bool> AddTagAsync(string url, TagRequestModel tag);
        /// <summary>
        /// Асинхронное добавление задачи.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<bool> AddTaskAsync(string url, TaskRequestModel task);
        /// <summary>
        /// Асинхронное редактирование категории.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        Task<bool> EditCategoryAsync(string url, CategoryRequestModel cat);
        /// <summary>
        /// Асинхронное редактирование метки.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<bool> EditTagAsync(string url, TagRequestModel tag);
        /// <summary>
        /// Асинхронное редактирование задачи.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<bool> EditTaskAsync(string url, TaskRequestModel task);
        /// <summary>
        /// Асинхронная загрузка списка категорий.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<List<CategoryResponseModel>> LoadCategoriesAsync(string url, List<CategoryResponseModel> list);
        /// <summary>
        /// Асинхронная загрузка списка меток.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<List<TagResponseModel>> LoadTagsAsync(string url, List<TagResponseModel> list);
        /// <summary>
        /// Асинхронная загрузка списка задач.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<List<TaskResponseModel>> LoadTasksAsync(string url, List<TaskResponseModel> list);
        /// <summary>
        /// Асинхронная удаление категории.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveCategoryAsync(string url, int id);
        /// <summary>
        /// Асинхронная удаление метки.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveTagAsync(string url, int id);
        /// <summary>
        /// Асинхронная удаление задачи.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveTaskAsync(string url, int id);
    }
}
