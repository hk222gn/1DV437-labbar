using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.ObjectModel;

namespace Upg1.Model
{
    class BallSimulation
    {
        private List<Ball> m_balls = new List<Ball>();
        private const int MAX_BALLS = 10;
        private float m_mouseAreaX = 0.05f;
        private float m_mouseAreaY = 0.05f;

        public ReadOnlyCollection<Ball> Balls
        {
            get { return m_balls.AsReadOnly(); }
        }

        public BallSimulation()
        {
            Random rand = new Random();
            for (int i = 0; i < MAX_BALLS; i++)
            {
                Vector2 startPos = new Vector2((float)rand.NextDouble() * (0.9f - 0.2f) + 0.2f, (float)rand.NextDouble() * (0.9f - 0.2f) + 0.2f);
                Vector2 velocity = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
                m_balls.Add(new Ball(startPos, velocity, 0.1f));
            }
        }

        public void Update(float a_elapsedSeconds)
        {
            foreach (Ball ball in m_balls)
            {
                if (ball.alive)
                {
                    ball.Position = new Vector2(ball.Position.X + ball.Velocity.X * a_elapsedSeconds, ball.Position.Y);

                    if (ball.Position.X + ball.Diameter / 2 > 1.0f || ball.Position.X - ball.Diameter / 2 < 0.0f)
                        ball.Velocity = new Vector2(ball.Velocity.X * -1.0f, ball.Velocity.Y);

                    ball.Position = new Vector2(ball.Position.X, ball.Position.Y + ball.Velocity.Y * a_elapsedSeconds);

                    if (ball.Position.Y + ball.Diameter / 2 > 1.0f || ball.Position.Y - ball.Diameter / 2 < 0.0f)
                        ball.Velocity = new Vector2(ball.Velocity.X, ball.Velocity.Y * -1.0f);
                }
            }
        }

        internal void CheckForHit(Vector2 a_mousePos)
        {
            foreach (Ball ball in m_balls)
            {
                if (ball.alive)
                {
                    if (ball.Position.X + ball.Diameter / 2 > a_mousePos.X - m_mouseAreaX
                        && ball.Position.X - ball.Diameter / 2 < a_mousePos.X + m_mouseAreaX
                        && ball.Position.Y + ball.Diameter / 2 > a_mousePos.Y - m_mouseAreaY
                        && ball.Position.Y - ball.Diameter / 2 < a_mousePos.Y + m_mouseAreaY)
                        ball.alive = false;
                }
            }
        }
    }
}