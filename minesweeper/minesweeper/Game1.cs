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
ms_mouse_state          mouse;
ms_gui_dimension        dims;
toolbar_type             toolbar;
menu_type                 game_menu;

/*--------------------------------------
Minesweeper view status
--------------------------------------*/
ms_face_status          face_status;
ms_mine_status[,]       field_status;

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

/*--------------------------------------
Fonts
--------------------------------------*/
SpriteFont              menu_font;

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
Draw background rectangle
----------------------------------------------------------*/
DrawShape.Rectangle( dims.inner_header, new Color(189, 189, 189) );

/*----------------------------------------------------------
Draw all game aspects
----------------------------------------------------------*/
draw_game();
draw_header();
draw_field();
toolbar.draw();

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

for( int i = 0; i < ms_model.field_width; i++ )
for( int j = 0; j < ms_model.field_height; j++ )
    {
    /*----------------------------------------------------------
    Draw each field according to its status
    ----------------------------------------------------------*/
    spriteBatch.Draw
        (
        fields[(int)field_status[i, j]],
        new Vector2( dims.mine_field.X + dims.field_image.Width * i, dims.mine_field.Y + dims.field_image.Height * j ),
        Color.White
        );
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
int x1 = dims.view.X;
int x2 = dims.view.Width - dims.border_image.Width;
int y1 = dims.outer_header.Y;
int y2 = dims.mine_field.Y - dims.border_image.Width;
int y3 = dims.view.Height - dims.border_image.Width;

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
for( int i = 0; i < ms_model.field_width;  i++ )
    {
    spriteBatch.Draw( border_h, new Vector2( dims.mine_field.X + dims.field_image.Width * i, y1 ), Color.White );
    spriteBatch.Draw( border_h, new Vector2( dims.mine_field.X + dims.field_image.Width * i, y2 ), Color.White );
    spriteBatch.Draw( border_h, new Vector2( dims.mine_field.X + dims.field_image.Width * i, y3 ), Color.White );
    }

/*----------------------------------------------------------
Draw vertical borders
----------------------------------------------------------*/
for( int i = 0; i < ms_model.field_height;  i++ )
    {
    spriteBatch.Draw( border_v, new Vector2( x1, dims.mine_field.Y + dims.field_image.Height * i ), Color.White );
    spriteBatch.Draw( border_v, new Vector2( x2, dims.mine_field.Y + dims.field_image.Height * i ), Color.White );
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
int game_time = 0;

/*----------------------------------------------------------
Calculate time
----------------------------------------------------------*/
if( ms_model.status == ms_game_status.ACTIVE )
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
if( ( ms_model.mines_rem > 0 ) && ( ms_model.status != ms_game_status.LOST ) )
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
    spriteBatch.Draw( dig_vals[mines[i]], new Vector2( dims.minecount.X + dims.digval_image.Width * i, dims.minecount.Y ), Color.White );
    spriteBatch.Draw( dig_vals[time[i]],  new Vector2( dims.time.X      + dims.digval_image.Width * i, dims.time.Y      ), Color.White );
    }

/*----------------------------------------------------------
Draw face
----------------------------------------------------------*/
spriteBatch.Draw( faces[(int)face_status], new Vector2( dims.face.X, dims.face.Y ), Color.White );

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

public Game1( ms_game model, ms_controller ctlr )
{
/*----------------------------------------------------------
Initialize graphics
----------------------------------------------------------*/
graphics = new GraphicsDeviceManager(this);
Content.RootDirectory = "Content";
dims = new ms_gui_dimension();
mouse = new ms_mouse_state();
toolbar = new toolbar_type();
game_menu = new menu_type( "Game" );

game_menu.add_item( "New Game",     new_game_same         );
game_menu.add_item( "Beginner",     new_game_beginner     );
game_menu.add_item( "Intermediate", new_game_intermediate );
game_menu.add_item( "Expert",       new_game_expert       );
game_menu.add_item( "Exit",         Exit                  );

/*----------------------------------------------------------
Save the game model and controller
----------------------------------------------------------*/
ms_model = model;
ms_ctlr = ctlr;

/*----------------------------------------------------------
Start new game
----------------------------------------------------------*/
new_game( 9, 9, 10 );

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

/*----------------------------------------------------------
Initialize toolbar
----------------------------------------------------------*/
menu_font = Content.Load<SpriteFont>("MenuFont");
toolbar.Initialize( spriteBatch, menu_font );

/*----------------------------------------------------------
Load textures for drawing shapes
----------------------------------------------------------*/
DrawShape.Initialize( spriteBatch );
DrawShape.LoadTextures( Content );

/*----------------------------------------------------------
Run code that requires all initialization to be done
----------------------------------------------------------*/
init_complete();

} /* LoadContent() */


/***********************************************************
*
*   Method:
*       init_complete
*
*   Description:
*       Query for any required services and load any
*       non-graphic related content.
*
***********************************************************/

private void init_complete()
{
/*----------------------------------------------------------
Add menus to toolbar
----------------------------------------------------------*/
toolbar.add_menu( game_menu );

/*----------------------------------------------------------
Start new game
----------------------------------------------------------*/
new_game( 9, 9, 10 );

} /* init_complete() */


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
dims.reset( width, height );
ms_ctlr.new_game( width, height, mine_count );

/*----------------------------------------------------------
Initialize image status variables
----------------------------------------------------------*/
face_status = ms_face_status.ACTIVE;
field_status = new ms_mine_status[width, height];

for( int i = 0; i < width; i++  )
for( int j = 0; j < height; j++ )
    {
    field_status[i, j] = new ms_mine_status();
    field_status[i, j] = ms_model.mine_field[i, j].mine_status;
    }

/*----------------------------------------------------------
Reset window size
----------------------------------------------------------*/
graphics.PreferredBackBufferWidth = dims.view.Width;
graphics.PreferredBackBufferHeight = dims.view.Height;
graphics.ApplyChanges();

toolbar.configure( dims.toolbar, Color.White, menu_font );

GraphicsDevice.Clear( Color.White );

} /* new_game() */


/***********************************************************
*
*   Method:
*       new_game_same
*
*   Description:
*       Starts a new game.
*
***********************************************************/

public void new_game_same()
{
new_game( ms_model.field_width, ms_model.field_height, ms_model.mine_count );
} /* new_game_same() */


/***********************************************************
*
*   Method:
*       new_game_beginner
*
*   Description:
*       Starts a new game.
*
***********************************************************/

public void new_game_beginner()
{
new_game( 9, 9, 10 );
} /* new_game_beginner() */


/***********************************************************
*
*   Method:
*       new_game_intermediate
*
*   Description:
*       Starts a new game.
*
***********************************************************/

public void new_game_intermediate()
{
new_game( 16, 16, 40 );
} /* new_game_intermediate() */


/***********************************************************
*
*   Method:
*       new_game_expert
*
*   Description:
*       Starts a new game.
*
***********************************************************/

public void new_game_expert()
{
new_game( 31, 16, 99 );
} /* new_game_expert() */


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
Check if game should be exited
----------------------------------------------------------*/
if( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed )
    {
    Exit();
    }

/*----------------------------------------------------------
Check if the toolbar handled the input
----------------------------------------------------------*/
if( toolbar.input_handled() )
    {
    return;
    }

/*----------------------------------------------------------
Update mouse status
----------------------------------------------------------*/
mouse.update( dims );

/*----------------------------------------------------------
Check for searching a field
----------------------------------------------------------*/
if( ( ms_mouse_location.FIELD == mouse.capture_left ) &&
    ( button_state_type.UNCLICKED == mouse.left ) )
    {
    ms_ctlr.search_field( mouse.mine_loc.X, mouse.mine_loc.Y );
    set_field_image();
    }

/*----------------------------------------------------------
Check for indenting a field
----------------------------------------------------------*/
if( ( button_state_type.HELD == mouse.left ) &&
  ( ( ms_game_status.ACTIVE == ms_model.status ) ||
    ( ms_game_status.INACTIVE == ms_model.status ) ) )
    {
    set_field_image();

    if( ( ms_mouse_location.FIELD  == mouse.capture_left ) &&
      ( ( ms_mine_status.UNCHECKED == ms_model.mine_field[mouse.mine_loc.X, mouse.mine_loc.Y].mine_status ) ||
        ( ms_mine_status.QUESTION  == ms_model.mine_field[mouse.mine_loc.X, mouse.mine_loc.Y].mine_status ) ) )
        {
        field_status[mouse.mine_loc.X, mouse.mine_loc.Y] = ms_mine_status.CHECKED_0;
        }
    }

/*----------------------------------------------------------
Check for flagging a field
----------------------------------------------------------*/
if( ( ms_mouse_location.FIELD == mouse.capture_right ) &&
    ( button_state_type.UNCLICKED == mouse.right ) )
    {
    ms_ctlr.flag_field( mouse.mine_loc.X, mouse.mine_loc.Y );
    set_field_image();
    }

/*----------------------------------------------------------
Check for face clicked
----------------------------------------------------------*/
if( ( ms_mouse_location.FACE == mouse.capture_left ) &&
    ( ms_mouse_location.FACE == mouse.cursor_location ) &&
    ( button_state_type.UNCLICKED == mouse.left ) )
    {
    new_game_same();
    }

/*----------------------------------------------------------
Set the face status
----------------------------------------------------------*/
if( ( ms_mouse_location.FACE == mouse.capture_left ) &&
    ( ms_mouse_location.FACE == mouse.cursor_location ) &&
    ( button_state_type.HELD == mouse.left ) )
    {
    face_status = ms_face_status.PRESSED;
    }
else if( ( ms_mouse_location.FIELD == mouse.capture_left ) &&
       ( ( ms_game_status.ACTIVE   == ms_model.status ) ||
         ( ms_game_status.INACTIVE == ms_model.status ) ) )
    {
    face_status = ms_face_status.MOUSE_DOWN;
    }
else
    {
    switch( ms_model.status )
        {
        case ms_game_status.WON:
            face_status = ms_face_status.WON;
            break;

        case ms_game_status.LOST:
            face_status = ms_face_status.LOST;
            break;

        case ms_game_status.INACTIVE:
        case ms_game_status.ACTIVE:
            face_status = ms_face_status.ACTIVE;
            break;
        }
    }

base.Update(gameTime);

} /* Update() */


/***********************************************************
*
*   Method:
*       set_field_image
*
*   Description:
*       Sets the field images to the model state
*
***********************************************************/

private void set_field_image()
{

for( int i = 0; i < ms_model.field_width;  i++ )
for( int j = 0; j < ms_model.field_height; j++ )
    {
    field_status[i, j] = ms_model.mine_field[i, j].mine_status;
    }

} /* set_field_image() */


}
}
