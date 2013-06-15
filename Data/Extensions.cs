#region Using
using System;
using System.Windows.Forms; 
#endregion

namespace Data
{
    public static class Extensions
    {
        #region Extension Methods
        public static void InvokeIfRequired(this Control c, Action<Control> action)
        {
            if (c.InvokeRequired) {
                c.Invoke(new Action(() => action(c)));
            } else {
                action(c);
            }
        } 
        #endregion
    }
}
