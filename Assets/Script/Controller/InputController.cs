using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Utility.MonoSingleton<InputController>
{
    [Header("CD Type")]
    [SerializeField] private bool IsPlaying;
    [SerializeField] private bool IsShieldUP;

    [Header("Inputs")]
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction shootAction;
    [SerializeField] private InputAction shieldAction;
    [SerializeField] private InputAction pauseGame;


    #region UnityMessages
    private new void Awake()
    {
        base.Awake();
        IsPlaying = true;
    }

    private void OnEnable()
    {
        EventController.SpaceshipEnableInput += EnableInputs;
        EventController.SpaceshipDisableInput += DisableInputs;
        EventController.GameOverUI += DisableInputs;

        moveAction.started += OnMoveStarted;
        moveAction.canceled += OnMoveFinished;
        shootAction.started += OnShoot;
        shieldAction.started += OnShield;

        pauseGame.started += OnPause;
        pauseGame.Enable();
    }

    private void OnDisable()
    {
        EventController.SpaceshipEnableInput -= EnableInputs;
        EventController.SpaceshipDisableInput += DisableInputs;
        EventController.GameOverUI -= DisableInputs;

        moveAction.started -= OnMoveStarted;
        moveAction.canceled -= OnMoveFinished;
        shootAction.started -= OnShoot;
        shieldAction.started -= OnShield;

        pauseGame.started -= OnPause;
        pauseGame.Disable();
    }
    #endregion


    #region Inputs
    private void EnableInputs()
    {
        moveAction.Enable();
        shootAction.Enable();
        shieldAction.Enable();
    }
    private void DisableInputs()
    {
        moveAction.Disable();
        shootAction.Disable();
        shieldAction.Disable();
    }
    #endregion

    #region Movement

    private void OnMoveStarted(InputAction.CallbackContext _context)
    {
        EventController.RaiseOnSpaceshipMoveStarted(_context);
    }
    private void OnMoveFinished(InputAction.CallbackContext _context)
    {
        EventController.RaiseOnSpaceshipMoveFinished(_context);
    }
    #endregion

    #region Shoot
    private void OnShoot(InputAction.CallbackContext _context)
    {
        EventController.RaiseOnSpaceshipShoot(_context);
    }
    #endregion

    #region Ability
    private void OnShield(InputAction.CallbackContext _context)
    {
        EventController.RaiseOnSpaceshipShield(_context);
    }
    #endregion

    #region UI
    private void OnPause(InputAction.CallbackContext _context)
    {
        if (IsPlaying)
        {
            IsPlaying = false;
            DisableInputs();
            EventController.RaiseOnPauseGameUI();
        }
        else
        {
            IsPlaying = true;
            EnableInputs();
            EventController.RaiseOnResumeGameUI();
        }
    }
    #endregion

}
