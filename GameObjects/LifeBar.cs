using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    class LifeBar
    {
        Rectangle rec;
        int maxLife;
        int currentLife;
        int damage;
        bool moveDown; 

        public bool Finished { get; private set; }

        public LifeBar(Rectangle rec, int maxLife)
        {
            this.rec = rec;
            this.maxLife = maxLife;
            currentLife = maxLife;
            damage = 0;
            moveDown = false;
            Finished = false;
        }

        public void Update(Vector2 position)
        {
            rec.X = (int)position.X;
            rec.Y = (int)position.Y;

            if (moveDown)
            {
                currentLife -= 5;
                damage -= 5;

                if (damage <= 0)
                {
                    damage = 0;
                    moveDown = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpaceGame.pixel, rec, Color.Red);
            spriteBatch.Draw(SpaceGame.pixel, new Rectangle(rec.X, rec.Y, (int)((float)rec.Width*((float)currentLife/(float)maxLife)), rec.Height), Color.Maroon);
            spriteBatch.Draw(SpaceGame.pixel, new Rectangle(rec.X, rec.Y, (int)((float)rec.Width*((float)(currentLife - damage) / (float)maxLife)), rec.Height), Color.Yellow);
        }

        public void ReceiveDamage(int damage)
        {
            this.damage += damage;
            moveDown = true;

            if ((currentLife - this.damage) <= 0) Finished = true;
        }

        public void Reset()
        {
            damage = 0;
            moveDown = false;
            Finished = false;

        }
    }
}
