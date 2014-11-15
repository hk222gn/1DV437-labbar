using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb1.View
{
    class Camera
    {
        public int m_width;
        public int m_height;
        public float m_scale;
        public int m_padding;

        public Camera(Viewport a_viewport)
        {
            m_padding = 20;

            m_width = a_viewport.Width - m_padding * 2;
            m_height = a_viewport.Height - m_padding * 2;

            m_scale = m_height;

            if (m_scale < m_width)
                m_scale = m_width;
        }

        public float Scale
        {
            get { return m_scale; }
        }
    }
}