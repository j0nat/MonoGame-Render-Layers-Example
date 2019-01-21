# MonoGame Render Layers Example

![Preview1](./Media/preview.gif)

This example shows how you can use RenderTarget2D and DepthStencilState to combine level / background graphics for your game.

This method was used for our game [STWFA](http://jonoie.com/STWFA/) to combine levels with the applied shaders and a background image plus moving shadows or rain. In our case DepthStencilState was used to ensure that the rain was only rendered on the background and not the scene where the player was.

See WorldRenderer.cs and LevelRenderer.cs for the example code.