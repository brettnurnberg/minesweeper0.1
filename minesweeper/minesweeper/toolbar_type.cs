/*********************************************************************
*
*   Class:
*       toolbar_type
*
*   Description:
*       Creates a tool bar
*           should contain size of menu bar and list of menus
*           should also contain mouse hover behavior for bar and menus
*       add a tool bar controller? or combine it all here?
*       add menu to mouse game_status options - must check if a menu is open before field handling
*       don't clear window every time?
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

public class toolbar_type {

/*--------------------------------------------------------------------
                           CONSTANTS
--------------------------------------------------------------------*/

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

private Rectangle           location;
private Color               color;
private List<menu_type>     menus;
private menu_type           open_menu;
private int                 highlight_idx;
private SpriteFont          font;
private SpriteBatch         spriteBatch;
private Color               h_color;
private Color               hb_color;
private mouse_state_type    mouse;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       toolbar_type
*
*   Description:
*       Constructor.
*
***********************************************************/

public toolbar_type()
{
/*----------------------------------------------------------
Initialize
----------------------------------------------------------*/
menus = new List<menu_type>();
open_menu = null;
highlight_idx = -1;
h_color  = new Color( 179, 215, 243 );
hb_color = new Color(   0, 120, 215 );
mouse = new mouse_state_type();

} /* toolbar_type() */


/***********************************************************
*
*   Method:
*       Initialize
*
*   Description:
*       Initialize.
*
***********************************************************/

public void Initialize( SpriteBatch s, SpriteFont f )
{
/*----------------------------------------------------------
Initialize
----------------------------------------------------------*/
spriteBatch = s;
font = f;

} /* Initialize() */


/***********************************************************
*
*   Method:
*       configure
*
*   Description:
*       Sets the location and color of the tool bar
*
***********************************************************/

public void configure( Rectangle menu_location, Color menu_color, SpriteFont menu_font )
{
int i = 0;

/*----------------------------------------------------------
Reset tool bar location
----------------------------------------------------------*/
location = menu_location;
color = menu_color;
font = menu_font;

foreach( menu_type m in menus )
    {
    Vector2 origin;

    /*----------------------------------------------------------
    Determine origin point of menu
    ----------------------------------------------------------*/
    if( i > 0 )
        {
        origin = new Vector2( m.dims.toolbar_loc.Right, m.dims.toolbar_loc.Top );
        }
    else
        {
        origin = new Vector2( location.X, location.Y );
        }

    /*----------------------------------------------------------
    Set the menu dimensions
    ----------------------------------------------------------*/
    menu_dimension_type d = new menu_dimension_type( font, m, origin, location.Height );
    m.dims = d;

    i++;
    }

} /* configure() */


/***********************************************************
*
*   Method:
*       input_handled
*
*   Description:
*       Handle inputs to the menu. Returns true if the
*       input was handled by the menu.
*
***********************************************************/

public Boolean input_handled()
{
Boolean handled = false;
mouse.update();
int i = 0;

if( null != open_menu )
    {
    foreach( menu_item_type item in open_menu.items )
        {
        if( open_menu.dims.item_hglt_locs[i].Contains( mouse.cursor ) )
            {
            if( ( button_state_type.UNHELD == mouse.left ) ||
                ( button_state_type.HELD   == mouse.left ) )
                {
                item.highlight = true;
                }
            else if( button_state_type.UNCLICKED == mouse.left )
                {
                open_menu = null;
                item.highlight = false;
                item.handler();
                break;
                }

            handled = true;
            }
        else
            {
            item.highlight = false;
            }
        i++;
        }

    /*----------------------------------------------------------
    If click down occurs outside open menu, close the menu
    ----------------------------------------------------------*/
    if( ( null != open_menu ) &&
        ( button_state_type.UNCLICKED == mouse.left ) &&
        ( ( !open_menu.dims.menu_loc.Contains( mouse.cursor ) ) &&
          ( !open_menu.dims.name_loc.Contains( mouse.cursor ) ) ) )
        {
        open_menu = null;
        }

    handled = true;
    }

/*----------------------------------------------------------
Check if the mouse is on a menu option
----------------------------------------------------------*/
foreach( menu_type m in menus )
    {
    if( m.dims.highlight_loc.Contains( mouse.cursor ) )
        {
        if( button_state_type.UNHELD == mouse.left )
            {
            m.highlight = true;
            }
        else if( button_state_type.CLICKED == mouse.left )
            {
            if( open_menu == m )
                {
                open_menu = null;
                }
            else
                {
                open_menu = m;
                }
            }

        handled = true;
        }
    else
        {
        m.highlight = false;
        }
    }

return handled;
} /* input_handled() */


/***********************************************************
*
*   Method:
*       draw
*
*   Description:
*       Draws the menu
*
***********************************************************/

public void draw()
{
/*----------------------------------------------------------
Draw the toolbar
----------------------------------------------------------*/
DrawShape.Rectangle( location, color );

/*----------------------------------------------------------
Draw the highlight
----------------------------------------------------------*/
foreach( menu_type menu in menus )
    {
    if( menu.highlight )
        {
        DrawShape.BorderedRectangle( menu.dims.highlight_loc, 1, h_color, hb_color );
        }
    }

/*----------------------------------------------------------
Draw the text
----------------------------------------------------------*/
spriteBatch.Begin();
foreach( menu_type menu in menus )
    {
    spriteBatch.DrawString( font, menu.name, new Vector2( menu.dims.name_loc.X, menu.dims.name_loc.Y ), Color.Black );
    }
spriteBatch.End();

/*----------------------------------------------------------
Draw the open menu
----------------------------------------------------------*/
if( null != open_menu )
    {
    open_menu.draw( spriteBatch, font, h_color, hb_color );
    }

} /* draw() */


/***********************************************************
*
*   Method:
*       add_menu
*
*   Description:
*       Adds menu to the toolbar.
*
***********************************************************/

public void add_menu( menu_type menu )
{
/*----------------------------------------------------------
Local variable
----------------------------------------------------------*/
Vector2 origin;

/*----------------------------------------------------------
Determine origin point of menu
----------------------------------------------------------*/
if( menus.Count > 0 )
    {
    menu_type m = menus[menus.Count - 1];
    origin = new Vector2( m.dims.toolbar_loc.Right, m.dims.toolbar_loc.Top );
    }
else
    {
    origin = new Vector2( location.X, location.Y );
    }

/*----------------------------------------------------------
Set the menu dimensions
----------------------------------------------------------*/
menu_dimension_type d = new menu_dimension_type( font, menu, origin, location.Height );
menu.dims = d;

/*----------------------------------------------------------
Add the menu to the toolbar
----------------------------------------------------------*/
menus.Add( menu );

} /* add_menu() */


}
}
