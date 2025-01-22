using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Класс движения игрока: Movement, Jump, Dash
public class PlayerMovement : MonoBehaviour
{
    [Header("Reference")]
    private GameInput _gameInput;
    private Rigidbody _rb;
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _playerCam;

    [Header ("Movement setting")]
    [SerializeField] private int _movementSpeed;
    [SerializeField] private float _groundDrag = 5;

    [Header("Jump setting")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    private bool _readyToJump;
    private bool _resetVel = true;

    [Header("Dash Settings")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;  
    [SerializeField] private float _dashCooldown;  

    private bool _readyToDash = true;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;
    private float _groundDistance = 0.4f;
    private bool _grounded;



    public void Inject(GameInput gameInput) 
    {

        _gameInput = gameInput;
        _gameInput.OnJumpAction += Jump;
        _gameInput.OnDashAction += Dash;

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

    private void Dash()
    {
        if (_readyToDash && _grounded)
        {
            _readyToDash = false;

            // Направление рывка
            Vector2 inputVector = _gameInput.GetMovementVector();
            Vector3 dashDirection = _orientation.forward * inputVector.y + _orientation.right * inputVector.x;

            if (dashDirection == Vector3.zero)
            {
                dashDirection = _orientation.forward; // Если игрок не движется, рывок будет вперед
            }

            // Запускаем рывок
            StartCoroutine(PerformDash(dashDirection.normalized));

            // Устанавливаем перезарядку рывка
            Invoke(nameof(ResetDash), _dashCooldown);
        }
    }

    private IEnumerator PerformDash(Vector3 dashDirection)
    {
        float startTime = Time.time;

        while (Time.time < startTime + _dashDuration)
        {
            _rb.AddForce(dashDirection * _dashForce, ForceMode.Force);
            yield return null; // Ждем до следующего кадра
        }
    }

    private void ResetDash()
    {
        _readyToDash = true;
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
        _gameInput.OnDashAction -= Dash;

    }

}
