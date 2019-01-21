using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GraphicLayersShowcase
{
    //
    // LevelRenderer is where we draw only the level scenery (for example a room) to a render target
    // so that it can be drawn together with the rest of the scene.
    //
    class LevelRenderer
    {
        private Texture2D levelTexture;
        private Vector2 levelPosition;
        private Effect effect;
        private RenderTarget2D levelTarget;
        private Vector2 lightPosition;
        private Vector2 lightStartPosition;
        private Vector4 lightColor;
        private float lightIntensity;
        private float lightRadius;
        private float lightAngle;

        public LevelRenderer()
        {
            levelPosition = new Vector2(200, 75);
            lightPosition = new Vector2(700, 500);
            lightStartPosition = new Vector2(lightPosition.X, lightPosition.Y);
            lightColor = new Vector4(255, 255, 255, 70);
            lightIntensity = 0.1f;
            lightRadius = 4f;
            lightAngle = 0;
        }

        public void LoadContent(Game game)
        {
            // Load the bear texture to levelTexture
            levelTexture = game.Content.Load<Texture2D>("bear");

            // Load the effect file ligt.fx. This file is a HLSL shader used for lights.
            effect = game.Content.Load<Effect>("light");
            effect.CurrentTechnique = effect.Techniques["Technique1"];
            effect.Parameters["screenWidth"].SetValue((float)game.GraphicsDevice.Viewport.Width);
            effect.Parameters["screenHeight"].SetValue((float)game.GraphicsDevice.Viewport.Height);
            effect.Parameters["lightColor"].SetValue(lightColor);
            effect.Parameters["lightIntensity"].SetValue(lightIntensity);
            effect.Parameters["lightRadius"].SetValue(lightRadius);

            // The RenderTarget2D that the scene will be drawn to.
            levelTarget = new RenderTarget2D(game.GraphicsDevice,
                game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
        }

        public void DrawRenderTarget(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            // Tells the graphics device to set render to levelTarget so we can draw to it.
            graphicsDevice.SetRenderTarget(levelTarget);

            // Clear levelTarget
            graphicsDevice.Clear(Color.Transparent);

            // Draw the bear to the render target
            spriteBatch.Begin();
            spriteBatch.Draw(levelTexture, new Rectangle((int)levelPosition.X, (int)levelPosition.Y, levelTexture.Width, levelTexture.Height), Color.White);
            spriteBatch.End();

            // Reset graphics device target
            graphicsDevice.SetRenderTarget(null);
        }

        public void Update(GameTime gameTime)
        {
            // Moves the light in a circle

            float radius = 120;

            float newLightPositionX = (float)((lightStartPosition.X / 2) + radius * Math.Cos(lightAngle));
            float newLightPositionY = (float)((lightStartPosition.Y / 2) + radius * Math.Sin(lightAngle));

            lightPosition = new Vector2(newLightPositionX, newLightPositionY);

            lightAngle += 0.05f;

            effect.Parameters["lightPosition"].SetValue(lightPosition);
        }

        public RenderTarget2D GetRenderTarget()
        {
            return levelTarget;
        }

        public Effect GetEffect()
        {
            return effect;
        }
    }
}
