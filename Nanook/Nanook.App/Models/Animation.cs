using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanook.App.Models
{
    public class Animation
    {
        public Animation(int index, int frames, int speed)
        {
            Index = index;
            Frames = frames;
            Speed = speed;
        }

        public int Index { get; private set; }
        public int Frames { get; private set; }
        public int Speed { get; private set; }
    }
}
