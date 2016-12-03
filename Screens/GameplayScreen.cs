using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    class GameplayScreen
    {
        const int MAX_ENEMIES = 15;

        Texture2D background, finalTex;
        SpriteFont font;

        Player player;
        Enemy[] enemies;

        int framesCounter;

        int score;
        int Hscore;

        int scrolling;
        int scrolling2;
        int framesCounterTime;

        int endingCount;

        public bool ending;
        public bool permiso;

        public GameplayScreen(ContentManager Content) 
        {
            background = Content.Load<Texture2D>("graphics/background");

            font = Content.Load<SpriteFont>("fonts/font");

            player = new Player(Content.Load<Texture2D>("graphics/spaceship"), 
                                Content.Load<Texture2D>("graphics/shot"), 
                                Content.Load<Texture2D>("graphics/explosion_pro"));

            enemies = new Enemy[MAX_ENEMIES];

            ending = false;
            permiso = false;

            finalTex = Content.Load<Texture2D>("graphics/gameover");

            score = 0;

            scrolling = 0; 

            scrolling2 = 1280;

            Hscore = 0;

            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                enemies[i] = new Enemy(Content.Load<Texture2D>("graphics/flyenemy"),
                                       Content.Load<Texture2D>("graphics/shot"),
                                       Content.Load<Texture2D>("graphics/explosion_pro"));
            }

            framesCounter = 0;
        }

        public void Update(InputManager input)
        {
             if (!ending)
            {
                player.Update(input);

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

                for (int i = 0; i < MAX_ENEMIES; i++) enemies[i].Update();

                if (endingCount < 8)
                {
                    permiso = false;
                    if (endingCount == 8)
                    {
                        permiso = true;
                    }
                }
                // Check collision player vs enemies[]
                
                    for (int i = 0; i < MAX_ENEMIES; i++)
                    {
                        if (!permiso)
                        {
                            if (player.CheckCollision(enemies[i].Bounds))
                            {
                                enemies[i].ReceiveDamage();
                                AudioManager.PlaySound(Fx.Enemy);
                                player.ReceiveDamage();
                                endingCount += 1;
                            }
                        }
                    }
                

                // Check collision player.shots[] vs enemies[]
                for (int i = 0; i < MAX_ENEMIES; i++)
                {
                    if (player.CheckCollisionShots(enemies[i].Bounds))
                    {
                        enemies[i].ReceiveDamage();
                        AudioManager.PlaySound(Fx.Hit);
                        if (enemies[i].Mscore == true)
                        {
                            score += 20;
                            if (score > Hscore)
                            {
                                Hscore = score;
                            }
                        }
                    }
                }
                if (endingCount >= 8)
                {
                    framesCounter++;
                    if (framesCounter == 95)
                    {
                        ending = true;
                    }
                }
                // Check collision player vs enemies[].shots[]
                for (int i = 0; i < MAX_ENEMIES; i++)
                {
                    if (enemies[i].CheckCollisionShots(player.Bounds))
                    {
                    }

                }
            }
            if(ending)
            {
                framesCounterTime++;
                if(framesCounterTime > 60) framesCounterTime = 0;

                if (input.IsKeyDown(Keys.R))
                {
                    AudioManager.PlaySound(Fx.PressStart);
                    Reset();
                }
            }

            // Check collision player.shots[] vs enemies[].shots[]
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!ending)
            {
                spriteBatch.Draw(background, new Vector2(scrolling, 0), Color.CornflowerBlue);

                spriteBatch.Draw(background, new Vector2(scrolling2, 0), Color.CornflowerBlue);

                spriteBatch.DrawString(font, "SCORE:          " + score.ToString("000000"), new Vector2(100, 20), Color.AntiqueWhite);

                spriteBatch.DrawString(font, "HIGH SCORE:          " + Hscore.ToString("000000"), new Vector2(500, 20), Color.AntiqueWhite);

                player.Draw(spriteBatch);
                for (int i = 0; i < MAX_ENEMIES; i++) enemies[i].Draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(finalTex, new Vector2(0, 0), Color.Wheat);
                if (framesCounterTime > 30 && framesCounterTime < 60) spriteBatch.DrawString(font, "PRESS R to RESTART", new Vector2(500, 500), Color.Yellow);
            }
        }

        public void Reset()
        {
            score = 0;
            ending = false;
            player.Reset();
            endingCount = 0;
            permiso = false;
            framesCounter = 0;
            for (int i = 0; i < MAX_ENEMIES; i++) enemies[i].Reset();
        }
    }
}
