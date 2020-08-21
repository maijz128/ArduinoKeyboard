using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoKeyboard {
    public class ArduinoMessageHandler {

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int ToUnicode(
            uint virtualKeyCode,
            uint scanCode,
            byte[] keyboardState,
            StringBuilder receivingBuffer,
            int bufferSize,
            uint flags
        );

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        //public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        public static extern void keybd_event_byte(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        Dictionary<String, byte> sToByteMap = new Dictionary<string, byte>()
        {
            { "*",          106 },
            { "+",          107 },
            { "Enter",      108 },
            { "-",          109 },
            { ".",          110 },
            { "/",          111 },
            { "BrowserBack",                    166  },
            { "BrowserForward",                 167  },
            { "BrowserRefresh",                 168  },
            { "BrowserStop",                    169  },
            { "BrowserSearch",                  170  },
            { "BrowserFavorites",               171  },
            { "BrowserHome",                    172  },
        };

        public void Handle(string message) {
            //keybd_event(Keys.A, 0, 0, 0);
            Console.Write("message: " + message);

            byte key = GetKeyValue(message);
        
            Console.WriteLine("   key code >> " + message + ": " + key.ToString());
            //keybd_event_byte(key, 0, 0, 0);
        }

        public void Handle(byte keycode) {
            //keybd_event(Keys.A, 0, 0, 0);
            Console.Write("keycode: " + keycode);

            byte key = keycode;

            StringBuilder charPressed = new StringBuilder(256);
            ToUnicode((uint)keycode, 0, new byte[256], charPressed, charPressed.Capacity, 0);

            Console.WriteLine("  key: "  + charPressed.ToString());
            keybd_event_byte(key, 0, 0, 0);
        }

        /// <summary>
        /// 字符串转换为键码值的集合
        /// </summary>
        /// <param name="str">待转换的字符串</param>
        /// <returns>返回键码集合</returns>
        public ArrayList GetKeyValues(string str) {
            ArrayList kvList = new ArrayList();
            for (int i = 0; i < str.Length; i++) {
                var vbKey = string.Concat("vbKey", str[i].ToString().ToUpper());
                if (vbKey.Trim() != "vbKey") {
                    kvList.Add((byte)Enum.Parse(typeof(KeyValueEnum), vbKey));
                }

            }
            return kvList;
        }

        public byte GetKeyValue(string str) {
            var vbKey = string.Concat("vbKey", str.Trim().ToUpper());
            if (vbKey != "vbKey") {
                return (byte)Enum.Parse(typeof(KeyValueEnum), vbKey);
            }
            return MappingKeyValue(str);
        }

        public byte MappingKeyValue(string str) {
            foreach (var key in sToByteMap.Keys) {
                if (key.ToLower() == str.Trim().ToLower()) {
                    return sToByteMap[key];
                }
            }
            throw new Exception("Not found vbKey: " + str);
        }

        static string GetCharsFromKeys(Keys keys, bool shift) {
            var buf = new StringBuilder(256);
            var keyboardState = new byte[256];
            if (shift) {
                keyboardState[(int)Keys.ShiftKey] = 0xff;
            }
            ToUnicode((uint)keys, 0, keyboardState, buf, 256, 0);
            return buf.ToString();
        }

    }
}
