using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Upg1.View
{
    class Camera
    {
        public int m_width;
        public int m_height;
        public float m_scale;
        public int m_padding;
        public int m_frameWidth;
        public int m_frameHeight;

        public Camera(Viewport a_viewport)
        {
            m_padding = 20;

            m_width = a_viewport.Width - m_padding * 2;
            m_height = a_viewport.Height - m_padding * 2;

            m_scale = m_width;

            if (m_height < m_scale)
                m_scale = m_height;

            m_frameWidth = (int)m_scale;
            m_frameHeight = (int)m_scale;
        }

        public float Scale
        {
            get { return m_scale; }
        }

        public Vector2 ConvertToModelCoords(Vector2 a_vector)
        {
            return new Vector2((a_vector.X - m_padding) / m_scale, (a_vector.Y - m_padding) / m_scale);
        }
    }
}