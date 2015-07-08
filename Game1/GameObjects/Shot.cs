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
    public class Shot : MovedObject
    {
        #region Объявление переменных        
        public float Angle;//угол поворота пули

        private Vector2 origin;//центр пули

        public bool ShotHasCollisions { get; set; }//Есть Коллизия пули и объекта

        private SpriteInfo spriteInfoBigBang;//текстура взрыва

        public BigBang BigBangObjects;//экземпляр взрыва, ему будет присвоен взрыв с координатами коллизии пули и объекта
        
        Func<MovedObject, bool> HasCollisions;

        #endregion

        //Конструктор Пули
        public Shot(Vector2 Position, SpriteInfo spriteInfoShot, float Speed, SpriteInfo spriteInfoBigBang, Func<MovedObject, bool> HasCollisions)
            : base(Position, spriteInfoShot, 1.0f, Speed)
        {
            this.HasCollisions = HasCollisions;
            this.ShotHasCollisions = false;
            this.spriteInfoBigBang = spriteInfoBigBang;
            origin = new Vector2(spriteInfoShot.FrameWidth / 2f, spriteInfoShot.FrameHeight / 2f);
        }

        //Рисуем Пулю
        public override void Draw(SpriteBatch spriteBatch)
        {
            var sourceRect = new Rectangle(0, 0, Sprite.FrameWidth, Sprite.FrameHeight);
            spriteBatch.Draw(Sprite.Texture, Position, sourceRect, Color.White, Angle, origin, 1f, SpriteEffects.None, 0f);
        }

        //Логика Пули
        public override void Update(GameTime gameTime)
        {


            Vector2 direction = new Vector2();      

            //если танк направлен ВЛЕВО
            if (Angle == (float)MathHelper.PiOver2)
            {
                direction += new Vector2(-Speed, 0);             
            }


            //если танк направлен ВВЕРХ
            if (Angle == (float)MathHelper.Pi)
            {
                direction += new Vector2(0, -Speed);
            }
            

            //если танк направлен ВПРАВО
            if (Angle == -(float)MathHelper.PiOver2)
            {
                direction += new Vector2(Speed, 0);
            }


            //если танк направлен ВНИЗ
            if (Angle == (float)MathHelper.TwoPi)
            {
                direction += new Vector2(0, Speed);
            }
            
            
            if (direction.Length() > 0f)
            {
                Position += direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (HasCollisions(this))
                {
                    ShotHasCollisions = true;
                    BigBang bigbang = new BigBang(Position, spriteInfoBigBang)
                    {
                        Position = new Vector2((Position.X + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds, Position.Y),
                        SpeedOfAnimation = 0.1f
                    };
                    BigBangObjects = bigbang;
                }
            }







        }
    }
}
              



