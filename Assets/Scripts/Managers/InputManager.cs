using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    private static GameControls _gameControls;
    public static void Init(Player myPlayer)
    {
        _gameControls = new GameControls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        _gameControls.Permanent.Enable();

        _gameControls.InGame.Movement.performed += ctx =>
        {
            myPlayer.SetMovementDirection(ctx.ReadValue<Vector3>());
        };

        _gameControls.InGame.Jump.performed += ctx =>
        {
            myPlayer.Jump();
        };

        _gameControls.InGame.Shoot.performed += ctx =>
        {
            myPlayer.Shoot();
            Debug.Log("Shoot");
        };

        _gameControls.InGame.Look.performed += ctx =>
        {
            myPlayer.SetLookDirection(ctx.ReadValue<Vector2>());
        };

        
    }

    public static void SetGameControls()
    {
        _gameControls.InGame.Enable();
        _gameControls.UI.Disable();
    }

    public static void SetUIControls()
    {
        _gameControls.UI.Enable();
        _gameControls.InGame.Disable();
    }
}
