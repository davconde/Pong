using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Sprites {
    public class Ball : Sprite {
        private bool _gameStarted;
        private int _timer;
        private float _speedIncrementOverTime = 1f;

        public Ball(Texture2D texture) : base(texture) {
            Reset();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites) {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _gameStarted = true;

            if (!_gameStarted)
                return;

            _timer += (int)gameTime.ElapsedGameTime.Milliseconds;

            if (_timer >= 10000) {
                _timer = 0;
                Speed += _speedIncrementOverTime;
            }

            foreach (var sprite in sprites) {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) {
                    this.Velocity = GetPadBounce(sprite);
                    Game1.SoundBank.PlayCue("Pad");
                } else if (this.Velocity.X < 0 && this.IsTouchingRight(sprite)) {
                    this.Velocity = GetPadBounce(sprite);
                    Game1.SoundBank.PlayCue("Pad");
                } else if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) {
                    this.Velocity.Y = -this.Velocity.Y;
                    Game1.SoundBank.PlayCue("Pad");
                } else if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)) {
                    this.Velocity.Y = -this.Velocity.Y;
                    Game1.SoundBank.PlayCue("Pad");
                }
            }

            if (Position.Y <= 0 || Position.Y + _texture.Height >= Resolution.GameHeight) {
                Velocity.Y = -Velocity.Y;
                Game1.SoundBank.PlayCue("Wall");
            }

            if (Position.X + _texture.Width >= Resolution.GameWidth) {
                GameState.Score.Score1++;
                Game1.SoundBank.PlayCue("Score");
                Reset();
                return;
            } else if (Position.X <= 0) {
                GameState.Score.Score2++;
                Game1.SoundBank.PlayCue("Score");
                Reset();
                return;
            }

            Position += Velocity * Speed;
        }

        private Vector2 GetPadBounce(Sprite pad) {
            Rectangle padRect = pad.Rectangle;

            Rectangle collisionRect = Rectangle.Intersect(this.Rectangle, padRect);
            if (collisionRect.IsEmpty) {
                return Velocity;
            }

            float collisionPointOnPad = (float)(collisionRect.Center.Y - padRect.Top) / (padRect.Bottom - padRect.Top) - 0.5f;
            Vector2 returnVector = new Vector2((float)Math.Cos(collisionPointOnPad * Math.PI / 2) * -Math.Sign(Velocity.X), (float)Math.Sin(collisionPointOnPad * Math.PI / 2));

            return returnVector;
        }

        private void Reset() {
            _gameStarted = false;
            _timer = 0;
            Position = new Vector2((Resolution.GameWidth - _texture.Width) / 2, (Resolution.GameHeight - _texture.Height) / 2);
            Speed = 6f;

            int startDirection = Game1.Random.Next(0, 4);
            switch (startDirection) {
                case 0:
                    Velocity = new Vector2(-0.5f, -0.5f);
                    break;
                case 1:
                    Velocity = new Vector2(0.5f, -0.5f);
                    break;
                case 2:
                    Velocity = new Vector2(-0.5f, 0.5f);
                    break;
                case 3:
                    Velocity = new Vector2(0.5f, 0.5f);
                    break;
            }
            Velocity.Normalize();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
