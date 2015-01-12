using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.Model
{
    class Player
    {
        Vector2 m_position = new Vector2(0.05f, 0.94f);//new Vector2(0.15f, 0.09f);
        Vector2 m_speed = new Vector2(0f, 0f);
        Vector2 m_playerTextureSize = new Vector2(14f, 15f);
        private bool m_isAlive = true;

        #region Get/Set
        public Vector2 PlayerTextureSize
        {
            get { return m_playerTextureSize; }
            set { m_playerTextureSize = value; }
        }
        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public Vector2 Speed
        {
            get { return m_speed; }
            set { m_speed = value; }
        }
        #endregion Get/Set

        public void Kill()
        {
            m_isAlive = false;
        }

        public bool IsDead()
        {
            return !m_isAlive;
        }

        internal void Update(float a_elapsedTime)
        {
            Vector2 gravityAcceleration = new Vector2(0f, 1.8f); 

            m_position = m_position + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;

            m_speed = m_speed + a_elapsedTime * gravityAcceleration;
        }

        internal void Jump()
        {
            m_speed.Y = -0.5291f;
        }
    }
}
