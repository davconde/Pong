using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.States {
    public partial class MainMenuState : State {
        public override void PlatformSpecificInitialize(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) {

        }

        public override void PlatformSpecificUpdate(GameTime gameTime) {
            Button.CheckKeyboardSelection(ref _selectedComponent, _components);
        }

        public override void PlatformSpecificDraw(GameTime gameTime, SpriteBatch spriteBatch) {
            
        }
    }
}
