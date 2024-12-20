using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, IPlayerInputHandler
{

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _shootAction;

    private void Awake() {
        
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        //_shootAction = _playerInput.actions["Shoot"];
    }

    public Vector2 GetMoveInput()
    {
        return _moveAction.ReadValue<Vector2>();
    }

    public bool GetJumpInput()
    {
        return _jumpAction.triggered;
    }

    public bool GetShootInput()
    {   
        //Burayı sonra düzenlicem
        return false;
    }

}
