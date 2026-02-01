using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseManager
{
    public static class DataGridViewExtensions
    {
        public static void EnableDoubleBuffering(this DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgv,
                new object[] { true }
            );
        }

        public static bool IsDoubleBuffered(this DataGridView dgv)
        {
            return (bool)typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty,
                null,
                dgv,
                null);
        }
    }
}
