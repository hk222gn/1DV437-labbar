using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upg1.View
{
    class SmokeParticle
    {
        public Vector2 m_position;
        public Vector2 m_velocity;
        public Vector2 m_acceleration = new Vector2(0f, -0.3f);
        public float m_lifeTime = 0f;
        public float m_maxLifeTime = 2f;
        public float m_minSize = 2f;
        public float m_maxSize = 3f;
        public float m_fade = 1f;

        public SmokeParticle(Vector2 a_position, Vector2 a_velocity)
        {
            m_position = a_position;
            m_velocity = a_velocity;
        }

        public SmokeParticle()
        {
            // TODO: Complete member initialization
        }

        public void Revive(Vector2 a_startPosition, Vector2 a_velocity)
        {
            m_position = a_startPosition;
            m_velocity = a_velocity;//C
            m_lifeTime = 0f;
            m_minSize = 1.5f;
            m_maxSize = 2f;
            m_fade = 1f;
        }
    }
}