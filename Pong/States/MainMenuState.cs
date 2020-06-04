using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.IO;
using Pong.Sprites;
using Pong.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.States {
    public partial class MainMenuState : State {
        private SpriteFont _font;
        private Texture2D _titleTexture;
        private Texture2D _buttonTexture;
        private Texture2D _gitHubButtonTexture;
        private List<IUIComponent> _components;

        private State _demoGameState;
        private Effect _titleEffect;
        private int? _selectedComponent;

        public MainMenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) :
            base(game, graphicsDevice, content) {
            _font = content.Load<SpriteFont>("Fonts/ButtonTextFont");
            _titleTexture = content.Load<Texture2D>("Sprites/Title");
            _buttonTexture = content.Load<Texture2D>("Sprites/Button");
            _gitHubButtonTexture = content.Load<Texture2D>("Sprites/GitHub_Logo");
            _titleEffect = content.Load<Effect>("Effects/TitleEffect");

            var onePlayerButton = new Button(_buttonTexture, _font) {
                Position = new Vector2(50, 150),
                Text = "Player vs CPU",
            };

            onePlayerButton.Click += OnePlayerButton_Click;

            var twoPlayerButton = new Button(_buttonTexture, _font) {
                Position = new Vector2(50, 390),
                Text = "Player vs Player",
            };

            twoPlayerButton.Click += TwoPlayerButton_Click;

            var gitHubButton = new Button(_gitHubButtonTexture, _font) {
                Position = new Vector2(170, 600),
                Text = "",
            };

            gitHubButton.Click += GitHubButton_Click;

            _components = new List<IUIComponent>() {
                onePlayerButton,
                twoPlayerButton,
                gitHubButton
            };

            _demoGameState = new GameState(_game, _graphicsDevice, _content) { NumberOfPlayers = 0, DemoMode = true };
            PlatformSpecificInitialize(game, graphicsDevice, content);
        }

        private void OnePlayerButton_Click(object sender, EventArgs e) {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content) { NumberOfPlayers = 1 });
        }

        private void TwoPlayerButton_Click(object sender, EventArgs e) {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content) { NumberOfPlayers = 2 });
        }

        private void GitHubButton_Click(object sender, EventArgs e) {
            WebBrowser.LoadWebsite("https://www.github.com/davconde/Pong");
        }

        public override void Update(GameTime gameTime) {
            if ((!Inputs.CurrentKeys.IsKeyDown(Keys.Escape) && Inputs.PrevKeys.IsKeyDown(Keys.Escape)) || GamePad.GetState(0).IsButtonDown(Buttons.Back))
                _game.Exit();

            PlatformSpecificUpdate(gameTime);

            foreach (var component in _components)
                component.Update(gameTime);

            _demoGameState.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            _demoGameState.Draw(gameTime, spriteBatch);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: _titleEffect);
            _titleEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            _titleEffect.Parameters["TimePeriod"].SetValue(2f);
            _titleEffect.Parameters["Speed"].SetValue(10f);
            spriteBatch.Draw(_titleTexture, new Vector2(580, 150), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            PlatformSpecificDraw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
