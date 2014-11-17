using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labb2Upg3
{
    class AnimatedSprite
    {
        private Texture2D m_spriteSheet;
        private Vector2 m_position;
        private int m_numberOfFrames = 24;
        private float m_timeElapsed = 0f;
        private float m_maxTime = 0.75f;
        private Point m_frameSize = new Point(128, 128);
        private int m_numFramesX = 4;

        public AnimatedSprite(Vector2 a_position)
        {
            m_position = a_position;
        }

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            m_spriteSheet = a_content.Load<Texture2D>("explosion");
        }

        internal void Update(GameTime a_gameTime)
        {
            m_timeElapsed += (float)a_gameTime.ElapsedGameTime.TotalSeconds;
        }

        internal void Draw(SpriteBatch a_spriteBatch, GameTime a_gameTime)
        {
            float percentAnimated = m_timeElapsed / m_maxTime;
            if (percentAnimated >= 1f)
                m_timeElapsed = 0;
            int frame = (int)(percentAnimated * m_numberOfFrames);


            int frameX = frame % m_numFramesX;
            int frameY = frame / m_numFramesX;

            a_spriteBatch.Begin();

            a_spriteBatch.Draw(m_spriteSheet, m_position, new Rectangle(frameX * m_frameSize.X, frameY * m_frameSize.Y, m_frameSize.X, m_frameSize.Y),
                                Color.White, 1f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            a_spriteBatch.End();
        }
    }
}