using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States {
    public partial class GameState : State {
        private Texture2D _backButtonTexture;
        private Pong.UI.Button _backButton;

        public override void PlatformSpecificInitialize(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) {
            _backButtonTexture = content.Load<Texture2D>("Sprites/BackButton");
            _backButton = new Pong.UI.Button(_backButtonTexture, _font) {
                Position = new Vector2(Resolution.GameWidth / 2 - _backButtonTexture.Width / 2, 70),
                Text = "",
            };
            _backButton.Click += _backButton_Click;
        }

        private void _backButton_Click(object sender, EventArgs e) {
            _game.ChangeState(new MainMenuState(_game, _graphicsDevice, _content));
        }

        public override void PlatformSpecificUpdate(GameTime gameTime) {
            if (!DemoMode)
                _backButton.Update(gameTime);
        }

        public override void PlatformSpecificDraw(GameTime gameTime, SpriteBatch spriteBatch) {
            if (!DemoMode)
                _backButton.Draw(gameTime, spriteBatch);
        }
    }
}