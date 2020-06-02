using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.UI {
    public partial class Button {
        public void Update(GameTime gameTime) {
            if (this.Rectangle.Contains(Inputs.CurrentMousePosition)) {
                _isHovering = true;

                if (Inputs.CurrentMouse.LeftButton == ButtonState.Released && Inputs.PrevMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            } else
                _isHovering = false;
        }
    }
}
