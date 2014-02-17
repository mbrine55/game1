using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class PlayerGoal
    {
        Vector2 position;
        Texture2D texture;
        Rectangle screenBounds;

        public PlayerGoal(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            SetInStartPosition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        public void SetInStartPosition()
        {
            position.X = 0;
            position.Y = 202;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);
        }
    }
}
