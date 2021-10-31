using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BL.Managers
{
    /// <summary>
    /// Базовый класс менеджера.
    /// </summary>
    public class BaseManager
    {
        /// <summary>
        /// Метод получения результата обновления.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected bool GetUpdateResult(RestSharp.IRestResponse response)
        {
            return response.StatusCode != 0 && response.StatusCode != HttpStatusCode.BadRequest;
        }
    }
}
