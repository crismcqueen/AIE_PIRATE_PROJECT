using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Utilities;
using System;

namespace AIE_PIRATE_PROJECT
{
    
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        public static int tile =64;
        public static float meter = tile;
        public static Vector2 maxVelocity = new Vector2(meter * 20f, meter * 20f);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playerSprite;
        Texture2D enemySprite;
        Texture2D enemyBossSprite; 
        Texture2D health;
        Texture2D splashScreen;
        Texture2D cannonballSprite;
        TiledMapRenderer mapRenderer;
        TiledMap seaMap;
        Camera2D cam;
        SpriteFont gameText;
        int score = 0;
        int lives = 3;
        //private int ScreenY;
        //private int screenX;



        Player player = new Player();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Screen res
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferMultiSampling = true;
            

        }
        public int ScreenY
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Width;
            }
        }

        public int ScreenX
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Height;
            }
        }


        protected override void Initialize()
        {
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            cam = new Camera2D(GraphicsDevice);
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Sprite content
            playerSprite = Content.Load<Texture2D>("Player/PSH");
            enemySprite = Content.Load<Texture2D>("Enemy/enemyShipAlive");
            enemyBossSprite = Content.Load<Texture2D>("Enemy/enemyShipDead");
            cannonballSprite = Content.Load<Texture2D>("Misc/cannonBall");
            health = Content.Load<Texture2D>("Misc/SkullHealth");
            //splashScreen = Content.Load<Texture2D>("Screens/Splash");
            gameText = Content.Load<SpriteFont>("Fonts/Primitive");
            seaMap = Content.Load<TiledMap>("Misc/pirateSeaMap");
            health = Content.Load<Texture2D>("GUI/SkullHealth");

            Enemy.enemies.Add(new enemyShip(new Vector2(100, 400)));
            Enemy.enemies.Add(new enemyBoss(new Vector2(300, 450)));
        }






        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            player.Update(gameTime);
            foreach (Enemy e in Enemy.enemies)
            {
                e.Update(gameTime, player.Position);
            }
            cam.LookAt(player.Position);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            GraphicsDevice.BlendState = BlendState.NonPremultiplied;

            mapRenderer.Draw(seaMap, cam.GetViewMatrix());

            // world space here
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            player.playerOffset = new Vector2(playerSprite.Width / 2, playerSprite.Height / 2);
            //spriteBatch.Draw(splashScreen, new Vector2(0, 0), Color.Silver);

            spriteBatch.DrawString(gameText, "test press escape to exit", new Vector2(100, 100), Color.DarkRed, 0, new Vector2(0, 0), 1 * 3, SpriteEffects.None, 0);

            //spriteBatch.Draw(playerSprite, player.Position, Color.White);
            spriteBatch.Draw(playerSprite, player.Position, null, Color.White, player.playerRotation, player.playerOffset, 0.5f, SpriteEffects.None, 0);
            
            foreach (Enemy e in Enemy.enemies)
            {
                Texture2D enemyDraw;
                if (e.GetType() == typeof(enemyShip))
                {
                    enemyDraw = enemySprite;
                }
                else
                {
                    enemyDraw = enemyBossSprite;
                }
                spriteBatch.Draw(enemyDraw, e.Position, Color.White);

            }
            foreach (Projectile can in player.projectiles)
            {
                spriteBatch.Draw(cannonballSprite, can.Position, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            } 
            /*
            if (cannonProjectile.cannonAlive == false)
            {
                //spriteBatch.Draw(cannonballSprite, cannonProjectile.cannonPosition, null, Color.White, 0, cannonProjectile.cannonOffset, 0.5f, SpriteEffects.None, 0);
            }
            */
            spriteBatch.End();
            spriteBatch.Begin();
            
            spriteBatch.DrawString(gameText, "SCORE " + score, new Vector2(30, 30), Color.Black, 0, new Vector2(0, 0), 1 * 1.5f, SpriteEffects.None, 0);
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(health, new Vector2(1280 - 80 - i * 64, 16), Color.White);
            }
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            base.Draw(gameTime);
        }
    }
    //old reference code below
    /*
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public int ScreenWidth
    {
        get
        {
            return graphics.GraphicsDevice.Viewport.Width;
        }
    }

    public int ScreenHeight
    {
        get
        {
            return graphics.GraphicsDevice.Viewport.Height;
        }
    }

    //enemy data

    private Texture2D enemyShip;
    private Texture2D shipWreck;
    private const int numberOfEnemy = 4;
    private Vector2[] enemyPosition = new Vector2[numberOfEnemy];
    private Vector2[] enemyOffset = new Vector2[numberOfEnemy];
    private float enemySpeed = 50.0f;
    private float[] enemyRotation = new float[numberOfEnemy];
    private float enemyRadius = 20;
    private bool[] enemyAlive = new bool[numberOfEnemy];

    // bullet data

    private Texture2D bulletTexture;
    private Vector2 bulletPosition = new Vector2(0, 0);
    private Vector2 bulletVelocity = new Vector2(0, 0);
    private Vector2 bulletOffset = new Vector2(0, 0);
    private float bulletSpeed = 200;
    private float bulletRadius = 5;
    private bool bulletAlive = true;

    // static data
    private Texture2D beachBG = null;
    private Texture2D loseGameBG;
    private Texture2D winPirateBG;
    private Texture2D oceanTile;
    private SpriteFont text;
    private int score = 0;
    private int screenHeight;
    private int screenWidth;

    // game states

    private const int STATE_SPLASH = 0;
    private const int STATE_GAME = 1;
    private const int STATE_GAMEOVERDIE = 2;
    private const int STATE_GAMEOVERWIN = 3;
    private int gameState = STATE_SPLASH;

    // fps function

    private int currentFPS;
    private int FPSCounter;
    private float fpsTimer;
    //private int state;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        this.IsFixedTimeStep = false;
        this.graphics.SynchronizeWithVerticalRetrace = false;
    }


    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    /// 
    protected override void Initialize()

    {
        ResetGame();
        base.Initialize();
    }

    public void ResetGame()
    {  // TODO: Add your initialization logic here
        screenHeight = graphics.GraphicsDevice.Viewport.Height;
        screenWidth = graphics.GraphicsDevice.Viewport.Width;

        //for (int i = 0; i < numberOfEnemy; i++)

        enemyPosition[0] = new Vector2(80, screenHeight / 2.0F);
        enemyRotation[0] = 4.5f;
        enemyAlive[0] = true;

        enemyPosition[1] = new Vector2(screenWidth - 80, screenHeight / 2.0F);
        enemyRotation[1] = 1.0f;
        enemyAlive[1] = true;

        enemyPosition[2] = new Vector2(screenWidth / 2.0F, 80);
        enemyRotation[2] = 0.2f;
        enemyAlive[2] = true;

        enemyPosition[3] = new Vector2(screenWidth / 2.0F, screenHeight - 80);
        enemyRotation[3] = 3.3f;
        enemyAlive[3] = true;
        bulletAlive = true;
        score = 0;
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
        /*
        oceanTile = Content.Load<Texture2D>("oceanTile");
        beachBG = Content.Load<Texture2D>("beachBG");
        winPirateBG = Content.Load<Texture2D>("winPirate");
        loseGameBG = Content.Load<Texture2D>("loseGame");
        enemyShip = Content.Load<Texture2D>("enemyShip");
        text = Content.Load<SpriteFont>("HYPERSPACE");
        bulletTexture = Content.Load<Texture2D>("cannonBall");
        shipWreck = Content.Load<Texture2D>("shipWreck");


        bulletOffset = new Vector2(bulletTexture.Width / 2, bulletTexture.Height / 2);
        for (int i = 0; i < numberOfEnemy; i++)
        {
            enemyOffset[i] = new Vector2(enemyShip.Width / 2, enemyShip.Height / 2);
        }
    }


    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    private void ShootBullet(Vector2 position, float rotation)
    {
        if (bulletAlive == false)
            return;

        Vector2 direction = new Vector2((float)-Math.Sin(rotation), (float)Math.Cos(rotation));
        direction.Normalize();
        bulletVelocity = direction * bulletSpeed;
        bulletPosition = position;
        bulletAlive = false;
    }

    private void UpdateBullet(float deltaTime)
    {
        bulletPosition += bulletVelocity * deltaTime;

        if (bulletPosition.X < 0 || bulletPosition.X > screenWidth || bulletPosition.Y < 0 || bulletPosition.Y > screenHeight)
        {
            bulletAlive = true;
        }
    }
    protected void UpdateEnemyCollisions()
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            // collision between enemy and dead enemy

            //if (enemyAlive[i] == false)
            //  continue;

            for (int j = 1; j < numberOfEnemy; j++)
            {

                if (enemyAlive[j] == false)
                    continue;

                if (i == j)
                    continue;

                if (IsColliding(enemyPosition[i], enemyRadius, enemyPosition[j], enemyRadius) == true)
                {
                    if (enemyAlive[i] == true)
                        enemyRotation[i] += 3.14159f;

                    if (enemyAlive[j] == true)
                        enemyRotation[j] += 3.14159f;
                    return;
                }
            }
        }
    }





    protected override void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || state.IsKeyDown(Keys.Escape)) Exit();



        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        fpsTimer += deltaTime;
        FPSCounter++;
        if (fpsTimer >= 1.0f)

        {
            currentFPS = FPSCounter;
            FPSCounter = 0;
            fpsTimer -= 1.0f;
        }

        // switch game states data
        switch (gameState)
        {
            case STATE_SPLASH:
                UpdateSplashState(deltaTime);
                break;
            case STATE_GAME:
                UpdateGameState(deltaTime);
                break;
            case STATE_GAMEOVERDIE:
                UpdateGameOverState(deltaTime);
                break;
            case STATE_GAMEOVERWIN:
                UpdateGameOverState(deltaTime);
                break;

        }


        base.Update(gameTime);
    }


    private void UpdatePlayer(float deltaTime)
    {

        if (playerAlive == false)
            return;
        KeyboardState state = Keyboard.GetState();

        // shoot a bullet 
        if (state.IsKeyDown(Keys.Space) == true)
        {
            ShootBullet(playerPosition, playerRotation);
        }

    }


    protected void UpdateEnemies(float deltaTime)
    {
        // call UpdateEnemy for every enemy in our array
        for (int i = 0; i < numberOfEnemy; i++)
        {
            if (enemyAlive[i] == true)
            {
                Vector2 velocity = new Vector2
                    ((float)(-enemySpeed * Math.Sin(enemyRotation[i])), (float)(enemySpeed * Math.Cos(enemyRotation[i])));

                if (enemyPosition[i].X < 0)
                {
                    enemyPosition[i].X = 0;
                    velocity.X = -velocity.X;
                    enemyRotation[i] = (float)Math.Atan2(velocity.Y, velocity.X) - 1.5708f;
                }
                if (enemyPosition[i].Y < 0)
                {
                    enemyPosition[i].Y = 0;
                    velocity.Y = -velocity.Y;
                    enemyRotation[i] = (float)Math.Atan2(velocity.Y, velocity.X) - 1.5708f;
                }
                if (enemyPosition[i].X > screenWidth)
                {
                    enemyPosition[i].X = screenWidth;
                    velocity.X = -velocity.X;
                    enemyRotation[i] = (float)Math.Atan2(velocity.Y, velocity.X) - 1.5708f;
                }
                if (enemyPosition[i].Y > screenHeight)
                {
                    enemyPosition[i].Y = screenHeight;
                    velocity.Y = -velocity.Y;
                    enemyRotation[i] = (float)Math.Atan2(velocity.Y, velocity.X) - 1.5708f;
                }
                enemyPosition[i] += velocity * deltaTime;
            }
        }
    }


    //circle collision detection
    private bool IsColliding(Vector2 position1, float radius1, Vector2 position2, float radius2)
    {
        Vector2 direction = position1 - position2;
        float length = direction.Length();

        if (direction.Length() < radius1 + radius2)
        {
            return true;
        }
        return false;
    }
    protected void UpdateSplashState(float deltaTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
        {
            gameState = STATE_GAME;

        }
    }
    private void DrawSplashState(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(beachBG, new Vector2(0, 0), Color.White);
        spriteBatch.DrawString(text, "press enter to start", new Vector2(230, 150), Color.Black);
    }
    private void UpdateGameState(float deltaTime)
    {

        UpdatePlayer(deltaTime);
        UpdateEnemies(deltaTime);
        UpdateEnemyCollisions();

        for (int i = 0; i < numberOfEnemy; i++)
        {
            //stops dead enemy collision
            //if (enemyAlive[i] == false)
            //continue;

            if (IsColliding(enemyPosition[i], enemyRadius, playerPosition, playerRadius) == true)
            {
                playerAlive = false;
                gameState = STATE_GAMEOVERDIE;
                break;
            }
        }

        if (bulletAlive == false)
        {
            UpdateBullet(deltaTime);

            for (int i = 0; i < numberOfEnemy; i++)
            {
                if (enemyAlive[i] == true)
                {
                    bool isColliding = IsColliding(bulletPosition, bulletRadius, enemyPosition[i], enemyRadius);

                    if (isColliding == true)
                    {
                        bulletAlive = true;
                        enemyAlive[i] = false;
                        score++;
                        break;
                    }
                }
            }
        }
        // check enemy state
        int aliveCount = 0;
        foreach (bool lifeState in enemyAlive)
        {
            if (lifeState == true)
                aliveCount++;
        }
        if (aliveCount == 0)
        {
            gameState = STATE_GAMEOVERWIN;
        }
    }

    private void DrawGameState(SpriteBatch spriteBatch)
    {
        int tileWidth = (graphics.GraphicsDevice.Viewport.Width / oceanTile.Width) + 1;
        int tileHeight = (graphics.GraphicsDevice.Viewport.Height / oceanTile.Height) + 1;

        for (int column = 0; column < tileWidth; column += 1)
        {
            for (int row = 0; row < tileHeight; row += 1)
            {
                Vector2 position = new Vector2(column * oceanTile.Width, row * oceanTile.Height);
                spriteBatch.Draw(oceanTile, position, Color.White);
            }
        }

        if (bulletAlive == false)
        {
            spriteBatch.Draw(bulletTexture, bulletPosition, null, Color.White, 0, bulletOffset, 0.5f, SpriteEffects.None, 0);
        }

        for (int i = 0; i < numberOfEnemy; i++)
        {
            if (enemyAlive[i] == true)
            {

                spriteBatch.Draw(enemyShip, enemyPosition[i], null, Color.White, enemyRotation[i], enemyOffset[i], 0.5f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(shipWreck, enemyPosition[i], null, Color.White, enemyRotation[i], enemyOffset[i], 0.5f, SpriteEffects.None, 0);
            }
        }

        //draw fonts
        spriteBatch.DrawString(text, "SCORE " + score, new Vector2(30, 0), Color.Black, 0, new Vector2(0, 0), 1 * 1.5f, SpriteEffects.None, 0);
        spriteBatch.DrawString(text, "FPS " + currentFPS, new Vector2(570, 0), Color.Black, 0, new Vector2(0, 0), 1 * 1.5f, SpriteEffects.None, 0);


    }
    private void UpdateGameOverState(float deltaTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
        {
            gameState = STATE_SPLASH;
            ResetGame();
        }
    }
    private void DrawGameOverStateDie(SpriteBatch spriteBatch)

    {
        spriteBatch.Draw(loseGameBG, new Vector2(0, 0), Color.White);
        spriteBatch.DrawString(text, "           GAME OVER\n     Press Enter to Restart \n      or press ESC to exit", new Vector2(150, 10), Color.Black);
    }
    private void DrawGameOverStateWin(SpriteBatch spriteBatch)

    {
        spriteBatch.Draw(winPirateBG, new Vector2(0, 0), Color.White);
        spriteBatch.DrawString(text, "             YOU WIN\n     Press Enter to Restart \n      or press ESC to exit", new Vector2(150, 10), Color.Black);
    }
    protected override void Draw(GameTime gameTime)

    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        spriteBatch.Begin();
        switch (gameState)
        {
            case STATE_SPLASH:
                DrawSplashState(spriteBatch);
                break;
            case STATE_GAME:
                DrawGameState(spriteBatch);
                break;
            case STATE_GAMEOVERDIE:
                DrawGameOverStateDie(spriteBatch);
                break;
            case STATE_GAMEOVERWIN:
                DrawGameOverStateWin(spriteBatch);
                break;
        }



        spriteBatch.End();

        base.Draw(gameTime);
    }
 */
}

