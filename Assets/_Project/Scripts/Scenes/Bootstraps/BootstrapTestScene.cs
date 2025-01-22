using UnityEngine;

public class BootstrapTestScene : MonoBehaviour
{
    private GameController _gameController;
    [SerializeField] private GameInput _gameinput;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Awake() 
    {
        EnableInputMap();
        _gameinput.Inject(_gameController);
        _playerMovement.Inject(_gameinput);
    }

    private void EnableInputMap() 
    {
        _gameController = new GameController();
        _gameController.Player3rdPerson.Enable();
    }
}
