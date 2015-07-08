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
    // TODO: Разбитие кирпичей   
    public class Panzer : MovedObject
    {
        #region Объявление переменных           
        public float Angle = (float)MathHelper.TwoPi;//угол поворота Танка

        private Vector2 origin;//координаты центра танка

        protected SpriteInfo spriteInfoPanzer;//текстура Танка
        protected SpriteInfo spriteInfoShot;//текстура Пули 
        protected SpriteInfo spriteInfoBigBang;//тексутра взрыва

        public HashSet<Shot> bulletObjects = new HashSet<Shot>();
        public HashSet<BigBang> BigBanObjects = new HashSet<BigBang>();

        public int counterOfShot = 0;//счетчик выстрелов

        protected Func<MovedObject, bool> HasCollisions;

        #endregion

        //Конструктор Танка
        public Panzer(Vector2 Position, SpriteInfo spriteInfoPanzer, float Speed, SpriteInfo spriteInfoShot, SpriteInfo spriteInfoBigBang, 
            Func<MovedObject,bool> HasCollisions) : base(Position, spriteInfoPanzer, 0.2f, Speed)
        {
            this.HasCollisions = HasCollisions;
            this.spriteInfoPanzer = spriteInfoPanzer;
            this.spriteInfoShot = spriteInfoShot;
            this.spriteInfoBigBang = spriteInfoBigBang;

            origin = new Vector2(spriteInfoPanzer.FrameWidth / 2f, spriteInfoPanzer.FrameHeight / 2f);
        }

        //Рисуем Танк
        public override void Draw(SpriteBatch spriteBatch)
        {
            var sourceRect = new Rectangle(0, 0, spriteInfoPanzer.FrameWidth, spriteInfoPanzer.FrameHeight);//Rect Танка
            spriteBatch.Draw(spriteInfoPanzer.Texture, Position, sourceRect, Color.White, Angle, origin, 0.2f, SpriteEffects.None, 0f);
        }       
    }
}
