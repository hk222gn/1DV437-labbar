using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.Controller
{
    class Handler
    {
        protected EventHandler m_handlerEvent;

        public Handler(EventHandler a_handlerEvent)
        {
            m_handlerEvent = a_handlerEvent;
        }

        public virtual void Update(GameTime a_gameTime)
        {

        }

        public virtual void Draw(SpriteBatch a_spriteBatch)
        {

        }
    }
}
