using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using SeniorProject.Abstract.Animation;
using SeniorProject.Concrete.Animation;


[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private float _playerSpeed = 2.0f;
    [SerializeField]
    private float _jumpHeight = 1.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    [SerializeField]
    private float _rotationSpeed = 5f;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform _barrelTransform;
    [SerializeField]
    private Transform _bulletParent;
    [SerializeField]
    private float _bulletHitMissedDis = 30f;


    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    private Transform _cameraTransform;

    // Input System Components
    private PlayerInput _playerInput;
    private IPlayerInputHandler _playerInputHandler;
    private bool _jumpInput;
    private InputAction _shootAction;

    //Animation
    private Animator _animator;
    private IAnimation _animation;
    private Vector2 _currentAnimBlendVector;
    private Vector2 _animVelocity;
    [SerializeField]
    private float _animSmoothTime = 0.1f;

    private void Awake()
    {   
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;

        //Input
        _playerInput = GetComponent<PlayerInput>();
        _playerInputHandler = GetComponent<IPlayerInputHandler>();
        _shootAction = _playerInput.actions["Shoot"];

        // Animation
        _animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        _animation = new CharacterAnimation(_animator);
        
    }

    private void Start() {
        // Lock the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        _shootAction.performed += _ => ShootGun();
    }

    private void OnDisable() {
        _shootAction.performed -= _ => ShootGun();
    }

    void Update()
    {
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Reading Input Value and Smooth Damp it
        Vector2 moveInput = _playerInputHandler.GetMoveInput();
        _currentAnimBlendVector = Vector2.SmoothDamp(_currentAnimBlendVector, moveInput, ref _animVelocity, _animSmoothTime);
        Vector3 move = new Vector3(_currentAnimBlendVector.x, 0, _currentAnimBlendVector.y);
        // Move Direction Adjusting and make sure we have no movement through sky !
        move = move.x * _cameraTransform.right.normalized + move.z * _cameraTransform.forward.normalized;
        move.y = 0f;
        // Move Action and Animation
        if(moveInput != Vector2.zero){
            _animation.MoveAnimation(_currentAnimBlendVector.x, _currentAnimBlendVector.y);
        }
        else{
            _animation.MoveAnimation(0, 0);
        }
        _animation.MoveAnimation(_currentAnimBlendVector.x, _currentAnimBlendVector.y);
        _characterController.Move(move * Time.deltaTime * _playerSpeed);

        // Jump Action
        _jumpInput = _playerInputHandler.GetJumpInput();
        if (_jumpInput && _isGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
        // Jump Animation
        _animation.JumpAnimation(!_isGrounded);

        // Fall Animation
        _animation.FallAnimation(!_isGrounded && _playerVelocity.y < 0);
        //_animation.FallAnimation(_characterController);
        // Land Animation
        //_animation.LandAnimation(_isGrounded);
        // Gravity Action
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);

        // Rotate the character towards camera direction
        float targetAngle = _cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }


    private void ShootGun(){
        // Layer tag and distance will be added
        // Pooling pattern will be added
        GameObject bullet = GameObject.Instantiate(_bulletPrefab, _barrelTransform.position, Quaternion.identity, _bulletParent);
        // Interface ile null olup olmadığını kontrol et sonra bu işlemi ekle
        BulletController bulletController = bullet.GetComponent<BulletController>();
        RaycastHit hit;
        if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, Mathf.Infinity)){
            bulletController.Target = hit.point;
            bulletController.Hit = true;
        }
        else{
            bulletController.Target = _cameraTransform.position + _cameraTransform.forward * _bulletHitMissedDis;
            bulletController.Hit = false;
        }
    }
}

