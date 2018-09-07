/*********************************************************************
*
*   Class:
*       DrawShape
*
*   Description:
*       Draws shapes
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/*--------------------------------------------------------------------
                           NAMESPACE
--------------------------------------------------------------------*/

namespace minesweeper {

/*--------------------------------------------------------------------
                           DELEGATES
--------------------------------------------------------------------*/

/*--------------------------------------------------------------------
                             CLASS
--------------------------------------------------------------------*/

public static class DrawShape {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

private static Texture2D solid;
private static SpriteBatch spriteBatch;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       Initialize
*
*   Description:
*       Saves the spritebatch
*
***********************************************************/

public static void Initialize( SpriteBatch _spriteBatch )
{
spriteBatch = _spriteBatch;

} /* Initialize() */


/***********************************************************
*
*   Method:
*       LoadTextures
*
*   Description:
*       Loads the necessary textures for drawing
*
***********************************************************/

public static void LoadTextures( ContentManager Content )
{

/*----------------------------------------------------------
Load necessary textures
----------------------------------------------------------*/
solid = Content.Load<Texture2D>( "solid" );

} /* LoadTextures() */


/***********************************************************
*
*   Method:
*       Rectangle
*
*   Description:
*       Draws rectangle at given location with given color
*
***********************************************************/

public static void Rectangle( Rectangle r, Color c )
{
/*----------------------------------------------------------
Draw the rectangle with the given color
----------------------------------------------------------*/
spriteBatch.Begin();
spriteBatch.Draw( solid, r, c );
spriteBatch.End();

} /* Rectangle() */


/***********************************************************
*
*   Method:
*       BorderedRectangle
*
*   Description:
*       Draws rectangle at given location with given color
*       and with given border
*
***********************************************************/

public static void BorderedRectangle( Rectangle r, int b_width, Color r_color, Color b_color )
{
/*----------------------------------------------------------
Determine inner rectangle size
----------------------------------------------------------*/
Rectangle inner = new Rectangle( r.X + b_width, r.Y + b_width, r.Width - 2 * b_width, r.Height - 2 * b_width );

/*----------------------------------------------------------
Draw the rectangle with the given color
----------------------------------------------------------*/
spriteBatch.Begin();
spriteBatch.Draw( solid, r,     b_color );
spriteBatch.Draw( solid, inner, r_color );
spriteBatch.End();

} /* BorderedRectangle() */


}
}
