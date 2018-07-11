﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AIE_PIRATE_PROJECT
{
    enum UI
    {
     STATE_TITLE,
     STATE_GAME,
     STATE_PAUSE,
     STATE_GAMEOVERDIE,
     STATE_GAMEOVERWIN
     
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int tile =64;
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
        Texture2D cannonballSprite;
        Texture2D tiles;
        TiledMapRenderer mapRenderer;
        TiledMap seaMap;
        Camera2D cam;
        SpriteFont gameText;
        TiledMapTileLayer collisionLayer;
        public ArrayList allCollisionTiles = new ArrayList();
        //public Sprite[,] levelGrid;
        public int tileHeight = 0;
        public int levelTileWidth = 0;
        public int levelTileHeight = 0;
        int score = 0;
        public int lives = 3;
        //private int ScreenY;
        //private int screenX;

        List<Enemy> enemies = new List<Enemy>();


        Player player = new Player();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Screen res
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            
            

            
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = true;

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
            foreach (TiledMapTileLayer layer in seaMap.TileLayers)
            {
                if (layer.Name == "Collision")
                    collisionLayer = layer;
            }
            foreach (TiledMapObjectLayer layer in seaMap.ObjectLayers)
            {
                if (layer.Name == "Enemies")
                {
                    foreach (TiledMapObject obj in layer.Objects)
                    {
                        Enemy enemy = new Enemy(new Vector2(obj.Position.X, obj.Position.Y), enemyShip, 2);
                        enemies.Add(enemy);
                    }
                }
                if (layer.Name == "EnemyBoss")
                {
                    foreach (TiledMapObject obj in layer.Objects)
                    {
                        Enemy enemy = new Enemy(new Vector2(obj.Position.X, obj.Position.Y), enemyBossSprite, 3);
                        enemies.Add(enemy);
                    }
                }
                if (layer.Name == "Goal")
                {
                    TiledMapObject obj = layer.Objects[0];
                    if (obj != null)
                    {
                        AnimatedTexture anim = new AnimatedTexture(Vector2.Zero, 0, 0.5f, 1);
                        anim.Load(Content, "chest", 1, 1);
                        //goal = new Sprite();
                        //goal.Add(anim, 18, 24);
                        //goal.position = new Vector2(obj.Position.X, obj.Position.Y);
                    }
                }
                
            }
        }






        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (player != null)
            {
                
                foreach (Enemy e in enemies)
                {
                    e.Update(gameTime, player.PlayerPosition);
                }
            }
                player.Update(gameTime, enemies);
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime, player.PlayerPosition);
            }
            cam.LookAt(player.PlayerPosition);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            GraphicsDevice.BlendState = BlendState.NonPremultiplied;

            spriteBatch.Begin();

            for (int r = 0; r <= ScreenY / 64; r++)
            {
                for (int c = 0; c <= ScreenX / 64; c++)
                {
                    spriteBatch.Draw(tiles, new Vector2(64 * (r), 64 * (c)), new Rectangle(384, 64, 64, 64), Color.White);
                }
            }

            spriteBatch.End();

            mapRenderer.Draw(seaMap, cam.GetViewMatrix());

            // world space here
            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            ShapeExtensions.DrawPoint(spriteBatch, Vector2.Zero, Color.Orange, 3);

            player.playerOffset = new Vector2(playerSprite.Width / 2, playerSprite.Height / 2);
            //spriteBatch.Draw(splashScreen, new Vector2(0, 0), Color.Silver);

            spriteBatch.DrawString(gameText, "test press escape to exit", new Vector2(100, 100), Color.DarkRed, 0, new Vector2(0, 0), 1 * 3, SpriteEffects.None, 0);
            foreach (Projectile can in player.projectiles)
            {
                spriteBatch.Draw(cannonballSprite, can.CannonPosition, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);

            }
            //spriteBatch.Draw(playerSprite, player.Position, Color.White);
            spriteBatch.Draw(playerSprite, player.PlayerPosition, null, Color.White, player.playerRotation, player.playerOffset, 1, SpriteEffects.None, 0);
            ShapeExtensions.DrawPoint(spriteBatch, player.PlayerPosition, Color.Orange, 3);

            foreach (Enemy e in enemies)
            {
                spriteBatch.Draw(e.texture, e.Position, null, Color.White, e.enemyRotation, new Vector2(e.texture.Width / 2, e.texture.Height / 2), 1, SpriteEffects.None, 1);
                ShapeExtensions.DrawPoint(spriteBatch, e.Position, Color.Orange, 3);
            }
            
            
            spriteBatch.End();
            spriteBatch.Begin();
            
            spriteBatch.DrawString(gameText, Convert.ToString(player.PlayerPosition), new Vector2(30, 30), Color.Black, 0, new Vector2(0, 0), 1 * 1.5f, SpriteEffects.None, 0);
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(health, new Vector2(1280 - 80 - i * 64, 16), Color.White);
            }
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            base.Draw(gameTime);
        }
    }
    
}

