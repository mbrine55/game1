using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    class Ball
    {
        Vector2 motion;
        Vector2 position;
        Rectangle bounds;
        float ballSpeed;
        const float ballStartSpeed = 4;

        Texture2D texture;
        Rectangle screenBounds;
        

        public Rectangle Bounds
        {
            get
            {
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
                return bounds;
            }
        }

        public Ball(Texture2D texture, Rectangle screenBounds)
        {
            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
            this.texture = texture;
            this.screenBounds = screenBounds;
        }

        public void Update()
        {
           
            position += motion * ballSpeed;
            ballSpeed += 0.001f;

            CheckWallCollision();
        }

        private void CheckWallCollision()
        {
            if (position.X < 0)
            {
                position.X = 0;
                motion.X *= -1;
            }
            if (position.X + texture.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - texture.Width;
                motion.X *= -1;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                motion.Y *= -1;
            }
        }

        public void SetInStartPosition(Rectangle paddleLocation)
        {
            motion = new Vector2(0, 5);
            motion.Normalize();

            position.Y = 175;
            position.X = 600;

            ballSpeed = ballStartSpeed;
        }

        public bool OffBottom()
        {
            if (position.Y > screenBounds.Height)
                return true;
            return false;
        }

        public void PaddleCollision(Rectangle paddleLocation)
        {
            Rectangle ballLocation = new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);

            if (paddleLocation.Intersects(ballLocation))
            {
                position.Y = paddleLocation.Y - texture.Height;
                motion.Y *= -1;
            }
        }

        public void FieldCollision(Rectangle fieldLocation)
        {
            Rectangle ballLocation = new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);

            if (fieldLocation.Intersects(ballLocation))
            {
                position.Y = fieldLocation.Y - texture.Height;
                motion.Y *= 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
