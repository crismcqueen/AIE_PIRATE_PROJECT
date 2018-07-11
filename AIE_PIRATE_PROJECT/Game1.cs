using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AIE_PIRATE_PROJECT
{
    enum UI
    {
        STATE_TITLE,
        STATE_GAME,
        STATE_END
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Song gameMusic;

        SoundEffect cannon;
        SoundEffectInstance cannonInstance;
        
        public static int tile = 64;
        public static float meter = tile;
        public static Vector2 maxVelocity = new Vector2(meter * 20f, meter * 20f);
        private UI STATE = UI.STATE_TITLE;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D goal;
        Texture2D playerSprite;
        Texture2D enemyShip;
        Texture2D enemyBossSprite;
        Texture2D health;
        Texture2D splashScreen;
        Texture2D endScreen;
        Texture2D cannonballSprite;
        Texture2D tiles;
        TiledMapRenderer mapRenderer;
        TiledMap seaMap;
        Camera2D cam;
        SpriteFont gameText;
        public ArrayList allCollisionTiles = new ArrayList();
        Random rnd = new Random();
        //public Sprite[,] levelGrid;
        public int tileHeight = 0;
        public int levelTileWidth = 0;
        public int levelTileHeight = 0;
        public int score = 0;
        public int lives = 3;
        //private int ScreenY;
        //private int screenX;

        public List<Enemy> enemies = new List<Enemy>();

        public Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Screen res
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;




            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = true;

        }
        public int ScreenX
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Width;
            }
        }

        public int ScreenY
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
            splashScreen = Content.Load<Texture2D>("GUI/beachBG");
            endScreen = Content.Load<Texture2D>("GUI/loseGame");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Sprite content
            playerSprite = Content.Load<Texture2D>("Player/PSH");
            enemyShip = Content.Load<Texture2D>("Enemy/enemyShipAlive");
            enemyBossSprite = Content.Load<Texture2D>("Enemy/enemyShipDead");
            cannonballSprite = Content.Load<Texture2D>("Misc/cannonBall");
            health = Content.Load<Texture2D>("Misc/SkullHealth");
            //splashScreen = Content.Load<Texture2D>("Screens/Splash");
            gameText = Content.Load<SpriteFont>("Fonts/Primitive");
            seaMap = Content.Load<TiledMap>("Misc/pirateSeaMap");
            health = Content.Load<Texture2D>("GUI/SkullHealth");
            goal = Content.Load<Texture2D>("Misc/chest");
            tiles = Content.Load<Texture2D>("Misc/tiles_sheet");

            cannon = Content.Load<SoundEffect>("cannon");
            cannonInstance = cannon.CreateInstance();

            gameMusic = Content.Load<Song>("Thunderchild_theme");
            MediaPlayer.Play(gameMusic);

            player = new Player(this);
        }

        private void Reset()
        {
            player.PlayerHealth = 3;
            player.playerRotation = 0;
            player.PlayerPosition = new Vector2(seaMap.WidthInPixels, seaMap.HeightInPixels) / 2;

            enemies.Clear();

            for (int i = 0; i < 10; i++)
            {
                AddEnemy();
            }
        }



        private void TitleUpdate()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Reset();
                STATE = UI.STATE_GAME;
            }
        }

        private void GameUpdate(GameTime gameTime)
        {
            player.Update(gameTime, cannonInstance);
            
            List<Enemy> deaths = new List<Enemy>();

            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, player);
                if (e.health <= 0)
                {
                    deaths.Add(e);
                }
            }
            foreach (Enemy e in deaths)
            {
                enemies.Remove(e);
            }
            deaths.Clear();

            if (enemies.Count < 15)
            {
                AddEnemy();
            }

            if (player.PlayerHealth <= 0)
            {
                STATE = UI.STATE_END;
            }

            cam.LookAt(player.PlayerPosition);
        }

        private void AddEnemy()
        {
            int outerLowerX = 64;
            int innerLowerX = Math.Max(outerLowerX, (int)Math.Ceiling(player.PlayerPosition.X) - (ScreenX / 2)) + 70;
            int outerUpperX = 1536;
            int innerUpperX = Math.Min(outerUpperX, (int)Math.Ceiling(player.PlayerPosition.X) + (ScreenX / 2)) + 70;

            int outerLowerY = 64;
            int innerLowerY = Math.Max(outerLowerY, (int)Math.Ceiling(player.PlayerPosition.Y) - (ScreenY / 2)) - 70;
            int outerUpperY = 1536;
            int innerUpperY = Math.Min(outerUpperY, (int)Math.Ceiling(player.PlayerPosition.Y) + (ScreenY / 2)) + 70;

            int maxXVal = (outerUpperY - outerLowerY) - (innerUpperY - innerLowerY);
            int maxYVal = (outerUpperY - outerLowerY) - (innerUpperY - innerLowerY);

            int Xpos = rnd.Next(maxXVal) + outerLowerX;
            int Ypos = rnd.Next(maxYVal) + outerLowerY;

            if (Xpos > innerLowerX)
            {
                Xpos += innerUpperX - innerLowerX;
            }

            if (Ypos > innerLowerY)
            {
                Ypos += innerUpperY - innerLowerY;
            }
            if (rnd.Next(0, 5) == 0)
            {
                enemies.Add(new Enemy(new Vector2(Xpos, Ypos), enemyBossSprite, 3, 3));
            }
            else
            {
                enemies.Add(new Enemy(new Vector2(Xpos, Ypos), enemyShip, 2, 1));
            }
        }

        private void EndUpdate()
        {
            TitleUpdate();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (STATE)
            {
                case UI.STATE_TITLE:
                    TitleUpdate();
                    break;
                case UI.STATE_GAME:
                    GameUpdate(gameTime);
                    break;
                case UI.STATE_END:
                    EndUpdate();
                    break;
            }
            base.Update(gameTime);
        }


        private void TitleDraw()
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(splashScreen, Vector2.Zero, Color.White);
            spriteBatch.DrawString(gameText, "Press enter to play", (new Vector2(ScreenX, ScreenY) - gameText.MeasureString("Press enter to play")) / 2, Color.DarkOrange);
            spriteBatch.End();
        }


        private void GameDraw()
        {
            GraphicsDevice.Clear(new Color(46, 204, 113));
            GraphicsDevice.BlendState = BlendState.NonPremultiplied;

            mapRenderer.Draw(seaMap, cam.GetViewMatrix());

            // world space here
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            player.playerOffset = new Vector2(playerSprite.Width / 2, playerSprite.Height / 2);
            //spriteBatch.Draw(splashScreen, new Vector2(0, 0), Color.Silver);

            foreach (Projectile can in player.projectiles)
            {
                spriteBatch.Draw(cannonballSprite, can.CannonPosition, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            }
            //spriteBatch.Draw(playerSprite, player.Position, Color.White);
            spriteBatch.Draw(playerSprite, player.PlayerPosition, null, Color.White, player.playerRotation, player.playerOffset, 1, SpriteEffects.None, 0);

            foreach (Enemy e in enemies)
            {
                spriteBatch.Draw(e.texture, e.Position, null, Color.White, e.enemyRotation, new Vector2(e.texture.Width / 2, e.texture.Height / 2), 1, SpriteEffects.None, 1);
                ShapeExtensions.DrawLine(spriteBatch, e.Position + new Vector2(18, -e.texture.Height / 1.5f), 44, 0, Color.Black, 6);
                ShapeExtensions.DrawLine(spriteBatch, e.Position + new Vector2(20, -e.texture.Height / 1.5f), e.health * 40 / e.maxHealth, 0, Color.Green, 4);
            }


            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.DrawString(gameText, "Score: " + score, new Vector2(30, 30), Color.Black, 0, new Vector2(0, 0), 1 * 1.5f, SpriteEffects.None, 0);
            for (int i = 0; i < player.PlayerHealth; i++)
            {
                spriteBatch.Draw(health, new Vector2(ScreenX - (health.Width + 30) - (i * 64), 30), Color.White);
            }
            spriteBatch.End();
        }


        private void EndDraw()
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(endScreen, Vector2.Zero, Color.SteelBlue);
            spriteBatch.DrawString(gameText, "You Got " + score + " points!", (new Vector2(ScreenX, ScreenY) - gameText.MeasureString("You Got " + score + " points!")) / 2, Color.White);
            spriteBatch.DrawString(gameText, "Press enter to play again", (new Vector2(ScreenX, ScreenY + gameText.MeasureString("Press enter to play again").Y * 3) - gameText.MeasureString("Press enter to play again")) / 2, Color.White);
            spriteBatch.End();
        }


        protected override void Draw(GameTime gameTime)
        {
            switch (STATE)
            {
                case UI.STATE_TITLE:
                    TitleDraw();
                    break;
                case UI.STATE_GAME:
                    GameDraw();
                    break;
                case UI.STATE_END:
                    EndDraw();
                    break;
            }

            spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            base.Draw(gameTime);
        }
    }

}