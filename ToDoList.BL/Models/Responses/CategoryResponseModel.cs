using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BL.Models.Responses
{
    /// <summary>
    /// Модель получения категории
    /// </summary>
    public class CategoryResponseModel
    {
        /// <summary>
        /// Id категории
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя категории
        /// </summary>
        public string Name { get; set; }
    }
}
