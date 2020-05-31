using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Pong.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.IO {
    public class Input {
        public Sprite Parent;

        public Keys UpKey;
        public Keys DownKey;

        public bool Up {
            get {
#if WINDOWS
                return Keyboard.GetState().IsKeyDown(UpKey);
#elif ANDROID
                if (TouchPanel.GetState().Count > 0)
                    foreach (TouchLocation touch in TouchPanel.GetState())
                        if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                            if ((int)touch.Position.Y < (int)Parent.Center.Y - Parent.Speed)
                                return true;
                return false;
#endif
            }
        }

        public bool Down {
            get {
#if WINDOWS
                return Keyboard.GetState().IsKeyDown(DownKey);
#elif ANDROID
                if (TouchPanel.GetState().Count > 0)
                    foreach (TouchLocation touch in TouchPanel.GetState())
                        if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                            if ((int)touch.Position.Y > (int)Parent.Center.Y + Parent.Speed)
                                return true;
                return false;
#endif
            }
        }
    }
}
