using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrexGame.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    internal class IdleTrexBackground : IGameEntity
    {
        private Vector2 _position;
        private readonly Sprite _sprite;
        private static readonly Rectangle _spriteSource = new(40, 0, 44, 52);

        public int DrawOrder { get; set; }
        public float Speed { get; set; }
        public static int SpriteWidth { get => _spriteSource.Width; }

        public IdleTrexBackground(Texture2D spriteSheet, Vector2 position)
        {
            _sprite = new(spriteSheet, _spriteSource);
            _position = position;
            DrawOrder = 100;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position);
        }

        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
