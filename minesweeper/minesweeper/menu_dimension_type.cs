/*********************************************************************
*
*   Class:
*       menu_dimension_type
*
*   Description:
*       Contains dimensions for a given menu
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using System;
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

public class menu_dimension_type {

/*--------------------------------------------------------------------
                           CONSTANTS
--------------------------------------------------------------------*/

private const int       name_spacing = 14;
private const int       highlight_spacing = 6;
private const int       item_spacing = 3;

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

public  Rectangle       toolbar_loc;
public  Rectangle       name_loc;
public  Rectangle       highlight_loc;
public  Rectangle       menu_loc;
public  Vector2[]       item_str_locs;
public  Rectangle[]     item_hglt_locs;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       menu_dimension_type
*
*   Description:
*       Constructor.
*
***********************************************************/

public menu_dimension_type( SpriteFont f, menu_type menu, Vector2 origin, int menu_height )
{
item_str_locs = new Vector2[menu.items.Count];
item_hglt_locs = new Rectangle[menu.items.Count];

Vector2 string_size = f.MeasureString( menu.name );
int i;

/*----------------------------------------------------------
Initialize
----------------------------------------------------------*/
toolbar_loc.X = (int)origin.X;
toolbar_loc.Y = (int)origin.Y;
toolbar_loc.Height = menu_height;
toolbar_loc.Width = (int)string_size.X + ( 2 * name_spacing );

name_loc.Width = (int)string_size.X;
name_loc.Height = (int)string_size.Y;
name_loc.X = toolbar_loc.Center.X - name_loc.Width  / 2 - 1;
name_loc.Y = toolbar_loc.Center.Y - name_loc.Height / 2;

highlight_loc.Width = toolbar_loc.Width - 2 * highlight_spacing;
highlight_loc.Height = toolbar_loc.Height - highlight_spacing;
highlight_loc.X = toolbar_loc.Center.X - highlight_loc.Width  / 2;
highlight_loc.Y = toolbar_loc.Center.Y - highlight_loc.Height / 2;

menu_loc.X = highlight_loc.X;
menu_loc.Y = highlight_loc.Y + highlight_loc.Height - 1;
menu_loc.Width = 0;
menu_loc.Height = 0;

i = 0;
foreach( menu_item_type item in menu.items )
    {
    Vector2 str_size = f.MeasureString( item.name );
    Vector2 str_loc = new Vector2();
    Rectangle hglt_loc = new Rectangle();

    str_loc.X = highlight_loc.X + name_spacing;
    str_loc.Y = menu_loc.Y + menu_loc.Height + item_spacing;

    hglt_loc.Width  = (int)str_size.X + 2 * highlight_spacing;
    hglt_loc.Height = (int)str_size.Y + 2 * item_spacing;
    hglt_loc.X = highlight_loc.X + 1;
    hglt_loc.Y = menu_loc.Y + menu_loc.Height;

    menu_loc.Height += ( (int)str_size.Y + 2 * item_spacing );
    menu_loc.Width = Math.Max( menu_loc.Width, ( (int)str_size.X + 2 * name_spacing ) );

    if( 0 == i )
        {
        hglt_loc.Height--;
        hglt_loc.Y++;
        }
    else if( ( menu.items.Count - 1 ) == i )
        {
        hglt_loc.Height--;
        }

    item_str_locs[i] = str_loc;
    item_hglt_locs[i] = hglt_loc;

    i++;
    }

i = 0;
foreach( menu_item_type item in menu.items )
    {
    item_hglt_locs[i].Width = menu_loc.Width - 2;
    i++;
    }

} /* menu_dimension_type() */

}
}
