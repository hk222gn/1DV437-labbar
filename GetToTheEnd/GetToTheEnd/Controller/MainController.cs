#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using GetToTheEnd.View;
using GetToTheEnd.Model;
#endregion

namespace GetToTheEnd.Controller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainController : Game
    {
        //State handlers
        private Handler m_activeHandler;
        private MenuHandler m_menuHandler;
        private GameHandler m_gameHandler;
        private OptionsHandler m_optionsHandler;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MainController()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 900;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            m_menuHandler = new MenuHandler(new EventHandler(MenuHandlerEvent), this.Content);
            m_gameHandler = new GameHandler(new EventHandler(GameHandlerEvent), this.Content, GraphicsDevice);
            m_optionsHandler = new OptionsHandler(new EventHandler(OptionsHandlerEvent), this.Content);

            m_activeHandler = m_menuHandler;

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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Delete))
                Exit();

            m_activeHandler.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            m_activeHandler.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void MenuHandlerEvent(object obj, EventArgs e)
        {
            MyEventArgs me;
            try 
	        {	        
		        me = (MyEventArgs)e;
	        }
	        catch (Exception)
	        {
                //TODO: Do something about this
		        throw;
	        }

            switch (me.MenuItemSelected)
            {
                case MenuItems.StartGame:
                {
                    m_activeHandler = new GameHandler(new EventHandler(GameHandlerEvent), this.Content, GraphicsDevice);
                    break;
                }
                case MenuItems.Options:
                {
                    m_activeHandler = m_optionsHandler;
                    break;
                }
                case MenuItems.Exit:
                {
                    Exit();
                    break;
                }
                default:
                break;
            }
        }

        public void GameHandlerEvent(object obj, EventArgs e)
        {
            m_activeHandler = m_menuHandler;
        }

        public void OptionsHandlerEvent(object obj, EventArgs e)
        {
            m_activeHandler = m_menuHandler;
        }
    }
}
