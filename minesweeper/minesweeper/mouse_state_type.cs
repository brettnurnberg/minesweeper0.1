/*********************************************************************
*
*   Class:
*       mouse_state_type
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

public class mouse_state_type {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

public  button_state_type   left;
public  button_state_type   right;
public  Point               cursor;
private Boolean             prev_left_pressed;
private Boolean             prev_right_pressed;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       mouse_state_type
*
*   Description:
*       Constructor.
*
***********************************************************/

public mouse_state_type()
{
/*----------------------------------------------------------
Initialize mouse state
----------------------------------------------------------*/
prev_left_pressed  = false;
prev_right_pressed = false;

} /* mouse_state_type() */


/***********************************************************
*
*   Method:
*       update
*
*   Description:
*       Update the mouse state.
*
***********************************************************/

public void update()
{
/*----------------------------------------------------------
Initialize mouse state
----------------------------------------------------------*/
Boolean left_pressed;
Boolean right_pressed;

/*----------------------------------------------------------
Determine current mouse cursor location
----------------------------------------------------------*/
cursor = Mouse.GetState().Position;

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

} /* update() */

}
}
