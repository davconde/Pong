using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.States {
    public abstract class State {
        protected Game1 _game;
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _content;

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PlatformSpecificInitialize();
        public abstract void PlatformSpecificUpdate(GameTime gameTime);
        public abstract void PlatformSpecificDraw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
