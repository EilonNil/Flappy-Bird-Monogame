//using System;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{
    internal class Collision
    {

        public bool isCollide(Rectangle r1, Rectangle r2)
        {
            return r1.Intersects(r2);
        }
        
        public void handleCollision(ref bool end, ref Pillar[] pillars, ref Rectangle posB, ref SpriteBatch _spriteBatch, ref int score)
        {
            end = true;
            for (int j = 0; j < 4; j++)
            {
                pillars[j].pos = new Rectangle(10000, 10000, 500, 500);
            }
            posB = new Rectangle(-10000, -10000, 500, 500);
            string message = String.Format("GAME OVER!!!{0}Your score is: {1}", Environment.NewLine, score);
            _spriteBatch.End();
            var colorTask = MessageBox.Show("Game Over!", message, new[] { "OK" });
            _spriteBatch.Begin();
        }
    }
}
