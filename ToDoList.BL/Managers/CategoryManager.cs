using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Helpers;

namespace ToDoList.BL.Managers
{
    public class CategoryManager : BaseManager, IManager
    {
        public bool Add<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}category/create", obj);
            return GetUpdateResult(response);
        }

        public bool Edit<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}category/edit", obj);
            return GetUpdateResult(response);
        }

        public List<T> Load<T>(string url, List<T> list)
        {
            var response = RequestsSender.ClientExecuter(0, $"{url}category");
            return RequestsSender.GetDeserializedObject(list, response.Content);
        }

        public bool Remove(string url, int id)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}category/remove?id={id}");
            return GetUpdateResult(response);
        }
    }
}
