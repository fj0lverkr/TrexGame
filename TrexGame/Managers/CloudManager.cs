using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TrexGame.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Managers
{
    internal class CloudManager : IGameEntity
    {
        private const int MIN_CLOUDS = 6;
        private const int MAX_CLOUDS = 12;

        private readonly Texture2D _spriteSheet;
        private readonly int _skyWidth;
        private readonly int _skyHeight;

        private List<CloudTile> _cloudTiles;
        private float _speed;

        public int DrawOrder { get; set; }

        public CloudManager(Texture2D spriteSeet, float gameSpeed, int skyWidth, int skyHeight)
        {
            _spriteSheet = spriteSeet;
            _speed = gameSpeed;
            _skyWidth = skyWidth;
            _skyHeight = skyHeight;
            _cloudTiles = new();

            int numInitialClouds = new Random().Next(MIN_CLOUDS, MAX_CLOUDS);
            for (int i = 0; i < numInitialClouds; i++)
            {
                DrawOrder = i + 1;
                _cloudTiles.Add(new(DrawOrder, GenerateCloudPosition(), _spriteSheet));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _cloudTiles.ForEach(tile => { tile.Draw(gameTime, spriteBatch); });
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        private Vector2 GenerateCloudPosition()
        {
            int x = new Random().Next(_skyWidth + 5);
            int y = new Random().Next(_skyHeight + 5);
            return new Vector2(x, y);
        }
    }
}
