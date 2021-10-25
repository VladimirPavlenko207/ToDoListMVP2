using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BL.Managers
{
    public class BaseManager
    {
        protected bool GetUpdateResult(RestSharp.IRestResponse response)
        {
            return response.StatusCode != 0 && response.StatusCode != HttpStatusCode.BadRequest;
        }
    }
}
