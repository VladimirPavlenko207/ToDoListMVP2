using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Helpers;

namespace ToDoList.BL.Managers
{
    public class TagManager : BaseManager, IManager
    {
        public bool Add<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}tag/create", obj);
            return GetUpdateResult(response);
        }

        public bool Edit<T>(string url, T obj)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}tag/edit", obj);
            return GetUpdateResult(response);
        }

        public List<T> Load<T>(string url, List<T> list)
        {
            var response = RequestsSender.ClientExecuter(0, $"{url}tag");
            return RequestsSender.GetDeserializedObject(list, response.Content);
        }

        public bool Remove(string url, int id)
        {
            var response = RequestsSender.ClientExecuter(1, $"{url}tag/remove?id={id}");
            return GetUpdateResult(response);
        }
    }
}
