using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    class Explosion
    {
        const int NUM_FRAMES_PER_LINE = 8;
        const int NUM_LINES = 6;

        Texture2D texture;
        Vector2 position;

        int framesCounter;

        Rectangle source;
        int currentFrame;
        int currentLine;
        int frameWidth;
        int frameHeight;

        public bool Active { get; set; }

        public Explosion(Texture2D tex)
        {
            // Initialize all fields and properties
            position = Vector2.Zero;
            texture = tex;

            framesCounter = 0;

            currentFrame = 0;
            currentLine = 0;
            frameWidth = texture.Width / NUM_FRAMES_PER_LINE;
            frameHeight = texture.Height / NUM_LINES;

            source = new Rectangle(currentFrame * frameWidth, currentLine * frameHeight, frameWidth, frameHeight);
        }

        public void Update()
        {
            //  Update everything that requires updating
            if (Active)
            {
                framesCounter++;
                if (framesCounter > 1)
                {
                    currentFrame++;
                    if (currentFrame >= NUM_FRAMES_PER_LINE)
                    {
                        currentFrame = 0;
                        currentLine++;

                        if (currentLine >= NUM_LINES)
                        {
                            Reset();
                        }
                    }

                    source.X = currentFrame * frameWidth;
                    source.Y = currentLine * frameHeight;

                    framesCounter = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw everything that requires drawing
            if (Active) spriteBatch.Draw(texture, position, source, Color.White);
        }

        public void Reset()
        {
            //  Reset object state
            position = Vector2.Zero;
            Active = false;

            framesCounter = 0;
            currentFrame = 0;
            currentLine = 0;
        }

        public void Activate(Vector2 position)
        {
            // Explode logic
            if (!Active)
            {
                this.position = new Vector2(position.X - frameWidth / 2, position.Y - frameHeight / 2);
                Active = true;
            }
        }
    }
}
