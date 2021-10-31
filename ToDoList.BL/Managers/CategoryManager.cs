using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Helpers;

namespace ToDoList.BL.Managers
{
    /// <summary>
    /// Менеджер по управлению категориями.
    /// </summary>
    public class CategoryManager : BaseManager, IManager
    {
        /// <summary>
        /// Добавление новой категории.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Add<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}category/create", obj);
            return GetUpdateResult(response);
        }

        /// <summary>
        /// Редактирование данной категории.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Edit<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}category/edit", obj);
            return GetUpdateResult(response);
        }

        /// <summary>
        /// Загрузка списка категорий.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T> Load<T>(string url, List<T> list)
        {
            var response = RequestsSender.ClientExecuter(0, $"{url}category");
            return RequestsSender.GetDeserializedObject(list, response.Content);
        }

        /// <summary>
        /// Удаление категории с данным id.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(string url, int id)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}category/remove?id={id}");
            return GetUpdateResult(response);
        }
    }
}
