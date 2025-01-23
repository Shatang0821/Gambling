using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using Game.Core;
using UnityEngine;

public class TimeManager : UnitySingleton<TimeManager>
{
    private bool _isPausing = false;
    
    private float _elapsedTime = 0.0f;        // 経過時間を保存
    private bool _isMeasuring = false;       // 測定中かどうか
    private Coroutine _measureCoroutine = null;
    
    private bool _isCountingDown = false;   // カウントダウン中かどうか
    private float _remainingTime = 0.0f;   // 残り時間
    private Action _onCountdownComplete;   // カウントダウン終了時のコールバック
    private Coroutine _countdownCoroutine = null;
    /// <summary>
    /// 一時停止処理を開始します
    /// </summary>
    /// <param name="duration">一時停止の持続時間（秒）</param>
    /// <param name="timeScale">一時停止中の時間スケール（通常は0またはそれに近い値）</param>
    public void PauseTime(float duration, float timeScale = 0f)
    {
        if (!_isPausing)
        {
            StartCoroutine(PauseCoroutine(duration, timeScale));
        }
    }
    
    private IEnumerator PauseCoroutine(float duration, float timeScale)
    {
        _isPausing = true;

        // 現在の時間スケールを保存
        float originalTimeScale = Time.timeScale;

        // 一時停止の時間スケールを設定
        Time.timeScale = timeScale;

        // 必要に応じて、物理計算の時間ステップを調整（オプション）
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 指定した時間待機（Time.timeScaleの影響を受けない）
        yield return new WaitForSecondsRealtime(duration);

        // 元の時間スケールに戻す
        Time.timeScale = originalTimeScale;

        // 元の物理時間ステップを復元
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        _isPausing = false;
    }

    #region カウントダウン

    /// <summary>
    /// カウントダウンを開始します。
    /// </summary>
    /// <param name="duration">カウントダウン時間（秒）</param>
    /// <param name="onComplete">カウントダウン終了時に呼び出されるアクション</param>
    public void StartCountdown(float duration, Action onComplete)
    {
        if (_isCountingDown) return;

        _remainingTime = duration;
        _onCountdownComplete = onComplete;
        _isCountingDown = true;

        _countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }
    
    /// <summary>
    /// カウントダウンを停止します。
    /// </summary>
    public void StopCountdown()
    {
        if (!_isCountingDown) return;

        _isCountingDown = false;
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }
    }

    /// <summary>
    /// 残り時間を取得します。
    /// </summary>
    /// <returns>残り時間（秒）</returns>
    public float GetRemainingTime()
    {
        return _remainingTime;
    }

    /// <summary>
    /// カウントダウンのコルーチン。
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountdownCoroutine()
    {
        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            yield return null;
        }

        // カウントダウン終了
        _isCountingDown = false;
        _remainingTime = 0;

        // コールバックを呼び出す
        _onCountdownComplete?.Invoke();
    }

    #endregion
    
    

    #region タイマー

    /// <summary>
    /// 時間測定を開始します。
    /// </summary>
    public void StartMeasurement()
    {
        if (_isMeasuring) return;
        
        _isMeasuring = true;
        _measureCoroutine = StartCoroutine(MeasurementCoroutine());
    }

    /// <summary>
    /// 時間測定を停止します。
    /// </summary>
    public void StopMeasurement()
    {
        if (!_isMeasuring) return;

        _isMeasuring = false;
        if (_measureCoroutine != null)
        {
            StopCoroutine(_measureCoroutine);
            _measureCoroutine = null;
        }
    }
    
    /// <summary>
    /// 測定された経過時間を取得します。
    /// </summary>
    /// <returns>経過時間（秒）</returns>
    public float GetElapsedTime()
    {
        return _elapsedTime;
    }

    /// <summary>
    /// 時間測定をリセットします。
    /// </summary>
    public void ResetMeasurement()
    {
        _elapsedTime = 0.0f;
    }

    /// <summary>
    /// 測定中の経過時間を更新するコルーチン。
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeasurementCoroutine()
    {
        while (_isMeasuring)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    #endregion
    
}