using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TrexGame.Entities;

namespace TrexGame.Controllers
{
    public class InputController
    {
        private Trex _trex;
        private KeyboardState _previousKeyboardState;

        public InputController(Trex trex)
        {
            _trex = trex;
        }

        public void Process(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space) && _trex.State != TrexState.Jumping)
            {
                _trex.StartJump();
            }
            else if (keyboardState.IsKeyUp(Keys.Space) && _trex.State == TrexState.Jumping)
            {
                _trex.CancelJump();
            }

            _previousKeyboardState = keyboardState;
        }
    }
}
