using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.UI {
    public partial class Button : IUIComponent {
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
