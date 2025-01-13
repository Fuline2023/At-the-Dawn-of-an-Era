using UnityEngine;

public class BootstrapTestScene : MonoBehaviour
{
    [SerializeField] private InjectorTestScene _injectorTestScene; 
    private GameController _gameController;

    private void Awake() 
    {
        EnableInputMap();
        _injectorTestScene.Constract(_gameController);
    }

    private void EnableInputMap() 
    {
        _gameController = new GameController();
        _gameController.Player3rdPerson.Enable();
    }
}
