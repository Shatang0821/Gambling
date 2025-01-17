using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

public class TimeManager : UnitySingleton<TimeManager>
{
    private bool _isPausing = false;
    
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
    
    
}