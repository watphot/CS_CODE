using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameProject
{
    public enum GameScreen { Logo, Title, Gameplay}

    public class SpaceGame : Game
    {
        public const int screenWidth = 1280;
        public const int screenHeight = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;

        // Screens Objects
        LogoScreen logoScreen;
        TitleScreen titleScreen;
        GameplayScreen gameplayScreen;
        public static Texture2D pixel;
        // Create static gameState to be available to all classes
        public static GameScreen CurrentScreen { private get; set; }
        public static Random Random { get; private set; }

        public SpaceGame()
        {
            // Create the graphics device manager
            graphics = new GraphicsDeviceManager(this);

            // Setup the relative directory to the executable directory for content loading with ContentManager
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Setup window size and apply changes
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            Window.Title = "Space Game";

            IsFixedTimeStep = false;
            IsMouseVisible = true;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create input manager object
            input = new InputManager(); 

            // Define initial game state
            CurrentScreen = GameScreen.Logo;
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            Random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create Screens Objects
            logoScreen = new LogoScreen(Content);
            titleScreen = new TitleScreen(Content);
            gameplayScreen = new GameplayScreen(Content);
            AudioManager.Initialize(Content);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // Unload spriteBatch object
            spriteBatch.Dispose();
            pixel.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update();

            // Check if Escape key has been pressed to exit
            if (input.IsKeyPressed(Keys.Escape)) Exit();
            

            // Game screens update
            switch (CurrentScreen)
            {
                case GameScreen.Logo: logoScreen.Update(); break;
                case GameScreen.Title: titleScreen.Update(input); break;
                case GameScreen.Gameplay: gameplayScreen.Update(input); break;
                default: break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw texture and text
            spriteBatch.Begin();

            // Game screens draw
            switch (CurrentScreen)
            {
                case GameScreen.Logo: logoScreen.Draw(spriteBatch); break;
                case GameScreen.Title: titleScreen.Draw(spriteBatch); break;
                case GameScreen.Gameplay: gameplayScreen.Draw(spriteBatch); break;
                default: break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
