using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        Rotate();
    }

    private void Rotate() {

        Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            _playerBody.forward = Vector3.Slerp(_playerBody.forward, inputDir.normalized, Time.deltaTime * _rotationSpeed);
        }

    }

}
