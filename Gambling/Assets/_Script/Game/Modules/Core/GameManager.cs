using FrameWork.Utils;
using Game.Input;

namespace Game.Core
{
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
}
