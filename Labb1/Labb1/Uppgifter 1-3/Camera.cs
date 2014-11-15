using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb1
{
    
    class Camera1
    {
        private float m_sizeOfTile = 64;
        private float m_borderSize = 64;
        private float m_levelWidth = 8;
        private float m_levelHeight = 8;
        private float m_scale;

        //Uppgift 1
        public Vector2 GetScreenCoords(Vector2 a_logicalCoords)
        {
            float X = m_borderSize + a_logicalCoords.X * m_sizeOfTile;
            float Y = m_borderSize + a_logicalCoords.Y * m_sizeOfTile;

            return new Vector2(X, Y);
        }

        //Uppgift 2
        public Vector2 GetFlippedScreenCords(Vector2 a_logicalCoords)
        {
            float X = m_borderSize + (7 - a_logicalCoords.X) * m_sizeOfTile;
            float Y = m_borderSize + (7 - a_logicalCoords.Y) * m_borderSize;

            return new Vector2(X, Y);
        }

        //Uppgift 3
        public void Scale(float a_width, float a_height)
        {
            float scaleX = (a_width - m_borderSize * 2) / m_levelWidth;
            float scaleY = (a_height - m_borderSize * 2) / m_levelHeight;

            m_scale = scaleX;
            if (scaleY < scaleX)
                m_scale = scaleY;
        }

        public float GetScale
        {
            get { return m_scale; }
        }
    }
}