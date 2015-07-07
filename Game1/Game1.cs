using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using Game1.GameObjects;
using System.Collections.Generic;


// TODO: Корректный размер окна
// TODO: Отображение жизней игрока
// TODO: Кирпичики у нас 16х16 пикселей, а танк очень большой. Что-то уменьшаем или увеличиваем.
// TODO: Враги
// TODO: Отображение счётчика, сколько осталось врагов
// TODO: Второй игрок
// TODO: Бонусы в игре

namespace Game1
{
    /// <summary>
    /// Это главный класс игры.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        private HashSet<ScenicObject> scenicObjects;
        
        private HashSet<Panzer> panzerObjects;
        
        private HashSet<Shot> bulletObjects;
        private HashSet<Shot> bulletForDeleted;
        
        private HashSet<BigBang> bigBangObjects = new HashSet<BigBang>();
        private HashSet<BigBang> bigBangForDeleted = new HashSet<BigBang>();

        private int clientWidth;
        private int clientHeight;


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Позволяет игре выполнить необходимую инициализацую перед запуском. Здесь игра сможет запросить любую необходимую услуги и загрузить любой не-графический контент.
        /// Вызов base.Initialize выполнит пересчёт всех компонентов и вдобавок инициализирует их. 
        /// </summary>
        protected override void Initialize()
        {
            clientWidth = Window.ClientBounds.Width;
            clientHeight = Window.ClientBounds.Height;

            // Укажите здесь вашу логику инициализации
            base.Initialize();
        }

        /// <summary>
        /// LoadContent вызовется лишь один раз и здесь можно загрузить весь ваш контент.
        /// </summary>
        
        
        protected override void LoadContent()
        {
            // Создание нового SpriteBatch, что позволит рисовать текстуры.
            // Batch (англ.) - пакет, набор.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            #region PanzerSprite
            var panzerSprite = new SpriteInfo
            {
                Texture = Content.Load<Texture2D>("panzer2"),
                FrameWidth = 80,
                FrameHeight = 111

            };
            #endregion

            #region ShotSprite
            var shotSprite = new SpriteInfo
            {
                Texture = Content.Load<Texture2D>("shot"),
                FrameWidth = 12,
                FrameHeight = 16,
                TimeToFrame = TimeSpan.FromSeconds(1)
            };
            #endregion

            #region BigBangSprite
            var bigBangSprite = new SpriteInfo
            {
                Texture = Content.Load<Texture2D>("BigBang2"),
                FrameWidth = 64,
                FrameHeight = 64,
                TimeToFrame = TimeSpan.FromMilliseconds(50),
                FrameCount = 25,
                FramesInRow = 5
            };
            #endregion

            #region Map
            var wallbrick = new SpriteInfo { Texture = Content.Load<Texture2D>("wall_brick") };
            var wallconcrete = new SpriteInfo { Texture = Content.Load<Texture2D>("wall_concrete") };
            var forest = new SpriteInfo { Texture = Content.Load<Texture2D>("forest") };

            scenicObjects = new HashSet<ScenicObject>();

            string[,] MasMapsToDraw = null;

            ReaderMap.getMap(ReaderMap.Reader(MasMapsToDraw), scenicObjects, wallbrick, wallconcrete, forest);

            #endregion
            //==========================================================

            panzerObjects = new HashSet<Panzer>();
            bulletObjects = new HashSet<Shot>();

            Panzer panzer = new Panzer(panzerSprite, shotSprite, bigBangSprite, HasCollisions)
            {
                Position = new Vector2(400, 300),
                Speed = 0.1f,
            };

            
            panzerObjects.Add(panzer);
            bulletObjects = panzer.bulletObjects;
            // Используйте this.Content, чтобы загрузить здесь контент вашей игры
        }

        /// <summary>
        /// UnloadContent вызовится один раз за игру и здесь выгружается особый игровой контент.
        /// </summary>
      
        protected override void UnloadContent()
        {
            // Выгрузите здесь ваш не имеющий отношения к ContentManager контент
        }

        /// <summary>
        /// Позволяет игре использовать логику игры каждый раз при обновлении мира.
        /// Например, проверка столкновений (коллизий), обработка входных данных и проигрывание аудио.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //PANZER
            foreach (var panzerObject in panzerObjects)
            {
                panzerObject.Update(gameTime);
            }

            //BigBang
            foreach (var bigBangObject in bigBangObjects)
            {
                bigBangObject.Update(gameTime);
            }

            //SHOT
            bulletForDeleted = new HashSet<Shot>();
            foreach (var bulletObject in bulletObjects)
            {
                bulletObject.Update(gameTime);
                if (bulletObject.ShotHasCollisions)
                {
                    bulletForDeleted.Add(bulletObject);
                }
                
            }
                      
            bulletObjects.RemoveWhere(element => element.ShotHasCollisions);

            foreach (var bfd in bulletForDeleted)
            {
                bigBangObjects.Add(bfd.BigBangObjects);
            }

            foreach (var b in bigBangObjects)
            {
                if (b.timerForBigBangStop)
                {
                    bigBangForDeleted.Add(b);
                }
            }

            bigBangObjects.RemoveWhere(element => element.timerForBigBangStop);
            base.Update(gameTime);
        }

        /// <summary>
        /// Вызывается, когда надо рисовать игру
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            #region SpriteBatch Begin-End
            spriteBatch.Begin();
            foreach (var bulletObject in bulletObjects)
            {
                bulletObject.Draw(spriteBatch);
            }

            foreach (var panzerObject in panzerObjects)
            {
                panzerObject.Draw(spriteBatch);
            }
            foreach (var scenicObject in scenicObjects)
            {
                scenicObject.Draw(spriteBatch);
            }

            foreach (var bigbandObject in bigBangObjects)
            {
                bigbandObject.Draw(spriteBatch);
            }
            spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }

        public bool HasCollisions(MovedObject obj)
        {
            foreach (var scenicObject in scenicObjects)
            {
                if (scenicObject.Intersect(obj))
                {
                    return true;
                }
            }

            foreach (var panzerObject in panzerObjects)
            {
                if (panzerObject.Intersect(obj) && panzerObject != obj)
                {
                    return true;
                }
            }
            return false;
        }
       
    }
}
