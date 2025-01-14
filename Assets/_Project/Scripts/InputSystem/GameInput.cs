using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    private GameController _gameController;

    public Action OnJumpAction;

    public void Inject(GameController gameController) 
    {
        _gameController = gameController;
        _gameController.Player3rdPerson.Jump.performed += Jump;

    }
    
    public Vector2 GetMovementVector() 
    {
        
        Vector2 inputVector = _gameController.Player3rdPerson.Movement.ReadValue<Vector2>();
        return inputVector;

    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJumpAction?.Invoke();
    }

    private void MeleeAttack(InputAction.CallbackContext context) 
    {
        Debug.Log("MeleeAttack");
    }

    private void MagicAttack(InputAction.CallbackContext context)
    {
        Debug.Log("MagicAttack");
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
    }

    private void UseTeleport(InputAction.CallbackContext context)
    {
        Debug.Log("UseTeleport");
    }

    private void UsePotion(InputAction.CallbackContext context)
    {
        Debug.Log("UsePotion");
    }

    private void Inventory(InputAction.CallbackContext context)
    {
        Debug.Log("Inventory");
    }

    private void Map(InputAction.CallbackContext context)
    {
        Debug.Log("Map");
    }

    private void Use(InputAction.CallbackContext context)
    {
        Debug.Log("Use");
    }

    private void SkillsMenu(InputAction.CallbackContext context)
    {
        Debug.Log("SkillsMenu");
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");
    }

    private void OnDisable()
    {
        _gameController.Player3rdPerson.Jump.performed -= Jump;

    }

}
