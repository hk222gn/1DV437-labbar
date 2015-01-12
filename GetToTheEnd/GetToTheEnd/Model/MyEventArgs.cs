using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GetToTheEnd.View;

namespace GetToTheEnd.Model
{
    class MyEventArgs : EventArgs
    {
        private MenuItems m_menuItemSelected;

        public MenuItems MenuItemSelected
        {
            get { return m_menuItemSelected; }
            set { m_menuItemSelected = value; }
        }

        public MyEventArgs(MenuItems a_menuItem)
        {
            MenuItemSelected = a_menuItem;
        }
    }
}
