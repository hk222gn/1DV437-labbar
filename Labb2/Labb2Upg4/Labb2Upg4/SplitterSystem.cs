using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2Upg4
{
    class SplitterSystem
    {
        private const int PARTICLE_AMOUNT = 150;
        private Texture2D m_splitterTexture;

        SplitterParticle[] m_particles = new SplitterParticle[PARTICLE_AMOUNT];
        
        public SplitterSystem()
        {
            CreateParticles();
        }

        internal void CreateParticles()
        {
            Random rand = new Random();
            for (int i = 0; i < PARTICLE_AMOUNT; i++)
            {
                Vector2 velocity = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);

                velocity.Normalize();
                velocity = velocity * ((float)rand.NextDouble() * 0.8f);

                m_particles[i] = new SplitterParticle(new Vector2(0.5f, 0.5f), velocity);
            }
        }

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            m_splitterTexture = a_content.Load<Texture2D>("spark");
        }

        internal void Update(GameTime a_gameTime)
        {
            float timeInSeconds = (float)a_gameTime.ElapsedGameTime.TotalSeconds;

            foreach (SplitterParticle particle in m_particles)
            {
                particle.m_velocity.X = particle.m_velocity.X + timeInSeconds * particle.m_acceleration.X;
                particle.m_velocity.Y = particle.m_velocity.Y + timeInSeconds * particle.m_acceleration.Y;

                particle.m_position.X += timeInSeconds * particle.m_velocity.X;
                particle.m_position.Y += timeInSeconds * particle.m_velocity.Y;
            }
        }

        internal void Draw(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            a_spriteBatch.Begin();
            

            foreach (SplitterParticle particle in m_particles)
            {
                Vector2 positionAndScale = particle.m_position * a_camera.Scale;

                a_spriteBatch.Draw(m_splitterTexture, new Rectangle((int)positionAndScale.X, (int)positionAndScale.Y, m_splitterTexture.Bounds.Width / 10, m_splitterTexture.Bounds.Height / 10), Color.White);
            }

            a_spriteBatch.End();
        }
    }
}