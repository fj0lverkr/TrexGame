using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private float _speed;
        private TrexState _state;
        private Vector2 _position;
        private int _drawOrder;
        private readonly Sprite _sprite;
        private static readonly Rectangle[] _idleSprites = { new(848, 0, 44, 52), new(892, 0, 44, 52) };
        private static readonly Rectangle[] _runSprites = { new(936, 0, 44, 52), new(980, 0, 44, 52) };
        private static readonly Rectangle[] _deadSprites = { new(1024, 0, 44, 52), new(1068, 0, 44, 52) };
        private static readonly Rectangle[] _duckSprites = { new(1112, 0, 60, 44), new(1172, 0, 60, 44) };

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
            _state = TrexState.Idle;
            _sprite = new(spriteSheet, _idleSprites[0]);
            _position = initialPosition;
            _drawOrder = 10;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
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
