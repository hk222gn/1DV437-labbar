using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GetToTheEnd.Model;

namespace GetToTheEnd.View
{
    class MenuView
    {
        private SpriteFont m_font;
        public KeyboardState m_oldKeyboardState;
        public KeyboardState m_keyboardState;
        private Color m_selectedMenuItemColor = Color.White;
        private Color m_menuItemColor = Color.Red;
        private float m_menuStartCoordTop = 20f;
        private float m_menuStartCoordLeft = 10f;
        private float m_menuTopMargin = 100f;
        private Color m_startGameColor;
        private Color m_optionsColor;
        private Color m_exitColor;

        public MenuView(ContentManager a_content)
        {
            m_font = a_content.Load<SpriteFont>("Fonts/General");
        }

        public void DrawMenu(SpriteBatch a_spriteBatch)
        {
            a_spriteBatch.Begin();

            //TODO: The position is temporary while i figure out how to get the coords from model->view coords in a good way.
            a_spriteBatch.DrawString(m_font, "Start game", new Vector2(m_menuStartCoordLeft, m_menuStartCoordTop), m_startGameColor);

            a_spriteBatch.DrawString(m_font, "Instructions", new Vector2(m_menuStartCoordLeft, m_menuStartCoordTop + m_menuTopMargin), m_optionsColor);

            a_spriteBatch.DrawString(m_font, "Exit", new Vector2(m_menuStartCoordLeft, m_menuStartCoordTop + m_menuTopMargin * 2), m_exitColor);

            a_spriteBatch.End();
        }

        public void UpdateMenuColors(MenuItems a_currentItem)
        {
            SetAllMenuItemsRed();

            switch (a_currentItem)
            {
                case MenuItems.StartGame:
                {
                    m_startGameColor = m_selectedMenuItemColor;
                    break;
                }
                case MenuItems.Options:
                {
                    m_optionsColor = m_selectedMenuItemColor;
                    break;
                }
                case MenuItems.Exit:
                {
                    m_exitColor = m_selectedMenuItemColor;
                    break;
                }
                default:
                    break;
            }
        }

        public bool DidUserSelect()
        {
            Keys activeKey = Keys.Enter;

            return HandleKeystroke(activeKey);
        }

        public bool DidPlayerMoveUp()
        {
            Keys activeKey = Keys.Up;

            return HandleKeystroke(activeKey);
        }

        public bool DidPlayerMoveDown()
        {
            Keys activeKey = Keys.Down;

            return HandleKeystroke(activeKey);
        }

        public void UpdateKeyboard()
        {
            m_keyboardState = Keyboard.GetState();
        }

        public void UpdateOldKeyboard()
        {
            m_oldKeyboardState = m_keyboardState;
        }

        private bool HandleKeystroke(Keys key)
        {
            bool selected = false;

            if (CheckKeystroke(key))
                selected = true;

            return selected;
        }

        private bool CheckKeystroke(Keys key)
        {
            return (m_keyboardState.IsKeyDown(key) && m_oldKeyboardState.IsKeyUp(key));
        }

        private void SetAllMenuItemsRed()
        {
            m_startGameColor = Color.Red;
            m_optionsColor = Color.Red;
            m_exitColor = Color.Red;
        }
    }
}
