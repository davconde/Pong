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
    public partial class GameState : State {
        private SpriteFont _font;
        private Texture2D _backgroundTexture;
        private Texture2D _batTexture;
        private Texture2D _ballTexture;
        private List<Sprite> _sprites;
        private int _numOfPlayers;
        private bool _demoMode;
        private Effect _demoEffect;

        public static Score Score;

        public bool DemoMode {
            get {
                return _demoMode;
            }
            set {
                _demoMode = value;
                foreach (var sprite in _sprites) {
                    if (sprite is Ball) {
                        ((Ball)sprite).AutoStart = true;
                    }
                }
            }
        }

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
            _font = content.Load<SpriteFont>("Fonts/ScoreFont");
            _backgroundTexture = content.Load<Texture2D>("Sprites/Background");
            _batTexture = content.Load<Texture2D>("Sprites/Bat");
            _ballTexture = content.Load<Texture2D>("Sprites/Ball");
            _demoEffect = content.Load<Effect>("Effects/DemoEffect");

            _sprites = new List<Sprite>() {
                new Sprite(_backgroundTexture),
                new Player(_batTexture, new PlayerInput() { UpKey = Keys.W, DownKey = Keys.S, TouchArea = new Rectangle(0, 0, Resolution.GameWidth / 2, Resolution.GameHeight) }) {
                    Position = new Vector2(20, Resolution.GameHeight / 2 - _batTexture.Height / 2)
                },
                new Player(_batTexture, new PlayerInput() { UpKey = Keys.Up, DownKey = Keys.Down, TouchArea = new Rectangle(Resolution.GameWidth / 2, 0, Resolution.GameWidth / 2, Resolution.GameHeight) }) {
                    Position = new Vector2(Resolution.GameWidth - 20 - _batTexture.Width, Resolution.GameHeight / 2 - _batTexture.Height / 2)
                },
                new Ball(_ballTexture)
            };

            Score = new Score(_font);

            PlatformSpecificInitialize();
        }

        public override void Update(GameTime gameTime) {
            if ((!Inputs.CurrentKeys.IsKeyDown(Keys.Escape) && Inputs.PrevKeys.IsKeyDown(Keys.Escape)) || GamePad.GetState(0).IsButtonDown(Buttons.Back))
                _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));

            PlatformSpecificUpdate(gameTime);

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if (DemoMode)
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: _demoEffect);
            else
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);
            Score.Draw(spriteBatch);

            PlatformSpecificDraw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
