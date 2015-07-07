using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameObjects
{
    public abstract class GameObject
    {
        public virtual void Update(GameTime gameTime) { }      
        public abstract void Draw(SpriteBatch spriteBatch);    
    }
}
