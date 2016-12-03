using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    class LogoScreen
    {
        Texture2D background, logo;

        int framesCounter;
        float alpha;

        public LogoScreen(ContentManager Content) 
        {
            background = Content.Load<Texture2D>("graphics/spaceLogo");
            logo = Content.Load<Texture2D>("graphics/logo");

            framesCounter = 0;
            alpha = 0.0f;
        }

        public void Update()
        {
            framesCounter++;
            if (framesCounter > 120 && framesCounter < 150) alpha += 0.05f;

            else if (framesCounter > 250) alpha -= 0.05f;

            if (alpha < 0.0f && framesCounter > 320) SpaceGame.CurrentScreen = GameScreen.Title;

            /*{
                AudioManager.PlayMusic(Track.Track02);
                SpaceGame.CurrentScreen = GameScreen.Title;
            }*/
            if (framesCounter == 120) AudioManager.PlaySound(Fx.LogoAppear);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.Wheat);

            if (framesCounter > 120) spriteBatch.Draw(logo, new Vector2(SpaceGame.screenWidth/2 - logo.Width/2, 200), Color.White * alpha);
        }
    }
}
