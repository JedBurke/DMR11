using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MangaRipper.Helper
{
    /// <summary>
    /// Represents a class used to get the state of a keyboard key.
    /// http://www.pinvoke.net/default.aspx/user32.getkeystate
    /// </summary>
    public class KeyState
    {
        [DllImport("user32.dll")]
        static extern short GetKeyState(VirtualKeyStates nVirtKey);

        const int KEY_PRESSED = 0x8000;

        public static bool IsKeyPressed(VirtualKeyStates vKey)
        {
            return Convert.ToBoolean(GetKeyState(vKey) & KEY_PRESSED);
        }

        public static bool IsShiftKeyPressed()
        {
            return IsKeyPressed(VirtualKeyStates.VK_SHIFT);
        }
    }
}
