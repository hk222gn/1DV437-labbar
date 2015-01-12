using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GetToTheEnd.Model;
using GetToTheEnd.View;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GetToTheEnd.Controller
{
    class GameHandler : Handler
    {
        private ContentManager m_content;
        private GameModel m_gameModel;
        private GameView m_gameView;
        private Camera m_camera;
        private GraphicsDevice m_graphicsDevice;
        private Level m_level;
        private SplitterSystem m_particles;

        private bool m_isPaused = false;
        private bool m_levelComplete = false;
        private float m_deathAnimationTimer = 0f;
        private float m_deathAnimationEnd = 0.7f;

        private CurrentLevel m_currentLevel = CurrentLevel.level1;

        public GameHandler(EventHandler a_handlerEvent, ContentManager a_content, GraphicsDevice a_graphicsDevice)
            :base(a_handlerEvent)
        {
            m_content = a_content;
            m_graphicsDevice = a_graphicsDevice;

            m_level = new Level();
            m_level.LoadLevel(m_currentLevel);
            m_gameModel = new GameModel(m_level);
            m_gameView = new GameView(a_content);
            m_camera = new Camera(a_graphicsDevice);
        }
     
        public override void Update(Microsoft.Xna.Framework.GameTime a_gameTime)
        {
            //Handle paused state
            if (m_isPaused)
            {
                if (m_gameView.DidPlayerPressQuit())
                    m_handlerEvent.Invoke(this, new EventArgs());
                if (m_gameView.DidPlayerPressPause())
                    m_isPaused = !m_isPaused;
            }
            else if (m_levelComplete)
            {
                if (m_gameView.DidPlayerPressQuit())
                {
                    if (m_currentLevel == CurrentLevel.END)
                    {
                        m_handlerEvent.Invoke(this, new EventArgs());
                    }
                    else
                        ResetGameAtCurrentLevel();
                }
            }
            else if (m_gameModel.IsPlayerDead())
            {
                //Add an if for a timer here, so if the timer is under 1sec or w/e, these ifs don't run. When that's done, destroy the particles and display the death screen like before.
                if (m_deathAnimationTimer < m_deathAnimationEnd)
                {
                    m_deathAnimationTimer += (float)a_gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (m_gameView.DidPlayerPressPause())
                {
                    m_handlerEvent.Invoke(this, new EventArgs());
                }
                else if (m_gameView.DidPlayerPressQuit())
                {
                    ResetGameAtCurrentLevel();
                }

                if (m_deathAnimationTimer > m_deathAnimationEnd && m_particles != null)
                {
                    m_particles = null;
                }
                else if (m_particles != null)
                    m_particles.Update(a_gameTime);
            }
            else
            {
                if (m_gameView.DidPlayerPressPause())
                    m_isPaused = !m_isPaused;

                //Input handling
                if (m_gameView.DidPlayerMoveLeft())
                    m_gameModel.movePlayerLeft();
                else if (m_gameView.DidPlayerMoveRight())
                    m_gameModel.movePlayerRight();
                else
                    m_gameModel.StopPlayerMovingSideways();

                if (m_gameView.DidPlayerJump())
                {
                    if (m_gameModel.Jump())
                        m_gameView.PlayJumpSound();
                }

                m_gameModel.Update((float)a_gameTime.ElapsedGameTime.TotalSeconds, m_camera);

                if (m_level.DidPlayIntersectWithLethalTile())
                {
                    m_gameModel.KillPlayer();

                    Vector2 playerPos = m_gameModel.GetPlayerPos();

                    m_particles = new SplitterSystem(playerPos);
                    m_particles.LoadContent(m_content);
                    m_gameView.PlayDeathSound();
                }
            }
            if (m_level.DidPlayerHitPortal() && !m_levelComplete)
            {
                m_currentLevel++;
                m_levelComplete = true;
            }

            m_gameView.UpdateKeyboard();

            base.Update(a_gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch a_spriteBatch)
        {
            if (m_isPaused)
            {
                m_gameView.DrawPauseMenu(a_spriteBatch, m_camera);
            }
            else if (m_levelComplete)
            {
                if (m_currentLevel == CurrentLevel.END)
                {
                    m_gameView.RenderGameWonScreen(a_spriteBatch, m_camera);
                }
                else
                m_gameView.RenderLevelCompleteScreen(a_spriteBatch, m_camera);
            }
            else if (m_gameModel.IsPlayerDead() && m_deathAnimationTimer > m_deathAnimationEnd)
            {
                m_gameView.RenderDeathScreen(a_spriteBatch, m_camera);
            }
            else
                m_gameView.DrawLevel(m_level, m_gameModel.GetPlayerTextureSize(), a_spriteBatch, m_gameModel.GetPlayerPos(), m_camera);

            if (m_particles != null)
            {
                m_particles.Draw(a_spriteBatch, m_camera);
            }

            base.Draw(a_spriteBatch);
        }

        private void ResetGameAtCurrentLevel()
        {
            m_level = new Level();
            m_level.LoadLevel(m_currentLevel);
            m_gameModel = new GameModel(m_level);
            m_deathAnimationTimer = 0f;
            m_levelComplete = false;
            m_isPaused = false;
        }
    }
}
