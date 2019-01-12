using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MatheMage
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Song MainTheme;

        Texture2D DungeonBackground;
        Texture2D ForestBackGround;
        Texture2D Hero1;
        Texture2D Hero2;
        Texture2D Sobaka;
        Texture2D ChooseMenu;
        Texture2D City;
        Texture2D Forge;
        Texture2D FireBall;
        Texture2D Map;
        Texture2D Ghost;
        SpriteFont PixelCry;

        Vector2 taskPos0 = new Vector2(135 * 3, 165 * 3);
        Vector2 taskPos1 = new Vector2(60 * 3, 197 * 3);
        Vector2 taskPos2 = new Vector2(60 * 3, 222 * 3);
        Vector2 taskPos3 = new Vector2(245 * 3, 197 * 3);
        Vector2 taskPos4 = new Vector2(245 * 3, 222 * 3);

        Vector2 EnemyHealthPos = new Vector2(300 * 3, 0);
        Vector2 HealthPos = new Vector2(30, 0);

        Vector2 HeroDmagePos = new Vector2(275 * 3, 120 * 3);

        string[] MathTasks = new string[6];

        int EnemyType = 1;
        int Health = 3;
        int EnemyHealth = 1;
        int HeroDamage = 1;
        int Gold = 0;
        int KilledEnemies = 0;

        int Wait = 0;
        int FrameCount = 1;

        int FireBallXPos =20 * 3;

        string level = "city";
        bool isAnswered = false;

        bool ChangeReady = false;
        bool UpgradeChangeReady = false;

        bool isEnemyAlive = false;
        bool isLevelStart = false;
        bool isAnswerCorrect = true;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.PreferredBackBufferWidth = 960;
            
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            //screenHeight = this.graphics.PreferredBackBufferHeight / 240;
            //screenWidth = this.graphics.PreferredBackBufferWidth / 320;

            this.IsMouseVisible = true;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MainTheme = this.Content.Load<Song>("lich-technical_941_use_only_when_nesessary");

            // TODO: use this.Content to load your game content here
            DungeonBackground = this.Content.Load<Texture2D>("BackGround");
            ForestBackGround = this.Content.Load<Texture2D>("ForestBackGround");
            ChooseMenu = this.Content.Load<Texture2D>("ChooseMenu");
            Hero1 = this.Content.Load<Texture2D>("Hero1");
            Hero2 = this.Content.Load<Texture2D>("Hero2");
            Sobaka = this.Content.Load<Texture2D>("Sobaka");
            PixelCry = this.Content.Load<SpriteFont>("PixelCry");
            City = this.Content.Load<Texture2D>("city");
            FireBall = this.Content.Load<Texture2D>("fireball");
            Map = this.Content.Load<Texture2D>("map");
            Ghost = this.Content.Load<Texture2D>("Ghost");
            Forge = this.Content.Load < Texture2D>("Forge");
            
            MediaPlayer.Play(MainTheme);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume = 1f;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            //Отсчёт кадров для анимаций
            if (FrameCount <= 60)
            {
                FrameCount++;
            }
            else FrameCount = 1;
            if (level == "city")
            {
                if(currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 265 * 3 && currentMouseState.Position.X < 320 * 3 && currentMouseState.Position.Y > 80 * 3 && currentMouseState.Position.Y < 115 * 3)
                {
                    level = "map";
                }else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 185 * 3 && currentMouseState.Position.X < 252 * 3 && currentMouseState.Position.Y > 40 * 3 && currentMouseState.Position.Y < 96 * 3)
                {
                    ChangeReady = true;
                }else
                if (ChangeReady == true && Wait == 15)
                {
                    level = "forge";
                    ChangeReady = false;
                    Wait = 0;
                }
                else if (ChangeReady == true) Wait++;
                
            }else if(level == "forge")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 18 && currentMouseState.Position.X < 86 * 3 && currentMouseState.Position.Y > 8 * 3 && currentMouseState.Position.Y < 40 * 3)
                {
                    ChangeReady = true;
                }
                else
                if (ChangeReady == true && Wait == 15)
                {
                    level = "city";
                    ChangeReady = false;
                    Wait = 0;
                }
                else if (ChangeReady == true) Wait++;
                else if (Gold >= 10 && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 158 * 3 && currentMouseState.Position.X < 272 * 3 && currentMouseState.Position.Y > 120 * 3 && currentMouseState.Position.Y < 161 * 3)
                {
                    UpgradeChangeReady = true;
                }
                else
                if (UpgradeChangeReady == true && Wait == 15)
                {
                    HeroDamage++;
                    UpgradeChangeReady = false;
                    Wait = 0;
                    Gold -= 10;
                }
                else if (UpgradeChangeReady == true) Wait++;
            }
            else if(level == "map")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 140 * 3 && currentMouseState.Position.X < 155 * 3 && currentMouseState.Position.Y > 50 * 3 && currentMouseState.Position.Y < 65 * 3)
                {
                    level = "dungeon";
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 197 * 3 && currentMouseState.Position.X < 210 * 3 && currentMouseState.Position.Y > 100 * 3 && currentMouseState.Position.Y < 113 * 3)
                {
                    level = "forest";
                }
            }
            else
            if (level == "dungeon")
            {
                //Создание нового противника
                if (!isEnemyAlive)
                {
                    EnemyType = Randomize.Rnd(1, 3);
                    if (EnemyType == 1)
                    {
                        EnemyHealth = 10;
                    }else if (EnemyType == 2)
                    {
                        EnemyHealth = 5;
                    }
                    isEnemyAlive = true;
                }
                //Ожидание после ответа(Нужно для адекватных анимаций)
                if (!isAnswered)
                {
                    if (Wait == 30)
                    {
                        MathTasks = TaskGen.Generate(1);
                        isAnswered = true;
                        Wait = 0;
                        isLevelStart = true;
                    }
                    else Wait++;
                }
                //Проверка ответа на правильность, если прошло 0,5 секунд после последнего ответа
                if (isAnswered)
                {
                    if (MathTasks[5] == "4" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * 3 && currentMouseState.Position.X < 310 * 3 && currentMouseState.Position.Y > 215 * 3 && currentMouseState.Position.Y < 235 * 3)  //НП
                    {
                        isAnswered = false;
                        EnemyHealth -= HeroDamage;
                        isAnswerCorrect = true;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5;
                            }else if (EnemyType == 2)
                            {
                                Gold += 10;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == 2)
                            {
                                KilledEnemies = 0;
                                level = "city";
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (MathTasks[5] == "3" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * 3 && currentMouseState.Position.X < 310 * 3 && currentMouseState.Position.Y > 192 * 3 && currentMouseState.Position.Y < 208 * 3)  //ВП
                    {
                        isAnswered = false;
                        EnemyHealth -= HeroDamage;
                        isAnswerCorrect = true;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == 2)
                            {
                                KilledEnemies = 0;
                                level = "city";
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (MathTasks[5] == "2" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 20 * 3 && currentMouseState.Position.X < 130 * 3 && currentMouseState.Position.Y > 215 * 3 && currentMouseState.Position.Y < 235 * 3)  //НЛ
                    {
                        isAnswered = false;
                        EnemyHealth -= HeroDamage;
                        isAnswerCorrect = true;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == 2)
                            {
                                KilledEnemies = 0;
                                level = "city";
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (MathTasks[5] == "1" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 20 * 3 && currentMouseState.Position.X < 130 * 3 && currentMouseState.Position.Y > 192 * 3 && currentMouseState.Position.Y < 208 * 3)  //ВЛ
                    {
                        isAnswered = false;
                        EnemyHealth -= HeroDamage;
                        isAnswerCorrect = true;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == 2)
                            {
                                KilledEnemies = 0;
                                level = "city";
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (currentMouseState.LeftButton == ButtonState.Pressed && ((currentMouseState.Position.X > 200 * 3 && currentMouseState.Position.X < 310 * 3 && currentMouseState.Position.Y > 192 * 3 && currentMouseState.Position.Y < 208 * 3) || (currentMouseState.Position.X > 200 * 3 && currentMouseState.Position.X < 310 * 3 && currentMouseState.Position.Y > 215 * 3 && currentMouseState.Position.Y < 235 * 3) || (currentMouseState.Position.X > 20 * 3 && currentMouseState.Position.X < 130 * 3 && currentMouseState.Position.Y > 215 * 3 && currentMouseState.Position.Y < 235 * 3) || (currentMouseState.Position.X > 20 * 3 && currentMouseState.Position.X < 130 * 3 && currentMouseState.Position.Y > 192 * 3 && currentMouseState.Position.Y < 208 * 3)))
                    {
                        isAnswered = false;
                        Health--;
                        isAnswerCorrect = false;
                        if (Health == 0)
                        {
                            Exit();
                        }
                    }
                }
                
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (level == "city")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(City, destinationRectangle: new Rectangle(0, 0, 320 * 3, 150 * 3));
                spriteBatch.DrawString(PixelCry, Gold.ToString(), HealthPos, Color.Yellow);

                spriteBatch.End();
            }
            else
            if (level == "map")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Map, destinationRectangle: new Rectangle(0, 0, 320 * 3, 150 * 3));

                spriteBatch.End();
            }else
            if (level == "forge")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Forge, destinationRectangle: new Rectangle(0, 0, 320 * 3, 240 * 3));
                spriteBatch.DrawString(PixelCry, HeroDamage.ToString(), HeroDmagePos, Color.White);

                spriteBatch.End();
            }else
            if (level == "dungeon")
            {

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(DungeonBackground, destinationRectangle: new Rectangle(0, 0, 320 * 3, 150 * 3));
                spriteBatch.Draw(ChooseMenu, destinationRectangle: new Rectangle(0, 150 * 3, 320 * 3, 90 * 3));
                //Анимация рав 0,5 секунды
                if (FrameCount <= 30)
                {
                    spriteBatch.Draw(Hero1, destinationRectangle: new Rectangle(20 * 3, 70 * 3, 64 * 3, 64 * 3));
                }else
                    spriteBatch.Draw(Hero2, destinationRectangle: new Rectangle(20 * 3, 70 * 3, 64 * 3, 64 * 3));

                if (EnemyType == 1)
                {
                    spriteBatch.Draw(Sobaka, destinationRectangle: new Rectangle(220 * 3, 75 * 3, 100 * 2, 100 * 2));
                }else if (EnemyType == 2)
                {
                    spriteBatch.Draw(Ghost, destinationRectangle: new Rectangle(220 * 3, 70 * 3, 32 * 5, 32 * 5));
                }
                //spriteBatch.DrawString(PixelCry, screenWidth.ToString(), Vector2.Zero, Color.Red);

                if (!isAnswered && isLevelStart && isAnswerCorrect)
                {
                    spriteBatch.Draw(FireBall, destinationRectangle: new Rectangle(FireBallXPos * 3, 100 * 3, 12 * 3, 12 * 3));
                    FireBallXPos += 7;
                }
                else
                    FireBallXPos = 10 * 3;

                //Отрисовка заданий
                if (isAnswered == true)
                {
                    spriteBatch.DrawString(PixelCry, MathTasks[0], taskPos0, Color.White);
                    spriteBatch.DrawString(PixelCry, MathTasks[1], taskPos1, Color.White); //ВЛ 1
                    spriteBatch.DrawString(PixelCry, MathTasks[2], taskPos2, Color.White); //НЛ 2
                    spriteBatch.DrawString(PixelCry, MathTasks[3], taskPos3, Color.White); //ВП 3
                    spriteBatch.DrawString(PixelCry, MathTasks[4], taskPos4, Color.White); //НП 4
                                                                                           //=======
                }
                //Отрисовка хп
                spriteBatch.DrawString(PixelCry, EnemyHealth.ToString(), EnemyHealthPos, Color.Red);
                spriteBatch.DrawString(PixelCry, Health.ToString(), HealthPos, Color.Red);

                spriteBatch.End();
            }else if(level == "forest")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(ForestBackGround, destinationRectangle: new Rectangle(0, 0, 320*3, 150*3));

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
