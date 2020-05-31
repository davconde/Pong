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
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong.UI {
    public partial class Button {
        public void Update(GameTime gameTime) {
            if (TouchPanel.GetState().Count > 0)
                foreach (TouchLocation touch in TouchPanel.GetState())
                    if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                        if (new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1).Intersects(this.Rectangle))
                            Click?.Invoke(this, new EventArgs());
        }
    }
}