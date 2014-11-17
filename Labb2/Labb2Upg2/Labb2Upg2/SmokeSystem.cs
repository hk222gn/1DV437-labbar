using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2Upg2
{
    class SmokeSystem
    {
        private const int PARTICLE_AMOUNT = 60;
        private float m_delay = 0.030f;
        private float m_delayTimer = 0f;
        private Texture2D m_smokeTexture;
        private Vector2 m_startPosition = new Vector2(0.5f, 0.90f);

        List<SmokeParticle> m_particles = new List<SmokeParticle>();

        public SmokeSystem()
        {
            
        }

        internal void CreateParticles()
        {
            Random rand = new Random();
            Vector2 velocity = new Vector2((float)rand.NextDouble() - 0.6f, (float)rand.NextDouble() - 0.2f);

            velocity.Normalize();
            velocity = velocity * ((float)rand.NextDouble() * 0.1f);

            m_particles.Add(new SmokeParticle(m_startPosition, velocity));
        }

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            m_smokeTexture = a_content.Load<Texture2D>("particlesmoke");
        }

        internal void Update(GameTime a_gameTime)
        {
            float timeInSeconds = (float)a_gameTime.ElapsedGameTime.TotalSeconds;
            
            Random rand = new Random();

            if (m_particles.Count < PARTICLE_AMOUNT && m_delayTimer > m_delay)
            {
                CreateParticles();
                m_delayTimer = 0;
            }
            else if (m_particles.Count < PARTICLE_AMOUNT)
                m_delayTimer += timeInSeconds;

            foreach (SmokeParticle particle in m_particles)
            {
                particle.m_velocity.X = particle.m_velocity.X + timeInSeconds * particle.m_acceleration.X;
                particle.m_velocity.Y = particle.m_velocity.Y + timeInSeconds * particle.m_acceleration.Y;

                particle.m_position.X += timeInSeconds * particle.m_velocity.X;
                particle.m_position.Y += timeInSeconds * particle.m_velocity.Y;

                //revive the particle
                if (particle.m_lifeTime >= particle.m_maxLifeTime)
                {
                    
                    Vector2 velocity = new Vector2((float)rand.NextDouble() - 0.6f, (float)rand.NextDouble() - 0.2f);

                    velocity.Normalize();
                    velocity = velocity * ((float)rand.NextDouble() * 0.1f);

                    particle.Revive(m_startPosition, velocity);
                }

                //particle.m_fade -= timeInSeconds / 3; //After 3 seconds it's completely faded out

                //Update lifetime
                particle.m_lifeTime += timeInSeconds;
            }
        }

        internal void Draw(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            a_spriteBatch.Begin();

            foreach (SmokeParticle particle in m_particles)
            {
                float lifePercent = particle.m_lifeTime / particle.m_maxLifeTime;
                float size = particle.m_minSize + lifePercent * particle.m_maxSize;

                Rectangle srcRect = new Rectangle(0, 0, m_smokeTexture.Bounds.Width, m_smokeTexture.Bounds.Height);
                Vector2 origin = new Vector2(m_smokeTexture.Bounds.Width / 2 , m_smokeTexture.Bounds.Height / 2);

                //Color color = new Color(particle.m_fade, particle.m_fade, particle.m_fade, particle.m_fade);

                float visibility = 0.0f * (particle.m_lifeTime / particle.m_maxLifeTime) + (1.0f - particle.m_lifeTime / particle.m_maxLifeTime) * 1.0f;
                float rotation = 1.0f * (particle.m_lifeTime / particle.m_maxLifeTime) + (1.0f - particle.m_lifeTime / particle.m_maxLifeTime) * 0.0f;
                Color color = new Color(visibility, visibility, visibility, visibility);

                a_spriteBatch.Draw(m_smokeTexture, particle.m_position * a_camera.Scale, srcRect, color, rotation, origin, size, SpriteEffects.None, 0);
            }

            a_spriteBatch.End();
        }
    }
}