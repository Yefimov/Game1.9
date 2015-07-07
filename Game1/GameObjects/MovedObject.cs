using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
    public abstract class MovedObject:RectGameObject
    {
        public MovedObject(Vector2 Position, SpriteInfo Sprite, float Scale, float Speed) : base(Position, Sprite, Scale)
        {
            this.Speed = Speed;
        }
        public float Speed { get; set; }

    }
}
