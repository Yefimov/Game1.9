using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameObjects
{
    public class ScenicObject : RectGameObject
    {
        public int Type { get; set; } // Тип этого объекта
        // 1 - кирпич
        // 2 - бетон
        // 3 - лес
        // 4 - вода

        public ScenicObject(Vector2 Position, int Type, SpriteInfo Image, float Scale)
            : base(Position, Image, Scale)
        {
            this.Position = Position;
            this.Type = Type;
        }       
    }
}

