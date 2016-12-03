using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    public enum EnemyState { Disabled, Active, Explosion }

    class Enemy
    {
        const int MAX_SHOTS = 20;

        Texture2D texture;
        Vector2 position;
        Vector2 offset;
        float phase;
        Vector2 speed;

        Shot[] shots;
        Explosion explosion;

        EnemyState state;
        int framesCounter;

        int timeCounter;

        public Rectangle Bounds { get; private set; }

        public bool Mscore;

        public Enemy(Texture2D texEnemy, Texture2D texShot, Texture2D texExplosion)
        {

            texture = texEnemy;

            offset = new Vector2(SpaceGame.Random.Next(1280, 1280 + SpaceGame.screenWidth), SpaceGame.Random.Next(SpaceGame.screenHeight));
            position = Vector2.Zero;
            position.X = offset.X;
            phase = MathHelper.ToRadians(SpaceGame.Random.Next(360));
            speed = new Vector2(8, 8);

            state = EnemyState.Active;
            framesCounter = 0;
            timeCounter = 0;
            Mscore = false;

            explosion = new Explosion(texExplosion);
        }

        public void Update()
        {

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);


            switch (state)
            {
                case EnemyState.Disabled: break;
                case EnemyState.Active:
                    {
                        position.X -= speed.X;

                        timeCounter++;

                        position.Y = offset.Y + 150 * (float)Math.Sin((2 * Math.PI / 200 * timeCounter) + phase);

                        if(position.X < -200)
                        {
                            position.X = SpaceGame.screenWidth + 120;

                            Reset();
                        }
                    }
                    break;
                case EnemyState.Explosion:
                    {
                        explosion.Update();
                        explosion.Activate(new Vector2(position.X + (texture.Width / 2), position.Y + (texture.Height / 2)));
                        Bounds = new Rectangle(40000, 5000, 3, 3);

                        framesCounter++;
                        if (framesCounter > 95)
                        {
                            Reset();
                            framesCounter = 0;
                        }
                    }
                    break;
                default: break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case EnemyState.Disabled: break;
                case EnemyState.Active:
                    {
                        spriteBatch.Draw(texture, position, Color.Blue);
                        
                    }
                    break;
                case EnemyState.Explosion:
                    {
                        explosion.Draw(spriteBatch);
                    }
                    break;
                default: break;

            }
        }

        public void Reset()
        {
            //position = new Vector2(1280, SpaceGame.screenWidth / 2 - texture.Width / 2);

            state = EnemyState.Active;

            position = new Vector2(SpaceGame.screenWidth + 200, SpaceGame.Random.Next(SpaceGame.screenHeight));

            framesCounter = 0;

            speed = new Vector2(6, 8);

            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            Mscore = false;

            timeCounter = 0;

        }

        public bool CheckCollisionShots(Rectangle bounds)
        {
            bool collision = false;

            // Check collision with any shot
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                //collision = shots[i].Bounds.Intersects(bounds);

                // Resolve collision
                if (collision)
                {
                    shots[i].Reset();
                    break;

                }
            }

            return collision;
        }

        public void ReceiveDamage()
        {
            Mscore = true;
            state = EnemyState.Explosion;
        }
    }
}
