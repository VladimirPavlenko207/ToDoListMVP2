using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListWForms.Enums
{
    /// <summary>
    /// Представляет режимы CRUD.
    /// </summary>
    public enum CrudMode
    {
        /// <summary>
        /// Режим просмотра.
        /// </summary>
        View,
        /// <summary>
        /// Режим добавления.
        /// </summary>
        Add,
        /// <summary>
        /// Режим редактирования.
        /// </summary>
        Edit,
        /// <summary>
        /// Режим удаления.
        /// </summary>
        Remove
    }
}
