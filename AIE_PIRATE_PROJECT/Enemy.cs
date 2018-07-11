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
    class Enemy
    {
        Game1 game = null;
        private Vector2 enemyPosition;
        public Vector2 enemyOffset = new Vector2(0, 0);
        protected int health;
        private float speed;
        protected int radius;
        public float enemyRotation = 0;
        float enemyTurnSpeed = 1;
        ///float xSpeed = 0;
        ///float ySpeed = 0;
        public Player GetPlayer { get; set; }
        public Texture2D texture;
        
        public Vector2 Position
        {
            get{return enemyPosition;}
            set{enemyPosition = value;}
        }
        public int Health
        {
            get{return health;}
            set{health = value;}
        }
        public int Radius
        {
            get{return radius;}
        }

        public Enemy(Vector2 newPos, Texture2D texture, float speed)
        {
            enemyPosition = newPos;
            this.texture = texture;
            this.speed = speed;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 direction = playerPosition - enemyPosition;
            direction.Normalize();
            Vector2 enemyVelocity = direction * speed * Game1.maxVelocity * dt;

            enemyPosition += enemyVelocity * dt;
            enemyRotation = direction.ToAngle();
        }       
    }
}

