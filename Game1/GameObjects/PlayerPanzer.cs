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
   public class PlayerPanzer:Panzer
    {
        private TimeSpan elapsedTime;
       
        public PlayerPanzer(Vector2 Position, SpriteInfo spriteInfoPanzer, float Speed, SpriteInfo spriteInfoShot, SpriteInfo spriteInfoBigBang, Func<MovedObject, bool> HasCollisions)
            : base(Position, spriteInfoPanzer, Speed, spriteInfoShot, spriteInfoBigBang, HasCollisions)
        {
          
        }

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
                Position += direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (HasCollisions(this))
                {
                    Position -= direction * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Space) && counterOfShot == 0)
            {
                Shot shot = new Shot(Position, spriteInfoShot, 0.5f, spriteInfoBigBang, HasCollisions)
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
