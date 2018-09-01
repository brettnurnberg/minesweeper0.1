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

public  int             view_width;
public  int             view_height;
public  int             header_height;
public  int             header_gap;
public  int             border_width;
public  int             field_x_min;
public  int             field_y_min;
public  int             field_x_max;
public  int             field_y_max;
public  int             field_count_x;
public  int             field_count_y;
public  int             field_width;
public  int             digval_width;
public  int             digval_height;
public  int             minecount_x;
public  int             minecount_y;
public  int             time_x;
public  int             time_y;
public  int             face_x_min;
public  int             face_y_min;
public  int             face_x_max;
public  int             face_y_max;
public  int             face_width;
public  int             face_height;


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
header_gap = 37;
border_width = 10;
digval_width = 13;
digval_height = 23;
field_width = 16;
face_width = 26;
face_height = 26;

header_height = header_gap + 2 * border_width;
field_x_min = border_width;
field_y_min = header_height;

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

view_width = width * field_width + border_width * 2;
view_height = height * field_width + header_height + border_width;

temp_digval_gap = ( header_gap - digval_height ) / 2;
minecount_y = time_y = border_width + temp_digval_gap;
minecount_x = minecount_y;
time_x = view_width - time_y - 3 * digval_width;

field_x_max = field_x_min + width  * field_width;
field_y_max = field_y_min + height * field_width;

minecount_x--;
time_x++;

field_count_x = width;
field_count_y = height;

face_x_min = ( view_width - face_width ) / 2;
face_y_min = ( header_height - face_height ) / 2;
face_x_max = face_x_min + face_width;
face_y_max = face_y_min + face_height;

} /* reset() */

}
}
