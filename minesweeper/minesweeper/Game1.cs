/*********************************************************************
*
*   Class:
*       Game1
*
*   Description:
*       The main class for the game. Contains methods to initialize
*       game, read input, update game logic, and update the view.
*
*********************************************************************/

/*--------------------------------------------------------------------
                            INCLUDES
--------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

public class Game1 : Game {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/

/*--------------------------------------
Monogame API objects
--------------------------------------*/
GraphicsDeviceManager   graphics;
SpriteBatch             spriteBatch;

/*--------------------------------------
Minesweeper data and controller
--------------------------------------*/
ms_game                 ms_model;
ms_controller           ms_ctlr;

/*--------------------------------------
Minesweeper view control
--------------------------------------*/
ms_gui_dimension        dims;
Boolean                 left_pressed;
Boolean                 right_pressed;
Boolean                 prev_left_pressed;
Boolean                 prev_right_pressed;
ms_mouse_capture_state  left_mouse_capture;
ms_mouse_capture_state  right_mouse_capture;
int                     game_time;
ms_field                prev_field;
ms_mine_status          prev_status;

/*--------------------------------------
Textures
--------------------------------------*/
Texture2D               border_tl;
Texture2D               border_tr;
Texture2D               border_bl;
Texture2D               border_br;
Texture2D               border_h;
Texture2D               border_v;
Texture2D[]             dig_vals;
Texture2D[]             faces;
Texture2D[]             fields;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       Draw
*
*   Description:
*       Draws the game.
*
***********************************************************/

protected override void Draw
    (
    GameTime gameTime
    )
{
/*----------------------------------------------------------
Clear screen to gray
----------------------------------------------------------*/
GraphicsDevice.Clear( new Color(189, 189, 189) );

/*----------------------------------------------------------
Draw all game aspects
----------------------------------------------------------*/
draw_game();
draw_header();
draw_field();

base.Draw( gameTime );
} /* Draw() */


/***********************************************************
*
*   Method:
*       draw_field
*
*   Description:
*       Draws the field.
*
***********************************************************/

private void draw_field()
{
spriteBatch.Begin();
for(int i = 0; i < ms_model.field_width; i++)
    {
    for( int j = 0; j < ms_model.field_height; j++ )
        {
        /*----------------------------------------------------------
        Draw each field according to its status
        ----------------------------------------------------------*/
        spriteBatch.Draw
                    (
                    fields[(int)ms_model.mine_field[i,j].mine_status],
                    new Vector2( dims.field_x_min + dims.field_width * i, dims.field_y_min + dims.field_width * j ),
                    Color.White
                    );
        }
    }
spriteBatch.End();
} /* draw_field() */


/***********************************************************
*
*   Method:
*       draw_game
*
*   Description:
*       Draws the game.
*
***********************************************************/

private void draw_game()
{
/*----------------------------------------------------------
Get game dimensions
----------------------------------------------------------*/
int x1 = 0;
int x2 = dims.view_width - dims.border_width;
int y1 = 0;
int y2 = dims.header_height - dims.border_width;
int y3 = dims.view_height - dims.border_width;

spriteBatch.Begin();

/*----------------------------------------------------------
Draw corners of border
----------------------------------------------------------*/
spriteBatch.Draw( border_tl, new Vector2( x1 , y1 ), Color.White );
spriteBatch.Draw( border_tr, new Vector2( x2 , y1 ), Color.White );
spriteBatch.Draw( border_bl, new Vector2( x1 , y3 ), Color.White );
spriteBatch.Draw( border_br, new Vector2( x2 , y3 ), Color.White );

/*----------------------------------------------------------
Draw horizontal borders
----------------------------------------------------------*/
for( int i = 0; i < dims.field_count_x;  i++ )
    {
    spriteBatch.Draw( border_h, new Vector2( dims.field_x_min + dims.field_width * i, y1 ), Color.White );
    spriteBatch.Draw( border_h, new Vector2( dims.field_x_min + dims.field_width * i, y2 ), Color.White );
    spriteBatch.Draw( border_h, new Vector2( dims.field_x_min + dims.field_width * i, y3 ), Color.White );
    }

/*----------------------------------------------------------
Draw vertical borders
----------------------------------------------------------*/
for( int i = 0; i < dims.field_count_y;  i++ )
    {
    spriteBatch.Draw( border_v, new Vector2( x1, dims.field_y_min + dims.field_width * i ), Color.White );
    spriteBatch.Draw( border_v, new Vector2( x2, dims.field_y_min + dims.field_width * i ), Color.White );
    }

spriteBatch.End();
} /* draw_game() */


/***********************************************************
*
*   Method:
*       draw_header
*
*   Description:
*       Draws the header.
*
***********************************************************/

private void draw_header()
{
/*----------------------------------------------------------
Local variables
----------------------------------------------------------*/
int[] mines = new int[3];
int[] time  = new int[3];

/*----------------------------------------------------------
Calculate time
----------------------------------------------------------*/
if( ( ms_model.status == ms_game_status.ACTIVE ) &&
    ( ms_model.time != 0 ) )
    {
    game_time = (int)( ( DateTime.Now.Ticks - ms_model.time ) / 10000000 );
    }

if( game_time < 1000 )
    {
    time[2] = game_time % 10;
    time[1] = ( game_time / 10 ) % 10;
    time[0] = game_time / 100;
    }
else
    {
    time[0] = time[1] = time[2] = 9;
    }

/*----------------------------------------------------------
Calculate mines remaining
----------------------------------------------------------*/
if( ms_model.mines_rem > 0 )
    {
    mines[2] = ms_model.mines_rem % 10;
    mines[1] = ( ms_model.mines_rem / 10 ) % 10;
    mines[0] = ms_model.mines_rem / 100;
    }
else
    {
    mines[0] = mines[1] = mines[2] = 0;
    }

spriteBatch.Begin();

/*----------------------------------------------------------
Draw time and mine counters
----------------------------------------------------------*/
for( int i = 0; i < 3; i++ )
    {
    spriteBatch.Draw( dig_vals[mines[i]], new Vector2( dims.minecount_x + dims.digval_width * i, dims.minecount_y ), Color.White );
    spriteBatch.Draw( dig_vals[time[i]],  new Vector2( dims.time_x      + dims.digval_width * i, dims.time_y      ), Color.White );
    }

/*----------------------------------------------------------
Draw face
----------------------------------------------------------*/
spriteBatch.Draw( faces[(int)ms_model.face_status], new Vector2( dims.face_x_min, dims.face_y_min ), Color.White );

spriteBatch.End();

} /* draw_header() */


/***********************************************************
*
*   Method:
*       Game1
*
*   Description:
*       Constructor.
*
***********************************************************/

public Game1
    (
    ms_game         model,
    ms_controller   ctlr
    )
{
/*----------------------------------------------------------
Initialize graphics
----------------------------------------------------------*/
graphics = new GraphicsDeviceManager(this);
Content.RootDirectory = "Content";
dims = new ms_gui_dimension();

/*----------------------------------------------------------
Save the game model and controller
----------------------------------------------------------*/
ms_model = model;
ms_ctlr = ctlr;

/*----------------------------------------------------------
Initialize mouse state
----------------------------------------------------------*/
prev_left_pressed = false;
prev_right_pressed = false;
left_mouse_capture = ms_mouse_capture_state.NONE;
right_mouse_capture = ms_mouse_capture_state.NONE;

/*----------------------------------------------------------
Start new game
----------------------------------------------------------*/
new_game( 31, 16, 99 );

} /* Game1() */


/***********************************************************
*
*   Method:
*       Initialize
*
*   Description:
*       Query for any required services and load any
*       non-graphic related content.
*
***********************************************************/

protected override void Initialize()
{
/*----------------------------------------------------------
View mouse
----------------------------------------------------------*/
IsMouseVisible = true;

/*----------------------------------------------------------
Enumerate through and initialize all components
----------------------------------------------------------*/
base.Initialize();

} /* Initialize() */


/***********************************************************
*
*   Method:
*       LoadContent
*
*   Description:
*       Called once per game. Loads all content.
*
***********************************************************/

protected override void LoadContent()
{
/*----------------------------------------------------------
Create SpriteBatch to draw textures
----------------------------------------------------------*/
spriteBatch = new SpriteBatch(GraphicsDevice);

/*----------------------------------------------------------
Load borders
----------------------------------------------------------*/
border_bl = Content.Load<Texture2D>("border_bl");
border_br = Content.Load<Texture2D>("border_br");
border_tl = Content.Load<Texture2D>("border_tl");
border_tr = Content.Load<Texture2D>("border_tr");
border_v  = Content.Load<Texture2D>("border_v");
border_h  = Content.Load<Texture2D>("border_h");

/*----------------------------------------------------------
Load digital numbers
----------------------------------------------------------*/
dig_vals = new Texture2D[10];
for( int i = 0; i < 10; i++ )
    {
    dig_vals[i] = Content.Load<Texture2D>(string.Format( "dig_" + i ) );
    }

/*----------------------------------------------------------
Load faces
----------------------------------------------------------*/
faces = new Texture2D[5];
faces[0] = Content.Load<Texture2D>("face_play");
faces[1] = Content.Load<Texture2D>("face_won");
faces[2] = Content.Load<Texture2D>("face_lost");
faces[3] = Content.Load<Texture2D>("face_down");
faces[4] = Content.Load<Texture2D>("face_click");

/*----------------------------------------------------------
Load field images
----------------------------------------------------------*/
fields = new Texture2D[15];
for( int i = 0; i < 9; i++ )
    {
    fields[i] = Content.Load<Texture2D>(string.Format( "sel_" + i ) );
    }
fields[9]  = Content.Load<Texture2D>("unsel");
fields[10] = Content.Load<Texture2D>("flag");
fields[11] = Content.Load<Texture2D>("question");
fields[12] = Content.Load<Texture2D>("mine_sel");
fields[13] = Content.Load<Texture2D>("mine");
fields[14] = Content.Load<Texture2D>("mine_not");

} /* LoadContent() */


/***********************************************************
*
*   Method:
*       new_game
*
*   Description:
*       Starts a new game.
*
***********************************************************/

private void new_game
    (
    int width,
    int height,
    int mine_count
    )
{
/*----------------------------------------------------------
Reset and start a new game
----------------------------------------------------------*/
game_time = 0;
dims.reset( width, height );
ms_ctlr.new_game( width, height, mine_count );

/*----------------------------------------------------------
Reset window size
----------------------------------------------------------*/
graphics.PreferredBackBufferWidth = dims.view_width;
graphics.PreferredBackBufferHeight = dims.view_height;
graphics.ApplyChanges();

} /* new_game() */


/***********************************************************
*
*   Method:
*       UnloadContent
*
*   Description:
*       Called once per game. Unloads game-specific content.
*
***********************************************************/

protected override void UnloadContent()
{

} /* UnloadContent() */


/***********************************************************
*
*   Method:
*       Update
*
*   Description:
*       Passes input events to game controller.
*
***********************************************************/

protected override void Update
    (
    GameTime gameTime
    )
{
/*----------------------------------------------------------
Local variables
----------------------------------------------------------*/
int x;
int y;
int field_x = 0;
int field_y = 0;
ms_field field = null;
Boolean in_field = false;
Boolean on_face = false;

/*----------------------------------------------------------
Check if game should be exited
----------------------------------------------------------*/
if( ( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ) ||
    ( Keyboard.GetState().IsKeyDown(Keys.Escape) ) )
    {
    Exit();
    }

/*----------------------------------------------------------
Get the current mouse state
----------------------------------------------------------*/
left_pressed  = ( ButtonState.Pressed == Mouse.GetState().LeftButton  );
right_pressed = ( ButtonState.Pressed == Mouse.GetState().RightButton );
x = Mouse.GetState().X;
y = Mouse.GetState().Y;

/*----------------------------------------------------------
Determine mouse location
----------------------------------------------------------*/
if( ( dims.field_x_min < x && x < dims.field_x_max ) &&
    ( dims.field_y_min < y && y < dims.field_y_max ) )
    {
    in_field = true;
    field_x = ( x - dims.field_x_min ) / dims.field_width;
    field_y = ( y - dims.field_y_min ) / dims.field_width;
    field = ms_model.mine_field[field_x, field_y];
    }
else if( ( dims.face_x_min < x && x < dims.face_x_max ) &&
         ( dims.face_y_min < y && y < dims.face_y_max ) )
    {
    on_face = true;
    }

/*----------------------------------------------------------
Left button released
----------------------------------------------------------*/
if( !left_pressed && prev_left_pressed )
    {
    /*----------------------------------------------------------
    If left released on field, search the field
    ----------------------------------------------------------*/
    if( in_field && left_mouse_capture == ms_mouse_capture_state.FIELD )
        {
        if( prev_field != null )
            {
            field.mine_status = prev_status;
            }
        ms_ctlr.search_field( field_x, field_y );
        ms_model.face_status = (ms_face_status)ms_model.status;
        }
    /*----------------------------------------------------------
    If left released on face, start a new game
    ----------------------------------------------------------*/
    else if( on_face && left_mouse_capture == ms_mouse_capture_state.FACE )
        {
        new_game( 31, 16, 99 );
        }
    left_mouse_capture = ms_mouse_capture_state.NONE;
    }

/*----------------------------------------------------------
Left button pressed
----------------------------------------------------------*/
else if( left_pressed && !prev_left_pressed )
    {
    /*----------------------------------------------------------
    If left button is pressed during the game on an unchecked
    field, indent the field and save its previous state
    ----------------------------------------------------------*/
    if( in_field && ( ms_model.status == ms_game_status.ACTIVE ||
                      ms_model.status == ms_game_status.INACTIVE ) )
        {
        left_mouse_capture = ms_mouse_capture_state.FIELD;
        ms_model.face_status = ms_face_status.MOUSE_DOWN;

        if( ( field.mine_status == ms_mine_status.QUESTION ) ||
            ( field.mine_status == ms_mine_status.UNCHECKED ) )
            {
            prev_field = field;
            prev_status = field.mine_status;
            field.mine_status = ms_mine_status.CHECKED_0;
            }
        else
            {
            prev_field = null;
            }
        }
    /*----------------------------------------------------------
    If left button pressed on face, indent the face
    ----------------------------------------------------------*/
    else if( on_face )
        {
        ms_model.face_status = ms_face_status.PRESSED;
        left_mouse_capture = ms_mouse_capture_state.FACE;
        }
    }

/*----------------------------------------------------------
Left button held
----------------------------------------------------------*/
else if( left_pressed )
    {
    /*----------------------------------------------------------
    If left mouse is pressed on face, indent the face.
    If left mouse leaves the face, restore the state.
    ----------------------------------------------------------*/
    if( left_mouse_capture == ms_mouse_capture_state.FACE )
        {
        if( on_face )
            {
            ms_model.face_status = ms_face_status.PRESSED;
            }
        else
            {
            if( ms_model.status == ms_game_status.INACTIVE )
                {
                ms_model.face_status = ms_face_status.ACTIVE;
                }
            else
                {
                ms_model.face_status = (ms_face_status)ms_model.status;
                }
            }
        }
    /*----------------------------------------------------------
    If left mouse is pressed in field, indent the current field.
    If left mouse leaves the field, lose capture.
    ----------------------------------------------------------*/
    else if( left_mouse_capture == ms_mouse_capture_state.FIELD )
        {
        if( !in_field )
            {
            ms_model.face_status = (ms_face_status)ms_model.status;
            left_mouse_capture = ms_mouse_capture_state.NONE;
            if( prev_field != null )
                {
                prev_field.mine_status = prev_status;
                }
            }
        else
            {
            if( prev_field != field )
                {
                if( prev_field != null )
                    {
                    prev_field.mine_status = prev_status;
                    }

                if( ( field.mine_status == ms_mine_status.QUESTION ) ||
                    ( field.mine_status == ms_mine_status.UNCHECKED ) )
                    {
                    prev_field = field;
                    prev_status = field.mine_status;
                    field.mine_status = ms_mine_status.CHECKED_0;
                    }
                else
                    {
                    prev_field = null;
                    }
                }
            }
        }
    }


/*----------------------------------------------------------
Right button released
----------------------------------------------------------*/
if( !right_pressed && prev_right_pressed )
    {
    if( in_field && right_mouse_capture == ms_mouse_capture_state.FIELD )
        {
        ms_ctlr.flag_field( field_x, field_y );
        }
    right_mouse_capture = ms_mouse_capture_state.NONE;
    }

/*----------------------------------------------------------
Right button pressed
----------------------------------------------------------*/
else if( right_pressed && !prev_right_pressed )
    {
    if( in_field )
        {
        right_mouse_capture = ms_mouse_capture_state.FIELD;
        }
    }

/*----------------------------------------------------------
Right button held
----------------------------------------------------------*/
else if( right_pressed )
    {
    if( !in_field )
        {
        right_mouse_capture = ms_mouse_capture_state.NONE;
        }
    }

/*----------------------------------------------------------
Save the previous mouse button states
----------------------------------------------------------*/
prev_left_pressed = left_pressed;
prev_right_pressed = right_pressed;

base.Update(gameTime);

} /* Update() */


}
}
