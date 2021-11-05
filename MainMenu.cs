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

namespace MainMenu
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;





        //declare every gamestate
        enum GameState
        {
            MainMenu,
            Option,
            Playing,
            Exit,
        }
        GameState CurrentGameState = GameState.MainMenu;

        // screen size
        int screenwidth = 800;
        int screenheight = 600;


        // declare the buttons
        cButton btnplay;
        cButton btnstop;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here

            //screen

            graphics.PreferredBackBufferWidth = screenwidth;
            graphics.PreferredBackBufferHeight = screenheight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            //set start button on screen
            btnplay = new cButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnplay.setPosition(new Vector2(350, 300));
            
            //set stop button on screen
            btnstop = new cButton(Content.Load<Texture2D>("Button2"), graphics.GraphicsDevice); 
            btnstop.setPosition(new Vector2(10, 10)); 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

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
                    Exit();
                    break;

            }
             spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
