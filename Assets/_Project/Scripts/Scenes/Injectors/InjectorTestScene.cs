using UnityEngine;

public class InjectorTestScene : MonoBehaviour
{

    [SerializeField] private PlayerMovement _playerMovement;

    public void Constract(GameController gameController) 
    {
        
        _playerMovement.Inject(gameController);

    }

}
