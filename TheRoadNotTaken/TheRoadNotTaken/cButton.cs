
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snorlax
{
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 size;

        // constructor of the buttons
        public cButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            // Screenw  = 800, ScreenH  = 600
            // ImgW     = 100, ImgH     = 20

            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 20);
        }



       // bool down;
        public bool isCLicked;
        
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
              //  if (color.A == 255) down = false;
              //  if (color.A == 0) down = true;
                //if (down) color.A += 3; else color.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed) isCLicked = true;
            }

            
            //else if (color.A < 255)
            //{
            //    color.A += 3;
            //    isCLicked = false;
            //}
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}