using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MatheMage
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        const int ScreenMultiply = 3;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Song MainTheme;
        Song CityTheme;
        Song DungeonTheme;

        Texture2D MainMenu;
        Texture2D ShopBackground;
        Texture2D BlowParticle;
        Texture2D HospitalRoom;
        Texture2D DungeonBackground;
        Texture2D WhileBackground;
        Texture2D CityTutorial;
        Texture2D CityTutorial2;
        Texture2D DungeonTutorial;
        Texture2D DungeonTutorial2;
        Texture2D DungeonTutorial3;
        Texture2D DungeonTutorial4;
        Texture2D[] DungeonTutorialArr;
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

        int[] BlowParticlePositionX = new int[50];
        int[] BlowParticlePositionY = new int[50];

        string[] SaveFileI = SaveManager.Loader();
        string[] SaveFileO = new string[6];
        string[] MathTasks = new string[6];

        int EnemyType = 1;
        int BaseHealth = 3;
        int Health = 3;
        int EnemyHealth = 1;
        int EnemyDamage = 1;
        int HeroDamage = 1;
        int Gold = 0;
        int KilledEnemies = 0;
        int HowMuchToKill = 2;

        int CityTutorialPart = 1;
        int DungeonTutorialPart = 0;

        int DogHealth = 10;
        int GhostHealth = 5;

        double DifficultyMultiply = 0;
        double GoldMultiply = 0;

        int BackGroundX1 = 0 * ScreenMultiply;
        int BackGroundX2 = 320 * ScreenMultiply;

        int TutorialWait;
        int WaitAfterKill = 120;
        int Wait = 0;
        int FrameCount = 1;

        int TimerXPos = 0;
        int FireBallXPos = 20 * ScreenMultiply;
        int BlowParticleSize = 0;

        string level = "menu";
        bool isAnswered = false;

        bool LastEnemy = false;
        bool ChangeReady = false;
        bool UpgradeChangeReady = false;

        bool isEnemyAlive = false;
        bool isLevelStart = false;
        bool isLevelInit = false;
        bool isAnswerCorrect = true;

        bool cityTutorialActive = true;
        bool dungeonTutorialActive = true;
        bool isDungeonReadyAfterTutorial = false;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            if (SaveFileI[0] != "nothing")
            {
                BaseHealth = int.Parse(SaveFileI[1]);
                HeroDamage = int.Parse(SaveFileI[2]);
                Gold = int.Parse(SaveFileI[3]);
                cityTutorialActive = bool.Parse(SaveFileI[4]);
                dungeonTutorialActive = bool.Parse(SaveFileI[5]);
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

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);

            MainTheme = this.Content.Load<Song>("lich-technical_941_use_only_when_nesessary");
            DungeonTheme = this.Content.Load<Song>("dungeontheme");
            CityTheme = this.Content.Load<Song>("citytheme");

            BlowParticle = this.Content.Load<Texture2D>("BlowParticle");

            PixelCry = this.Content.Load<SpriteFont>("PixelCry");

            MainMenu = this.Content.Load<Texture2D>("MainMenu");
            ShopBackground = this.Content.Load<Texture2D>("Shop");
            HospitalRoom = this.Content.Load<Texture2D>("HospitalRoom");
            DungeonBackground = this.Content.Load<Texture2D>("BackGround");
            WhileBackground = this.Content.Load<Texture2D>("WhileBackground");
            City = this.Content.Load<Texture2D>("city");
            Map = this.Content.Load<Texture2D>("map");
            Forge = this.Content.Load<Texture2D>("Forge");
            ChooseMenu = this.Content.Load<Texture2D>("ChooseMenu");

            CityTutorial = this.Content.Load<Texture2D>("CityTutorial");
            CityTutorial2 = this.Content.Load<Texture2D>("CityTutorial2");
            DungeonTutorial = this.Content.Load<Texture2D>("DungeonTutorial");
            DungeonTutorial2 = this.Content.Load<Texture2D>("DungeonTutorial2");
            DungeonTutorial3 = this.Content.Load<Texture2D>("DungeonTutorial3");
            DungeonTutorial4 = this.Content.Load<Texture2D>("DungeonTutorial4");

            DungeonTutorialArr = new Texture2D[4] { DungeonTutorial, DungeonTutorial2, DungeonTutorial3, DungeonTutorial4 };

            Hero1 = this.Content.Load<Texture2D>("Hero1");
            Ghost = this.Content.Load<Texture2D>("Ghost");
            Hero2 = this.Content.Load<Texture2D>("Hero2");
            Sobaka = this.Content.Load<Texture2D>("Sobaka");

            FireBall = this.Content.Load<Texture2D>("fireball");

            MediaPlayer.Play(MainTheme);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume = 1f;
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            //Отсчёт кадров для анимаций
            if (FrameCount <= 60)
            {
                FrameCount++;
            }
            else FrameCount = 1;

            if (level == "menu")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 104 * ScreenMultiply && currentMouseState.Position.X < 221 * ScreenMultiply && currentMouseState.Position.Y > 88 * ScreenMultiply && currentMouseState.Position.Y < 130 * ScreenMultiply)
                {
                    level = "city";
                    MediaPlayer.Play(CityTheme);
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 1 * ScreenMultiply && currentMouseState.Position.X < 75 * ScreenMultiply && currentMouseState.Position.Y > 1 * ScreenMultiply && currentMouseState.Position.Y < 34 * ScreenMultiply)
                {
                    SaveManager.Saver(new string[] { "test", BaseHealth.ToString(), HeroDamage.ToString(), Gold.ToString(), cityTutorialActive.ToString(), dungeonTutorialActive.ToString() });
                    Exit();
                }
            }

            else if (level == "city")
            {

                if (cityTutorialActive)
                {
                    if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 248 * ScreenMultiply && currentMouseState.Position.X < 318 * ScreenMultiply && currentMouseState.Position.Y > 211 * ScreenMultiply && currentMouseState.Position.Y < 238 * ScreenMultiply)
                    {
                        if (CityTutorialPart == 1)
                        {
                            CityTutorialPart++;
                        }
                        else if(TutorialWait > 60)
                        {
                            cityTutorialActive = false;
                            SaveManager.Saver(new string[] { "test", BaseHealth.ToString(), HeroDamage.ToString(), Gold.ToString(), cityTutorialActive.ToString(), dungeonTutorialActive.ToString() });
                            TutorialWait = 0;
                        }
                    }
                    if(CityTutorialPart == 2)
                    {
                        TutorialWait++;
                    }
                }
                else
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 265 * ScreenMultiply && currentMouseState.Position.X < 320 * ScreenMultiply && currentMouseState.Position.Y > 80 * ScreenMultiply && currentMouseState.Position.Y < 115 * ScreenMultiply)
                {
                    level = "dungeonTutorialLevel";
                    MediaPlayer.Play(DungeonTheme);
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 185 * ScreenMultiply && currentMouseState.Position.X < 252 * ScreenMultiply && currentMouseState.Position.Y > 40 * ScreenMultiply && currentMouseState.Position.Y < 96 * ScreenMultiply)
                {
                    ChangeReady = true;
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 21 * ScreenMultiply && currentMouseState.Position.X < 75 * ScreenMultiply && currentMouseState.Position.Y > 52 * ScreenMultiply && currentMouseState.Position.Y < 97 * ScreenMultiply)
                {
                    level = "shop";
                }
                else if (ChangeReady == true && Wait == 15)
                {
                    level = "forge";
                    ChangeReady = false;
                    Wait = 0;
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 1 * ScreenMultiply && currentMouseState.Position.X < 35 * ScreenMultiply && currentMouseState.Position.Y > 133 * ScreenMultiply && currentMouseState.Position.Y < 148 * ScreenMultiply)
                {
                    level = "menu";
                    MediaPlayer.Play(MainTheme);
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 98 * ScreenMultiply && currentMouseState.Position.X < 158 * ScreenMultiply && currentMouseState.Position.Y > 41 * ScreenMultiply && currentMouseState.Position.Y < 94 * ScreenMultiply)
                {
                    level = "hospital";
                }
                else if (ChangeReady == true) Wait++;
            }
            else if(level == "hospital")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 18 * ScreenMultiply && currentMouseState.Position.X < 86 * ScreenMultiply && currentMouseState.Position.Y > 8 * ScreenMultiply && currentMouseState.Position.Y < 40 * ScreenMultiply)
                {
                    ChangeReady = true;
                }
                else
                if (ChangeReady == true && Wait == 15)
                {
                    level = "city";
                    MediaPlayer.Play(CityTheme);
                    ChangeReady = false;
                    Wait = 0;
                }
                else if (ChangeReady == true) Wait++;

                else if (Gold >= 50 && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 158 * ScreenMultiply && currentMouseState.Position.X < 272 * ScreenMultiply && currentMouseState.Position.Y > 120 * ScreenMultiply && currentMouseState.Position.Y < 161 * ScreenMultiply)
                {
                    UpgradeChangeReady = true;
                }
                else
                if (UpgradeChangeReady == true && Wait == 3)
                {
                    BaseHealth++;
                    UpgradeChangeReady = false;
                    Wait = 0;
                    Gold -= 50;
                    Health = BaseHealth;
                }
                else if (UpgradeChangeReady == true) Wait++;
            }
            else if (level == "shop")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 1 * ScreenMultiply && currentMouseState.Position.X < 48 * ScreenMultiply && currentMouseState.Position.Y > 1 * ScreenMultiply && currentMouseState.Position.Y < 34 * ScreenMultiply)
                {
                    level = "city";
                    MediaPlayer.Play(CityTheme);
                }
                if (Gold >= 100000 && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 93 * ScreenMultiply && currentMouseState.Position.X < 232 * ScreenMultiply && currentMouseState.Position.Y > 107 * ScreenMultiply && currentMouseState.Position.Y < 136 * ScreenMultiply)
                {
                    level = "city";
                    MediaPlayer.Play(CityTheme);
                    Gold -= 1000000;
                }
            }
            else if (level == "forge")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 18 * ScreenMultiply && currentMouseState.Position.X < 86 * ScreenMultiply && currentMouseState.Position.Y > 8 * ScreenMultiply && currentMouseState.Position.Y < 40 * ScreenMultiply)
                {
                    ChangeReady = true;
                }
                else
                if (ChangeReady == true && Wait == 15)
                {
                    level = "city";
                    MediaPlayer.Play(CityTheme);
                    ChangeReady = false;
                    Wait = 0;
                }
                else if (ChangeReady == true) Wait++;
                else if (Gold >= 10 && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 158 * ScreenMultiply && currentMouseState.Position.X < 272 * ScreenMultiply && currentMouseState.Position.Y > 120 * ScreenMultiply && currentMouseState.Position.Y < 161 * ScreenMultiply)
                {
                    UpgradeChangeReady = true;
                }
                else
                if (UpgradeChangeReady == true && Wait == 3)
                {
                    HeroDamage++;
                    UpgradeChangeReady = false;
                    Wait = 0;
                    Gold -= 10;
                }
                else if (UpgradeChangeReady == true) Wait++;
            }
            else if (level == "map")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 140 * ScreenMultiply && currentMouseState.Position.X < 155 * ScreenMultiply && currentMouseState.Position.Y > 50 * ScreenMultiply && currentMouseState.Position.Y < 65 * ScreenMultiply)
                {
                    DifficultyMultiply = HeroDamage * BaseHealth / 30 + 1;
                    GoldMultiply = HeroDamage * BaseHealth / 10 + 1;

                    level = "dungeon";

                    DogHealth *= (int)DifficultyMultiply;
                    GhostHealth *= (int)DifficultyMultiply;
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 1 * ScreenMultiply && currentMouseState.Position.X < 75 * ScreenMultiply && currentMouseState.Position.Y > 1 * ScreenMultiply && currentMouseState.Position.Y < 34 * ScreenMultiply)
                {
                    level = "city";
                    MediaPlayer.Play(CityTheme);
                }
            }
            else
            if (level == "dungeonTutorialLevel")
            {
                if (dungeonTutorialActive)
                {
                    if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 248 * ScreenMultiply && currentMouseState.Position.X < 318 * ScreenMultiply && currentMouseState.Position.Y > 211 * ScreenMultiply && currentMouseState.Position.Y < 238 * ScreenMultiply)
                    {
                        if (DungeonTutorialPart != 3 && TutorialWait > 60)
                        {
                            DungeonTutorialPart++;
                            TutorialWait = 0;
                        }
                        else if (TutorialWait > 60 && DungeonTutorialPart == 3)
                        {
                            dungeonTutorialActive = false;
                            SaveManager.Saver(new string[] { "test", BaseHealth.ToString(), HeroDamage.ToString(), Gold.ToString(), cityTutorialActive.ToString(), dungeonTutorialActive.ToString() });
                            isDungeonReadyAfterTutorial = true;
                            TutorialWait = 0;
                        }
                    }
                    if (isDungeonReadyAfterTutorial && TutorialWait > 40)
                    {
                        TutorialWait = 0;
                        level = "dungeon";
                    }
                    TutorialWait++;
                }
                else
                    level = "dungeon";
            }
            if (level == "dungeon")
            {
                //Движение заднего фона
                if (!LastEnemy && WaitAfterKill < 120)
                {
                    if (WaitAfterKill > 60)
                    {
                        for (int x = 0; x < BlowParticlePositionX.Length; x++)
                        {
                            BlowParticlePositionX[x] = Randomize.Rnd(220 * ScreenMultiply, 284 * ScreenMultiply);
                        }

                        for (int y = 0; y < BlowParticlePositionY.Length; y++)
                        {
                            BlowParticlePositionY[y] = Randomize.Rnd(75 * ScreenMultiply, 110 * ScreenMultiply);
                        }

                    }

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
                    if (Wait == 120 || !isLevelInit)
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
                    TimerXPos++;
                    if (MathTasks[5] == "4" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply)  //НП
                    {
                        isAnswered = false;
                        EnemyHealth -= HeroDamage;
                        isAnswerCorrect = true;
                        TimerXPos = 0;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5 * (int)GoldMultiply;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10 * (int)GoldMultiply;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();
                                SaveFileO[4] = cityTutorialActive.ToString();
                                SaveFileO[5] = dungeonTutorialActive.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                MediaPlayer.Play(CityTheme);
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
                        TimerXPos = 0;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5 * (int)GoldMultiply;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10 * (int)GoldMultiply;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();
                                SaveFileO[4] = cityTutorialActive.ToString();
                                SaveFileO[5] = dungeonTutorialActive.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                MediaPlayer.Play(CityTheme);
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
                        TimerXPos = 0;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5 * (int)GoldMultiply;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10 * (int)GoldMultiply;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();
                                SaveFileO[4] = cityTutorialActive.ToString();
                                SaveFileO[5] = dungeonTutorialActive.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                MediaPlayer.Play(CityTheme);
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
                        TimerXPos = 0;
                        if (EnemyHealth <= 0)
                        {
                            if (EnemyType == 1)
                            {
                                Gold += 5 * (int)GoldMultiply;
                            }
                            else if (EnemyType == 2)
                            {
                                Gold += 10 * (int)GoldMultiply;
                            }
                            KilledEnemies++;
                            if (KilledEnemies == HowMuchToKill)
                            {
                                SaveFileO[1] = BaseHealth.ToString();
                                SaveFileO[2] = HeroDamage.ToString();
                                SaveFileO[3] = Gold.ToString();
                                SaveFileO[4] = cityTutorialActive.ToString();
                                SaveFileO[5] = dungeonTutorialActive.ToString();

                                isLevelInit = false;
                                isLevelStart = false;
                                Health = BaseHealth;

                                KilledEnemies = 0;
                                level = "city";
                                MediaPlayer.Play(CityTheme);
                                SaveManager.Saver(SaveFileO);
                            }
                            isEnemyAlive = false;
                        }
                    }
                    else

                    if ((currentMouseState.LeftButton == ButtonState.Pressed && ((currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 192 * ScreenMultiply && currentMouseState.Position.Y < 208 * ScreenMultiply) || (currentMouseState.Position.X > 200 * ScreenMultiply && currentMouseState.Position.X < 310 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply) || (currentMouseState.Position.X > 20 * ScreenMultiply && currentMouseState.Position.X < 130 * ScreenMultiply && currentMouseState.Position.Y > 215 * ScreenMultiply && currentMouseState.Position.Y < 235 * ScreenMultiply) || (currentMouseState.Position.X > 20 * ScreenMultiply && currentMouseState.Position.X < 130 * ScreenMultiply && currentMouseState.Position.Y > 192 * ScreenMultiply && currentMouseState.Position.Y < 208 * ScreenMultiply))) || TimerXPos >= 320)
                    {
                        isAnswered = false;
                        Health -= EnemyDamage;
                        isAnswerCorrect = false;
                        TimerXPos = 0;
                        if (Health <= 0)
                        {
                            level = "city";
                            MediaPlayer.Play(CityTheme);
                            Gold = 0;
                            HeroDamage = 1;

                            SaveFileO[1] = BaseHealth.ToString();
                            SaveFileO[2] = HeroDamage.ToString();
                            SaveFileO[3] = Gold.ToString();
                            SaveFileO[4] = cityTutorialActive.ToString();
                            SaveFileO[5] = dungeonTutorialActive.ToString();

                            isLevelInit = false;
                            isLevelStart = false;
                            Health = BaseHealth;

                            KilledEnemies = 0;

                            SaveManager.Saver(SaveFileO);
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
            if (level == "menu")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(MainMenu, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));

                spriteBatch.End();
            }
            if (level == "dungeonTutorialLevel")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(DungeonTutorialArr[DungeonTutorialPart], destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 240 * ScreenMultiply));

                spriteBatch.End();
            }
            if (level == "city")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                if (cityTutorialActive && CityTutorialPart == 1)
                {
                    spriteBatch.Draw(CityTutorial, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 240 * ScreenMultiply));
                }
                else if(cityTutorialActive && CityTutorialPart == 2)
                {
                    spriteBatch.Draw(CityTutorial2, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 240 * ScreenMultiply));
                }
                else
                {
                    spriteBatch.Draw(City, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));
                    spriteBatch.DrawString(PixelCry, "Золото: " + Gold.ToString(), new Vector2(220 * ScreenMultiply, 0), Color.Yellow);
                }
                spriteBatch.End();
            }
            else
            if (level == "shop")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(ShopBackground, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));

                spriteBatch.End();
            }
            else
            if (level == "map")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Map, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 150 * ScreenMultiply));

                spriteBatch.End();
            }
            else
            if (level == "forge")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Forge, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 240 * ScreenMultiply));
                spriteBatch.DrawString(PixelCry, HeroDamage.ToString(), new Vector2(49*ScreenMultiply, 151*ScreenMultiply), Color.White);

                spriteBatch.End();
            }
            else
            if (level == "hospital")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(HospitalRoom, destinationRectangle: new Rectangle(0, 0, 320 * ScreenMultiply, 240 * ScreenMultiply));
                spriteBatch.DrawString(PixelCry, BaseHealth.ToString(), new Vector2 (107*ScreenMultiply, 152*ScreenMultiply), Color.Red);

                spriteBatch.End();
            }
            else
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
                }
                else
                    spriteBatch.Draw(Hero2, destinationRectangle: new Rectangle(20 * ScreenMultiply, 70 * ScreenMultiply, 64 * ScreenMultiply, 64 * ScreenMultiply));

                if (EnemyType == 1 && isEnemyAlive)
                {
                    spriteBatch.Draw(Sobaka, destinationRectangle: new Rectangle(220 * ScreenMultiply, 75 * ScreenMultiply, 100 * (ScreenMultiply - 1), 100 * (ScreenMultiply - 1)));
                }
                else if (EnemyType == 2 && isEnemyAlive)
                {
                    spriteBatch.Draw(Ghost, destinationRectangle: new Rectangle(220 * ScreenMultiply, 70 * ScreenMultiply, 32 * (ScreenMultiply + 2), 32 * (ScreenMultiply + 2)));
                }

                if (!isAnswered && isLevelStart && isAnswerCorrect)
                {
                    if (FireBallXPos <= 250)
                    {
                        spriteBatch.Draw(FireBall, destinationRectangle: new Rectangle(FireBallXPos * ScreenMultiply, 100 * ScreenMultiply, 12 * ScreenMultiply, 12 * ScreenMultiply));
                        FireBallXPos += 2 * ScreenMultiply;
                        BlowParticleSize = FireBallXPos;
                    }
                    else if(BlowParticleSize <= 150*ScreenMultiply && isEnemyAlive)
                    {
                        spriteBatch.Draw(BlowParticle, destinationRectangle: new Rectangle((FireBallXPos * ScreenMultiply) - 20 * ScreenMultiply, (100 * ScreenMultiply) - 20 * ScreenMultiply, BlowParticleSize/20 * ScreenMultiply, BlowParticleSize/20 * ScreenMultiply));
                        BlowParticleSize += 3 * ScreenMultiply;
                    }
                }
                else
                {
                    FireBallXPos = 10 * ScreenMultiply;
                }


                //Отрисовка заданий и таймера
                if (isAnswered == true)
                {
                    spriteBatch.Draw(WhileBackground, destinationRectangle: new Rectangle(0 - TimerXPos * ScreenMultiply, 150 * ScreenMultiply, 320 * ScreenMultiply, 5 * ScreenMultiply), color: Color.Red);

                    spriteBatch.DrawString(PixelCry, MathTasks[0], taskPos0, Color.White);
                    spriteBatch.DrawString(PixelCry, MathTasks[1], taskPos1, Color.White); //ВЛ 1
                    spriteBatch.DrawString(PixelCry, MathTasks[2], taskPos2, Color.White); //НЛ 2
                    spriteBatch.DrawString(PixelCry, MathTasks[3], taskPos3, Color.White); //ВП 3
                    spriteBatch.DrawString(PixelCry, MathTasks[4], taskPos4, Color.White); //НП 4
                }
                //Отрисовка хп
                spriteBatch.DrawString(PixelCry, EnemyHealth.ToString(), EnemyHealthPos, Color.Red);
                spriteBatch.DrawString(PixelCry, Health.ToString(), HealthPos, Color.Red);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
