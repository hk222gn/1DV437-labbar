using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upg1.View
{
    class EffectSystem
    {
        private SplitterSystem m_splitter;
        private SmokeSystem m_smoke;
        private ShockwaveSystem m_shockwave;
        private SoundEffect m_soundEffect;

        public EffectSystem(Vector2 a_position, ContentManager a_content)
        {
            m_splitter = new SplitterSystem(a_position);
            m_splitter.LoadContent(a_content);

            m_smoke = new SmokeSystem(a_position);
            m_smoke.LoadContent(a_content);

            m_shockwave = new ShockwaveSystem(a_position);
            m_shockwave.LoadContent(a_content);

            m_soundEffect = a_content.Load<SoundEffect>("fire");
            m_soundEffect.Play();
        }
        
        public void Update(GameTime a_gameTime)
        {
            m_splitter.Update(a_gameTime);
            m_smoke.Update(a_gameTime);
            m_shockwave.Update(a_gameTime);
        }

        public void Draw(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            m_splitter.Draw(a_spriteBatch, a_camera);
            m_smoke.Draw(a_spriteBatch, a_camera);
            m_shockwave.Draw(a_spriteBatch, a_camera);
        }
    }
}