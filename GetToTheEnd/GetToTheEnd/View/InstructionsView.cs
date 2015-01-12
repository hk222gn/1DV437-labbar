using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.View
{
    class InstructionsView
    {
        private SpriteFont m_font;

        public InstructionsView(SpriteFont a_font)
        {
            m_font = a_font;
        }

        public void DrawInstructions(SpriteBatch a_spriteBatch)
        {
            a_spriteBatch.Begin();
            a_spriteBatch.DrawString(m_font, "Instructions", new Vector2(10, 5), Color.Red);
            a_spriteBatch.DrawString(m_font, "To jump, press C.", new Vector2(100, 200), Color.Red);
            a_spriteBatch.DrawString(m_font, "Use the arrow keys to move.", new Vector2(100, 250), Color.Red);
            a_spriteBatch.DrawString(m_font, "Get to the portal to reach the next level.", new Vector2(100, 300), Color.Red);
            a_spriteBatch.DrawString(m_font, "Also, don't die (Spikes kill you).", new Vector2(100, 350), Color.Red);
            a_spriteBatch.DrawString(m_font, "Back - Esc", new Vector2(10, 860), Color.Red);
            a_spriteBatch.End();
        }

        internal bool EscapePressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }
}
