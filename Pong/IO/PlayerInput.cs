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
    public partial class PlayerInput {
        public Sprite Parent;

        public Keys UpKey;
        public Keys DownKey;
        public Rectangle TouchArea;
    }
}
