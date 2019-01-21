using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GraphicLayersShowcase
{
    //
    // WorldRenderer is where we draw the complete world complete with characters, level and background graphics
    //
    class WorldRenderer : DrawableGameComponent
    {
        private LevelRenderer levelRenderer;
        private SpriteBatch spriteBatch;
        private Texture2D textureBackground;

        private DepthStencilState depthStencil1;
        private DepthStencilState depthStencil2;

        public WorldRenderer(Game game) : base(game)
        {
            levelRenderer = new LevelRenderer();
        }

        protected override void LoadContent()
        {
            // Used for drawing
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load texture tile from content
            textureBackground = Game.Content.Load<Texture2D>("background");

            // Load the scene content 
            levelRenderer.LoadContent(this.Game);

            // Used to combine the two layers - backgrund and scene
            depthStencil1 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };

            depthStencil2 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.LessEqual,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // DrawRenderTarget draws the bear to the RenderTarget2D
            levelRenderer.DrawRenderTarget(GraphicsDevice, spriteBatch);

            // Draw the tiled background using DepthStencilState depthStencil1.
            // Uses SamplerState.LinearWrap to tile the texture.
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, depthStencil1, null, null);
            spriteBatch.Draw(textureBackground, new Vector2(0, 0),
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();

            // Draw the RenderTarget2D GetRenderTarget() using DepthStencilState depthStencil2
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearWrap, depthStencil2, null, null);
            spriteBatch.Draw(levelRenderer.GetRenderTarget(), Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // Tells scene to update. It moves the light around in a circle in this case.
            levelRenderer.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
