using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.UI {
    public partial class Button {
        private MouseState _prevMouse;
        private MouseState _currentMouse;

        public void Update(GameTime gameTime) {
            _prevMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle((int)(_currentMouse.X / Resolution.Scale.X),
                                               (int)(_currentMouse.Y / Resolution.Scale.Y),
                                               1,
                                               1);

            if (mouseRectangle.Intersects(Rectangle)) {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _prevMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            } else
                _isHovering = false;
        }
    }
}
