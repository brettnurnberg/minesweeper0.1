/*********************************************************************
*
*   Class:
*       Game1
*
*   Description:
*       The main class for the game. Contains methods to initialize
*       game, read input, update game logic, and update the view.
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*--------------------------------------------------------------------
                           NAMESPACE
--------------------------------------------------------------------*/

namespace minesweeper{

/*--------------------------------------------------------------------
                           DELEGATES
--------------------------------------------------------------------*/

/*--------------------------------------------------------------------
                             CLASS
--------------------------------------------------------------------*/

public class Game1 : Game{

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

GraphicsDeviceManager   graphics;
SpriteBatch             spriteBatch;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       Draw
*
*   Description:
*       Draws the game.
*
***********************************************************/

protected override void Draw
    (
    GameTime gameTime
    )
{
GraphicsDevice.Clear(new Color(189, 189, 189));

base.Draw(gameTime);
} /* Draw() */


/***********************************************************
*
*   Method:
*       Game1
*
*   Description:
*       Constructor.
*
***********************************************************/

public Game1()
{
graphics = new GraphicsDeviceManager(this);
Content.RootDirectory = "Content";

} /* Game1() */


/***********************************************************
*
*   Method:
*       Initialize
*
*   Description:
*       Query for any required services and load any
*       non-graphic related content.
*
***********************************************************/

protected override void Initialize()
{
IsMouseVisible = true;
graphics.PreferredBackBufferWidth = 164;
graphics.PreferredBackBufferHeight = 207;
graphics.ApplyChanges();

/*----------------------------------------------------------
Enumerate through and initialize all components
----------------------------------------------------------*/
base.Initialize();

} /* Initialize() */


/***********************************************************
*
*   Method:
*       LoadContent
*
*   Description:
*       Called once per game. Loads all content.
*
***********************************************************/

protected override void LoadContent()
{
/*----------------------------------------------------------
Create SpriteBatch to draw textures
----------------------------------------------------------*/
spriteBatch = new SpriteBatch(GraphicsDevice);

} /* LoadContent() */


/***********************************************************
*
*   Method:
*       UnloadContent
*
*   Description:
*       Called once per game. Unloads game-specific content.
*
***********************************************************/

protected override void UnloadContent()
{

} /* UnloadContent() */


/***********************************************************
*
*   Method:
*       Update
*
*   Description:
*       Runs logic such as updating the world, checking for
*       collisions, gathering input, and playing audio.
*
***********************************************************/

protected override void Update
    (
    GameTime gameTime
    )
{
/*----------------------------------------------------------
Check if game should be exited
----------------------------------------------------------*/
if( ( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ) ||
    ( Keyboard.GetState().IsKeyDown(Keys.Escape) ) )
    {
    Exit();
    }

base.Update(gameTime);

} /* Update() */


}}
