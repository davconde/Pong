using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.IO {
    public partial class PlayerInput {
        public bool Up {
            get {
                return Inputs.CurrentKeys.IsKeyDown(UpKey);
            }
        }

        public bool Down {
            get {
                return Inputs.CurrentKeys.IsKeyDown(DownKey);
            }
        }
    }
}
