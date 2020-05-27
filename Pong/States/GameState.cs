using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.IO;
using Pong.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.States {
    public class GameState : State {
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _batTexture;
        private Texture2D _ballTexture;
        private List<Sprite> _sprites;
        private int _numOfPlayers;

        public static Score Score;
        public int NumberOfPlayers {
            get {
                return _numOfPlayers;
            }
            set {
               _numOfPlayers = value;
               var players = _numOfPlayers;
                foreach (var sprite in _sprites) {
                    if (sprite is Player) {
                        if (players > 0)
                            ((Player)sprite).ControlledByAI = false;
                        else
                            ((Player)sprite).ControlledByAI = true;
                        players--;
                    }
                }
            }
        }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) :
            base(game, graphicsDevice, content) {
            game.IsMouseVisible = false;

            _font = content.Load<SpriteFont>("Fonts/ScoreFont");
            _backgroundTexture = content.Load<Texture2D>("Sprites/Background");
            _batTexture = content.Load<Texture2D>("Sprites/Bat");
            _ballTexture = content.Load<Texture2D>("Sprites/Ball");

            _sprites = new List<Sprite>() {
                new Sprite(_backgroundTexture),
                new Player(_batTexture) {
                    Position = new Vector2(20, Resolution.GameHeight / 2 - _batTexture.Height / 2),
                    Input = new Input() {
                        UpKey = Keys.W,
                        DownKey = Keys.S,

                        UpArea = new Rectangle(0, 0, Resolution.GameWidth / 2, Resolution.GameHeight / 2),
                        DownArea = new Rectangle(0, Resolution.GameHeight / 2, Resolution.GameWidth / 2, Resolution.GameHeight)
        }
                },
                new Player(_batTexture) {
                    Position = new Vector2(Resolution.GameWidth - 20 - _batTexture.Width, Resolution.GameHeight / 2 - _batTexture.Height / 2),
                    Input = new Input() {
                        UpKey = Keys.Up,
                        DownKey = Keys.Down,

                        UpArea = new Rectangle(Resolution.GameWidth / 2, 0, Resolution.GameWidth, Resolution.GameHeight / 2),
                        DownArea = new Rectangle(Resolution.GameWidth / 2, Resolution.GameHeight / 2, Resolution.GameWidth, Resolution.GameHeight)
                    }
                },
                new Ball(_ballTexture)
            };

            Score = new Score(_font);
        }

        public override void Update(GameTime gameTime) {
            _prevKeys = _currentKeys;
            _currentKeys = Keyboard.GetState();

            if (!_currentKeys.IsKeyDown(Keys.Escape) && _prevKeys.IsKeyDown(Keys.Escape))
                _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);
            Score.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
