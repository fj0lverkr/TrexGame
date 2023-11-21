using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrexGame.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    internal class Pterodactyl : IGameEntity
    {
        private readonly Texture2D _spriteSheet;
        private readonly List<AnimatedSpriteFrame> _frames;

        private static readonly List<Rectangle> _textures = new() { new(134, 2, 46, 40), new(180, 2, 46, 40) };

        private AnimatedSprite _spriteAnimation;

        public Vector2 Position { get; set; }
        public float PositionX { get; set; }
        public int DrawOrder { get; set; }
        public float Speed { get; set; }

        public Pterodactyl(int drawOrder, Vector2 position, Texture2D spriteSheet, float gameSpeed)
        {
            DrawOrder = drawOrder;
            Position = position;
            PositionX = position.X;
            _spriteSheet = spriteSheet;
            Speed = gameSpeed;
            _frames = new();
            SetupAnimation();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float y = Position.Y;
            Position = new(PositionX, y);
            _spriteAnimation.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            _spriteAnimation.Update(gameTime);
        }

        private void SetupAnimation()
        {
            _frames.Clear();
            _textures.ForEach(t =>
            {
                Sprite sprite = new(_spriteSheet, t);
                _frames.Add(new(sprite, 5 / Speed));
            });
            _spriteAnimation = new(true, _frames);
            _spriteAnimation.Play();
        }
    }
}
