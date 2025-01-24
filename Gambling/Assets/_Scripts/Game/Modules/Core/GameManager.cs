using System;
using Framework.Aduio;
using FrameWork.Utils;
using Game.Input;
using System.Collections;
using FrameWork.EventCenter;
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
        public GameState CurrentState => _currentState;
        public bool IsGameWin;
        private void Start()
        {
            ChangeState(GameState.Idle);
            
            //playerManager.SpawnPlayer();
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
                    EventCenter.TriggerEvent(GameState.Idle);
                    Debug.Log("Game is idle.");
                    break;
                case GameState.Countdown:
                    Debug.Log("Countdown started.");
                    EventCenter.TriggerEvent(GameState.Countdown);
                    break;
                case GameState.InGame:
                    EventCenter.TriggerEvent(GameState.InGame);
                    Debug.Log("Game started.");
                    // タイマースタート
                    TimeManager.Instance.ResetMeasurement();
                    TimeManager.Instance.StartMeasurement();
                    InputManager.Instance.EnableGameplayInput();
                    playerManager.SpawnPlayer();
                    enemyManager.SpawnEnemy(new (4, -1.0f, 0));
                    break;
                case GameState.Result:
                    TimeManager.Instance.StopMeasurement();
                    EventCenter.TriggerEvent(GameState.Result);
                    enemyManager.DestroyEnemy();
                    playerManager.DestroyPlayer();
                    Debug.Log("Game ended. Showing result.");
                    //_uiManager.ShowResult();
                    break;
            }
        }
    }
}
