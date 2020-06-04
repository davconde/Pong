using Microsoft.Xna.Framework;
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

        public static Point CurrentMousePosition {
            get {
                return MousePosition(CurrentMouse);
            }
        }

        public static Point PrevMousePosition {
            get {
                return MousePosition(PrevMouse);
            }
        }

        public static void Update() {
            PrevKeys = CurrentKeys;
            CurrentKeys = Keyboard.GetState();

            PrevMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            CurrentTouchs = TouchPanel.GetState();
        }

        public static Point MousePosition(MouseState mouseState) {
            return new Point((int)(mouseState.X / Resolution.Scale.X), (int)(mouseState.Y / Resolution.Scale.Y));
        }

        public static Vector2 TouchPosition(TouchLocation touchLocation) {
            return Vector2.Transform(touchLocation.Position, Matrix.Invert(Resolution.ScaleMatrix));
            //return new Vector2(touchLocation.Position.X / Resolution.Scale.X, touchLocation.Position.Y / Resolution.Scale.Y);
        }
    }
}
