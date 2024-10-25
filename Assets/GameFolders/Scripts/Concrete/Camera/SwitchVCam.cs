using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine;
using TMPro;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private int priorityBoostAmount = 10;

    private CinemachineVirtualCamera _vCam;
    private InputAction _aimAction;

    [SerializeField]
    private Canvas _thirdPersonCanvas;
    [SerializeField]
    private Canvas _aimCanvas;

    private void Awake() {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _aimAction = _playerInput.actions["Aim"];
    }

    private void OnEnable() {
        _aimAction.performed += _ => StartAim();
        _aimAction.canceled += _ => StopAim();
    }

    private void OnDisable() {
        _aimAction.performed -= _ => StartAim();
        _aimAction.canceled -= _ => StopAim();
    }

    private void StartAim(){
        _vCam.Priority += priorityBoostAmount;
        _aimCanvas.enabled = true;
        _thirdPersonCanvas.enabled = false;
    }

    private void StopAim(){
        _vCam.Priority -= priorityBoostAmount;
        _aimCanvas.enabled = false;
        _thirdPersonCanvas.enabled = true;
    }
}
