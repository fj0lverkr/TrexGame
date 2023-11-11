using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    public class ScoreBoard : IGameEntity
    {
        private int _drawOrder;
        private int _score;
        private int _highScore;

        public int DrawOrder
        {
            get => _drawOrder;
            set => _drawOrder = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public int HighScore
        {
            get => _highScore;
            set => _highScore = value;
        }

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
