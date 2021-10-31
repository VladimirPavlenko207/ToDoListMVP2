using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Helpers;

namespace ToDoList.BL.Managers
{
    /// <summary>
    /// Менеджер по управлению метками.
    /// </summary>
    public class TagManager : BaseManager, IManager
    {
        /// <summary>
        /// Добавление новой метки.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Add<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}tag/create", obj);
            return GetUpdateResult(response);
        }

        /// <summary>
        /// Редактирование данной метки.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Edit<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}tag/edit", obj);
            return GetUpdateResult(response);
        }

        /// <summary>
        /// Загрузка списка меток.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T> Load<T>(string url, List<T> list)
        {
            var response = RequestsSender.ClientExecuter(0, $"{url}tag");
            return RequestsSender.GetDeserializedObject(list, response.Content);
        }

        /// <summary>
        /// Удаление метки с данным id.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(string url, int id)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}tag/remove?id={id}");
            return GetUpdateResult(response);
        }
    }
}
