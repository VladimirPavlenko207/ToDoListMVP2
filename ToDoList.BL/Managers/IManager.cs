using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BL.Managers
{
    /// <summary>
    /// Представляет менеджер для работы с сущностями.
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Универсальный метод загрузки списка сущностей с сервера по адресу url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        List<T> Load<T>(string url, List<T> list);
        /// <summary>
        /// Универсальный метод добавления екземпляра сущности на сервер по адресу url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Add<T>(string url, T obj);
        /// <summary>
        /// Универсальный метод редактирования екземпляра сущности и сохранения его на сервере по адресу url.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Edit<T>(string url, T obj);
        /// <summary>
        /// Универсальный метод удаления екземпляра сущности с серверф по адресу url.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Remove(string url, int id);
    }
}
