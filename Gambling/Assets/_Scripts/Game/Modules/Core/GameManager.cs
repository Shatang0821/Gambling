using System;
using Framework.Aduio;
using FrameWork.Utils;
using Game.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public enum GameState
    {
        Idle,
        Countdown,
        InGame,
        Result
    }
    public class GameManager : UnitySingleton<GameManager>
    {
        [SerializeField]private PlayerManager playerManager;
        [SerializeField]private EnemyManager enemyManager;
        
        [SerializeField] private GameState _currentState;
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

        private void Start()
        {
            ChangeState(GameState.Idle);
            
            playerManager.SpawnPlayer();
            //enemyManager.SpawnEnemy();
            
        }
        
        private void Update()
        {
            switch (_currentState)
            {
                case GameState.Idle:
                    break;
                case GameState.Countdown:
                    break;
                case GameState.InGame:
                    playerManager.UpdatePlayer();
                    break;
                case GameState.Result:
                    break;
            }
        }

        private void FixedUpdate()
        {
            switch (_currentState)
            {
                case GameState.Idle:
                    break;
                case GameState.Countdown:
                    break;
                case GameState.InGame:
                    playerManager.FixedUpdatePlayer();
                    break;
                case GameState.Result:
                    break;
            }
        }
        
        public void ChangeState(GameState newState)
        {
            _currentState = newState;
            switch (_currentState)
            {
                case GameState.Idle:
                    Debug.Log("Game is idle.");
                    break;
                case GameState.Countdown:
                    Debug.Log("Countdown started.");
                    //_uiManager.StartCountdown(() => ChangeState(GameState.InGame));
                    break;
                case GameState.InGame:
                    Debug.Log("Game started.");
                    //_playerManager.SpawnPlayer();
                    //_enemyManager.SpawnEnemies();
                    break;
                case GameState.Result:
                    Debug.Log("Game ended. Showing result.");
                    //_uiManager.ShowResult();
                    break;
            }
        }
    }
}
