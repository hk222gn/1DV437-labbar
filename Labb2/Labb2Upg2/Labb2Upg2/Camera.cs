using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2Upg2
{
    class Camera
    {
        public int m_width;
        public int m_height;
        private float m_scale;

        public Camera(Viewport a_viewport)
        {
            m_width = a_viewport.Width;
            m_height = a_viewport.Height;

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