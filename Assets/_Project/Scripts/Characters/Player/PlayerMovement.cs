using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameController _gameController;

    [Header ("Movement setting")]
    [SerializeField] private float _movementSpeed;


    public void Inject(GameController gameController) 
    {

        _gameController = gameController;

    }

    private void Update()
    {

        Movement();

    }

    private void Movement() 
    {

        Vector2 inputVector = _gameController.Player3rdPerson.Movement.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * _movementSpeed *Time.deltaTime;

    }

}
