using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        Falling,
        Ducking,
        Dropping,
        Dead
    }

    public class Trex : IGameEntity
    {
        private const float RANDOM_FRAME_MIN_DURATION = 2f;
        private const float RANDOM_FRAME_MAX_DURATION = 10f;
        private const float JUMP_UP_VELOCITY = -480f;
        private const float GRAVITY = 1600f;
        private const float CANCEL_JUMP_VELOCITY = -100f;
        private const int MINIMUM_JUMP_HEIGHT = 40;

        private readonly Texture2D _spriteSheet;
        private readonly SoundEffect _jumpSound;
        private readonly Vector2 _initialPosition;

        private static readonly List<Rectangle> _idleSprites = new() { new(848, 0, 44, 52), new(892, 0, 44, 52) };
        private static readonly List<Rectangle> _runSprites = new() { new(936, 0, 44, 52), new(980, 0, 44, 52) };
        private static readonly List<Rectangle> _deadSprites = new() { new(1024, 0, 44, 52), new(1068, 0, 44, 52) };
        private static readonly List<Rectangle> _duckSprites = new() { new(1112, 0, 60, 44), new(1172, 0, 60, 44) };

        private AnimatedSprite _spriteAnimation;
        private float _verticalVelocity;

        public float Speed { get; set; } = 20f;
        public TrexState State { get; private set; }
        public Vector2 Position { get; set; }
        public int DrawOrder { get; set; }

        public Trex(Texture2D spriteSheet, Vector2 initialPosition, SoundEffect jumpSound)
        {
            _spriteSheet = spriteSheet;
            _initialPosition = initialPosition;
            Position = initialPosition;
            _jumpSound = jumpSound;
            DrawOrder = 10;
            SetState(TrexState.Idle);
        }

        public void SetState(TrexState state)
        {
            State = state;
            List<AnimatedSpriteFrame> frames = new();
            bool repeatAnimation = true;
            switch (state)
            {
                case TrexState.Idle:
                case TrexState.Jumping:
                case TrexState.Falling:
                    repeatAnimation = false;
                    Sprite idle1 = new(_spriteSheet, _idleSprites[0]);
                    Sprite idle2 = new(_spriteSheet, _idleSprites[1]);
                    frames = new() { new(idle1, 2f), new(idle2, .2f) };
                    break;
                case TrexState.Running:
                    _runSprites.ForEach(r =>
                    {
                        Sprite s = new(_spriteSheet, r);
                        frames.Add(new(s, 5 / Speed));
                    });
                    break;
                case TrexState.Dead:
                    repeatAnimation = false;
                    _deadSprites.ForEach(r =>
                    {
                        Sprite s = new(_spriteSheet, r);
                        frames.Add(new(s, 0.5f));
                    });
                    break;
                //TODO add cases for other states and handle default case with an exception.
                default:
                    break;

            }
            if (_spriteAnimation != null)
            {
                _spriteAnimation.ChangeFrames(frames, repeatAnimation);
            }
            else
            {
                _spriteAnimation = new(repeatAnimation, frames);
            }
            _spriteAnimation.Play();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteAnimation.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            if (State == TrexState.Idle && !_spriteAnimation.IsPlaying)
            {
                float newDuration = RANDOM_FRAME_MIN_DURATION + (float)new Random().NextDouble() * (RANDOM_FRAME_MAX_DURATION - RANDOM_FRAME_MIN_DURATION);
                _spriteAnimation.ChangeFrameDuration(0, newDuration);
                _spriteAnimation.Play();
            }
            else if (State == TrexState.Jumping || State == TrexState.Falling)
            {
                if (Position.Y <= _initialPosition.Y)
                {
                    Position = new(Position.X, Position.Y + _verticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    _verticalVelocity += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_verticalVelocity >= 0)
                    {
                        State = TrexState.Falling;
                    }
                }
                else
                {
                    SetState(TrexState.Running);
                    Position = _initialPosition;
                }
            }
            _spriteAnimation.Update(gameTime);
        }

        public void Start()
        {
            SetState(TrexState.Running);
        }

        public bool StartJump()
        {
            if (State == TrexState.Jumping || State == TrexState.Falling)
                return false;
            _jumpSound.Play();
            SetState(TrexState.Jumping);
            _verticalVelocity = JUMP_UP_VELOCITY;
            return true;
        }

        public bool CancelJump()
        {
            if (State != TrexState.Jumping || _initialPosition.Y - Position.Y < MINIMUM_JUMP_HEIGHT)
                return false;

            State = TrexState.Falling;
            _verticalVelocity = _verticalVelocity < CANCEL_JUMP_VELOCITY ? CANCEL_JUMP_VELOCITY : 0;
            return true;
        }

        public void Duck()
        {
            State = TrexState.Ducking;
        }

        public void Drop()
        {
            State = TrexState.Dropping;
        }

        public void Die()
        {
            State = TrexState.Dead;
        }
    }
}
