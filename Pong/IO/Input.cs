using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.IO {
    public class Input {
        public Keys UpKey;
        public Keys DownKey;

        public Rectangle UpArea = new Rectangle(0, 0, Resolution.GameWidth / 2, Resolution.GameHeight / 2);
        public Rectangle DownArea = new Rectangle(0, Resolution.GameHeight / 2, Resolution.GameWidth / 2, Resolution.GameHeight);

        public bool Up {
            get {
#if WINDOWS
                return Keyboard.GetState().IsKeyDown(UpKey);
#elif ANDROID
                if (TouchPanel.GetState().Count > 0)
                    foreach (TouchLocation touch in TouchPanel.GetState())
                        if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                            if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(UpArea))
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
                            if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(DownArea))
                                return true;
                return false;
#endif
            }
        }
    }
}
