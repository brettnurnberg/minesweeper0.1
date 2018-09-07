/*********************************************************************
*
*   Class:
*       menu_type
*
*   Description:
*       Creates a menu
*           should contain list of menu items
*           should contain height and width according to widest item
*               list item is separate class
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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

public class menu_type {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

public string name;
public menu_dimension_type dims;
public List<menu_item_type> items;
public Boolean highlight;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       menu_type
*
*   Description:
*       Constructor.
*
***********************************************************/

public menu_type( string menu_name )
{
/*----------------------------------------------------------
Initialize
----------------------------------------------------------*/
name = menu_name;
items = new List<menu_item_type>();
highlight = false;

} /* menu_type() */


/***********************************************************
*
*   Method:
*       draw
*
*   Description:
*       Draws the menu.
*
***********************************************************/

public void draw( SpriteBatch spriteBatch, SpriteFont font, Color h_color, Color hb_color )
{
int i = 0;

/*----------------------------------------------------------
Initialize
----------------------------------------------------------*/
DrawShape.BorderedRectangle( dims.highlight_loc, 1, new Color( 252, 252, 252 ), new Color( 128, 128, 128 ) );
DrawShape.BorderedRectangle( dims.menu_loc, 1, new Color( 253, 253, 253 ), new Color( 128, 128, 128 ) );
DrawShape.Rectangle( new Rectangle( dims.highlight_loc.X + 1, dims.menu_loc.Y, dims.highlight_loc.Width - 2, 1 ), new Color( 253, 253, 253 ) );

foreach( menu_item_type item in items )
    {
    if( item.highlight )
        {
        DrawShape.BorderedRectangle( dims.item_hglt_locs[i], 1, h_color, hb_color );
        }
    i++;
    }

spriteBatch.Begin();
spriteBatch.DrawString( font, name, new Vector2( dims.name_loc.X, dims.name_loc.Y ), Color.Black );
i = 0;
foreach( menu_item_type item in items )
    {
    spriteBatch.DrawString( font, item.name, new Vector2( dims.item_str_locs[i].X, dims.item_str_locs[i].Y ), Color.Black );
    i++;
    }

spriteBatch.End();

} /* draw() */


/***********************************************************
*
*   Method:
*       add_item
*
*   Description:
*       Adds item to the given menu
*
***********************************************************/

public void add_item( string name, menu_item_handler hndlr )
{
/*----------------------------------------------------------
Create item and add to list
----------------------------------------------------------*/
items.Add( new menu_item_type( name, hndlr ) );

} /* add_item() */

}
}
