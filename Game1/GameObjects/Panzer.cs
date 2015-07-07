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

        private SpriteInfo spriteInfoPanzer;//текстура Танка
        private SpriteInfo spriteInfoShot;//текстура Пули 
        private SpriteInfo spriteInfoBigBang;//тексутра взрыва

        public HashSet<Shot> bulletObjects = new HashSet<Shot>();
        public HashSet<BigBang> BigBanObjects = new HashSet<BigBang>();

        public int counterOfShot = 0;//счетчик выстрелов

        private TimeSpan elapsedTime;
     
        Func<MovedObject, bool> HasCollisions;

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

        //Логика для Танка
        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            //если нужное время на выстрел прошло то можно еще раз стрелять
            if (elapsedTime >= spriteInfoShot.TimeToFrame)
            {
                elapsedTime -= spriteInfoShot.TimeToFrame;
                counterOfShot = 0;
            }

            //Реакции на клавиши
            var keyboardState = Keyboard.GetState();

            #region Реакции на клавиши

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //if (((Position.X - Height / 2) + (-Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds) < 0)
                //{

                //}
                
                {
                    Position += new Vector2(-Speed, 0) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (HasCollisions(this))
                    {
                        Position -= new Vector2(-Speed, 0) * (float)gameTime.ElapsedGameTime.TotalMilliseconds; 
                    }

                    Angle = (float)MathHelper.PiOver2;
                }
            }

            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (((Position.Y - Height / 2) + (-Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds) < 0)
                {

                }
                else
                {
                    Position += new Vector2(0, -Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    Angle = (float)MathHelper.Pi;
                }
            }

            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                if ((Position.X + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds >= 800)
                {

                }
                else
                {
                    Position += new Vector2(Speed, 0) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    Angle = -(float)MathHelper.PiOver2;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (((Position.Y + Height / 2) + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds) >= 480)
                {

                }
                else
                {
                    Position += new Vector2(0, Speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    Angle = (float)MathHelper.TwoPi;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Space) && counterOfShot == 0)
            {
                Shot shot = new Shot(Position, spriteInfoShot, 0.5f, spriteInfoBigBang)
                {
                    Angle = this.Angle,                    
                };                
                bulletObjects.Add(shot);
                counterOfShot++;
            }
            #endregion
        }
    }
}
