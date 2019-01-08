using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace MatheMage_REDUX
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Dungeon1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D DungeonBackground;
        Texture2D Hero1;
        Texture2D Hero2;
        Texture2D Sobaka;
        Texture2D ChooseMenu;
        Texture2D City;
        Texture2D FireBall;
        Texture2D Map;
        SpriteFont PixelCry;

        Vector2 taskPos0 = new Vector2(135 * 3, 165 * 3);
        Vector2 taskPos1 = new Vector2(60 * 3, 197 * 3);
        Vector2 taskPos2 = new Vector2(60 * 3, 222 * 3);
        Vector2 taskPos3 = new Vector2(245 * 3, 197 * 3);
        Vector2 taskPos4 = new Vector2(245 * 3, 222 * 3);

        Vector2 EnemyHealthPos = new Vector2(300 * 3, 0);
        Vector2 HealthPos = new Vector2(30, 0);

        string[] MathTasks = new string[6];

        int Health = 3;
        int EnemyHealth = 10;

        int Wait = 0;
        int FrameCount = 1;

        int FireBallXPos =20 * 3;

        string level = "city";
        bool isAnswered = false;

        bool isLevelStart = false;
        bool isAnswerCorrect = true;

        public Dungeon1()
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

            // TODO: use this.Content to load your game content here
            DungeonBackground = this.Content.Load<Texture2D>("BackGround");
            ChooseMenu = this.Content.Load<Texture2D>("ChooseMenu");
            Hero1 = this.Content.Load<Texture2D>("Hero1");
            Hero2 = this.Content.Load<Texture2D>("Hero2");
            Sobaka = this.Content.Load<Texture2D>("Sobaka");
            PixelCry = this.Content.Load<SpriteFont>("PixelCry");
            City = this.Content.Load<Texture2D>("city");
            FireBall = this.Content.Load<Texture2D>("fireball");
            Map = this.Content.Load<Texture2D>("map");
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
                }
            }
            else if(level == "map")
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 140 * 3 && currentMouseState.Position.X < 155 * 3 && currentMouseState.Position.Y > 50 * 3 && currentMouseState.Position.Y < 65 * 3)
                {
                    level = "dungeon";
                }
            }
            else
            if (level == "dungeon")
            {
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
                if (isAnswered)
                {
                    if (MathTasks[5] == "3" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * 3 && currentMouseState.Position.X < 310 * 3 && currentMouseState.Position.Y > 192 * 3 && currentMouseState.Position.Y < 208 * 3)  //ВП
                    {
                        isAnswered = false;
                        EnemyHealth--;
                        isAnswerCorrect = true;
                        if (EnemyHealth == 0)
                        {
                            Exit();
                        }
                    }
                    else

                    if (MathTasks[5] == "4" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 200 * 3 && currentMouseState.Position.X < 310 * 3 && currentMouseState.Position.Y > 215 * 3 && currentMouseState.Position.Y < 235 * 3)  //НП
                    {
                        isAnswered = false;
                        EnemyHealth--;
                        isAnswerCorrect = true;
                        if (EnemyHealth == 0)
                        {
                            Exit();
                        }
                    }
                    else

                    if (MathTasks[5] == "2" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 20 * 3 && currentMouseState.Position.X < 130 * 3 && currentMouseState.Position.Y > 215 * 3 && currentMouseState.Position.Y < 235 * 3)  //НЛ
                    {
                        isAnswered = false;
                        EnemyHealth--;
                        isAnswerCorrect = true;
                        if (EnemyHealth == 0)
                        {
                            Exit();
                        }
                    }
                    else

                    if (MathTasks[5] == "1" && currentMouseState.LeftButton == ButtonState.Pressed && currentMouseState.Position.X > 20 * 3 && currentMouseState.Position.X < 130 * 3 && currentMouseState.Position.Y > 192 * 3 && currentMouseState.Position.Y < 208 * 3)  //ВЛ
                    {
                        isAnswered = false;
                        EnemyHealth--;
                        isAnswerCorrect = true;
                        if (EnemyHealth == 0)
                        {
                            Exit();
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

                spriteBatch.End();
            }
            else
            if (level == "map")
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(Map, destinationRectangle: new Rectangle(0, 0, 320 * 3, 150 * 3));

                spriteBatch.End();
            }else
            if (level == "dungeon")
            {
                // TODO: Add your drawing code here

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                spriteBatch.Draw(DungeonBackground, destinationRectangle: new Rectangle(0, 0, 320 * 3, 150 * 3));
                spriteBatch.Draw(ChooseMenu, destinationRectangle: new Rectangle(0, 150 * 3, 320 * 3, 90 * 3));
                if (FrameCount <= 30)
                {
                    spriteBatch.Draw(Hero1, destinationRectangle: new Rectangle(20 * 3, 70 * 3, 64 * 3, 64 * 3));
                }else
                    spriteBatch.Draw(Hero2, destinationRectangle: new Rectangle(20 * 3, 70 * 3, 64 * 3, 64 * 3));

                spriteBatch.Draw(Sobaka, destinationRectangle: new Rectangle(220 * 3, 70 * 3, 80 * 2, 80 * 2));
                //spriteBatch.DrawString(PixelCry, screenWidth.ToString(), Vector2.Zero, Color.Red);

                if (!isAnswered && isLevelStart && isAnswerCorrect)
                {
                    spriteBatch.Draw(FireBall, destinationRectangle: new Rectangle(FireBallXPos * 3, 100 * 3, 12 * 3, 12 * 3));
                    FireBallXPos += 7;
                }
                else
                    FireBallXPos = 10 * 3;


                if (isAnswered == true)
                {
                    spriteBatch.DrawString(PixelCry, MathTasks[0], taskPos0, Color.White);
                    spriteBatch.DrawString(PixelCry, MathTasks[1], taskPos1, Color.White); //ВЛ 1
                    spriteBatch.DrawString(PixelCry, MathTasks[2], taskPos2, Color.White); //НЛ 2
                    spriteBatch.DrawString(PixelCry, MathTasks[3], taskPos3, Color.White); //ВП 3
                    spriteBatch.DrawString(PixelCry, MathTasks[4], taskPos4, Color.White); //НП 4
                                                                                           //=======
                }

                spriteBatch.DrawString(PixelCry, EnemyHealth.ToString(), EnemyHealthPos, Color.Red);
                spriteBatch.DrawString(PixelCry, Health.ToString(), HealthPos, Color.Red);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
