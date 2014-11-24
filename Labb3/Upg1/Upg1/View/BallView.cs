using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upg1.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace Upg1.View
{
    class BallView
    {
        private Texture2D m_ballTexture;
        private Texture2D m_deadBallTexture;
        private Texture2D m_lineTexture;

        private Color[] m_colorData = { Color.Black };

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content, GraphicsDevice a_device)
        {
            m_ballTexture = a_content.Load<Texture2D>("Ball");
            m_deadBallTexture = a_content.Load<Texture2D>("dead");
            m_lineTexture = new Texture2D(a_device, 1, 1);
            m_lineTexture.SetData<Color>(m_colorData);
        }
        
        public BallView()
        {
        }

        public void DrawSimulation(ReadOnlyCollection<Ball> a_balls, float a_elapsedMilliseconds, SpriteBatch a_spriteBatch, Camera a_camera)
        {
            foreach (Ball ball in a_balls)
            {
                int size = (int)(ball.Diameter * a_camera.Scale);
                int padding = a_camera.m_padding;

                Rectangle ballRect = new Rectangle((int)(ball.Position.X * a_camera.Scale + padding - size / 2), (int)(ball.Position.Y * a_camera.Scale + padding - size / 2), size, size);

                a_spriteBatch.Begin();

                if (ball.alive)
                    a_spriteBatch.Draw(m_ballTexture, ballRect, Color.White);
                else
                    a_spriteBatch.Draw(m_deadBallTexture, ballRect, Color.White);

                a_spriteBatch.Draw(m_lineTexture, new Rectangle(padding, padding, a_camera.m_frameWidth, 1), Color.White);
                a_spriteBatch.Draw(m_lineTexture, new Rectangle(padding, padding, 1, a_camera.m_frameHeight), Color.White);
                a_spriteBatch.Draw(m_lineTexture, new Rectangle(a_camera.m_frameWidth + padding, padding, 1, a_camera.m_frameHeight), Color.White);
                a_spriteBatch.Draw(m_lineTexture, new Rectangle(padding, a_camera.m_frameHeight + padding, a_camera.m_frameWidth, 1), Color.White);

                a_spriteBatch.End();
            }
        }
    }
}