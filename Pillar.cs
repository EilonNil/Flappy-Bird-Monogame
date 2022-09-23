using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;

namespace FlappyBird
{
    internal class Pillar
    {
        public Rectangle pos;
        public Texture2D tp;

        public Pillar(Rectangle posTP, Texture2D tp)
        {
            this.pos = posTP;
            this.tp = tp;
        }
        public Pillar()
        {
            this.pos = new Rectangle(0, 0, 0, 0);
            this.tp = null;

        }
    }
}
