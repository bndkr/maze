using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mazegame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameState _gameState;
    private TileSet _tileSet;
    public static SpriteFont font;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _gameState = new GameState(10);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tileSet = new TileSet(this);
        font = Content.Load<SpriteFont>("MyFont");
    }

    protected override void Update(GameTime gameTime)
    {
        ProcessInput.processInput(_gameState);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        MazeDrawer.drawMaze(_gameState, _spriteBatch, _tileSet, gameTime, _graphics.GraphicsDevice);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
