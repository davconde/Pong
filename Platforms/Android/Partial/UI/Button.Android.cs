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
using Pong.IO;

namespace Pong.UI {
    public partial class Button {
        private bool _pressingDown;

        public void Update(GameTime gameTime) {
            if (Inputs.CurrentTouchs.Count > 0) {
                TouchLocation touch = Inputs.CurrentTouchs[0];

                if (this.Rectangle.Contains(Inputs.TouchPosition(touch))) {
                    if (touch.State == TouchLocationState.Pressed)
                        _pressingDown = true;
                    if (_pressingDown && (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved))
                        _isHovering = true;
                    if (touch.State == TouchLocationState.Released) {
                        _isHovering = false;
                        if (_pressingDown)
                            Click?.Invoke(this, new EventArgs());
                    }
                } else {
                    _pressingDown = false;
                    _isHovering = false;
                }
            } else {
                _pressingDown = false;
                _isHovering = false;
            }
        }
    }
}