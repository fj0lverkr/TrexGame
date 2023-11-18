using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TrexGame.Interfaces;

namespace TrexGame.Graphics
{
    internal class CloudTile : IGameEntity
    {
        private const int SPRITE_WIDTH = 46;
        private const int SPRITE_HEIGHT = 44;
        private const int SPRITE_X = 86;
        private const int SPRITE_Y = 2;
        private const int SPRITE_SIZES = 3;

        private readonly Sprite _sprite;

        private bool _flipX;

        public int DrawOrder { get; set; }
        public Vector2 Position { get; private set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float TileSizeFactor { get; private set; }
        public Vector2 Scale { get; private set; }

        public CloudTile(int drawOrder, Vector2 position, Texture2D spriteSheet)
        {
            DrawOrder = drawOrder;
            Position = position;
            Rectangle spriteRect = new(SPRITE_X, SPRITE_Y, SPRITE_WIDTH, SPRITE_HEIGHT);
            _sprite = new Sprite(spriteSheet, spriteRect);
            Reset(position.X, position.Y);
        }

        public void Reset(float x, float y)
        {
            PositionX = x;
            PositionY = y;
            TileSizeFactor = new Random().Next(1, SPRITE_SIZES);
            Scale = new Vector2(SPRITE_WIDTH / TileSizeFactor, SPRITE_HEIGHT / TileSizeFactor);
            int seed = new Random().Next(1, 40);
            _flipX = seed % 2 == 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Position = new(PositionX, PositionY);
            _sprite.Draw(spriteBatch, Position, Scale, _flipX);
        }

        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
