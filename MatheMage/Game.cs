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
        const int ScreenMultiply = 3;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Song MainTheme;

        Texture2D MapStart;
        Texture2D MapRoad;
        Texture2D MapEnd;
        Texture2D DungeonBackground;
        Texture2D ForestBackGround;
        Texture2D WhileBackground;
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

        Vector2 taskPos0 = new Vector2(135 * ScreenMultiply, 165 * ScreenMultiply);
        Vector2 taskPos1 = new Vector2(60 * ScreenMultiply, 197 * ScreenMultiply);
        Vector2 taskPos2 = new Vector2(60 * ScreenMultiply, 222 * ScreenMultiply);
        Vector2 taskPos3 = new Vector2(245 * ScreenMultiply, 197 * ScreenMultiply);
        Vector2 taskPos4 = new Vector2(245 * ScreenMultiply, 222 * ScreenMultiply);

        Vector2 EnemyHealthPos = new Vector2(300 * ScreenMultiply, 0);
        Vector2 HealthPos = new Vector2(30, 0);

        Vector2 HeroDmagePos = new Vector2(275 * ScreenMultiply, 120 * ScreenMultiply);

        string[] SaveFileI = SaveManager.Loader();
        string[] SaveFileO = new string[4];
        string[] MathTasks = new string[6];

        int EnemyType = 1;
        int BaseHealth = 3;
        int Health = 3;
        int EnemyHealth = 1;
        int EnemyDamage = 1;
        int HeroDamage = 1;
        int Gold = 0;
        int KilledEnemies = 0;
        int HowMuchToKill = 5;

        int DogHealth = 1;
        int GhostHealth = 1;

        int BackGroundX1 = 0 * ScreenMultiply;
        int BackGroundX2 = 320 * ScreenMultiply;

        int WaitAfterKill = 120;
        int Wait = 0;
        int FrameCount = 1;

        int FireBallXPos = 20 * ScreenMultiply;

        string level = "city";
        bool isAnswered = false;

        bool LastEnemy = false;
        bool ChangeReady = false;
        bool UpgradeChangeReady = false;

        bool isEnemyAlive = false;
        bool isLevelStart = false;
        bool isLevelInit = false;
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
            if (SaveFileI[0] != "nothing")
            {
                BaseHealth = int.Parse(SaveFileI[1]);
                HeroDamage = int.Parse(SaveFileI[2]);
                Gold = int.Parse(SaveFileI[3]);
            }

            this.graphics.PreferredBackBufferHeight = 240 * ScreenMultiply;
            this.graphics.PreferredBackBufferWidth = 320 * ScreenMultiply;
            
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            //screenHeight = this.graphics.PreferredBackBufferHeight / 240;
            //screenWidth = this.graphics.PreferredBackBufferWidth / 320;

            Health = BaseHealth;

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

            PixelCry = this.Content.Load<SpriteFont>("PixelCry");
            // TODO: use this.Content to load your game content here
            DungeonBackground = this.Content.Load<Texture2D>("BackGround");
            WhileBackground = this.Content.Load<Texture2D>("WhileBackground");
            ForestBackGround = this.Content.Load<Texture2D>("ForestBackGround");
            City = this.Content.Load<Texture2D>("city");
            Map = this.Content.Load<Texture2D>("map");
            Forge = this.Content.Load<Texture2D>("Forge");
            ChooseMenu = this.Content.Load<Texture2D>("ChooseMenu");

            Hero1 = this.Content.Load<Texture2D>("Hero1");
            Ghost = this.Content.Load<Texture2D>("Ghost");
            Hero2 = this.Content.Load<Texture2D>("Hero2");
            Sobaka = this.Content.Load<Texture2D>("Sobaka");
            
            FireBall = this.Content.Load<Texture2D>("fireball");

            MapStart = this.Content.Load<Texture2D>("MapStart");
            MapRoad = this.Content.Load<Texture2D>("MapRoad");
            MapEnd = this.Content.Load<Texture2D>("MapEnd");

            MediaPlayer.Play(MainTheme);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume = 1f;
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
                if(currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 265 * ScreenMultiply && currentMouseState.Position.X < 320 * ScreenMultiply && currentMouseState.Position.Y > 80 * ScreenMultiply && currentMouseState.Position.Y < 115 * ScreenMultiply)
                {
                    level = "map";
                }else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 185 * ScreenMultiply && currentMouseState.Position.X < 252 * ScreenMultiply && currentMouseState.Position.Y > 40 * ScreenMultiply && currentMouseState.Position.Y < 96 * ScreenMultiply)
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
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 18 * ScreenMultiply && currentMouseState.Position.X < 86 * ScreenMultiply && currentMouseState.Position.Y > 8 * ScreenMultiply && currentMouseState.Position.Y < 40 * ScreenMultiply)
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
                else if (Gold >= 10 && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 158 * ScreenMultiply && currentMouseState.Position.X < 272 * ScreenMultiply && currentMouseState.Position.Y > 120 * ScreenMultiply && currentMouseState.Position.Y < 161 * ScreenMultiply)
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
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 140 * ScreenMultiply && currentMouseState.Position.X < 155 * ScreenMultiply && currentMouseState.Position.Y > 50 * ScreenMultiply && currentMouseState.Position.Y < 65 * ScreenMultiply)
                {
                    level = "dungeon";
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 197 * ScreenMultiply && currentMouseState.Position.X < 210 * ScreenMultiply && currentMouseState.Position.Y > 100 * ScreenMultiply && currentMouseState.Position.Y < 113 * ScreenMultiply)
                {
                    level = "forest";
                }
            }
            else
            if (level == "dungeon")
            {
                //Движение заднего фона
                if (!LastEnemy && WaitAfterKill < 120)
                {
                    if (WaitAfterKill < 60)
                    {
                        BackGroundX1 -= 10;
                        BackGroundX2 -= 10;
                        if (BackGroundX1 <= -320 * ScreenMultiply)
                        {
                            BackGroundX1 = 0;
                            BackGroundX2 = 320 * ScreenMultiply;
                        }
                    }
                }

                //Создание нового противника
                if (!isEnemyAlive)
                {
                    if (WaitAfterKill <= 0 || !isLevelInit)
                    {
                        EnemyType = Randomize.Rnd(1, 3);
                        if (EnemyType == 1)
                        {
                            EnemyDamage = 1;
                            EnemyHealth = DogHealth;
                        }
                        else if (EnemyType == 2)
                        {
                            EnemyDamage = 2;
                            EnemyHealth = GhostHealth;
                        }
                        isEnemyAlive = true;
                        WaitAfterKill = 120;

                    }
                    else WaitAfterKill--;
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

                //Уровень загрузился
                isLevelInit = true;

                //Проверка ответа на правильность, если прошло 0,5 секунд после последнего ответа
                if (isAnswered)
                {
                    if (MathTasks[5] == "4" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply)  //НП
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
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                SaveManager.Saver(SaveFileO);
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (MathTasks[5] == "3" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 192 * ScreenMultiply && currentMouseState.Position.Y < 208 * ScreenMultiply)  //ВП
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
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                SaveManager.Saver(SaveFileO);
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (MathTasks[5] == "2" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 20 * ScreenMultiply && currentMouseState.Position.X < 130 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply)  //НЛ
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
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                SaveManager.Saver(SaveFileO);
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (MathTasks[5] == "1" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 20 * ScreenMultiply && currentMouseState.Position.X < 130 * ScreenMultiply && currentMouseState.Position.Y > 192 * ScreenMultiply && currentMouseState.Position.Y < 208 * ScreenMultiply)  //ВЛ
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
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                SaveManager.Saver(SaveFileO);
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if (currentMouseState.LeftButton == ButtonState.Pressed && ((currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 192 * ScreenMultiply && currentMouseState.Position.Y < 208 * ScreenMultiply) || (currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply) || (currentMouseState.Position.X > 20 * ScreenMultiply && currentMouseState.Position.X < 130 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply) || (currentMouseState.Position.X > 20 * ScreenMultiply && currentMouseState.Position.X < 130 * ScreenMultiply && currentMouseState.Position.Y > 192 * ScreenMultiply && currentMouseState.Position.Y < 208 * ScreenMultiply)))
                    {
                        isAnswered = false;
                        Health -= EnemyDamage;
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

                spriteBatch.Draw(City, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));
                spriteBatch.DrawString(PixelCry, Gold.ToString(), HealthPos, Color.Yellow);

                spriteBatch.End();
            }
            else
            if (level == "map")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Map, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));

                spriteBatch.End();
            }else
            if (level == "forge")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Forge, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 240 * ScreenMultiply));
                spriteBatch.DrawString(PixelCry, HeroDamage.ToString(), HeroDmagePos, Color.White);

                spriteBatch.End();
            }else
            if (level == "dungeon")
            {

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                if (LastEnemy == true)
                {
                    spriteBatch.Draw(DungeonBackground, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));

                }
                else
                {
                    spriteBatch.Draw(WhileBackground, destinationRectangle: new Rectangle(BackGroundX1, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));
                    spriteBatch.Draw(WhileBackground, destinationRectangle: new Rectangle(BackGroundX2, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));
                    
                }
                spriteBatch.Draw(ChooseMenu, destinationRectangle: new Rectangle(0, 150 * ScreenMultiply, 320 * ScreenMultiply, 90 * ScreenMultiply));
                //Анимация раз в 0,5 секунды
                if (FrameCount <= 30)
                {
                    spriteBatch.Draw(Hero1, destinationRectangle: new Rectangle(20 * ScreenMultiply, 70 * ScreenMultiply, 64 * ScreenMultiply, 64 * ScreenMultiply));
                }else
                    spriteBatch.Draw(Hero2, destinationRectangle: new Rectangle(20 * ScreenMultiply, 70 * ScreenMultiply, 64 * ScreenMultiply, 64 * ScreenMultiply));

                if (EnemyType == 1 && isEnemyAlive)
                {
                    spriteBatch.Draw(Sobaka, destinationRectangle: new Rectangle(220 * ScreenMultiply, 75 * ScreenMultiply, 100 * (ScreenMultiply - 1), 100 * (ScreenMultiply - 1)));
                }else if (EnemyType == 2 && isEnemyAlive)
                {
                    spriteBatch.Draw(Ghost, destinationRectangle: new Rectangle(220 * ScreenMultiply, 70 * ScreenMultiply, 32 * (ScreenMultiply + 2), 32 * (ScreenMultiply+2)));
                }
                //spriteBatch.DrawString(PixelCry, screenWidth.ToString(), Vector2.Zero, Color.Red);

                if (!isAnswered && isLevelStart && isAnswerCorrect)
                {
                    spriteBatch.Draw(FireBall, destinationRectangle: new Rectangle(FireBallXPos * ScreenMultiply, 100 * ScreenMultiply, 12 * ScreenMultiply, 12 * ScreenMultiply));
                    FireBallXPos += 7;
                }
                else
                    FireBallXPos = 10 * ScreenMultiply;

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

                spriteBatch.Draw(ForestBackGround, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
