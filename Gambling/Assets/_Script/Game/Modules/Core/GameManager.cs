using FrameWork.Utils;
using Modules.Input;

public class GameManager : UnityPersistentSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        InputManager.Instance.Initialize();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnEnable();
    }

    private void OnDisable()
    {
        InputManager.Instance.OnDisable();
    }
}
