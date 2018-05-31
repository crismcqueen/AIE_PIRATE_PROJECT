using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using

namespace AIE_PIRATE_PROJECT
{
    class Player
    {
        private Texture2D shipTexture;
        private Vector2 playerPosition = new Vector2(0, 0);
        private Vector2 playerOffset = new Vector2(0, 0);
        private Vector2 playerOffset2 = new Vector2(0, 0);
        private float playerSpeed = 150.0f;
        private float playerStop = 0;
        private float playerWind = 25.0f;
        private float playerDrift = 20.0f;
        private float playerTurnSpeed = 1;
        private float playerRotation = 20;
        private float playerRadius = 20;
        private bool playerAlive = true;
    }
}
