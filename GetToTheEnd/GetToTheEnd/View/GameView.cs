using GetToTheEnd.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheEnd.View
{
    class GameView
    {
        private SpriteFont m_font;
        private Texture2D m_playerTexture;

        //Tile textures
        private Texture2D m_emptyTexture;
        private Texture2D m_blockedTexture;
        private Texture2D m_lethalTexture;
        private Texture2D m_portalTexture;

        private KeyboardState m_keyboard;

        private SoundEffect m_deathSound;
        private SoundEffect m_jumpSound;

        private int m_textureTileSize = 30;
        public GameView(ContentManager a_content)
        {
            m_font = a_content.Load<SpriteFont>("Fonts/General");
            m_playerTexture = a_content.Load<Texture2D>("Player");
            m_emptyTexture = a_content.Load<Texture2D>("Empty");
            m_blockedTexture = a_content.Load<Texture2D>("Blocked");
            m_lethalTexture = a_content.Load<Texture2D>("Lethal");
            m_portalTexture = a_content.Load<Texture2D>("Portal");

            m_deathSound = a_content.Load<SoundEffect>("Sounds/Deathv2");
            m_jumpSound = a_content.Load<SoundEffect>("Sounds/jump");
        }

        internal void DrawLevel(Level a_level, Vector2 a_playerTextureSize, SpriteBatch a_spriteBatch, Vector2 a_position, Camera a_camera)
        {
            float scale = a_camera.GetScale();

            //draw all images
            a_spriteBatch.Begin();

            //draw level
            for (int x = 0; x < a_level.GetLevelWidth(); x++)
            {
                for (int y = 0; y < a_level.GetLevelHeight(); y++)
                {
                    Vector2 viewPos = a_camera.GetViewPosition(x, y);
                    DrawTile(viewPos.X / 30f, viewPos.Y / 30f, m_textureTileSize, a_level.m_tiles[x, y], a_spriteBatch);
                }
            }
            
            DrawPlayer(a_spriteBatch, a_position, a_playerTextureSize, a_camera);
            a_spriteBatch.End();
        }

        internal void DrawPlayer(SpriteBatch a_spriteBatch, Vector2 a_position, Vector2 a_playerTextureSize, Camera a_camera)
        {
            float scale = a_camera.GetScale();
            Vector2 viewPos = a_camera.GetViewPosition(a_position.X, a_position.Y);
            Rectangle destinationRectangle = new Rectangle((int)(viewPos.X - a_playerTextureSize.X / 2), (int)(viewPos.Y - a_playerTextureSize.Y), (int)a_playerTextureSize.X, (int)a_playerTextureSize.Y);

            a_spriteBatch.Draw(m_playerTexture, destinationRectangle, Color.AliceBlue);
        }

        private void DrawTile(float a_x, float a_y, float a_scale, Model.Tile a_tile, SpriteBatch a_spriteBatch)
        {
            //Get the source rectangle (pixels on the texture) for the tile type 
            Rectangle sourceRectangle = new Rectangle(0, 0, m_textureTileSize, m_textureTileSize);

            //Destination rectangle in windows coordinates only scaling
            Rectangle destRect = new Rectangle((int)a_x, (int)a_y, (int)(m_textureTileSize), (int)(m_textureTileSize));

            Texture2D textureToRender;

            switch (a_tile.GetTileType())
            {
                case TileType.EMPTY:
                    textureToRender = m_emptyTexture;
                    break;
                case TileType.BLOCKED:
                    textureToRender = m_blockedTexture;
                    break;
                case TileType.LETHAL:
                    textureToRender = m_lethalTexture;
                    break;
                case TileType.FAKE:
                    //It's rendered as a normal block, but it really aint.
                    textureToRender = m_blockedTexture;
                    break;
                case TileType.PORTAL:
                    textureToRender = m_portalTexture;
                    break;
                default:
                    textureToRender = m_emptyTexture;
                    break;
            }
            a_spriteBatch.Draw(textureToRender, destRect, sourceRectangle, Color.White);
        }
 
        public bool DidPlayerJump()
        {
            Keys activeKey = Keys.C;
            
            return Keyboard.GetState().IsKeyDown(activeKey) && Keyboard.GetState() != m_keyboard;
        }

        public bool DidPlayerMoveRight()
        {
            Keys activeKey = Keys.Right;

            return Keyboard.GetState().IsKeyDown(activeKey);
        }

        public bool DidPlayerMoveLeft()
        {
            Keys activeKey = Keys.Left;

            return Keyboard.GetState().IsKeyDown(activeKey);
        }

        public bool DidPlayerPressPause()
        {
            Keys activeKey = Keys.Escape;

            return Keyboard.GetState().IsKeyDown(activeKey) && Keyboard.GetState() != m_keyboard;
        }

        public bool DidPlayerPressQuit()
        {
            Keys activeKey = Keys.Enter;

            return Keyboard.GetState().IsKeyDown(activeKey) && Keyboard.GetState() != m_keyboard;
        }

        public void UpdateKeyboard()
        {
            m_keyboard = Keyboard.GetState();
        }

        public void PlayDeathSound()
        {
            m_deathSound.Play();
        }

        public void PlayJumpSound()
        {
            m_jumpSound.Play();
        }

        internal void DrawPauseMenu(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            float scale = a_camera.GetScale();

            //draw all images
            a_spriteBatch.Begin();
            a_spriteBatch.GraphicsDevice.Clear(Color.Gray);
            a_spriteBatch.DrawString(m_font, "Press escape to continue", new Vector2(0.3f * scale, 0.45f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "Enter will take you back to the menu", new Vector2(0.20f * scale, 0.5f * scale), Color.White);
            a_spriteBatch.End();
        }

        internal void RenderDeathScreen(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            float scale = a_camera.GetScale();

            //draw all images
            a_spriteBatch.Begin();
            a_spriteBatch.GraphicsDevice.Clear(Color.Black);
            a_spriteBatch.DrawString(m_font, "You died! If you read the instructions,", new Vector2(0.2f * scale, 0.35f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "you'd know better than to touch the spikes.", new Vector2(0.2f * scale, 0.40f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "Press escape to go back to the meny", new Vector2(0.2f * scale, 0.50f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "Press enter to restart the current level!", new Vector2(0.2f * scale, 0.555f * scale), Color.White);
            a_spriteBatch.End();
        }

        internal void RenderLevelCompleteScreen(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            float scale = a_camera.GetScale();

            //draw all images
            a_spriteBatch.Begin();
            a_spriteBatch.GraphicsDevice.Clear(Color.Black);
            a_spriteBatch.DrawString(m_font, "You completed the level!", new Vector2(0.2f * scale, 0.35f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "Press enter to go to the next one!", new Vector2(0.2f * scale, 0.5f * scale), Color.White);
            a_spriteBatch.End();
        }

        internal void RenderGameWonScreen(SpriteBatch a_spriteBatch, Camera a_camera)
        {
            float scale = a_camera.GetScale();

            //draw all images
            a_spriteBatch.Begin();
            a_spriteBatch.GraphicsDevice.Clear(Color.Black);
            a_spriteBatch.DrawString(m_font, "Congratulations!!", new Vector2(0.32f * scale, 0.10f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "You completed all the levels!", new Vector2(0.225f * scale, 0.35f * scale), Color.White);
            a_spriteBatch.DrawString(m_font, "Press enter to return to the menu!", new Vector2(0.19f * scale, 0.5f * scale), Color.White);
            a_spriteBatch.End();
        }
    }
}
