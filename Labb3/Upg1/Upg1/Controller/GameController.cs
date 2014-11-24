using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upg1.View;

namespace Upg1.Controller
{
    class GameController
    {
        private ContentManager m_content;
        private GameView m_gameView;

        private List<EffectSystem> m_effects;

        public GameController(ContentManager a_content)
        {
            m_content = a_content;
            m_effects = new List<EffectSystem>();
            m_gameView = new GameView();
        }

        //Returns true if the mouse was clicked
        public Vector2 HandleMouseInput(Camera a_camera)
        {
            if (m_gameView.CheckForLeftClick())
            {
                Vector2 pos = a_camera.ConvertToModelCoords(new Vector2(Mouse.GetState().X + a_camera.m_padding, Mouse.GetState().Y + a_camera.m_padding));
                m_effects.Add(new EffectSystem(pos, m_content));
                return pos;
            }

            return Vector2.Zero;
        }

        public void UpdateEffects(GameTime a_gameTime)
        {
            foreach (EffectSystem effect in m_effects)
            {
                effect.Update(a_gameTime);
            }
        }

        public void DrawEffects(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            foreach (EffectSystem effect in m_effects)
            {
                effect.Draw(a_spriteBatch, a_camera);
            }
        }
    }
}