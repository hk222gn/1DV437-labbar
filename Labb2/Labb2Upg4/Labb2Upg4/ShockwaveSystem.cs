using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb2Upg4
{
    class ShockwaveSystem
    {
        private Texture2D m_shockwaveTexture;
        private ShockwaveParticle m_shockwave;

        public ShockwaveSystem()
        {
            m_shockwave = new ShockwaveParticle(new Vector2(0.5f, 0.5f));
        }

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            m_shockwaveTexture = a_content.Load<Texture2D>("shockwave");
        }

        internal void Update(GameTime a_gameTime)
        {
            if (m_shockwave != null)
            {
                float timeInSeconds = (float)a_gameTime.ElapsedGameTime.TotalSeconds;

                m_shockwave.m_lifeTime += timeInSeconds;

                if (m_shockwave.m_lifeTime >= m_shockwave.m_maxLifeTime)
                    m_shockwave = null;
            }
        }

        internal void Draw(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            if (m_shockwave != null)
            {
                a_spriteBatch.Begin();

                float lifePercent = m_shockwave.m_lifeTime / m_shockwave.m_maxLifeTime;
                float size = m_shockwave.m_minSize + lifePercent * m_shockwave.m_maxSize;

                Rectangle srcRect = new Rectangle(0, 0, m_shockwaveTexture.Bounds.Width, m_shockwaveTexture.Bounds.Height);
                Vector2 origin = new Vector2(m_shockwaveTexture.Bounds.Width / 2, m_shockwaveTexture.Bounds.Height / 2);
                float visibility = 0.0f * (m_shockwave.m_lifeTime / m_shockwave.m_maxLifeTime) + (1.0f - m_shockwave.m_lifeTime / m_shockwave.m_maxLifeTime) * 1.0f;

                Color color = new Color(visibility, visibility, visibility, visibility);

                float rotation = 1.0f * (m_shockwave.m_lifeTime / m_shockwave.m_maxLifeTime) + (1.0f - m_shockwave.m_lifeTime / m_shockwave.m_maxLifeTime) * 0.0f;

                a_spriteBatch.Draw(m_shockwaveTexture, m_shockwave.m_position * a_camera.Scale, srcRect, color, rotation, origin, size, SpriteEffects.None, 0);

                a_spriteBatch.End();
            }
        }
    }
}
