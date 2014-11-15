using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Labb1.Model
{
    class BallSimulation
    {
        private Ball m_b;

        public Ball Ball
        {
            get { return m_b; }
        }

        public BallSimulation()
        {
            m_b = new Ball(new Vector2(0.1f, 0.1f), new Vector2(-0.3f, -0.5f), 0.1f);
        }

        public void Update(float a_elapsedSeconds)
        {
            m_b.Position = new Vector2(m_b.Position.X + m_b.Velocity.X * a_elapsedSeconds, m_b.Position.Y);

            if (m_b.Position.X + m_b.Diameter / 2 > 1.0f || m_b.Position.X - m_b.Diameter / 2 < 0.0f)
                m_b.Velocity = new Vector2(m_b.Velocity.X * -1.0f, m_b.Velocity.Y);

            m_b.Position = new Vector2(m_b.Position.X, m_b.Position.Y + m_b.Velocity.Y * a_elapsedSeconds);

            if (m_b.Position.Y + m_b.Diameter / 2 > 1.0f || m_b.Position.Y - m_b.Diameter / 2 < 0.0f)
                m_b.Velocity = new Vector2(m_b.Velocity.X, m_b.Velocity.Y * -1.0f);
        }
    }
}