using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoKeyboard {
    /// <summary>
    /// 枚举类型，键盘按键的键码值
    /// </summary>
    public enum KeyValueEnum : byte {
        //常用键：字母键A到Z
        vbKeyA = 65,
        vbKeyB = 66,
        vbKeyC = 67,
        vbKeyD = 68,
        vbKeyE = 69,
        vbKeyF = 70,
        vbKeyG = 71,
        vbKeyH = 72,
        vbKeyI = 73,
        vbKeyJ = 74,
        vbKeyK = 75,
        vbKeyL = 76,
        vbKeyM = 77,
        vbKeyN = 78,
        vbKeyO = 79,
        vbKeyP = 80,
        vbKeyQ = 81,
        vbKeyR = 82,
        vbKeyS = 83,
        vbKeyT = 84,
        vbKeyU = 85,
        vbKeyV = 86,
        vbKeyW = 87,
        vbKeyX = 88,
        vbKeyY = 89,
        vbKeyZ = 90,

        // 数字键盘：0 - 9
        vbKey0 = 48,    // 0 键
        vbKey1 = 49,    // 1 键
        vbKey2 = 50,    // 2 键
        vbKey3 = 51,    // 3 键
        vbKey4 = 52,    // 4 键
        vbKey5 = 53,    // 5 键
        vbKey6 = 54,    // 6 键
        vbKey7 = 55,    // 7 键
        vbKey8 = 56,    // 8 键
        vbKey9 = 57,    // 9 键

        // 小键盘按键
        vbKeyNumpad0 = 0x60,    // 0 键
        vbKeyNumpad1 = 0x61,    // 1 键
        vbKeyNumpad2 = 0x62,    // 2 键
        vbKeyNumpad3 = 0x63,    // 3 键
        vbKeyNumpad4 = 0x64,    // 4 键
        vbKeyNumpad5 = 0x65,    // 5 键
        vbKeyNumpad6 = 0x66,    // 6 键
        vbKeyNumpad7 = 0x67,    // 7 键
        vbKeyNumpad8 = 0x68,    // 8 键
        vbKeyNumpad9 = 0x69,    // 9 键
        vbKeyMultiply = 0x6A,   // MULTIPLICATIONSIGN(*)键
        vbKeyAdd = 0x6B,        // PLUS SIGN(+) 键
        vbKeySeparator = 0x6C,  // ENTER 键
        vbKeySubtract = 0x6D,   // MINUS SIGN(-) 键
        vbKeyDecimal = 0x6E,    // DECIMAL POINT(.) 键
        vbKeyDivide = 0x6F,     // DIVISION SIGN(/) 键


        // F1到F12按键
        vbKeyF1 = 0x70,   // F1 键
        vbKeyF2 = 0x71,   // F2 键
        vbKeyF3 = 0x72,   // F3 键
        vbKeyF4 = 0x73,   // F4 键
        vbKeyF5 = 0x74,   // F5 键
        vbKeyF6 = 0x75,   // F6 键
        vbKeyF7 = 0x76,   // F7 键
        vbKeyF8 = 0x77,   // F8 键
        vbKeyF9 = 0x78,   // F9 键
        vbKeyF10 = 0x79,  // F10 键
        vbKeyF11 = 0x7A,  // F11 键
        vbKeyF12 = 0x7B,  // F12 键
        vbKeyF13 = 124,  // F13 键
        vbKeyF14 = 125,  // F14 键
        vbKeyF15 = 126,  // F15 键
        vbKeyF16 = 127,  // F16 键
        vbKeyF17 = 128,  // F17 键
        vbKeyF18 = 129,  // F18 键
        vbKeyF19 = 130,  // F19 键
        vbKeyF20 = 131,  // F20 键
        vbKeyF21 = 132,  // F21 键
        vbKeyF22 = 133,  // F22 键
        vbKeyF23 = 134,  // F23 键
        vbKeyF24 = 135,  // F24 键

        // 其他常用按键
        vbKeyLButton = 0x1,    // 鼠标左键
        vbKeyRButton = 0x2,    // 鼠标右键
        vbKeyCancel = 0x3,     // CANCEL 键
        vbKeyMButton = 0x4,    // 鼠标中键
        vbKeyBack = 0x8,       // BACKSPACE 键
        vbKeyTab = 0x9,        // TAB 键
        vbKeyClear = 0xC,      // CLEAR 键
        vbKeyReturn = 0xD,     // ENTER 键
        vbKeyShift = 0x10,     // SHIFT 键

        vbKeyControl = 0x11,   // CTRL 键
        vbKeyAlt = 18,         // Alt 键  (键码18)
        vbKeyMenu = 0x12,      // MENU 键
        vbKeyPause = 0x13,     // PAUSE 键
        vbKeyCapital = 0x14,   // CAPS LOCK 键
        vbKeyEscape = 0x1B,    // ESC 键
        vbKeySpace = 0x20,     // SPACEBAR 键

        vbKeyIns = 0x2E,        // Insert 键
        vbKeyInsert = 0x2E,     // Insert 键
        vbKeyDel = 0x2E,        // Delete 键
        vbKeyDelete = 0x2E,     // Delete 键
        vbKeyPageUp = 0x21,     // PAGE UP 键
        vbKeyPgUp = 0x21,       // PAGE UP 键
        vbKeyPageDown = 0x22,   // PAGE DOWN 键
        vbKeyPgDn = 0x22,       // PAGE DOWN 键
        vbKeyEnd = 0x23,        // End 键
        vbKeyHome = 0x24,       // HOME 键

        vbKeyLeft = 0x25,      // LEFT ARROW 键
        vbKeyUp = 0x26,        // UP ARROW 键
        vbKeyRight = 0x27,     // RIGHT ARROW 键
        vbKeyDown = 0x28,      // DOWN ARROW 键

        vbKeySelect = 0x29,    // Select 键
        vbKeyPrint = 0x2A,     // PRINT SCREEN 键
        vbKeyExecute = 0x2B,   // EXECUTE 键
        vbKeySnapshot = 0x2C,  // SNAPSHOT 键
        vbKeyHelp = 0x2F,      // HELP 键
        vbKeyNumlock = 0x90,   // NUM LOCK 键


        vbKeyMediaNextTrack = 176,      //媒体下一曲目键。
        vbKeyMediaPreviousTrack = 177,  //媒体上一曲目键。
        vbKeyMediaStop = 178,           //媒体停止键。
        vbKeyMediaPlayPause = 179,      //媒体播放暂停键。


    }
}
