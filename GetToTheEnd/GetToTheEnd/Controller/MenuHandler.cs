using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using GetToTheEnd.View;
using GetToTheEnd.Model;
using Microsoft.Xna.Framework.Input;

namespace GetToTheEnd.Controller
{
    class MenuHandler : Handler
    {
        private ContentManager m_content;
        private MenuView m_menuView;
        private Menu m_menu;

        public MenuHandler(EventHandler a_handlerEvent, ContentManager a_content)
            :base(a_handlerEvent)
        {
            m_content = a_content;
            m_menu = new Menu();
            m_menuView = new MenuView(a_content);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime a_gameTime)
        {
            //Checks if the user selects an item (enter key) and sends a screen change request.
            m_menuView.UpdateKeyboard();

            if (m_menuView.DidUserSelect())
                m_handlerEvent.Invoke(this, new MyEventArgs(m_menu.GetSelectedMenuItem()));

            if (m_menuView.DidPlayerMoveUp())
                m_menu.MoveMenuSelectionUp();
            else if (m_menuView.DidPlayerMoveDown())
                m_menu.MoveMenuSelectionDown();

            m_menuView.UpdateOldKeyboard();

            base.Update(a_gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch a_spriteBatch)
        {
            m_menuView.UpdateMenuColors(m_menu.GetSelectedMenuItem());

            m_menuView.DrawMenu(a_spriteBatch);
            
            base.Draw(a_spriteBatch);
        }
    }
}
