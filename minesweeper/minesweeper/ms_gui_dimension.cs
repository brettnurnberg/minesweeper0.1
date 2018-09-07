/*********************************************************************
*
*   Class:
*       ms_gui_dimension
*
*   Description:
*       Contains gui dimensions
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

public class ms_gui_dimension {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

public  Rectangle       view;
public  Rectangle       outer_header;
public  Rectangle       inner_header;
public  Rectangle       border_image;
public  Rectangle       mine_field;
public  Rectangle       field_image;
public  Rectangle       digval_image;
public  Rectangle       minecount;
public  Rectangle       time;
public  Rectangle       face;
public  Rectangle       toolbar;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       ms_gui_dimension
*
*   Description:
*       Constructor.
*
***********************************************************/

public ms_gui_dimension()
{
view.X = 0;
view.Y = 0;

border_image.Width = 10;
border_image.Height = 16;
digval_image.Width = 13;
digval_image.Height = 23;
field_image.Width = 16;
field_image.Height = 16;
face.Width = 26;
face.Height = 26;

toolbar.Height = 25;
toolbar.X = view.X;
toolbar.Y = view.Y;

outer_header.Height = 57;
outer_header.X = view.X;
outer_header.Y = toolbar.Y + toolbar.Height;

inner_header.Height = outer_header.Height - 2 * border_image.Width;
inner_header.X = outer_header.X + border_image.Width;
inner_header.Y = outer_header.Y + border_image.Width;

face.Y = inner_header.Y + ( inner_header.Height - face.Height ) / 2;

mine_field.X = outer_header.X + border_image.Width;
mine_field.Y = outer_header.Y + outer_header.Height;

} /* ms_gui_dimension() */



/***********************************************************
*
*   Method:
*       reset
*
*   Description:
*       Reset the gui dimensions.
*
***********************************************************/

public void reset( int width, int height )
{
int temp_digval_gap;

view.Width = ( width * field_image.Width ) + ( border_image.Width * 2 );
view.Height = mine_field.Y + ( height * field_image.Height ) + border_image.Width;

toolbar.Width = view.Width;

outer_header.Width = view.Width;
inner_header.Width = outer_header.Width - ( 2 * border_image.Width );

temp_digval_gap = ( inner_header.Height - digval_image.Height ) / 2;
minecount.Y = time.Y = outer_header.Y + border_image.Width + temp_digval_gap;
minecount.X = inner_header.X + temp_digval_gap - 1;
time.X = inner_header.X + inner_header.Width - ( 3 * digval_image.Width ) - temp_digval_gap + 1;

mine_field.Width = width * field_image.Width;
mine_field.Height = height * field_image.Height;

face.X = view.X + ( view.Width - face.Width ) / 2;

} /* reset() */

}
}
