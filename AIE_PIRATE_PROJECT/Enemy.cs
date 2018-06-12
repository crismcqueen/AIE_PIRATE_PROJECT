using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;




namespace AIE_PIRATE_PROJECT
{
    class Enemy
    {
        Sprite sprite;
        Game1 game = null;
        Vector2 enemyOffset = new Vector2(0, 0);
        Vector2 enemyPosition = new Vector2(200, 100);

        float enemySpeed = 100;
        float enemyRadius = 60;
        float enemyRotations = 4.5f;

       /* public Vector2 Position
        {
            get { return sprite.position; }


        } */


        public Enemy(Game1 game)
        {
            this.game = game;

        }

        public void Load(ContentManager content)
        {


        }

        public void Update(float deltaTime)
        {


        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            
        }*/

    }
}
