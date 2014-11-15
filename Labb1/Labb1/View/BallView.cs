using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labb1.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Labb1.View
{
    class BallView
    {
        private Camera m_c;
        private SpriteBatch m_spriteBatch;
        private Texture2D m_ballTexture;
        private Texture2D m_lineTexture;
        

        public BallView(SpriteBatch a_spriteBatch, Texture2D a_ballTexture, Texture2D a_lineTexture, Camera a_camera)
        {
            m_spriteBatch = a_spriteBatch;
            m_ballTexture = a_ballTexture;
            m_c = a_camera;
            m_lineTexture = a_lineTexture;
        }

        public void DrawSimulation(Ball a_ball, float a_elapsedMilliseconds)
        {
            int size = (int)(a_ball.Diameter * m_c.m_scale);
            int padding = m_c.m_padding;

            Rectangle ballRect = new Rectangle((int)(a_ball.Position.X * m_c.m_scale + padding - size / 2), (int)(a_ball.Position.Y * m_c.m_scale + padding - size / 2), size, size);
            
            m_spriteBatch.Begin();

            m_spriteBatch.Draw(m_ballTexture, ballRect, Color.White);

            m_spriteBatch.Draw(m_lineTexture, new Rectangle(padding, padding, m_c.m_width, 1), Color.White);
            m_spriteBatch.Draw(m_lineTexture, new Rectangle(padding, padding, 1, m_c.m_height), Color.White);
            m_spriteBatch.Draw(m_lineTexture, new Rectangle(m_c.m_width + padding, padding, 1, m_c.m_height), Color.White);
            m_spriteBatch.Draw(m_lineTexture, new Rectangle(padding, m_c.m_height + padding, m_c.m_width, 1), Color.White);

            m_spriteBatch.End();
        }
    }
}