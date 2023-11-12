using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TrexGame.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    public class IdleTrexBackground : IGameEntity
    {
        private Vector2 _position;
        private int _drawOrder;
        private readonly Sprite _sprite;
        private static readonly Rectangle _spriteSource = new(40, 0, 44, 52);

        public int DrawOrder
        {
            get => _drawOrder;
            set => _drawOrder = value;
        }

        public IdleTrexBackground(Texture2D spriteSheet, Vector2 position)
        {
            _sprite = new(spriteSheet, _spriteSource);
            _position = position;
            _drawOrder = 100;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
