using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.IO {
    public partial class Input {
        public bool Up {
            get {
                return Keyboard.GetState().IsKeyDown(UpKey);
            }
        }

        public bool Down {
            get {
                return Keyboard.GetState().IsKeyDown(DownKey);
            }
        }
    }
}
