using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmElF.Services
{
    /// <summary>
    /// Определяет модальность окна.
    /// </summary>
    public enum Modality
    {
        /// <summary>
        /// Модальное окно.
        /// </summary>
        Modal,

        /// <summary>
        /// Параллельное окно.
        /// </summary>
        Parallel
    }
}
