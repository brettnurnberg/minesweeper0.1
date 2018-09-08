/*********************************************************************
*
*   Class:
*       ms_game
*
*   Description:
*       Contains data for a single minesweeper game
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

public class ms_game {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

public  int             field_width;
public  int             field_height;
public  int             mine_count;
public  ms_field[,]     mine_field;
public  int             fields_rem;
public  int             mines_rem;
public  long            start_time;
public  int             current_time;
public  long            win_time;
public  ms_face_status  face_status;
public  ms_game_status  status;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       ms_game
*
*   Description:
*       Constructor.
*
***********************************************************/

public ms_game()
{
} /* ms_game() */


/***********************************************************
*
*   Method:
*       ms_new_game
*
*   Description:
*       Initialize new game board.
*
***********************************************************/

public void ms_new_game
    (
    int i_width,
    int i_height,
    int i_mine_count
    )
{
/*----------------------------------------------------------
Set all game values
----------------------------------------------------------*/
field_width = i_width;
field_height = i_height;
mine_count = i_mine_count;
mines_rem = mine_count;
current_time = 0;
fields_rem = field_height * field_width;
status = ms_game_status.INACTIVE;

/*----------------------------------------------------------
Create new mine field
----------------------------------------------------------*/
mine_field = new ms_field[field_width, field_height];

for( int i = 0; i < field_width;  i++ )
for( int j = 0; j < field_height; j++ )
    {
    mine_field[i, j] = new ms_field();
    }

/*----------------------------------------------------------
Generate mines
----------------------------------------------------------*/
gen_mines();

} /* ms_game() */


/***********************************************************
*
*   Method:
*       gen_mines
*
*   Description:
*       Generate mines for a new game.
*
***********************************************************/

private void gen_mines()
{
/*----------------------------------------------------------
Local variables
----------------------------------------------------------*/
int mines_generated = 0;
Random rand = new Random();
int x;
int y;

/*----------------------------------------------------------
Randomly generate mines
----------------------------------------------------------*/
while( mines_generated < mine_count )
    {
    x = rand.Next( 0, field_width  );
    y = rand.Next( 0, field_height );

    if( false == mine_field[x, y].is_mine )
        {
        mine_field[x, y].is_mine = true;
        mines_generated++;
        }
    }

/*----------------------------------------------------------
Calculate surrounding mine sums
----------------------------------------------------------*/
for( x = 0; x < field_width;  x++ )
for( y = 0; y < field_height; y++ )
    {
    if( mine_field[x, y].is_mine )
        {
        for( int i = Math.Max( 0, x - 1 ); i < Math.Min( field_width,  x + 2 ); i++ )
        for( int j = Math.Max( 0, y - 1 ); j < Math.Min( field_height, y + 2 ); j++ )
            {
            mine_field[i, j].mine_count++;
            }
        }
    }

} /* gen_mines() */


/***********************************************************
*
*   Method:
*       move_mine
*
*   Description:
*       Moves a mine into the upper left corner.
*
***********************************************************/

public void move_mine
    (
    int x,
    int y
    )
{
mine_field[x, y].is_mine = false;
int i;
int j;

/*----------------------------------------------------------
Recalculate surrounding values
----------------------------------------------------------*/
for( i = Math.Max( 0, x - 1 ); i < Math.Min( field_width,  x + 2 ); i++ )
for( j = Math.Max( 0, y - 1 ); j < Math.Min( field_height, y + 2 ); j++ )
    {
    mine_field[i, j].mine_count--;
    }

/*----------------------------------------------------------
Move mine to upper left corner
----------------------------------------------------------*/
for( x = 0; x < field_width;  x++ )
for( y = 0; y < field_height; y++ )
    {
    if( false == mine_field[x, y].is_mine )
        {
        mine_field[x, y].is_mine = true;

        for( i = Math.Max( 0, x - 1 ); i < Math.Min( field_width,  x + 2 ); i++ )
        for( j = Math.Max( 0, y - 1 ); j < Math.Min( field_height, y + 2 ); j++ )
            {
            mine_field[i, j].mine_count++;
            }

        x = field_width;
        y = field_height;
        }
    }

} /* move_mine() */

}
}
