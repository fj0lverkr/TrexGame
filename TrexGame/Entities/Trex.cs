using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TrexGame.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    public enum TrexState
    {
        Idle,
        Running,
        Jumping,
        Ducking,
        Dropping,
        Dead
    }

    public class Trex : IGameEntity
    {
        private float _speed = 10;
        private TrexState _state;
        private Vector2 _position;
        private int _drawOrder;
        private readonly Texture2D _spriteSheet;
        private AnimatedSprite _idleAnimation;
        private static readonly List<Rectangle> _idleSprites = new() { new(848, 0, 44, 52), new(892, 0, 44, 52) };
        private static readonly List<Rectangle> _runSprites = new() { new(936, 0, 44, 52), new(980, 0, 44, 52) };
        private static readonly List<Rectangle> _deadSprites = new() { new(1024, 0, 44, 52), new(1068, 0, 44, 52) };
        private static readonly List<Rectangle> _duckSprites = new() { new(1112, 0, 60, 44), new(1172, 0, 60, 44) };

        public float Speed
        {
            get => _speed;
        }
        public TrexState State
        {
            get => _state;
        }
        public Vector2 Position
        {
            get => _position;
        }
        public int DrawOrder
        {
            get => _drawOrder;
            set => _drawOrder = value;
        }

        public Trex(Texture2D spriteSheet, Vector2 initialPosition)
        {
            _spriteSheet = spriteSheet;
            _position = initialPosition;
            _drawOrder = 10;
            SetState(TrexState.Idle);
        }

        private void SetState(TrexState state)
        {
            _state = state;
            List<AnimatedSpriteFrame> frames = new();
            switch (state)
            {
                case TrexState.Idle:
                    Sprite idle1 = new(_spriteSheet, _idleSprites[0]);
                    Sprite idle2 = new(_spriteSheet, _idleSprites[1]);
                    frames = new() { new(idle1, 2f), new(idle2, .2f) };
                    break;
                case TrexState.Running:
                    _runSprites.ForEach(r =>
                    {
                        Sprite s = new(_spriteSheet, r);
                        frames.Add(new(s, 5 / _speed));
                    });
                    break;
                //TODO add cases for other states and handle default case with an exception.
                default:
                    break;

            }

            _idleAnimation = new(true, frames);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _idleAnimation.Play();
            _idleAnimation.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            _idleAnimation.Update(gameTime);
        }

        public void Start()
        {
            _state = TrexState.Running;
        }

        public void Jump()
        {
            _state = TrexState.Jumping;
        }

        public void Duck()
        {
            _state = TrexState.Ducking;
        }

        public void Drop()
        {
            _state = TrexState.Dropping;
        }

        public void Die()
        {
            _state = TrexState.Dead;
        }
    }
}
