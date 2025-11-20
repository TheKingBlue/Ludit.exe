using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Ludit.exe.Game.World;

namespace Ludit.exe;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Tilemap _tilemap; // stores dungeon grid
    private Dictionary<TileType, Texture2D> _tileTextures; // Maps each tile to the correct PNG
    private const int TILE_SIZE = 32; // Size of each tile

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        int seed = 12345;
        RNGService.Initialize(seed);

        _tilemap = new Tilemap(50, 50);

        DungeonGenerator dungeonGenerator = new DungeonGenerator(_tilemap);
        dungeonGenerator.Generate();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _tileTextures = new Dictionary<TileType, Texture2D>();

        // Load textures from Content folder
        _tileTextures[TileType.Floor] = Content.Load<Texture2D>("Textures/Tilesets/Floor_Tile");
        _tileTextures[TileType.Wall] = Content.Load<Texture2D>("Textures/Tilesets/Wall_Tile");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _tilemap.Draw(_spriteBatch, _tileTextures, TILE_SIZE);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
