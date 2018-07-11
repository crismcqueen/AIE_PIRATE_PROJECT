using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;




namespace AIE_PIRATE_PROJECT
{
    public class Enemy
    {
        Game1 game = null;
        private Vector2 enemyPosition;
        public Vector2 enemyOffset = new Vector2(0, 0);
        public int health = 1;
        private float speed;
        public int radius;
        public float enemyRotation = 0;
        public int maxHealth;
        ///float enemyTurnSpeed = 0.3f;
        ///float xSpeed = 0;
        ///float ySpeed = 0;
        public Player GetPlayer { get; set; }
        public Texture2D texture;
        
        public Vector2 Position
        {
            get{return enemyPosition;}
            set{enemyPosition = value;}
        }

        public Enemy(Vector2 newPos, Texture2D texture, float speed, int health)
        {
            enemyPosition = newPos;
            this.texture = texture;
            this.speed = speed;
            radius = texture.Height / 2;
            this.health = health;
            maxHealth = health;
        }

        public void Update(GameTime gameTime, Player player)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 direction = player.PlayerPosition - enemyPosition;

            direction.Normalize();

            Vector2 enemyVelocity = direction * speed * Game1.maxVelocity * dt;

            enemyPosition += enemyVelocity * dt;

            enemyRotation = direction.ToAngle();

            enemyPosition.X = Math.Max(64, Math.Min(64 * 24, enemyPosition.X));
            enemyPosition.Y = Math.Max(64, Math.Min(64 * 24, enemyPosition.Y));
        }
    }
}

