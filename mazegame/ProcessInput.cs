using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MazeGenerator;
using mazegame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;

static class ProcessInput
{
    private static List<Keys> currentlyPressedKeys = new List<Keys>();

    // Define a dictionary to map keys to functions
    private static Dictionary<Keys, Action<GameState>> keyActions = new Dictionary<Keys, Action<GameState>>()
    {
        { Keys.Up, (gameState) => gameState.MovePlayer(0, -1) },
        { Keys.Left, (gameState) => gameState.MovePlayer(-1, 0) },
        { Keys.Down, (gameState) => gameState.MovePlayer(0, 1) },
        { Keys.Right, (gameState) => gameState.MovePlayer(1, 0) },
        { Keys.W, (gameState) => gameState.MovePlayer(0, -1) },
        { Keys.A, (gameState) => gameState.MovePlayer(-1, 0) },
        { Keys.S, (gameState) => gameState.MovePlayer(0, 1) },
        { Keys.D, (gameState) => gameState.MovePlayer(1, 0) },
        { Keys.H, (gameState) => gameState.showSingleHint = !gameState.showSingleHint},
        { Keys.B, (gameState) => gameState.showBreadcrumbs = !gameState.showBreadcrumbs},
        { Keys.P, (gameState) => gameState.showShortestPath = !gameState.showShortestPath},
        { Keys.F1, (gameState) => gameState.reset(5)},
        { Keys.F2, (gameState) => gameState.reset(10)},
        { Keys.F3, (gameState) => gameState.reset(15)},
        { Keys.F4, (gameState) => gameState.reset(20)},
        { Keys.F5, (gameState) => gameState.showLeaderboards = !gameState.showLeaderboards},
        { Keys.F6, (gameState) => gameState.showCredits = !gameState.showCredits},
    };

    public static void processInput(GameState gameState)
    {
        KeyboardState state = Keyboard.GetState();
        Keys[] keys = state.GetPressedKeys();

        foreach (Keys key in keys)
        {
            if (!currentlyPressedKeys.Contains(key))
            {
                currentlyPressedKeys.Add(key);
                if (keyActions.ContainsKey(key))
                {
                    keyActions[key].Invoke(gameState);
                }
            }
        }
        currentlyPressedKeys.RemoveAll(key => state.IsKeyUp(key));
    }
}