#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Upg1.Model;
using Upg1.View;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace Upg1.Controller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MasterController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BallSimulation m_bs;
        BallView m_bv;
        Camera m_camera;
        GameController m_gameCon;

        public MasterController()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600; 
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_camera = new Camera(GraphicsDevice.Viewport);
            m_bv = new BallView();
            m_bs = new BallSimulation();
            m_gameCon = new GameController(Content);

            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            m_bv.LoadContent(Content, GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Update BallSimulation
            m_bs.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            m_gameCon.UpdateEffects(gameTime);

            Vector2 pos = m_gameCon.HandleMouseInput(m_camera);
            if (pos != Vector2.Zero)
                m_bs.CheckForHit(pos, m_camera.ConvertToModelCoords(new Vector2(m_camera.m_mouseHitAreaX + m_camera.m_padding, m_camera.m_mouseHitAreaY + m_camera.m_padding)));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            //Draw ball.
            m_bv.DrawSimulation(m_bs.Balls, gameTime.ElapsedGameTime.Milliseconds, spriteBatch, m_camera);
            m_gameCon.DrawEffects(spriteBatch, m_camera);

            base.Draw(gameTime);
        }
    }
}