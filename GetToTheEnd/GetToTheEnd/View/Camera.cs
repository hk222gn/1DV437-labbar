using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.View
{
    class Camera
    {
        private float m_scale = 32.0f;
        private GraphicsDevice m_graphicsDevice;

        public Camera(GraphicsDevice a_graphicsDevice)
        {
            m_graphicsDevice = a_graphicsDevice;

            Vector2 viewPort = GetViewPort();
            int scaleX = (int)(viewPort.X);
            int scaleY = (int)(viewPort.Y);

            m_scale = scaleX;
            if (scaleY < scaleX)
            {
                m_scale = scaleY;
            }
        }

        internal Vector2 GetViewPosition(float x, float y)
        {
            Vector2 modelPos = new Vector2(x, y);
            Vector2 viewPort = GetViewPort();

            return (modelPos * m_scale);
        }

        internal Vector2 GetModelPosition(float x, float y)
        {
            Vector2 viewPos = new Vector2(x, y);

            return viewPos / m_scale;
        }

        internal float GetScale()
        {
            return m_scale;
        }

        internal Vector2 GetViewPort()
        {
            return new Vector2(m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height);
        }
    }
}
