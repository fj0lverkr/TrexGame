using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TrexGame.Entities;

namespace TrexGame.Controllers
{
    internal class InputController
    {
        private readonly Trex _trex;
        private KeyboardState _previousKeyboardState;

        public InputController(Trex trex)
        {
            _trex = trex;
        }

        public void Process(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool isJumpKeyDown = keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W);
            bool wasJumpKeyDown = _previousKeyboardState.IsKeyDown(Keys.Space) || _previousKeyboardState.IsKeyDown(Keys.Up) || _previousKeyboardState.IsKeyDown(Keys.W);
            bool isCrouchKeyDown = keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S);
            bool wasCrouchKeyDown = _previousKeyboardState.IsKeyDown(Keys.LeftControl) || _previousKeyboardState.IsKeyDown(Keys.Down) || _previousKeyboardState.IsKeyDown(Keys.S);

            if (isJumpKeyDown && !wasJumpKeyDown && _trex.State == TrexState.Running)
            {
                _trex.Jump();
            }
            else if (!isJumpKeyDown && _trex.State == TrexState.Jumping)
            {
                _trex.Fall();
            }
            else if (isCrouchKeyDown && _trex.State == TrexState.Running)
            {
                _trex.Duck();
            }
            else if (!isCrouchKeyDown && wasCrouchKeyDown)
            {
                _trex.Rise();
            }

            _previousKeyboardState = keyboardState;
        }
    }
}
