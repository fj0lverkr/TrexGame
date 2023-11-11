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
        private Sprite _sprite;

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

        public Trex()
        {
            _state = TrexState.Idle;
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
