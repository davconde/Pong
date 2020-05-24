using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Sprites {
    public class Player : Sprite {
        public bool ControlledByAI;

        public Player(Texture2D texture) : base(texture) {
            Speed = 3f;
        }

        private void GetPlayerVelocity() {
            if (Input.Up)
                Velocity.Y -= Speed;
            if (Input.Down)
                Velocity.Y += Speed;
        }

        private void GetAIVelocity(List<Sprite> sprites) {
            foreach (var sprite in sprites) {
                if (sprite is Ball) {
                    if (sprite.Rectangle.Center.Y < this.Rectangle.Center.Y) {
                        Velocity.Y -= Speed;
                    }
                    if (sprite.Rectangle.Center.Y > this.Rectangle.Center.Y) {
                        Velocity.Y += Speed;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites) {
            if (Input == null)
                throw new Exception("Assign a value to Input");

            if (ControlledByAI)
                GetAIVelocity(sprites);
            else
                GetPlayerVelocity();

            Position += Velocity;
            Position.Y = MathHelper.Clamp(Position.Y, 0, Resolution.GameHeight - _texture.Height);
            Velocity.Y = 0;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
