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
        public bool IsRunning { get; set; }

        public CloudManager(Texture2D spriteSeet, float gameSpeed, int skyWidth, int skyHeight)
        {
            _spriteSheet = spriteSeet;
            _speed = gameSpeed;
            _skyWidth = skyWidth;
            _skyHeight = skyHeight;
            _cloudTiles = new();
            IsRunning = false;

            SpawnClouds(true);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _cloudTiles.ForEach(tile => { tile.Draw(gameTime, spriteBatch); });
        }

        public void Update(GameTime gameTime)
        {
            float cloudSpeed = IsRunning ? _speed * 2.5f : _speed / 2;
            _cloudTiles.ForEach(tile =>
            {
                if (tile.PositionX <= 0)
                {
                    SpawnClouds();
                }
                if (tile.PositionX < -tile.Scale.X)
                {
                    tile.Reset(_skyWidth + 5, GenerateCloudPosition().Y);
                }
                tile.PositionX -= cloudSpeed / tile.TileSizeFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;
            });
        }

        private Vector2 GenerateCloudPosition()
        {
            int x = new Random().Next(_skyWidth + 5);
            int y = new Random().Next(_skyHeight + 5);
            return new Vector2(x, y);
        }

        private void SpawnClouds(bool randomX = false)
        {
            if (MIN_CLOUDS + _cloudTiles.Count < MAX_CLOUDS)
            {
                int numInitialClouds = new Random().Next(MIN_CLOUDS + _cloudTiles.Count, MAX_CLOUDS);
                for (int i = 0; i < numInitialClouds; i++)
                {
                    DrawOrder = i + 1;
                    Vector2 cloudPos = randomX ? GenerateCloudPosition() : new(_skyWidth + 5, GenerateCloudPosition().Y);
                    _cloudTiles.Add(new(DrawOrder, cloudPos, _spriteSheet));
                }
            }
        }
    }
}
