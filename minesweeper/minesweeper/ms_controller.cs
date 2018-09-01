/*********************************************************************
*
*   Class:
*       ms_controller
*
*   Description:
*       Contains the minesweeper game logic
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

public class ms_controller {

/*--------------------------------------------------------------------
                        LITERAL CONSTANTS
--------------------------------------------------------------------*/

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

private ms_game     ms_model;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       ms_controller
*
*   Description:
*       Constructor.
*
***********************************************************/

public ms_controller
    (
    ms_game model
    )
{
ms_model = model;
} /* ms_controller() */


/***********************************************************
*
*   Method:
*       new_game
*
*   Description:
*       Creates a new game.
*
***********************************************************/

public void new_game
    (
    int width,
    int height,
    int mine_count
    )
{
/*----------------------------------------------------------
Create a new game
----------------------------------------------------------*/
ms_model.ms_new_game( width, height, mine_count );

} /* new_game() */


/***********************************************************
*
*   Method:
*       search_field
*
*   Description:
*       Searches a given field for a mine.
*
***********************************************************/

public void search_field
    (
    int x,
    int y
    )
{
/*----------------------------------------------------------
Get the searched field
----------------------------------------------------------*/
ms_field field = ms_model.mine_field[x, y];

/*----------------------------------------------------------
Verify the game is active
----------------------------------------------------------*/
if( ( ms_game_status.WON  == ms_model.status ) ||
    ( ms_game_status.LOST == ms_model.status ) )
    {
    return;
    }

/*----------------------------------------------------------
If the game is just starting, save the start time
----------------------------------------------------------*/
if( ms_game_status.INACTIVE == ms_model.status )
    {
    ms_model.time = DateTime.Now.Ticks;
    ms_model.status = ms_game_status.ACTIVE;

    if( field.is_mine )
        {
        ms_model.move_mine( x, y );
        }
    }

/*----------------------------------------------------------
Verify the field can be searched
----------------------------------------------------------*/
if( ( ms_mine_status.UNCHECKED == field.mine_status ) ||
    ( ms_mine_status.QUESTION  == field.mine_status ) )
    {
    /*----------------------------------------------------------
    If a mine is hit, game over
    ----------------------------------------------------------*/
    if( field.is_mine )
        {
        field.mine_status = ms_mine_status.MINE_HIT;
        ms_model.status = ms_game_status.LOST;
        game_lost();
        }
    /*----------------------------------------------------------
    If a mine is not hit, reveal the mine count. If the count
    is zero, search surrounding fields.
    ----------------------------------------------------------*/
    else
        {
        field.mine_status = (ms_mine_status)field.mine_count;
        ms_model.fields_rem--;

        if( 0 == field.mine_count )
            {
            for( int i = Math.Max( 0, x - 1 ); i < Math.Min( ms_model.field_width,  x + 2 ); i++ )
            for( int j = Math.Max( 0, y - 1 ); j < Math.Min( ms_model.field_height, y + 2 ); j++ )
                {
                search_field( i, j );
                }
            }
        }
    }

/*----------------------------------------------------------
Game won
----------------------------------------------------------*/
if( ms_model.fields_rem == ms_model.mine_count )
    {
    //save win time
    ms_model.status = ms_game_status.WON;
    game_won();
    }

} /* search_field() */


/***********************************************************
*
*   Method:
*       flag_field
*
*   Description:
*       Flags a given field for a mine.
*
***********************************************************/

public void flag_field
    (
    int x,
    int y
    )
{
/*----------------------------------------------------------
Get the flagged field
----------------------------------------------------------*/
ms_field field = ms_model.mine_field[x, y];

if( ms_game_status.ACTIVE != ms_model.status )
    {
    return;
    }

/*----------------------------------------------------------
Rotate the status
----------------------------------------------------------*/
switch( field.mine_status )
    {
    case ms_mine_status.FLAGGED:
        ms_model.mines_rem++;
        field.mine_status = ms_mine_status.QUESTION;
        break;

    case ms_mine_status.QUESTION:
        field.mine_status = ms_mine_status.UNCHECKED;
        break;

    case ms_mine_status.UNCHECKED:
        field.mine_status = ms_mine_status.FLAGGED;
        ms_model.mines_rem--;
        break;

    default:
        break;
    }

} /* flag_field() */


/***********************************************************
*
*   Method:
*       game_lost
*
*   Description:
*       Sets game into lost state.
*
***********************************************************/

private void game_lost()
{
ms_field field;

/*----------------------------------------------------------
Display all mines and all incorrect flags
----------------------------------------------------------*/
for( int i = 0; i < ms_model.field_width;  i++ )
for( int j = 0; j < ms_model.field_height; j++ )
    {
    field = ms_model.mine_field[i,j];

    if( field.is_mine && ( field.mine_status == ms_mine_status.UNCHECKED ||
        field.mine_status == ms_mine_status.QUESTION ) )
        {
        field.mine_status = ms_mine_status.MINE;
        }
    else if( !field.is_mine && field.mine_status == ms_mine_status.FLAGGED )
        {
        field.mine_status = ms_mine_status.NOT_MINE;
        }
    }
} /* game_lost() */


/***********************************************************
*
*   Method:
*       game_won
*
*   Description:
*       Sets game into won state.
*
***********************************************************/

private void game_won()
{
ms_field field;

/*----------------------------------------------------------
Flag all mines
----------------------------------------------------------*/
for( int i = 0; i < ms_model.field_width;  i++ )
for( int j = 0; j < ms_model.field_height; j++ )
    {
    field = ms_model.mine_field[i,j];

    if( field.is_mine )
        {
        field.mine_status = ms_mine_status.FLAGGED;
        }
    }
} /* game_won() */

}
}
