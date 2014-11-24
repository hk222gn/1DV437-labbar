using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upg1.View
{
    class ShockwaveParticle
    {
        public Vector2 m_position;
        public float m_minSize = 1f;
        public float m_maxSize = 40f;
        public float m_lifeTime = 0f;
        public float m_maxLifeTime = 0.3f;
        public float m_fade = 0f;

        public ShockwaveParticle(Vector2 a_position)
        {
            m_position = a_position;
        }
    }
}
