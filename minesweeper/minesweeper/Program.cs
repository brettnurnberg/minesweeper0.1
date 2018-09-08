/*********************************************************************
*
*   Class:
*       Program
*
*   Description:
*       The main class for the application. Contains
*       the entry point.
*
*   TODO:
*       Add double mouse release
*       Add option for custom game
*       Add high score tracking
*
*   DEBUG:
*       Double click release shouldn't flag or search
*       Set flag on mouse down
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

public static class Program {

/*--------------------------------------------------------------------
                           ATTRIBUTES
--------------------------------------------------------------------*/
public static ms_game ms_model;
public static ms_controller ms_ctlr;

/*--------------------------------------------------------------------
                            METHODS
--------------------------------------------------------------------*/

/***********************************************************
*
*   Method:
*       Main
*
*   Description:
*       Entry point for the application.
*
***********************************************************/

[STAThread]
static void Main()
{
ms_model = new ms_game();
ms_ctlr = new ms_controller( ms_model );

using ( Game1 game = new Game1( ms_model, ms_ctlr ) )
    {
    game.Run();
    }

} /* Main() */


}
}
