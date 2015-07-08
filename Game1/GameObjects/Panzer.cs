﻿using System;
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

            Vector2 direction = new Vector2();           

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Angle = (float)MathHelper.PiOver2;
                direction = new Vector2(-Speed, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Angle = -(float)MathHelper.PiOver2;
                direction = new Vector2(Speed, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Angle = (float)MathHelper.Pi;
                direction = new Vector2(0, -Speed);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Angle = (float)MathHelper.TwoPi;
                direction = new Vector2(0, Speed);
            }

            if (direction.Length() > 0f)
            {
                Position += direction * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                if (HasCollisions(this))
                {
                    Position -= direction * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
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
