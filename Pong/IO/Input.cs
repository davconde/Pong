using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.IO {
    public class Input {
        public Keys UpKey;
        public Keys DownKey;

        public bool Up {
            get {
#if WINDOWS || LINUX
                return Keyboard.GetState().IsKeyDown(UpKey);
#endif
            }
        }

        public bool Down {
            get {
                return Keyboard.GetState().IsKeyDown(DownKey);
            }
        }
    }
}
