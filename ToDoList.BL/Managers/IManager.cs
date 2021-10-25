using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BL.Managers
{
    public interface IManager
    {
        List<T> Load<T>(string url, List<T> list);
        bool Add<T>(string url, T obj);
        bool Edit<T>(string url, T obj);
        bool Remove(string url, int id);
    }
}
