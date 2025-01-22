using Framework.Aduio;
using FrameWork.Utils;
using Game.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class GameManager : UnityPersistentSingleton<GameManager>
    {

        public Text timerText; // UIのTextを割り当てる
        public Text resultText; // UIのTextを割り当てる
        private float elapsedTime; // 経過時間を追跡する変数
        private bool isRunning; // タイマーが動作中かを判定するフラグ

        public bool isResult;
        public bool isClear;
        public GameObject ResultPanel;
        public Color loadToColor = Color.black;

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
            elapsedTime = 0f;
            isRunning = true;
            isResult = false;
            isClear = false;
            StartCoroutine(CountUpTimer());
            ResultPanel.SetActive(false);

            EnemyManager.Instance.SpawnEnemy();
            

        }

        private void Update()
        {
            if (isResult)
            {
                timerText.text = "";
                ShowResult();
            }

            if (isClear)
            {
                StartCoroutine(AudioManager.Instance.FadeOutBGM(() => {
                    // フェードアウト後の処理
                    Debug.Log("BGM Faded Out. Starting Game...");
                }));
                Initiate.Fade("Title", loadToColor, 1.0f);
                isClear = false;
            }
        }

        private IEnumerator CountUpTimer()
        {
            while (isRunning)
            {
                // 経過時間を増加させる
                elapsedTime += Time.deltaTime;

                // 秒数をフォーマットして表示
                UpdateTimerDisplay(elapsedTime);

                // 次のフレームまで待機
                yield return null;
            }
        }

        private void UpdateTimerDisplay(float time)
        {
            // 時間をフォーマット (分:秒:ミリ秒)
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
            timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }

        public void ShowResult()
        {
            if (!ResultPanel.activeSelf)
            {
                // タイマー停止
                isRunning = false;

                // 結果パネルを表示
                ResultPanel.SetActive(true);

                // 結果をカウントアップで表示
                StartCoroutine(CountUpResult());
            }
        }

        private IEnumerator CountUpResult()
        {
            float displayTime = 0f; // 結果表示用のカウント
            float incrementSpeed = 0.03f; // カウントアップ速度
            float finalTime = elapsedTime; // 最終的な経過時間

            yield return new WaitForSeconds(1.5f);
            while (displayTime < finalTime)
            {
                displayTime += Time.deltaTime / incrementSpeed;

                // 表示用タイマーを更新
                UpdateResultDisplay(displayTime);

                // 次のフレームまで待機
                yield return null;
            }

            // 最終的な正確な時間を表示
            UpdateResultDisplay(finalTime);
        }

        private void UpdateResultDisplay(float time)
        {
            // 時間をフォーマット (分:秒:ミリ秒)
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
            resultText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }

        public void NextButton()
        {
            isResult = false;
            ResultPanel.SetActive(false);
            elapsedTime = 0;
            EnemyManager.Instance.SpawnEnemy();
        }
    }
}
