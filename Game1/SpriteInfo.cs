using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class SpriteInfo
    {    
        public Texture2D Texture { get; set; }//контент
        public TimeSpan TimeToFrame { get; set; }//время на 1ин Frame
        public int FrameWidth { get; set; }//ширина Frame
        public int FrameHeight { get; set; }//высота Frame
        public int FrameCount { get; set; }//общее кол-ов Frame
        public int FramesInRow { get; set; }//кол-во Frame в строке
       
    }
}
