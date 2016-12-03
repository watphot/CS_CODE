using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    class TitleScreen
    {
        Texture2D background, title, title2;
        SpriteFont font;

        Vector2 titlePosition;

        int framesCounter;
        int framesCounterTime;
        int scrolling;
        int scrolling2;

        public TitleScreen(ContentManager Content) 
        {
            background = Content.Load<Texture2D>("graphics/8=D");
            title = Content.Load<Texture2D>("graphics/title");
            font = Content.Load<SpriteFont>("fonts/font");
            titlePosition = new Vector2(SpaceGame.screenWidth / 2 - title.Width / 2, 0 - title.Height);

            scrolling = 0;

            scrolling2 = 1280;

            framesCounter = 0;
        }

        public void Update(InputManager input)
        {
            framesCounter++;
            framesCounterTime++;
            if (framesCounter > 30 && titlePosition.Y < 200) titlePosition.Y += 5;
 
            else if (titlePosition.Y >= 200) titlePosition.Y = 200;

            scrolling -= 10;
            scrolling2 -= 10;
            if (scrolling == -1280)
            {
                scrolling = 0;
            }
            if (scrolling2 == 0)
            {
                scrolling2 = 1280;
            }

            if (input.IsKeyPressed(Keys.Enter))
            {
                AudioManager.StopMusic();
                AudioManager.PlaySound(Fx.PressStart);
                AudioManager.PlayMusic(Track.Track01);
                SpaceGame.CurrentScreen = GameScreen.Gameplay;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(scrolling, 0), Color.DarkGoldenrod);
            spriteBatch.Draw(background, new Vector2(scrolling2, 0), Color.DarkGoldenrod);

            if (framesCounterTime > 30 && framesCounterTime < 60) spriteBatch.DrawString(font, "PRESS ENTER to START", new Vector2(500, 500), Color.White);
  
            if (framesCounterTime > 60) framesCounterTime = 0;

            spriteBatch.Draw(title, new Vector2(titlePosition.X, titlePosition.Y), Color.White);
        }
    }
}
   