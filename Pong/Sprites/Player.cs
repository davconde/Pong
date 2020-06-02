using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Sprites {
    public class Player : Sprite {
        private float _aiTolerance = 10f;
        private int _newBallVelocitySign = 0;

        public bool ControlledByAI;
        public bool JustBounced;

        public Player(Texture2D texture, PlayerInput input) : base(texture) {
            Input = input;
            Input.Parent = this;
            Speed = 5f;
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
                    if (JustBounced) {
                        JustBounced = false;
                        _aiTolerance = Game1.Random.Next(10, 50);
                        _newBallVelocitySign = Math.Sign(sprite.Velocity.X);
                    }

                    if (Math.Sign(sprite.Velocity.X) == _newBallVelocitySign) {
                        if (this.Rectangle.Center.Y < Resolution.GameHeight / 2 - Speed)
                            Velocity.Y += Speed;
                        if (this.Rectangle.Center.Y > Resolution.GameHeight / 2 + Speed)
                            Velocity.Y -= Speed;
                        return;
                    }

                    if (Math.Abs(sprite.Rectangle.Center.Y - this.Rectangle.Center.Y) < _aiTolerance) {
                        Velocity.Y = 0;
                        return;
                    }
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

            if (ControlledByAI) {
                GetAIVelocity(sprites);
            } else
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
