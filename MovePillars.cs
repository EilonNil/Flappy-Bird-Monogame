using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird
{
    internal class MovePillars
    {
        static Random rand1 = new Random();

        public void move(ref Pillar p1, ref Pillar p2, ref int speed, ref SpriteBatch _spriteBatch)
        {
            p1.pos.X -= speed;
            p2.pos.X -= speed;
            _spriteBatch.Draw(p1.tp, p1.pos, Color.White);
            _spriteBatch.Draw(p2.tp, p2.pos, Color.White);
        }

        public void sendBack(ref Pillar r1, ref Pillar r2, ref int score, ref int speed)
        {
            r1.pos = new Rectangle(800, rand1.Next(30,270) - 400, 107, 400);
            r2.pos = new Rectangle(800, 300, 107, 400);
            r2.pos.Y = r1.pos.Y + 580;
            score++;
            if (score % 6 == 0)
            {
                speed++;
            }
        }
    }
}
