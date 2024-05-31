using BIOSTAR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BIOSTAR.Core
{
    public static class FrameNavigate
    {
        public static Frame FrameObject { get; set; }
        public static RepairServiceDBEntities DB { get; set; }
        public static bool IsAdminAuthenticated { get; set; } = false; // Статическое свойство для состояния аутентификации
    }
}
