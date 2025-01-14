using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameInput _gameInput;

    [Header ("Movement setting")]
    [SerializeField] private int _movementSpeed;
    [SerializeField] private float _groundDrag = 5;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    private bool _readyToJump;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;
    private float _groundDistance = 0.4f;
    private bool _grounded;

    [SerializeField] private Transform _orientation;
    private Rigidbody _rb;


    public void Inject(GameInput gameInput) 
    {

        _gameInput = gameInput;
        _gameInput.OnJumpAction += Jump;

    }

    private void Start()
    {
        _readyToJump = true;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        CheckGround(_groundDrag);
        SpeedControl(_movementSpeed);

    }

    private void FixedUpdate()
    {
        Movement(_movementSpeed);
    }

    private void Movement(int movementSpeed) 
    {

        Vector2 inputVector = _gameInput.GetMovementVector();
        Vector3 moveDir = _orientation.forward * inputVector.y + _orientation.right * inputVector.x;
        if (_grounded)
        {

            _rb.AddForce(moveDir * movementSpeed * 10f, ForceMode.Force);

        }
        else if (!_grounded)
        {

            _rb.AddForce(moveDir * movementSpeed * 10f * _airMultiplier, ForceMode.Force);

        }

    }

    private void Jump()
    {
        Debug.Log("jump");
        if (_readyToJump && _grounded)
        {

            _readyToJump = false;
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), _jumpCooldown);

        }

    }

    private void ResetJump()
    {

        _readyToJump = true;

    }

    private void SpeedControl(int moveSpeed)
    {

        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);

        }

    }

    private void CheckGround(float groundDrag)
    {

        _grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _whatIsGround);

        if (_grounded)
        {

            _rb.drag = groundDrag;

        }
        else
        {

            _rb.drag = 0;

        }

    }

    private void OnDisable()
    {

        _gameInput.OnJumpAction -= Jump;

    }

}
