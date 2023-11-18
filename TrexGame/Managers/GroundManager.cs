using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TrexGame.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Managers
{
    internal class GroundManager : IGameEntity
    {
        private const int SPRITE_WIDTH = 600;
        private const int SPRITE_HEIGHT = 14;
        private const int SPRITE_X = 2;
        private const int SPRITE_Y = 54;

        private readonly List<Rectangle> _groundSprites;

        private readonly Texture2D _spriteSheet;
        private readonly List<GroundTile> _groundTiles;

        public int DrawOrder { get; set; }
        public float Speed { get; set; }
        public bool IsRunning { get; set; }

        public GroundManager(Texture2D spriteSheet, float speed)
        {
            DrawOrder = 1;
            _spriteSheet = spriteSheet;
            _groundSprites = new() { new(SPRITE_X, SPRITE_Y, SPRITE_WIDTH, SPRITE_HEIGHT), new(SPRITE_WIDTH, SPRITE_Y, SPRITE_WIDTH, SPRITE_HEIGHT) };
            _groundTiles = new() { new(0f, new(_spriteSheet, _groundSprites[0]), DrawOrder) };
            Speed = speed;
            IsRunning = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _groundTiles.ForEach(tile => { tile.Draw(gameTime, spriteBatch); });
        }

        public void Update(GameTime gameTime)
        {
            if (IsRunning)
            {
                if (_groundTiles[0].PositionX <= 0 && _groundTiles.Count == 1)
                {
                    int rand = new Random().Next();
                    int index = rand % 2 == 0 ? 0 : 1;
                    Sprite sprite = new(_spriteSheet, _groundSprites[index]);
                    GroundTile tile = new(_groundTiles[0].PositionX + SPRITE_WIDTH, sprite, DrawOrder);
                    _groundTiles.Add(tile);
                }
                if (_groundTiles[0].PositionX <= -SPRITE_WIDTH)
                {
                    _groundTiles.RemoveAt(0);
                }
                _groundTiles.ForEach(t => t.PositionX -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}
