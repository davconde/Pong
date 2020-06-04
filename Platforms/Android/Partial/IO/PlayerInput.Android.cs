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

namespace Pong.IO {
    public partial class PlayerInput {
        public bool Up {
            get {
                if (Inputs.CurrentTouchs.Count > 0)
                    foreach (TouchLocation touch in TouchPanel.GetState())
                        if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                            if (TouchArea.Contains(Inputs.TouchPosition(touch)))
                                if ((int)Inputs.TouchPosition(touch).Y < (int)Parent.Center.Y - Parent.Speed)
                                    return true;
                return false;
            }
        }

        public bool Down {
            get {
                if (Inputs.CurrentTouchs.Count > 0)
                    foreach (TouchLocation touch in TouchPanel.GetState())
                        if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                            if (TouchArea.Contains(Inputs.TouchPosition(touch)))
                                if ((int)Inputs.TouchPosition(touch).Y > (int)Parent.Center.Y + Parent.Speed)
                                    return true;
                return false;
            }
        }
    }
}