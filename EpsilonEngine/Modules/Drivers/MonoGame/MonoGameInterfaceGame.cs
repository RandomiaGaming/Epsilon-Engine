﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace EpsilonEngine.Modules.Drivers.MonoGame
{
    public class MonogameInterfaceGame : Microsoft.Xna.Framework.Game
    {
        private static List<MonogameInterfaceGame> MGWInterfaceGames = new List<MonogameInterfaceGame>();
        public static MonogameInterfaceGame GetFromEpsilonGame(Game game)
        {
            if (MGWInterfaceGames == null)
            {
                MGWInterfaceGames = new List<MonogameInterfaceGame>();
            }
            for (int i = 0; i < MGWInterfaceGames.Count; i++)
            {
                if (MGWInterfaceGames[i].epsilonGame == game)
                {
                    return MGWInterfaceGames[i];
                }
            }
            return null;
        }

        public Texture frameBuffer = null;
        private readonly Game epsilonGame = null;
        public MonogameInterfaceGame(Game epsilonGame)
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this)
            {
                SynchronizeWithVerticalRetrace = false
            };
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;
            Window.IsBorderless = false;
            Window.Title = "Epsilon - 1.0 - RandomiaGaming";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan(10000000 / 60);
            this.epsilonGame = epsilonGame;

            if (MGWInterfaceGames == null)
            {
                MGWInterfaceGames = new List<MonogameInterfaceGame>();
            }

            MGWInterfaceGames.Add(this);
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            //Console.WriteLine($"{gameTime.ElapsedGameTime.Ticks / 1000}k TPF");
            epsilonGame.Update();
            if (epsilonGame.requestingToQuit)
            {
                Exit();
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            if (frameBuffer.width > 0 && frameBuffer.height > 0)
            {
                Texture2D frame = new Texture2D(GraphicsDevice, frameBuffer.width, frameBuffer.height);
                Microsoft.Xna.Framework.Color[] data = new Microsoft.Xna.Framework.Color[frameBuffer.width * frameBuffer.height];
                int i = 0;
                for (int y = 0; y < frameBuffer.height; y++)
                {
                    for (int x = 0; x < frameBuffer.width; x++)
                    {
                        Color pixelColor = frameBuffer.GetPixelUnsafe((ushort)x, (ushort)(frameBuffer.height - y - 1));
                        data[i] = new Microsoft.Xna.Framework.Color(pixelColor.r, pixelColor.g, pixelColor.b);
                        i++;
                    }
                }
                frame.SetData(data);
                SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                spriteBatch.Draw(frame, new Microsoft.Xna.Framework.Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Microsoft.Xna.Framework.Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}