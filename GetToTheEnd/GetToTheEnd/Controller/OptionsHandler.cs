using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GetToTheEnd.View;

namespace GetToTheEnd.Controller
{
    class OptionsHandler : Handler
    {
        private ContentManager m_content;
        private SpriteFont m_font;
        private InstructionsView m_instView;

        public OptionsHandler(EventHandler a_handlerEvent, ContentManager a_content)
            :base(a_handlerEvent)
        {
            m_content = a_content;
            m_font = a_content.Load<SpriteFont>("Fonts/General");
            m_instView = new InstructionsView(m_font);
        }
     
        public override void Update(Microsoft.Xna.Framework.GameTime a_gameTime)
        {
            if (m_instView.EscapePressed())
                m_handlerEvent.Invoke(this, new EventArgs());

            base.Update(a_gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch a_spriteBatch)
        {
            m_instView.DrawInstructions(a_spriteBatch);
            base.Draw(a_spriteBatch);
        }
    }
}
