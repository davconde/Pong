using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.States {
    public class MainMenuState : State {
        private SpriteFont _font;
        private Texture2D _buttonTexture;
        private List<IUIComponent> _components;

        public MainMenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) :
            base(game, graphicsDevice, content) {
            game.IsMouseVisible = true;

            _font = content.Load<SpriteFont>("Fonts/ButtonTextFont");
            _buttonTexture = content.Load<Texture2D>("Sprites/Button");

            var onePlayerButton = new Button(_buttonTexture, _font) {
                Position = new Vector2(50, 150),
                Text = "Player vs CPU",
            };

            onePlayerButton.Click += OnePlayerButton_Click;

            var twoPlayerButton = new Button(_buttonTexture, _font) {
                Position = new Vector2(50, 300),
                Text = "Player vs Player",
            };

            twoPlayerButton.Click += TwoPlayerButton_Click;

            _components = new List<IUIComponent>() {
                onePlayerButton,
                twoPlayerButton
            };
        }

        private void OnePlayerButton_Click(object sender, EventArgs e) {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content) { NumberOfPlayers = 1 });
        }

        private void TwoPlayerButton_Click(object sender, EventArgs e) {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content) { NumberOfPlayers = 2 });
        }

        public override void Update(GameTime gameTime) {
            _prevKeys = _currentKeys;
            _currentKeys = Keyboard.GetState();

            if (!_currentKeys.IsKeyDown(Keys.Escape) && _prevKeys.IsKeyDown(Keys.Escape))
                _game.Exit();

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
