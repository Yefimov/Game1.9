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
    class EnemyPanzer:Panzer
    {
        private TimeSpan elapsedTime;

        public EnemyPanzer (Vector2 Position, SpriteInfo spriteInfoPanzer, float Speed, SpriteInfo spriteInfoShot, SpriteInfo spriteInfoBigBang, Func<MovedObject, bool> HasCollisions)
            : base(Position, spriteInfoPanzer, Speed, spriteInfoShot, spriteInfoBigBang, HasCollisions)
        {
          
        }
    
    }
}
