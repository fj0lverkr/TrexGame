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

        private readonly Sprite _sprite;
        private readonly Vector2 _scale;
        private readonly bool _flipX;

        public int DrawOrder { get; set; }
        public Vector2 Position { get; set; }

        public CloudTile(int drawOrder, Vector2 position, Texture2D spriteSheet)
        {
            DrawOrder = drawOrder;
            Position = position;
            Rectangle spriteRect = new(SPRITE_X, SPRITE_Y, SPRITE_WIDTH, SPRITE_HEIGHT);
            _sprite = new Sprite(spriteSheet, spriteRect);
            int rand = new Random().Next(1, 3);
            _scale = new Vector2(SPRITE_WIDTH / rand, SPRITE_HEIGHT / rand);
            int seed = new Random().Next(1, 40);
            _flipX = seed % 2 == 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, Position, _scale, _flipX);
        }

        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
