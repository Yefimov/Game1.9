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
   public class BigBang: RectGameObject
   {
       #region Объявление переменных
        public float SpeedOfAnimation { get; set; }//скорость взрыва

        private SpriteInfo spriteInfoBigBang;//тексутра взрыва
        private Vector2 origin;//координаты центра взрыва
        public float Angle;//угол поворота взырва      
       
        public TimeSpan timerForBigBang;//время на взрыв        
        public bool timerForBigBangStop = false;//проверяем прошло ли время отведенное на взрыв, если да удаляем взрыв

        private TimeSpan elapsedTime;
        private TimeSpan elapsedTimeSecond;//тот же что и ElapsedTime только отдельная переменныя для подсчета сколько прошла времени с запуска очередного взрыва 

        private int currentFrame;

       #endregion

        //Конструктор Взрыва
        public BigBang(Vector2 Position, SpriteInfo spriteInfoBigBang ) : base(Position, spriteInfoBigBang, 1.0f)
        {

            this.spriteInfoBigBang = spriteInfoBigBang;

            origin = new Vector2(spriteInfoBigBang.FrameWidth / 2f, spriteInfoBigBang.FrameHeight / 2f);

            double frameCount = spriteInfoBigBang.FrameCount;//кол-во Frame
            double timeToFrame = spriteInfoBigBang.TimeToFrame.TotalMilliseconds;//время на один Frame
            this.timerForBigBang = TimeSpan.FromMilliseconds(frameCount * timeToFrame);//время на взрыв = время на один Frame * на кол-во Frame
        }

       //Логика Взрыва
        public override void Update(GameTime gameTime)
        {

            elapsedTime += gameTime.ElapsedGameTime;
            elapsedTimeSecond += gameTime.ElapsedGameTime;

            while (elapsedTime >= spriteInfoBigBang.TimeToFrame)//Меняем кадры спрайта "Взрыв"
            {
                currentFrame = (currentFrame + 1) % spriteInfoBigBang.FrameCount;
                elapsedTime -= spriteInfoBigBang.TimeToFrame;
            }

            if (elapsedTimeSecond >= timerForBigBang)//останавливаем взрыв после опр. времени
            {
                timerForBigBangStop = true;            
            }
        } 


       //Рисуем Взрыв
        public override void Draw(SpriteBatch spriteBatch)
        {
            var sourceRect = new Rectangle((currentFrame % spriteInfoBigBang.FramesInRow) * spriteInfoBigBang.FrameWidth, (currentFrame / spriteInfoBigBang.FramesInRow) * spriteInfoBigBang.FrameHeight, spriteInfoBigBang.FrameWidth, spriteInfoBigBang.FrameHeight);
            spriteBatch.Draw(spriteInfoBigBang.Texture, Position, sourceRect, Color.White, Angle, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
