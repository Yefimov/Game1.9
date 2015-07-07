using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
  public abstract  class RectGameObject: GameObject
    {
      public Vector2 Position { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }     

      public bool Intersect(RectGameObject that)
      {
          Rectangle a = new Rectangle((int) this.Position.X, (int) this.Position.Y, this.Width, this.Height);
          Rectangle b = new Rectangle((int) that.Position.X, (int) that.Position.Y, that.Width, that.Height);
          return a.Intersects(b);       
      }
    
    }
}
