using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Upg1.View
{
    class GameView
    {
        private MouseState m_mouseState;
        private MouseState m_oldMouseState;

        public bool CheckForLeftClick()
        {
            bool clicked = false;

            m_mouseState = Mouse.GetState();

            if (m_mouseState.LeftButton == ButtonState.Pressed && m_oldMouseState.LeftButton == ButtonState.Released)
            {
                clicked = !clicked;
            }

            m_oldMouseState = m_mouseState;

            return clicked;
        }
    }
}
