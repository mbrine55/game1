using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    class Player
    {
        Vector2 position;
        Vector2 motion;
        public float paddleSpeed = 8f;

        KeyboardState keyboardState;
        GamePadState gamePadState;

        Texture2D texture;
        Rectangle screenBounds;

        bool jumping = false;
        float startY, jumpSpeed = 0;

        public Player(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            SetInStartPosition();
        }

        public void Update()
        {
            motion = Vector2.Zero;

            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Left) ||
                gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                gamePadState.IsButtonDown(Buttons.DPadLeft))
                motion.X = -1;

            if (keyboardState.IsKeyDown(Keys.Right) ||
                gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) ||
                gamePadState.IsButtonDown(Buttons.DPadRight))
                motion.X = 1;

            if (jumping)
            {
                position.Y += jumpSpeed;
                jumpSpeed += 1;
                if (position.Y >= startY)
                {
                    position.Y = startY;
                    jumping = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    jumpSpeed = -14;
                }
            }

            motion.Y *= paddleSpeed;
            motion.X *= paddleSpeed;
            position += motion;
            LockPaddle();
        }

        private void LockPaddle()
        {
            if (position.X < 0)
                position.X = 0;
            if (position.X + texture.Width > screenBounds.Width)
                position.X = screenBounds.Width - texture.Width;
        }

        public void SetInStartPosition()
        {
            position.X = 150;
            position.Y = 250;
            startY = position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
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
