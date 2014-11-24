using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upg1.View
{
    class SplitterParticle
    {
        public Vector2 m_position;
        public Vector2 m_velocity;
        public Vector2 m_acceleration = new Vector2(0f, 0.6f);

        public SplitterParticle(Vector2 a_position, Vector2 a_velocity)
        {
            m_position = a_position;
            m_velocity = a_velocity;
        }
    }
}