#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Labb1.Model;
using Labb1.View;
#endregion

namespace Labb1
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

        private Color[] m_colorData = { Color.White };

        public MasterController()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;

            m_bs = new BallSimulation();
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

            Texture2D ballTexture = Content.Load<Texture2D>("Ball");
            Texture2D lineTexture = new Texture2D(GraphicsDevice, 1, 1);
            lineTexture.SetData<Color>(m_colorData);

            Camera camera = new Camera(GraphicsDevice.Viewport);

            m_bv = new BallView(spriteBatch, ballTexture, lineTexture, camera);

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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            //Draw ball.
            m_bv.DrawSimulation(m_bs.Ball, gameTime.ElapsedGameTime.Milliseconds);

            base.Draw(gameTime);
        }
    }
}