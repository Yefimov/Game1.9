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
        public SpriteInfo Image;

        public ScenicObject() // Беспараметричный конструктор
        {
            Position = new Vector2();
            Width = 0;
            Height = 0;
            Type = 1; // По умолчанию этот блок стены из кирпича
        }

        public ScenicObject(Vector2 Position, int Width, int Height, int Type, SpriteInfo Image) // Конструктор
        {
            this.Position = Position;
            this.Width = Width;
            this.Height = Height;
            this.Type = Type;
            this.Image = Image;
        }       

        public override void Draw(SpriteBatch spriteBatchs)
        {
            spriteBatchs.Draw(Image.Texture, Position, Color.White);
        }
    }
}

