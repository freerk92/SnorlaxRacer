using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#region Namespace
namespace Snorlax
{
#endregion
    #region Class
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
    #endregion

        #region Attributes
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Menu
        Menu menu;
        
        cButton btnplay;
        cButton btnstop;

        Texture2D mTrack4;
        Texture2D mTrack3;
        Texture2D mTrack2;
        Texture2D mTrack;
        Texture2D mCar;
        Texture2D mTrackOverlay;
        Texture2D mTrackTop;
        SpriteFont Font;
        SpriteFont Font2;
        SpriteFont Font3;

        // Checkpoints
        bool Onfinishline = false;
        bool checkpoint = false;
        bool checkpoint1 = false;
        bool checkpoint2 = false;
        bool p2Onfinishline = false;
        bool p2checkpoint = false;
        bool p2checkpoint1 = false;
        bool p2checkpoint2 = false;

        bool treat = false;
        bool treat1 = false;
        bool trick = false;
        bool trick1 = false;

        Texture2D active;
        bool activa = false;
        bool activa1 = false;
        public float actieftime = 0;
        public float actieftime1 = 0;

        //Rondes
        int round = 0;
        int round1 = 0;

        double pUpFinish;
        double pUpFinish1;

        RenderTarget2D mTrackRender;
        RenderTarget2D mTrackRenderRotated;

        RenderTarget2D mTrackRender1;
        RenderTarget2D mTrackRenderRotated1;
        Texture2D mCar1;

        Vector2 mCarPosition = new Vector2(500, 600);
        int mCarHeight;
        int mCarWidth;
        float mCarRotation = 3.15f;
        double mCarScale = 0.8;
        float timer = 0;
        bool gameover { get; set; }
        bool gameover1 { get; set; }
        bool gameover2 { get; set; }
        public double Health = 100;
        public double damage = 0;
        public double usage = 1;
        public int range;

        KeyboardState mPreviousKeyboard;
        public double Move { get; set; }
        public double Fuel = 50;
        Rectangle mCarArea;

        Vector2 mCarPosition1 = new Vector2(500, 550);
        int mCarHeight1;
        int mCarWidth1;
        float mCarRotation1 = 3.15f;
        double mCarScale1 = 0.8;
        public double Health1 = 100;
        public double damage1 = 0;
        public double usage1 = 1;
        public int range1;

        public double Move1 { get; set; }
        public double Fuel1 = 50;
        Rectangle mCarArea1;

        private ContentManager contentManager;
        private SoundEffect sound1;
        private SoundEffect sound2;
        private SoundEffect sound3;
        private SoundEffect sound4;
        private SoundEffect sound5;
        //private SoundEffect sound6;
        //private SoundEffect sound7;
        private SoundEffect sound8;
        private SoundEffect sound9;
        private SoundEffect sound10;
        private SoundEffect sound11;


        private SoundEffectInstance idle;
        private SoundEffectInstance acceleration;
        private SoundEffectInstance topspeed;
        private SoundEffectInstance reverse;
        private SoundEffectInstance crash1;
        //private SoundEffectInstance crash2;
        //private SoundEffectInstance turbo;
        private SoundEffectInstance idle2;
        private SoundEffectInstance acceleration2;
        private SoundEffectInstance topspeed2;
        private SoundEffectInstance theme;

        private static System.Timers.Timer playsound;
        private static System.Timers.Timer playsound2;

        bool sound = true;
        bool soundp2 = true;
        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1324;
            graphics.PreferredBackBufferHeight = 704;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        #region Initialize
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            menu = new Menu(); //(graphics,Content,spriteBatch,GraphicsDevice,this);
            
            base.Initialize();
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IsMouseVisible = true;

            //set start button on screen
            btnplay = new cButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnplay.setPosition(new Vector2(500, 250));

            //set stop button on screen
            btnstop = new cButton(Content.Load<Texture2D>("Button2"), graphics.GraphicsDevice);
            btnstop.setPosition(new Vector2(500, 300));

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //font = Content.Load<SpriteFont>("Courier New");

            mTrack = Content.Load<Texture2D>("Track");
            active = Content.Load<Texture2D>("Active");
            mCar = Content.Load<Texture2D>("Car");
            mTrack2 = Content.Load<Texture2D>("Track2");
            mTrack3 = Content.Load<Texture2D>("Track3");
            mTrack4 = Content.Load<Texture2D>("Track4");
            //Load the images from computer into the Texture2D objects
            mTrack = Content.Load<Texture2D>("Track");

            mCar = Content.Load<Texture2D>("Car");
            mTrackOverlay = Content.Load<Texture2D>("TrackOverlay");
            mTrackTop = Content.Load<Texture2D>("TrackTop");

            //Scale the height and width of the car appropriately
            mCarWidth = (int)(mCar.Width * mCarScale);
            mCarHeight = (int)(mCar.Height * mCarScale);

            mTrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
                mCarHeight + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);

            //mTrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
            //    mCarHeight + 100, 1, SurfaceFormat.Color);
            mTrackRenderRotated = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
                mCarHeight + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);
            Font = Content.Load<SpriteFont>("Arial");
            Font2 = Content.Load<SpriteFont>("Kootenay");
            Font3 = Content.Load<SpriteFont>("Kootenay1");

            mCar1 = Content.Load<Texture2D>("Car1");
            mCarWidth1 = (int)(mCar1.Width * mCarScale1);
            mCarHeight1 = (int)(mCar1.Height * mCarScale1);

            mTrackRender1 = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth1 + 100,
            mCarHeight1 + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);
            mTrackRenderRotated1 = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth1 + 100,
                mCarHeight1 + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);
            //mTrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
            //    mCarHeight + 100, 1, SurfaceFormat.Color);
            mTrackRenderRotated1 = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth1 + 100,
                mCarHeight1 + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);

            if (null == this.contentManager)
                this.contentManager = new ContentManager(Services, "Content");

            this.sound1 = this.contentManager.Load<SoundEffect>(@"idle");
            this.idle = this.sound1.CreateInstance();

            this.sound2 = this.contentManager.Load<SoundEffect>(@"acceleration");
            this.acceleration = this.sound2.CreateInstance();

            this.sound3 = this.contentManager.Load<SoundEffect>(@"topspeed");
            this.topspeed = this.sound3.CreateInstance();

            this.sound4 = this.contentManager.Load<SoundEffect>(@"reverse");
            this.reverse = this.sound4.CreateInstance();

            this.sound5 = this.contentManager.Load<SoundEffect>(@"crash1");
            this.crash1 = this.sound5.CreateInstance();

            //this.sound6 = this.contentManager.Load<SoundEffect>(@"crash2");
            //this.crash2 = this.sound6.CreateInstance();

            //this.sound7 = this.contentManager.Load<SoundEffect>(@"turbo");
            //this.turbo = this.sound7.CreateInstance();

            this.sound8 = this.contentManager.Load<SoundEffect>(@"idle2");
            this.idle2 = this.sound8.CreateInstance();

            this.sound9 = this.contentManager.Load<SoundEffect>(@"acceleration2");
            this.acceleration2 = this.sound9.CreateInstance();

            this.sound10 = this.contentManager.Load<SoundEffect>(@"topspeed2");
            this.topspeed2 = this.sound10.CreateInstance();

            this.sound11 = this.contentManager.Load<SoundEffect>(@"theme");
            this.theme = this.sound11.CreateInstance();

            this.topspeed.IsLooped = true;
            this.idle.IsLooped = true;
            //this.turbo.IsLooped = true;
            this.topspeed2.IsLooped = true;
            this.idle2.IsLooped = true;
            this.theme.IsLooped = true;

            playsound = new System.Timers.Timer(10000);
            playsound.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            playsound.Interval = 1800;

            playsound2 = new System.Timers.Timer(10000);
            playsound2.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            playsound2.Interval = 2600;
        }
        #endregion
        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.idle.Dispose();
            this.acceleration.Dispose();
            this.topspeed.Dispose();
            this.reverse.Dispose();
            this.crash1.Dispose();
            //this.crash2.Dispose();
            //this.turbo.Dispose();
            this.idle2.Dispose();
            this.acceleration2.Dispose();
            this.topspeed2.Dispose();
            this.theme.Dispose();

            this.sound1.Dispose();
            this.sound2.Dispose();
            this.sound3.Dispose();
            this.sound4.Dispose();
            this.sound5.Dispose();
            //this.sound6.Dispose();
            //this.sound7.Dispose();
            this.sound8.Dispose();
            this.sound9.Dispose();
            this.sound10.Dispose();
            this.sound11.Dispose();

            this.contentManager.Unload();
        }
        #endregion
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public enum GameState
        {// Menu options
            MainMenu,
            Playing,
            Exit,
        }
        
        public GameState CurrentGameState = GameState.MainMenu;
        #region Update
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                    //Menu 
                    case GameState.MainMenu:
                    if (btnplay.isCLicked == true) CurrentGameState = GameState.Playing;
                    btnplay.Update(mouse);


                    if (btnstop.isCLicked == true) CurrentGameState = GameState.Exit;
                    btnstop.Update(mouse); //Exit state
                    break;

                    case GameState.Playing:
                    //Playing state, will be called in Draw
                    break;

                case GameState.Exit:
                    this.Exit();
                    break;
            }

            GamePadState aGamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState aKeyboard = Keyboard.GetState();

            //Check to see if the game should be exited
            if (aGamePad.Buttons.Back == ButtonState.Pressed || aKeyboard.IsKeyDown(Keys.Escape) == true)
            {
                this.Exit();
            }

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Rotate the Car sprite with the Left Thumbstick or the up and down arrows

            //Rotate the Car sprite with the Left Thumbstick or the up and down arrows

            mCarRotation = mCarRotation + (float)(aGamePad.ThumbSticks.Left.X * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            if (aKeyboard.IsKeyDown(Keys.Left) == true && Fuel > 0)
            {// Left steering
                if (Move != 0)
                {
                    mCarRotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move > 0)
                {
                    mCarRotation -= (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move = Move - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move < 0)
                {
                    mCarRotation -= (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move = Move - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (aKeyboard.IsKeyDown(Keys.Right) == true && Fuel > 0)
            { // Right steering
                if (Move != 0)
                {
                    mCarRotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move > 0)
                {
                    mCarRotation += (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move = Move - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move < 0)
                {
                    mCarRotation += (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move = Move - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (aKeyboard.IsKeyDown(Keys.LeftShift) == true && Move > 0 && Health > 0)
            { //Turbo
                Move = Move + 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                if (Move != 0 && Fuel > 0)
                {
                    Fuel = Fuel - 2 * gameTime.ElapsedGameTime.TotalSeconds;
                }
            }


            //Setup the Movement increment. If a collision doesn't occur with this movement,
            //then this amount will be applied to the car's current position

            if (aKeyboard.IsKeyDown(Keys.Up) == true && Fuel > 0 && Health > 0 && CurrentGameState == GameState.Playing)
            {// P1 Up key
                if (Fuel > 0)
                {
                    Fuel = Fuel - 1 * gameTime.ElapsedGameTime.TotalSeconds;
                    Move += 10 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (Fuel < 0 || Fuel == 0)
                {
                    Fuel = Fuel * 0;
                    Move = Move * 0;
                }
                if (Move > 7 && trick == false && treat1 == false)
                {
                    Move = 7;
                }
                else if (Move > 10 && trick == true)
                {
                    Move = 10;
                }
                else if (Move > 5 && treat == true)
                {
                    Move = 5;
                }

                if (sound == true)
                {
                    if (this.idle.State != SoundState.Stopped)
                    {
                        this.idle.Stop();
                    }
                    if (this.reverse.State != SoundState.Stopped)
                    {
                        this.reverse.Stop();
                    }
                    playsound.Enabled = true;

                    this.acceleration.Play();

                    sound = false;
                }
            }
            else if (aKeyboard.IsKeyDown(Keys.Down) == true && Fuel > 0 && Health > 0 && CurrentGameState == GameState.Playing)
            { //P1 key down
                sound = true;

                if (Fuel > 0)
                {
                    Fuel = Fuel - 1 * gameTime.ElapsedGameTime.TotalSeconds;
                    Move -= 10 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (Fuel < 0 || Fuel == 0)
                {
                    Fuel = Fuel * 0;
                    Move = Move * 0;
                }
                if (Move < -4)
                {
                    Move = -4;
                }

                if (sound == true)
                {
                    if (this.acceleration.State != SoundState.Stopped)
                    {
                        this.acceleration.Stop();
                    }
                    this.reverse.Play();
                    this.idle.Play();
                }
            }

            //Check to see if a collision occured. If a collision didn't occur, then move the sprite
            if (CollisionOccurred(Move, mCarRotation) == false)
            {
                mCarPosition.X += (float)(Move * Math.Cos(mCarRotation));
                mCarPosition.Y += (float)(Move * Math.Sin(mCarRotation));
            }

            if (aKeyboard.IsKeyUp(Keys.Up) == true && Move >= 0)
            {
                sound = true;
                playsound.Enabled = false;

                Move -= 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                if (Move < 0)
                {
                    Move = Move * 0;
                }

                if (this.idle.State == SoundState.Stopped && sound == true && CurrentGameState == GameState.Playing)
                {
                    if (this.acceleration.State != SoundState.Stopped)
                    {
                        this.acceleration.Stop();
                    }
                    if (this.reverse.State != SoundState.Stopped)
                    {
                        this.reverse.Stop();
                    }
                    if (this.topspeed.State != SoundState.Stopped)
                    {
                        this.topspeed.Stop();
                    }
                    this.idle.Play();
                }
            }

            if (aKeyboard.IsKeyUp(Keys.Down) == true && Move <= 0)
            {
                if (this.reverse.State != SoundState.Stopped)
                {
                    this.reverse.Stop();
                }
                Move += 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                if (Move > 0)
                {
                    Move = Move * 0;
                }
            }

            if (CollisionOccurred(Move, mCarRotation) == true)
            {
                Move = Move * -0.25;
                if (Move != 0)
                {
                    if (Move > 0)
                    {
                        Health = Health - 0.75 * Math.Pow(Move, 4);
                        if (Health < 0)
                        {
                            Health = Health * 0;
                        }
                    }
                    if (Move < 0)
                    {
                        Health = Health - 0.75 * Math.Pow(Move, 4);
                        if (Health < 0)
                        {
                            Health = Health * 0;
                        }
                    }
                    Fuel = Fuel - 2 * gameTime.ElapsedGameTime.TotalSeconds;

                    damage = 100 - Health;
                    usage = 1 + (damage / 75);

                    this.crash1.Play();
                }
            }

            if (PitStop(Move, mCarRotation) == true && Move == 0)
            {
                if (Fuel < 50)
                {
                    Fuel = Fuel + 5 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Fuel = Fuel * 1;
                }
                if (Health < 100)
                {
                    Health = Health + 5 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Health = Health * 1;
                }

                if (usage > 1)
                {
                    usage = usage - 0.075 * gameTime.ElapsedGameTime.TotalSeconds; ;
                }
                else
                {
                    usage = usage * 1;
                }
            }

            if (Fuel == 0 || Fuel < 0)
            {
                if (Move > 0)
                {
                    Move -= 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Move < 0)
                {
                    Move += 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Move == 0)
                {
                    Move = Move * 0;
                }
            }


            mCarRotation1 = mCarRotation1 + (float)(aGamePad.ThumbSticks.Left.X * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            if (aKeyboard.IsKeyDown(Keys.A) == true && Fuel1 > 0)
            {
                if (Move1 != 0)
                {
                    mCarRotation1 -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move1 > 0)
                {
                    mCarRotation1 -= (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move1 = Move1 - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move1 < 0)
                {
                    mCarRotation1 -= (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move1 = Move1 - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (aKeyboard.IsKeyDown(Keys.D) == true && Fuel1 > 0)
            {
                if (Move1 != 0)
                {
                    mCarRotation1 += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move1 > 0)
                {
                    mCarRotation1 += (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move1 = Move1 - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (aKeyboard.IsKeyDown(Keys.Space) == true && Move1 < 0)
                {
                    mCarRotation1 += (float)(1 * 4.0f * gameTime.ElapsedGameTime.TotalSeconds);
                    Move1 = Move1 - 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (aKeyboard.IsKeyDown(Keys.LeftShift) == true && Move1 > 0 && Health1 > 0)
            {
                Move1 = Move1 + 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                if (Move1 != 0 && Fuel1 > 0)
                {
                    Fuel1 = Fuel1 - 2 * gameTime.ElapsedGameTime.TotalSeconds;
                }
            }


            //Setup the Movement increment. If a collision doesn't occur with this movement,
            //then this amount will be applied to the car's current position

            if (aKeyboard.IsKeyDown(Keys.W) == true && Fuel1 > 0 && Health1 > 0 && CurrentGameState == GameState.Playing)
            { //W key for steering
                if (Fuel1 > 0)
                {
                    Fuel1 = Fuel1 - 1 * gameTime.ElapsedGameTime.TotalSeconds;
                    Move1 += 10 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (Fuel1 < 0 || Fuel1 == 0)
                {
                    Fuel1 = Fuel1 * 0;
                    Move1 = Move1 * 0;
                }
                if (Move1 > 7 && trick1 == false && treat == false)
                {
                    Move1 = 7;
                }
                else if (Move1 > 10 && trick1 == true)
                {
                    Move1 = 10;
                }
                else if (Move1 > 5 && treat1 == true)
                {
                    Move1 = 5;
                }

                if (soundp2 == true)
                { //sound for player2
                    if (this.idle2.State != SoundState.Stopped)
                    {
                        this.idle2.Stop();
                    }
                    playsound2.Enabled = true;

                    this.acceleration2.Play();

                    soundp2 = false;
                }
            }
            else if (aKeyboard.IsKeyDown(Keys.S) == true && Fuel1 > 0 && Health1 > 0 && CurrentGameState == GameState.Playing)
            {// S key steering
                soundp2 = true;
                if (Fuel1 > 0)
                {
                    Fuel1 = Fuel1 - 1 * gameTime.ElapsedGameTime.TotalSeconds;
                    Move1 -= 10 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (Fuel1 < 0 || Fuel1 == 0)
                {
                    Fuel1 = Fuel1 * 0;
                    Move1 = Move1 * 0;
                }
                if (Move1 < -4)
                {
                    Move1 = -4;
                }
                if (soundp2 == true && CurrentGameState == GameState.Playing)
                {
                    if (this.acceleration2.State != SoundState.Stopped && CurrentGameState == GameState.Playing)
                    {
                        this.acceleration2.Stop();
                    }
                    this.idle2.Play();
                }
            }

            //Check to see if a collision occured. If a collision didn't occur, then move the sprite
            if (CollisionOccurred1(Move1, mCarRotation1) == false)
            {
                mCarPosition1.X += (float)(Move1 * Math.Cos(mCarRotation1));
                mCarPosition1.Y += (float)(Move1 * Math.Sin(mCarRotation1));
            }

            if (aKeyboard.IsKeyUp(Keys.W) == true && Move1 >= 0)
            { //When realeasing W key, car will stop
                soundp2 = true;
                playsound2.Enabled = false;

                Move1 -= 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                if (Move1 < 0)
                {
                    Move1 = Move1 * 0;
                }

                if (this.idle2.State == SoundState.Stopped && soundp2 == true && CurrentGameState == GameState.Playing)
                {
                    if (this.acceleration2.State != SoundState.Stopped)
                    {
                        this.acceleration2.Stop();
                    }
                    if (this.topspeed2.State != SoundState.Stopped)
                    {
                        this.topspeed2.Stop();
                    }
                    this.idle2.Play();
                }
            }

            if (aKeyboard.IsKeyUp(Keys.S) == true && Move1 <= 0)
            {//When releasing S key car will stop
                if (this.reverse.State != SoundState.Stopped)
                {
                    this.reverse.Stop();
                }
                Move1 += 10 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                if (Move1 > 0)
                {
                    Move1 = Move1 * 0;
                }
            }

            if (CollisionOccurred1(Move1, mCarRotation1) == true)
            {// Collision Player 2
                Move1 = Move1 * -0.25;
                if (Move1 != 0)
                {
                    if (Move1 > 0)
                    {
                        Health1 = Health1 - 0.75 * Math.Pow(Move1, 4);
                        if (Health1 < 0)
                        {
                            Health1 = Health1 * 0;
                        }
                    }
                    if (Move1 < 0)
                    {
                        Health1 = Health1 - 0.75 * Math.Pow(Move1, 4);
                        if (Health1 < 0)
                        {
                            Health1 = Health1 * 0;
                        }
                    }
                    Fuel1 = Fuel1 - 2 * gameTime.ElapsedGameTime.TotalSeconds;

                    damage1 = 100 - Health1;
                    usage1 = 1 + (damage1 / 75);

                    this.crash1.Play();
                }
            }

            if (PitStop1(Move1, mCarRotation1) == true && Move1 == 0)
            { //Collision Pitstop P2
                if (Fuel1 < 50)
                {
                    Fuel1 = Fuel1 + 5 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Fuel1 = Fuel1 * 1;
                }
                if (Health1 < 100)
                {
                    Health1 = Health1 + 5 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Health1 = Health1 * 1;
                }

                if (usage1 > 1)
                {
                    usage1 = usage1 - 0.075 * gameTime.ElapsedGameTime.TotalSeconds; ;
                }
                else
                {
                    usage1 = usage1 * 1;
                }
            }

            if (Fuel1 == 0 || Fuel1 < 0)
            {
                if (Move1 > 0)
                {
                    Move1 -= 20 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Move1 < 0)
                {
                    Move1 += 20 / 0.7 * gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Move1 == 0)
                {
                    Move1 = Move1 * 0;
                }
            }
            int elapsedTime = (int)gameTime.ElapsedGameTime.TotalSeconds;
            if (Health <= 0 || Fuel <= 0)
            {// gameover p1
                gameover = true;
                if (this.idle.State != SoundState.Stopped)
                {
                    this.idle.Stop();
                }
            }
            if (Health1 <= 0 || Fuel1 <= 0)
            {//gameover p2
                gameover1 = true;
                if (this.idle2.State != SoundState.Stopped)
                {
                    this.idle2.Stop();
                }
            }
            //actieradius
            range = Convert.ToInt16(Fuel) / 13;
            range1 = Convert.ToInt16(Fuel1) / 13;

            mPreviousKeyboard = aKeyboard;

            base.Update(gameTime);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {//sound for topspeed
            playsound.Enabled = false;
            if (this.acceleration.State == SoundState.Playing)
            {
                this.topspeed.Play();
            }
        }

        private void OnTimedEvent2(object source, ElapsedEventArgs e)
        {// sound for topspeed p2
            playsound2.Enabled = false;
            if (this.acceleration2.State == SoundState.Playing)
            {
                this.topspeed2.Play();
            }
        }

        private bool CollisionOccurred(double Move, float aCarRotation)
        {
            //Calculate the Position of the Car and create the collision Texture. This texture will contain
            //all of the pixels that are directly underneath the sprite currently on the Track image.
            pUpFinish = round + 5;
            pUpFinish1 = round1 + 5;
            float aXPosition = (float)((-mCarWidth / 2) + (mCarPosition.X + (Move * Math.Cos(aCarRotation))));
            float aYPosition = (float)((-mCarHeight / 2) + (mCarPosition.Y + (Move * Math.Sin(aCarRotation))));
            Texture2D aCollisionCheck = CreateCollisionTexture(aXPosition, aYPosition, aCarRotation);

            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture
            int aPixels = (mCarWidth - 10) * (mCarHeight);
            Color[] myColors = new Color[aPixels];
            mCarArea = new Rectangle(aCollisionCheck.Width / 2 - mCarWidth / 2 + 10, aCollisionCheck.Height / 2 - mCarHeight / 2, mCarWidth - 10, mCarHeight);
            aCollisionCheck.GetData<Color>(0, mCarArea, myColors, 0, aPixels);
#endregion
            //Cycle through all of the colors in the Array and see if any of them
            //are not Gray. If one of them isn't Gray, then the Car is heading off the road
            //and a Collision has occurred
            bool aCollision = false;
            foreach (Color aColor in myColors)
            { // Powerups
                if (aColor == new Color(255, 255, 0))//andere RGB ivm met ander bonus/plek
                {
                    activa = true;
            pUpFinish1 = round1 + 5;
                }
                if (activa == true)
                {
                    actieftime++;
                    treat1 = true;
                }
                if (actieftime >= 800000)
                {
                    activa = false;
                    actieftime = 0;
                    treat1 = false;
                }


                if (aColor == new Color(240, 240, 0))//Different RGB due to collision
                {
                    activa = true;
                    pUpFinish = round + 5;
		    pUpFinish1 = round1 + 5;
                } //Powerups p2
                if (activa == true)
                {
                    actieftime++;
                    trick = true;
                }
                if (actieftime >= 800000)
                {
                    activa = false;
                    actieftime = 0;
                    trick = false;
                }

                #region test
                //If one of the pixels in that area is not Gray, then the sprite is moving
                //off the allowed movement area
                if (aColor == new Color(255, 255, 255) || aColor == new Color(253, 255, 253) || aColor == new Color(254, 254, 254))
                {
                    aCollision = true;
                    break;
                }
                // Wanneer alle Checkpoints zijn behaald, word een ronde opgeteld bij de Finish
                if (aColor == new Color(0, 0, 0))
                {//Checkpoints
                    if (!Onfinishline)
                    {
                        if (checkpoint2 == true)
                            round++;
                        Onfinishline = false;
                        checkpoint2 = false;
                        checkpoint1 = false;
                        checkpoint = false;
                    }

                }// Checkpoint1
                else if (aColor == new Color(255, 0, 0))
                {
                    checkpoint = true;
                }
                else if (aColor == new Color(0, 0, 255))
                {//Checkpoint 2
                    if (checkpoint == true)
                    {
                        checkpoint1 = true;
                    }
                }
                else if (aColor == new Color(100, 100, 100))
                {//Checkpoint 3
                    if (checkpoint1 == true)
                    {
                        checkpoint2 = true;
                    }
                }

            }


            return aCollision;
         }



      
        private Texture2D CreateCollisionTexture(float theXPosition, float theYPosition, float aCarRotation)
        {
            //Grab a square of the Track image that is around the Car
            graphics.GraphicsDevice.SetRenderTarget(mTrackRender);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            if (round == 1 || round1 == 1)
            {
                if (trick1 == false && trick == false && treat1 == false && treat == false)
                { //Power ups showing up
                    spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCarWidth + 100, mCarHeight + 100),
               new Rectangle((int)(theXPosition - 50),
               (int)(theYPosition - 50), mCarWidth + 100, mCarHeight + 100), Color.White);
                }
                else
                {//Power ups showing up
                    spriteBatch.Draw(mTrack2, new Rectangle(0, 0, mCarWidth + 100, mCarHeight + 100),
                                new Rectangle((int)(theXPosition - 50),
                                (int)(theYPosition - 50), mCarWidth + 100, mCarHeight + 100), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(mTrack2, new Rectangle(0, 0, mCarWidth + 100, mCarHeight + 100),
                                new Rectangle((int)(theXPosition - 50),
                                (int)(theYPosition - 50), mCarWidth + 100, mCarHeight + 100), Color.White);
            }
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            Texture2D aPicture = mTrackRender;

            //Rotate the snapshot of the area Around the car sprite and return that 
            graphics.GraphicsDevice.SetRenderTarget(mTrackRenderRotated);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            return mTrackRenderRotated;
        }




        private bool PitStop(double Move, float aCarRotation)
        {
            //Calculate the Position of the Car and create the collision Texture. This texture will contain
            //all of the pixels that are directly underneath the sprite currently on the Track image.
            float aXPosition = (float)((-mCarWidth / 2) + (mCarPosition.X + (Move * Math.Cos(aCarRotation))));
            float aYPosition = (float)((-mCarHeight / 2) + (mCarPosition.Y + (Move * Math.Sin(aCarRotation))));
            Texture2D PitStopCheck = PitStopTexture(aXPosition, aYPosition, aCarRotation);

            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture
            int aPixels = (mCarWidth - 10) * (mCarHeight);
            Color[] myColors = new Color[aPixels];
            mCarArea = new Rectangle(PitStopCheck.Width / 2 - mCarWidth / 2 + 10, PitStopCheck.Height / 2 - mCarHeight / 2, mCarWidth - 10, mCarHeight);
            PitStopCheck.GetData<Color>(0, mCarArea, myColors, 0, aPixels);

            //Cycle through all of the colors in the Array and see if any of them
            //are not Gray. If one of them isn't Gray, then the Car is heading off the road
            //and a Collision has occurred
            bool Pit = false;
            foreach (Color aColor in myColors)
            {
                //If one of the pixels in that area is not Gray, then the sprite is moving
                //off the allowed movement area
                if (aColor == new Color(0, 255, 255))
                {
                    Pit = true;
                    break;
                }
            }

            return Pit;
        }



      
        private Texture2D PitStopTexture(float theXPosition, float theYPosition, float aCarRotation)
        {
            //Grab a square of the Track image that is around the Car
            graphics.GraphicsDevice.SetRenderTarget(mTrackRender);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCarWidth + 100, mCarHeight + 100),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), mCarWidth + 100, mCarHeight + 100), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            Texture2D aPicture = mTrackRender;

            //Rotate the snapshot of the area Around the car sprite and return that 
            graphics.GraphicsDevice.SetRenderTarget(mTrackRenderRotated);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            return mTrackRenderRotated;
        }


        private bool CollisionOccurred1(double Move1, float aCarRotation1)
        {
            //Calculate the Position of the Car and create the collision Texture. This texture will contain
            //all of the pixels that are directly underneath the sprite currently on the Track image.
            float aXPosition1 = (float)((-mCarWidth1 / 2) + (mCarPosition1.X + (Move1 * Math.Cos(aCarRotation1))));
            float aYPosition1 = (float)((-mCarHeight1 / 2) + (mCarPosition1.Y + (Move1 * Math.Sin(aCarRotation1))));
            Texture2D aCollisionCheck1 = CreateCollisionTexture1(aXPosition1, aYPosition1, aCarRotation1);

            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture
            int aPixels = (mCarWidth1 - 10) * (mCarHeight1);
            Color[] myColors = new Color[aPixels];
            mCarArea1 = new Rectangle(aCollisionCheck1.Width / 2 - mCarWidth1 / 2 + 10, aCollisionCheck1.Height / 2 - mCarHeight1 / 2, mCarWidth1 - 10, mCarHeight1);
            aCollisionCheck1.GetData<Color>(0, mCarArea1, myColors, 0, aPixels);
#endregion
            //Cycle through all of the colors in the Array and see if any of them
            //are not Gray. If one of them isn't Gray, then the Car is heading off the road
            //and a Collision has occurred
            bool aCollision1 = false;
            foreach (Color aColor in myColors)
            {// Powerup collission
                if (aColor == new Color(255, 255, 0))//andere RGB ivm met ander bonus/plek
                {
                    activa1 = true;
            pUpFinish1 = round1 + 5;
                }
                if (activa1 == true)
                {
                    actieftime1++;
                    treat = true;
                }
                if (actieftime1 >= 800000)
                {
                    activa1 = false;
                    actieftime1 = 0;
                    treat = false;
                }


                if (aColor == new Color(240, 240, 0))//andere RGB ivm met ander bonus/plek
                {
                    activa1 = true;
                    pUpFinish1 = round1 + 5;
                }
                if (activa1 == true)
                {
                    actieftime1++;
                    trick1 = true;
                }
                if (actieftime1 >= 800000)
                {
                    activa1 = false;
                    actieftime1 = 0;
                    trick1 = false;
                }
            
        
    
        

                //If one of the pixels in that area is not Gray, then the sprite is moving
                //off the allowed movement area
                if (aColor == new Color(255, 255, 255) || aColor == new Color(253, 255, 253) || aColor == new Color(254, 254, 254))
                {
                    aCollision1 = true;
                    break;
                }
                // Wanneer alle Checkpoints zijn behaald, word een ronde opgeteld bij de Finish
                if (aColor == new Color(0, 0, 0))
                {
                    if (!p2Onfinishline)
                    {
                        if (p2checkpoint2 == true)
                            round1++;
                        p2Onfinishline = false;
                        p2checkpoint2 = false;
                        p2checkpoint1 = false;
                        p2checkpoint = false;
                    }

                }//Checkpoint player 2
                else if (aColor == new Color(255, 0, 0))
                {
                    p2checkpoint = true;
                }
                else if (aColor == new Color(0, 0, 255))
                {
                    if (p2checkpoint == true)
                    {
                        p2checkpoint1 = true;
                    }
                }
                else if (aColor == new Color(100, 100, 100))
                {
                    if (p2checkpoint1 == true)
                    {
                        p2checkpoint2 = true;
                    }
                }

            }

            return aCollision1;
        }




        private Texture2D CreateCollisionTexture1(float theXPosition, float theYPosition, float aCarRotation1)
        {
            //Grab a square of the Track image that is around the Car
            graphics.GraphicsDevice.SetRenderTarget(mTrackRender1);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCarWidth1 + 100, mCarHeight1 + 100),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), mCarWidth1 + 100, mCarHeight1 + 100), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            Texture2D aPicture = mTrackRender1;

            //Rotate the snapshot of the area Around the car sprite and return that 
            graphics.GraphicsDevice.SetRenderTarget(mTrackRenderRotated1);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation1, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            return mTrackRenderRotated1;
        }




        private bool PitStop1(double Move1, float aCarRotation1)
        {
            //Calculate the Position of the Car and create the collision Texture. This texture will contain
            //all of the pixels that are directly underneath the sprite currently on the Track image.
            float aXPosition = (float)((-mCarWidth1 / 2) + (mCarPosition1.X + (Move1 * Math.Cos(aCarRotation1))));
            float aYPosition = (float)((-mCarHeight1 / 2) + (mCarPosition1.Y + (Move1 * Math.Sin(aCarRotation1))));
            Texture2D PitStopCheck1 = PitStopTexture1(aXPosition, aYPosition, aCarRotation1);

            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture
            int aPixels = (mCarWidth1 - 10) * (mCarHeight1);
            Color[] myColors = new Color[aPixels];
            mCarArea1 = new Rectangle(PitStopCheck1.Width / 2 - mCarWidth1 / 2 + 10, PitStopCheck1.Height / 2 - mCarHeight1 / 2, mCarWidth1 - 10, mCarHeight1);
            PitStopCheck1.GetData<Color>(0, mCarArea1, myColors, 0, aPixels);

            //Cycle through all of the colors in the Array and see if any of them
            //are not Gray. If one of them isn't Gray, then the Car is heading off the road
            //and a Collision has occurred
            bool Pit1 = false;
            foreach (Color aColor in myColors)
            {
                //If one of the pixels in that area is not Gray, then the sprite is moving
                //off the allowed movement area
                if (aColor == new Color(0, 255, 255))
                {
                    Pit1 = true;
                    break;
                }
            }

            return Pit1;
        }




        private Texture2D PitStopTexture1(float theXPosition, float theYPosition, float aCarRotation1)
        {
            //Grab a square of the Track image that is around the Car
            graphics.GraphicsDevice.SetRenderTarget(mTrackRender1);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCarWidth1 + 100, mCarHeight1 + 100),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), mCarWidth1 + 100, mCarHeight1 + 100), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            Texture2D aPicture = mTrackRender1;

            //Rotate the snapshot of the area Around the car sprite and return that 
            graphics.GraphicsDevice.SetRenderTarget(mTrackRenderRotated1);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation1, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            return mTrackRenderRotated1;
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            if (CurrentGameState == GameState.MainMenu)
            {
                //Main menu
                // TODO: Add your drawing code here
                    this.theme.Play();
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0,0,1324,704), Color.White);
                    btnplay.draw(spriteBatch);
                    btnstop.draw(spriteBatch);
            }
            if (CurrentGameState == GameState.Playing)
            { //Drawing game after pressing play game
                    this.theme.Stop();
                    spriteBatch.Draw(mTrackOverlay, new Rectangle(0, 0, mTrackOverlay.Width, mTrackOverlay.Height), Color.White);

                    if (pUpFinish == round || pUpFinish1 == round1)
                    {
                        spriteBatch.Draw(active, new Rectangle(210, 420, active.Width, active.Height), Color.White);
                        pUpFinish = -1;//reden -1 is omdat de andere speler ook nog 0 ronden kan hebben(in kort voorkomen dat de power up gelijk weer beschikbaar is)
                        pUpFinish1 = -1;
                    }
                //Car drawing
                    spriteBatch.Draw(mCar, new Rectangle((int)mCarPosition.X, (int)mCarPosition.Y, mCarWidth, mCarHeight),
                    new Rectangle(0, 0, mCar.Width, mCar.Height), Color.White, mCarRotation,
                    new Vector2(mCar.Width / 2, mCar.Height / 2), SpriteEffects.None, 0);
                    spriteBatch.Draw(mCar1, new Rectangle((int)mCarPosition1.X, (int)mCarPosition1.Y, mCarWidth1, mCarHeight1),
                    new Rectangle(0, 0, mCar1.Width, mCar1.Height), Color.White, mCarRotation1,
                    new Vector2(mCar1.Width / 2, mCar1.Height / 2), SpriteEffects.None, 0);
                    spriteBatch.Draw(mTrackTop, new Rectangle(0, 0, mTrackOverlay.Width, mTrackOverlay.Height), Color.White);
                    //HUD p1
                    spriteBatch.DrawString(Font, "Speler 1", new Vector2(137, 621), Color.White);
                    int Speed = Convert.ToInt16(Move);
                    spriteBatch.DrawString(Font, "Speed: " + Speed.ToString(), new Vector2(137, 631), Color.White);
                    spriteBatch.DrawString(Font, "Ronde: " + round, new Vector2(137, 641), Color.White);
                    int intFuel = Convert.ToInt16(Fuel);
                    if (intFuel > 10)
                    {
                        spriteBatch.DrawString(Font, "Fuel: " + intFuel.ToString(), new Vector2(137, 651), Color.White);
                    }
                    int atimer = Convert.ToInt16(timer);
                    if (intFuel < 10 || intFuel == 10)
                    {
                        if (atimer % 2 == 0)
                        {
                            spriteBatch.DrawString(Font, "Fuel: " + intFuel.ToString(), new Vector2(137, 651), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font, "Fuel: " + intFuel.ToString(), new Vector2(137, 651), Color.White);
                        }
                    }
                    if (Health >= 10)
                    {
                        spriteBatch.DrawString(Font, "Health: " + Health.ToString("0"), new Vector2(137, 660), Color.White);
                    }
                    if (Health < 10)
                    {
                        if (atimer % 2 == 0)
                        {
                            spriteBatch.DrawString(Font, "Health: " + Health.ToString("0"), new Vector2(137, 660), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font, "Health: " + Health.ToString("0"), new Vector2(137, 660), Color.White);
                        }
                    }
                    spriteBatch.DrawString(Font, "Range: " + range.ToString("0") + " Rondes", new Vector2(137, 669), Color.White);
                    //Hud p2
                    spriteBatch.DrawString(Font, "Speler 2", new Vector2(1078, 624), Color.White);
                    spriteBatch.DrawString(Font, "Speed: " + Move1.ToString("0"), new Vector2(1078, 633), Color.White);
                    spriteBatch.DrawString(Font, "Ronde: " + round1, new Vector2(1078, 642), Color.White);
                    if (Fuel1 > 10)
                    {
                        spriteBatch.DrawString(Font, "Fuel: " + Fuel1.ToString("0"), new Vector2(1078, 651), Color.White);
                    }
                    if (Fuel1 < 10 || Fuel1 == 10)
                    {
                        if (atimer % 2 == 0)
                        {
                            spriteBatch.DrawString(Font, "Fuel: " + Fuel1.ToString("0"), new Vector2(1078, 651), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font, "Fuel: " + Fuel1.ToString("0"), new Vector2(1078, 651), Color.White);
                        }
                    }
                    if (Health1 >= 10)
                    {
                        spriteBatch.DrawString(Font, "Health: " + Health1.ToString("0"), new Vector2(1078, 660), Color.White);
                    }
                    if (Health1 < 10)
                    {
                        if (atimer % 2 == 0)
                        {
                            spriteBatch.DrawString(Font, "Health: " + Health1.ToString("0"), new Vector2(1078, 660), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(Font, "Health: " + Health1.ToString("0"), new Vector2(1078, 660), Color.White);
                        }
                    }
                    spriteBatch.DrawString(Font, "Range: " + range1.ToString("0") + " Rondes", new Vector2(1078, 669), Color.White);
                if (gameover == true && gameover1 == false)
                {// Gameover p1
                    spriteBatch.DrawString(Font2, "SPELER 2 IS VICTORIOUS!", new Vector2(35, 375), Color.Red);
                    spriteBatch.DrawString(Font3, "Game over", new Vector2(mCarPosition.X - 10, mCarPosition.Y - 10), Color.Red);
                }
                if (gameover1 == true && gameover == false)
                {//Game over p2
                    spriteBatch.DrawString(Font2, "SPELER 1 IS VICTORIOUS!", new Vector2(35, 375), Color.Red);
                    spriteBatch.DrawString(Font3, "Game over", new Vector2(mCarPosition1.X - 10, mCarPosition1.Y - 10), Color.Red);
                }
                if (gameover1 == true && gameover == true)
                {//Game over both
                    KeyboardState aKeyboard = Keyboard.GetState();
                    spriteBatch.DrawString(Font2, "SPELER 1/2 ARE GAMEOVER!", new Vector2(35, 375), Color.Red);
                    spriteBatch.DrawString(Font3, "Game over", new Vector2(mCarPosition1.X - 10, mCarPosition1.Y - 10), Color.Red);
                    spriteBatch.DrawString(Font3, "Game over", new Vector2(mCarPosition.X - 10, mCarPosition.Y - 10), Color.Red);
                }
                spriteBatch.DrawString(Font3, "Speler 1", new Vector2(mCarPosition.X, mCarPosition.Y), Color.Red);
                spriteBatch.DrawString(Font3, "Speler 2", new Vector2(mCarPosition1.X, mCarPosition1.Y), Color.Red);

                if (activa == false && round == 5 || activa1 == false && round1 == 5)
                {
                    spriteBatch.Draw(active, new Rectangle(210, 420, active.Width, active.Height), Color.White);
                }
            }
            if (menu.CurrentGameState == Menu.GameState.Exit)
            {
                Exit();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}