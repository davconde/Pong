using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Components {
    public class Button : IUIComponent {
        private MouseState _prevMouse;
        private MouseState _currentMouse;
        private bool _isHovering;
        private Texture2D _texture;
        private SpriteFont _font;

        public Vector2 Position;
        public bool Clicked;
        public string Text;
        public Color TextColor;
        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public event EventHandler Click;

        public Button(Texture2D texture, SpriteFont font) {
            _texture = texture;
            _font = font;

            TextColor = Color.White;
        }

        public void Update(GameTime gameTime) {
#if ANDROID
            if (TouchPanel.GetState().Count > 0)
                foreach (TouchLocation touch in TouchPanel.GetState())
                    if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                        if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(this.Rectangle))
                            Click?.Invoke(this, new EventArgs());
#endif

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            var color = Color.White;

            if (_isHovering)
                color = Color.Gray;

            spriteBatch.Draw(_texture, Position, color);

            if (!string.IsNullOrEmpty(Text)) {
                Rectangle buttonRectangle = this.Rectangle;
                Vector2 textSize = _font.MeasureString(Text);

                float xPosition = buttonRectangle.X + buttonRectangle.Width / 2 - textSize.X / 2;
                float yPosition = buttonRectangle.Y + buttonRectangle.Height / 2 - textSize.Y / 2;

                spriteBatch.DrawString(_font, Text, new Vector2(xPosition, yPosition), TextColor);
            }
        }
    }
}
