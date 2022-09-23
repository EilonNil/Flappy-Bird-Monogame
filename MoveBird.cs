using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird
{
    class MoveBird
    {
        public void moveBird(ref Rectangle pos)
        {
            pos.Y -= 11;
        }

    }
    
}
