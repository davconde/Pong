using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong {
    public class Score {
        public int Score1;
        public int Score2;

        private SpriteFont _font;

        public Score(SpriteFont font) {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(_font, Score1.ToString(), new Vector2(Resolution.GameWidth / 2 - 60 - _font.MeasureString(Score1.ToString()).X, 70), Color.White);
            spriteBatch.DrawString(_font, Score2.ToString(), new Vector2(Resolution.GameWidth / 2 + 60, 70), Color.White);
        }
    }
}
