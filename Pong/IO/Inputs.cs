using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong.IO {
    public static class Inputs {
        public static KeyboardState PrevKeys;
        public static KeyboardState CurrentKeys;

        public static MouseState PrevMouse;
        public static MouseState CurrentMouse;

        public static TouchCollection CurrentTouchs;

        public static void Update() {
            PrevKeys = CurrentKeys;
            CurrentKeys = Keyboard.GetState();

            PrevMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            CurrentTouchs = TouchPanel.GetState();
        }
    }
}
