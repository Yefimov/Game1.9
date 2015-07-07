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
    public class ScenicObject
    {
        public Vector2 Pos; // TODO: Нужен ли нам Pos?
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; } // Height не нужен, т.к. все объекты квадратные
        public int type { get; set; } // Тип этого объекта
        // 1 - кирпич
        // 2 - бетон
        // 3 - лес
        // 4 - вода
        public SpriteInfo image;

        public ScenicObject() // Беспараметричный конструктор
        {
            x = 0;
            y = 0;
            width = 0;
            type = 1; // По умолчанию этот блок стены из кирпича
        }

        public ScenicObject(int x, int y, int width, int type, SpriteInfo image) // Конструктор
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.type = type;
            this.image = image;
        }       

        public void Draws(SpriteBatch spriteBatchs)
        {
            spriteBatchs.Draw(image.Texture, new Vector2(x,y), Color.White);
        }
    }
}

