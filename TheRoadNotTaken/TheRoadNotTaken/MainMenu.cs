using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Snorlax
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Menu
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice grapichDevice;
        SpriteBatch spriteBatch;
        ContentManager Content;
        Game1 game;

        


        //declare every gamestate
        public enum GameState
        {
            MainMenu,
            Option,
            Playing,
            Exit,
        }
        public GameState CurrentGameState = GameState.Playing;

        // screen size
        int screenwidth = 800;
        int screenheight = 600;


        // declare the buttons
        cButton btnplay;
        cButton btnstop;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

        public double damage = 0;
        public double usage = 1;
        
        public void Initialize()
        {
            // TODO: Add your initialization logic here

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            
            // TODO: use this.Content to load your game content here

            //screen

            graphics.PreferredBackBufferWidth = screenwidth;
            graphics.PreferredBackBufferHeight = screenheight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            game.IsMouseVisible = true;

            //set start button on screen
            btnplay = new cButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnplay.setPosition(new Vector2(300, 250));
            
            //set stop button on screen
            btnstop = new cButton(Content.Load<Texture2D>("Button2"), graphics.GraphicsDevice); 
            btnstop.setPosition(new Vector2(300, 300));

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();
            
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnplay.isCLicked == true) CurrentGameState = GameState.Playing;
                    btnplay.Update(mouse);
                    
                   
                    if (btnstop.isCLicked == true) CurrentGameState = GameState.Exit;
                    btnstop.Update(mouse);
                    break;

                case GameState.Playing:
                    break;
                //when button exit is pressed
                //case GameState.Exit:
                //    if (btnstop.isCLicked == true) CurrentGameState = GameState.Exit;
                //    btnstop.Update(mouse);
                //    break;

                

            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            grapichDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
             switch (CurrentGameState)
            {
                 case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0,0,screenwidth,screenheight), Color.White);
                    btnplay.draw(spriteBatch);
                    btnstop.draw(spriteBatch);
                    break;

                 case GameState.Playing:
                    
                    break;
                     //button exit
                 case GameState.Exit:
                    
                    break;

            }
             spriteBatch.End();
        }
    }
}
