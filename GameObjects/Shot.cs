using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    class Shot
    {
        Texture2D texture;
        Vector2 position;
        Vector2 speed;
        int framesCounter;

        public Rectangle Bounds { get; private set; }
        public bool Active { get; set; }

        //Extra properties required?

        public Shot(Texture2D tex)
        {
            // TODO: Initialize all fields and properties
            texture = tex;
            speed = new Vector2(10, 0);
            position = Vector2.Zero;
            framesCounter = 0;
        }

        public void Update()
        {
            // Update everything that requires updating
            if (Active)
            {
                framesCounter++;
                if (framesCounter < 2)
                {
                    AudioManager.PlaySound(Fx.Shot);
                }
                position += speed;
                if (position.X >= SpaceGame.screenWidth) Reset();

                Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw everything that requires drawing
            if (Active)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

        public void Reset()
        {
            // Reset object state
            position = Vector2.Zero;
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            Active = false;
            framesCounter = 0;
        }

        public void Activate(Vector2 position)
        {
            // Active shot and set initial position
            this.position = position;
            Active = true; 
        }
    }
}
