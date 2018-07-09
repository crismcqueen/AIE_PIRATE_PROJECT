﻿using System;
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
        protected int speed;
        protected int radius;
        public float enemyRotation = 0;
        float enemyTurnSpeed = 1;
        float xSpeed = 0;
        float ySpeed = 0;
        public Player GetPlayer { get; set; }
        
        public static List<Enemy> enemies = new List<Enemy>();

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
        public Enemy(Vector2 newPos)
        {
            enemyPosition = newPos;
        }

       /* float RotateTo(Vector2 pointTo)
        {
            float rot = 0;

            Vector2 direction = enemyPosition - pointTo;
            direction.Normalize();

            rot = (float)Math.Atan2((double)direction.Y, (double)direction.X);

            rot += MathHelper.ToRadians(90);

            return rot;
        } */

        public void Load(ContentManager content)
        {


        }


        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 direction = playerPosition - enemyPosition;
            direction.Normalize();
            Vector2 enemyVelocity = direction * speed * Game1.maxVelocity * dt;

            enemyPosition += enemyVelocity * dt;


            double x = (xSpeed * Math.Cos(enemyRotation)) - (ySpeed * Math.Sin(enemyRotation));
            double y = (xSpeed * Math.Sin(enemyRotation)) + (ySpeed * Math.Cos(enemyRotation));

            //Vector2 velocity = new Vector2((float)(enemyTurnSpeed * Math.Sin(enemyRotation)), (float)(enemyTurnSpeed * Math.Cos(enemyRotation)));
            enemyRotation = Angle(direction);

            //enemyRotation = RotateTo(GetPlayer.PlayerPosition);
            
        }

        public float Angle(Vector2 p_vector2)
        {
            if (p_vector2.X < 0)
            {
                return MathHelper.ToRadians(360 - ((float)Math.Atan2(p_vector2.X, p_vector2.Y))) - 1;
            }
            else
            {
                return MathHelper.ToRadians((float) Math.Atan2(p_vector2.X, p_vector2.Y));
            }
        } 


        private void ChasePlayer()
        {
             foreach (Enemy e in enemies)
             {

                 /*if (IsColliding(e.radius, playerPosition ) == true)
                 {
                    
                     
                     

                 }
                 else
                 {
                     

                 } */

             }

        } 

        protected bool IsColliding (Vector2 playerPosition, float radius1, Vector2 enemyPosition, float radius2)
        {
            Vector2 direction = enemyPosition - playerPosition;
            float length = direction.Length();

            if (radius1 + radius2 <= length)
            {
                return false;
            }

            return true;


        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
        
    }

    class enemyShip : Enemy
    {
        public enemyShip(Vector2 newPos) : base(newPos)
        {
            speed =1;
            
        }
    }
    class enemyBoss : Enemy
    {
        public enemyBoss(Vector2 newPos) : base(newPos)
        {
            speed =2;
        }
    }
}

