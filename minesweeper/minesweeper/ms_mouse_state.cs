/*********************************************************************
*
*   Class:
*       ms_mouse_state
*
*   Description:
*       Contains state of the mouse
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using System;
using Microsoft.Xna.Framework;
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

public class ms_mouse_state {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

public button_state_type     left;
public button_state_type     right;
public Point                mine_loc;
public ms_mouse_location    capture_left;
public ms_mouse_location    capture_right;
public ms_mouse_location    cursor_location;
private Boolean             prev_left_pressed;
private Boolean             prev_right_pressed;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       ms_mouse_state
*
*   Description:
*       Constructor.
*
***********************************************************/

public ms_mouse_state()
{
/*----------------------------------------------------------
Initialize mouse state
----------------------------------------------------------*/
prev_left_pressed  = false;
prev_right_pressed = false;
capture_left    = ms_mouse_location.NONE;
capture_right   = ms_mouse_location.NONE;
cursor_location = ms_mouse_location.NONE;

} /* ms_mouse_state() */


/***********************************************************
*
*   Method:
*       update
*
*   Description:
*       Update the mouse state.
*
***********************************************************/

public void update( ms_gui_dimension dims )
{
/*----------------------------------------------------------
Initialize mouse state
----------------------------------------------------------*/
Boolean left_pressed;
Boolean right_pressed;
Point mouse_loc;

/*----------------------------------------------------------
Determine current mouse location
----------------------------------------------------------*/
mouse_loc = Mouse.GetState().Position;

if( dims.mine_field.Contains( mouse_loc.X, mouse_loc.Y ) )
    {
    cursor_location = ms_mouse_location.FIELD;
    mine_loc = mouse_loc - new Point( dims.mine_field.X, dims.mine_field.Y );
    mine_loc /= new Point( dims.field_image.Width, dims.field_image.Height );
    }
else if( dims.face.Contains( mouse_loc.X, mouse_loc.Y ) )
    {
    cursor_location = ms_mouse_location.FACE;
    }
else
    {
    cursor_location = ms_mouse_location.NONE;
    }

/*----------------------------------------------------------
Get the current mouse button state
----------------------------------------------------------*/
left_pressed  = ( ButtonState.Pressed == Mouse.GetState().LeftButton  );
right_pressed = ( ButtonState.Pressed == Mouse.GetState().RightButton );

/*----------------------------------------------------------
Left button unclicked
----------------------------------------------------------*/
if( !left_pressed && prev_left_pressed )
    {
    left = button_state_type.UNCLICKED;
    }
/*----------------------------------------------------------
Left button clicked
----------------------------------------------------------*/
else if( left_pressed && !prev_left_pressed )
    {
    left = button_state_type.CLICKED;
    }
/*----------------------------------------------------------
Left button held
----------------------------------------------------------*/
else if( left_pressed && prev_left_pressed )
    {
    left = button_state_type.HELD;
    }
/*----------------------------------------------------------
Left button unheld
----------------------------------------------------------*/
else
    {
    left = button_state_type.UNHELD;
    }

/*----------------------------------------------------------
Right button unclicked
----------------------------------------------------------*/
if( !right_pressed && prev_right_pressed )
    {
    right = button_state_type.UNCLICKED;
    }
/*----------------------------------------------------------
Right button clicked
----------------------------------------------------------*/
else if( right_pressed && !prev_right_pressed )
    {
    right = button_state_type.CLICKED;
    }
/*----------------------------------------------------------
Right button held
----------------------------------------------------------*/
else if( right_pressed && prev_right_pressed )
    {
    right = button_state_type.HELD;
    }
/*----------------------------------------------------------
Right button unheld
----------------------------------------------------------*/
else
    {
    right = button_state_type.UNHELD;
    }

/*----------------------------------------------------------
Save the previous mouse button states
----------------------------------------------------------*/
prev_left_pressed = left_pressed;
prev_right_pressed = right_pressed;

/*----------------------------------------------------------
Get the left click capture state
----------------------------------------------------------*/
switch( left )
    {
    /*----------------------------------------------------------
    Capture mouse on click
    ----------------------------------------------------------*/
    case button_state_type.CLICKED:
        capture_left = cursor_location;
        break;

    /*----------------------------------------------------------
    Lose field capture upon leaving the field
    ----------------------------------------------------------*/
    case button_state_type.UNCLICKED:
    case button_state_type.HELD:
        if( ( ms_mouse_location.FIELD == capture_left ) &&
            ( ms_mouse_location.FIELD != cursor_location ) )
            {
            capture_left = ms_mouse_location.NONE;
            }
        break;

    /*----------------------------------------------------------
    No mouse capture for unheld button
    ----------------------------------------------------------*/
    case button_state_type.UNHELD:
        capture_left = ms_mouse_location.NONE;
        break;
    }

/*----------------------------------------------------------
Get the right click capture state
----------------------------------------------------------*/
switch( right )
    {
    /*----------------------------------------------------------
    Capture mouse on click
    ----------------------------------------------------------*/
    case button_state_type.CLICKED:
        capture_right = cursor_location;
        break;

    /*----------------------------------------------------------
    Lose field capture upon leaving the field
    ----------------------------------------------------------*/
    case button_state_type.UNCLICKED:
    case button_state_type.HELD:
        if( ( ms_mouse_location.FIELD == capture_right ) &&
            ( ms_mouse_location.FIELD != cursor_location ) )
            {
            capture_right = ms_mouse_location.NONE;
            }
        break;

    /*----------------------------------------------------------
    No mouse capture for unheld button
    ----------------------------------------------------------*/
    case button_state_type.UNHELD:
        capture_right = ms_mouse_location.NONE;
        break;
    }

} /* update() */

}
}
