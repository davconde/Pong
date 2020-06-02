using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Sprites {
    public class Sprite {
        protected Texture2D _texture;

        public Vector2 Position;
        public Vector2 Velocity;
        public float Speed;
        public PlayerInput Input;

        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Point Center {
            get {
                return new Point((int)Position.X + _texture.Width / 2, (int)Position.Y + _texture.Height / 2);
            }
        }

        public Sprite(Texture2D texture) {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites) {

        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        #region Collision Detection
        public bool IsTouchingLeft(Sprite other) {
            return (this.Rectangle.Right + Velocity.X > other.Rectangle.Left &&
                this.Rectangle.Left + Velocity.X < other.Rectangle.Left &&
                this.Rectangle.Top + Velocity.Y < other.Rectangle.Bottom &&
                this.Rectangle.Bottom + Velocity.Y > other.Rectangle.Top);
        }
        public bool IsTouchingRight(Sprite other) {
            return (this.Rectangle.Left + Velocity.X < other.Rectangle.Right &&
                this.Rectangle.Right + Velocity.X > other.Rectangle.Right &&
                this.Rectangle.Top + Velocity.Y < other.Rectangle.Bottom &&
                this.Rectangle.Bottom + Velocity.Y > other.Rectangle.Top);
        }
        public bool IsTouchingTop(Sprite other) {
            return (this.Rectangle.Bottom + Velocity.Y > other.Rectangle.Top &&
                this.Rectangle.Top + Velocity.Y < other.Rectangle.Top &&
                this.Rectangle.Left + Velocity.X < other.Rectangle.Right &&
                this.Rectangle.Right + Velocity.X > other.Rectangle.Left);
        }
        public bool IsTouchingBottom(Sprite other) {
            return (this.Rectangle.Top + Velocity.Y < other.Rectangle.Bottom &&
                this.Rectangle.Bottom + Velocity.Y > other.Rectangle.Bottom &&
                this.Rectangle.Left + Velocity.X < other.Rectangle.Right &&
                this.Rectangle.Right + Velocity.X > other.Rectangle.Left);
        }
        #endregion
    }
}
