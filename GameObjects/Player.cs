using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public enum PlayerState { Disabled, Active, Explosion }

    class Player
    {
        const int MAX_SHOTS = 20;

        Texture2D texture;
        Rectangle textureBounds;
        Vector2 position;
        Vector2 speed;
        LifeBar lifeBar;

       Shot[] shots;
        Texture2D textureShots;
        int cadence;
            
        Explosion explosion;

        PlayerState state;
        int framesCounter;
        int framesCounter2;
        int ending;

        public Rectangle Bounds { get; private set; }

        public Player(Texture2D texPlayer, Texture2D texShot, Texture2D texExplosion)
        {
            texture = texPlayer;
            textureBounds = new Rectangle(0, 20, texture.Width - 30, texture.Height);
            position = new Vector2(100, 300);
            speed = new Vector2(8);

            Bounds = new Rectangle((int)position.X, 
                                   (int)position.Y, 
                                   textureBounds.Width, textureBounds.Height);

            // Initialize shots[] and explosion
            textureShots = texShot;
            shots = new Shot[MAX_SHOTS];
            for (int i = 0; i < MAX_SHOTS; i++) shots[i] = new Shot(textureShots);

            cadence = 0;
            framesCounter2 = 0;
           //EXPLOSION
           explosion = new Explosion(texExplosion);

            lifeBar = new LifeBar(new Rectangle((int)position.X, (int)position.Y, 100, 10), 4000);
            state = PlayerState.Active;
            framesCounter = 0;
            ending = 0;
        }

        public void Update(InputManager input)
        {
            // Update everything that requires updating

            switch (state)
            {
                case PlayerState.Disabled: break;
                case PlayerState.Active:
                    {
                        //PLAYER LOGIC
                        if (input.IsKeyDown(Keys.Right)) position.X += speed.X;
                        else if (input.IsKeyDown(Keys.Left)) position.X -= speed.X;

                        if (input.IsKeyDown(Keys.Up)) position.Y -= speed.Y;
                        else if (input.IsKeyDown(Keys.Down)) position.Y += speed.Y;

                        // Recalculate position considering textureBounds
                        if ((position.X + textureBounds.X) < 0) position.X = -textureBounds.X;
                        else if ((position.X + textureBounds.X + textureBounds.Width) > SpaceGame.screenWidth)
                            position.X = SpaceGame.screenWidth - (textureBounds.X + textureBounds.Width);

                        if ((position.Y + textureBounds.Y) < 0) position.Y = -textureBounds.Y;
                        else if ((position.Y + textureBounds.Y + textureBounds.Height) > SpaceGame.screenHeight)
                            position.Y = SpaceGame.screenHeight - (textureBounds.Y + textureBounds.Height);

                        Bounds = new Rectangle((int)position.X + textureBounds.X,
                                   (int)position.Y + textureBounds.Y,
                                   textureBounds.Width, textureBounds.Height);

                        //LIFEBAR
                        lifeBar.Update(position);

                        //SHOTS LOGIC
                        if (input.IsKeyDown(Keys.X))
                        {
                            cadence++;
                            if (cadence > 10)
                            {
                                Shoot();
                                cadence = 0;
                            }
                        }
                        else if (input.IsKeyUp(Keys.X)) cadence = 10;

                    } break;
                case PlayerState.Explosion:
                    {
                        // Guess...
                        framesCounter++;
                        ending++;
                        explosion.Update();
                        explosion.Activate(new Vector2(position.X + (texture.Width / 2), position.Y + (texture.Height / 2)));

                        framesCounter2++;
                        if (framesCounter2 < 3)
                        {
                            AudioManager.PlaySound(Fx.Shot);
                        }

                        Bounds = new Rectangle(8000, 9000, 1, 1);

                        //TIME
                        
                    } break;
                default: break;
            }
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (shots[i].Active) shots[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw everything that requires drawing

            switch (state)
            {
                case PlayerState.Disabled: break;
                case PlayerState.Active:
                    {
                        spriteBatch.Draw(texture, position, Color.White);
                        lifeBar.Draw(spriteBatch);
                    }
                    break;
                case PlayerState.Explosion:
                    {
                        // Guess...
                        explosion.Draw(spriteBatch);

                    } break;
                default: break;
            }
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (shots[i].Active) shots[i].Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            // Reset object state
            framesCounter = 0;
            position = new Vector2(100, 300);
            Bounds = new Rectangle((int)position.X + textureBounds.X,
                                   (int)position.Y + textureBounds.Y,
                                   textureBounds.Width, textureBounds.Height);

            lifeBar = new LifeBar(new Rectangle((int)position.X, (int)position.Y, 100, 10), 4000);
            framesCounter2 = 0;

            state = PlayerState.Active;


        }

        public bool CheckCollision(Rectangle bounds)
        {
            bool collision = false;

            if (Bounds.Intersects(bounds))
            {
                collision = true;

                // Resolve collision with player
            }

            return collision;
        }

        public bool CheckCollisionShots(Rectangle bounds)
        {
            bool collision = false;

            // Check collision with any shot
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                collision = shots[i].Bounds.Intersects(bounds);

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
            lifeBar.ReceiveDamage(500);
            if (lifeBar.Finished)
            {
                state = PlayerState.Explosion;
            }
        }
        public void Shoot()
        {
            for (int i = 0; i < MAX_SHOTS; i++)
            {
                if (!shots[i].Active)
                {
                    shots[i].Activate(new Vector2((int)position.X + texture.Width/2, (int)position.Y + texture.Height/2 -15)); //Add range more late
                    break;
                }
            }
        }
    }
}
