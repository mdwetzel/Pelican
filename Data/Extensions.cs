using System;
using System.Windows.Forms;

namespace Data
{
    public static class Extensions
    {
        public static void InvokeIfRequired(this Control c, Action<Control> action)
        {
            if (c.InvokeRequired) {
                c.Invoke(new Action(() => action(c)));
            } else {
                action(c);
            }
        }
    }
}
