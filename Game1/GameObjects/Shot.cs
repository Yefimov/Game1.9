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

        private SpriteInfo spriteInfoShot;//текстура Пули
        private SpriteInfo spriteInfoBigBang;//текстура взрыва


        public BigBang BigBangObjects;//экземпляр взрыва, ему будет присвоен взрыв с координатами коллизии пули и объекта


        #endregion

        //Конструктор Пули
        public Shot(SpriteInfo spriteInfoShot, SpriteInfo spriteInfoBigBang)
        {
            this.ShotHasCollisions = false;
            this.spriteInfoShot = spriteInfoShot;
            this.spriteInfoBigBang = spriteInfoBigBang;
            origin = new Vector2(spriteInfoShot.FrameWidth / 2f, spriteInfoShot.FrameHeight / 2f);
            Height = spriteInfoShot.FrameHeight;
            Width = spriteInfoShot.FrameWidth;
        }

        //Рисуем Пулю
        public override void Draw(SpriteBatch spriteBatch)
        {
            var sourceRect = new Rectangle(0, 0, spriteInfoShot.FrameWidth, spriteInfoShot.FrameHeight);
            spriteBatch.Draw(spriteInfoShot.Texture, Position, sourceRect, Color.White, Angle, origin, 1f, SpriteEffects.None, 0f);
        }

        //Логика Пули
        public override void Update(GameTime gameTime)
        {

            //если танк направлен ВЛЕВО
            if (Angle == (float)MathHelper.PiOver2)
            {
                if (((Position.X - Height / 2) + (-Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds) < 0)
                {
                    ShotHasCollisions = true;
                    BigBang bigbang = new BigBang(spriteInfoBigBang)
                    {
                        Position = new Vector2((Position.X - Height / 2) + (-Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds, Position.Y),
                        SpeedOfAnimation = 0.1f
                    };
                    BigBangObjects = bigbang;

                }
                else
                {
                    ShotHasCollisions = false;
                    Position += new Vector2(-Speed, 0) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }


            //если танк направлен ВВЕРХ
            if (Angle == (float)MathHelper.Pi)
            {
                if (((Position.Y - Height / 2) + (-Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds) < 0)
                {
                    ShotHasCollisions = true;
                    BigBang bigbang = new BigBang(spriteInfoBigBang)
                    {
                        Position = new Vector2(Position.X, Position.Y),
                        SpeedOfAnimation = 0.1f
                    };
                    BigBangObjects = bigbang;

                }
                else
                {
                    ShotHasCollisions = false;
                    Position += new Vector2(0, -Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }

            //если танк направлен ВПРАВО
            if (Angle == -(float)MathHelper.PiOver2)
            {
                if ((Position.X + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds >= 800)
                {
                    ShotHasCollisions = true;
                    BigBang bigbang = new BigBang(spriteInfoBigBang)
                    {
                        Position = new Vector2((Position.X + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds, Position.Y),
                        SpeedOfAnimation = 0.1f
                    };
                    BigBangObjects = bigbang;
                }
                else
                {
                    ShotHasCollisions = false;
                    Position += new Vector2(Speed, 0) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }


            //если танк направлен ВНИЗ
            if (Angle == (float)MathHelper.TwoPi)
            {
                if (((Position.Y + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds) >= 480)
                {
                    ShotHasCollisions = true;
                    BigBang bigbang = new BigBang(spriteInfoBigBang)
                    {
                        Position = new Vector2(Position.X, ((Position.Y + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds)),
                        SpeedOfAnimation = 0.1f
                    };
                    BigBangObjects = bigbang;

                }
                else
                {
                    ShotHasCollisions = false;
                    Position += new Vector2(0, Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }
    }
}
              



