using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.UI {
    public partial class Button {
        public void Update(GameTime gameTime) {
            if (this.Rectangle.Contains(Inputs.CurrentMousePosition)) {
                _isHovering = true;

                if (Inputs.CurrentMouse.LeftButton == ButtonState.Released && Inputs.PrevMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            } else
                _isHovering = false;
        }

        public static void CheckKeyboardSelection(ref int? selection, List<IUIComponent> components) {
            foreach (var component in components)
                ((Button)component).Selected = false;

            if (Inputs.CurrentMouse.Position != Inputs.PrevMouse.Position)
                selection = null;
            else {
                if (Inputs.CurrentKeys.IsKeyDown(Keys.Down) && !Inputs.PrevKeys.IsKeyDown(Keys.Down))
                    selection = ((selection ?? -1) + 1) % components.Count;
                if (Inputs.CurrentKeys.IsKeyDown(Keys.Up) && !Inputs.PrevKeys.IsKeyDown(Keys.Up))
                    selection = (selection ?? 1) - 1 < 0 ? components.Count - 1 : (selection ?? 1) - 1;
                if (selection != null && components[(int)selection] is Button) {
                    ((Button)components[(int)selection]).Selected = true;
                }
                if (Inputs.CurrentKeys.IsKeyDown(Keys.Enter))
                    ((Button)components[(int)selection]).Click?.Invoke((Button)components[(int)selection], new EventArgs());
            }
        }
    }
}
