using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    internal class ScoreBoard : IGameEntity
    {
        public int DrawOrder { get; set; }
        public float Speed { get; set; }
        public int Score { get; set; }
        public int HighScore { get; set; }

        public ScoreBoard()
        {
            DrawOrder = 0;
            Score = 0;
            HighScore = 0; //TODO Load this from a file or database.
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
