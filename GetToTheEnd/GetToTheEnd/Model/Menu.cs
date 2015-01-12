using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.Model
{
    public enum MenuItems
    {
        //Do not change the StartGame position, it has to be the first one.
        StartGame = 1,
        Options,
        Exit,
        NULL
    }

    class Menu
    {
        private MenuItems m_menuItemSelected = MenuItems.StartGame;

        public Menu()
        {

        }

        internal MenuItems GetSelectedMenuItem()
        {
            return m_menuItemSelected;
        }

        internal void MoveMenuSelectionUp()
        {
            //If it's at the top, move to the bottom one.
            if (m_menuItemSelected == MenuItems.StartGame)
            {
                m_menuItemSelected = MenuItems.NULL - 1;
                return;
            }

            m_menuItemSelected--;
        }

        internal void MoveMenuSelectionDown()
        {
            //If it's at the last option, move to the top.
            if (m_menuItemSelected == MenuItems.NULL - 1)
            {
                m_menuItemSelected = MenuItems.StartGame;
                return;
            }

            m_menuItemSelected++;
        }
    }
}
