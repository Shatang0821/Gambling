using System.Collections;
using FrameWork.Utils;
using UnityEngine;

public class CameraManager : UnitySingleton<CameraManager>
{
    private Transform _cameraTransform;
    private Vector3 _originalPosition;
    private Coroutine _currentShakeCoroutine; // 現在実行中のシェイクを管理

    protected override void Awake()
    {
        base.Awake();
        _cameraTransform = Camera.main.transform;
        if (_cameraTransform != null)
        {
            _originalPosition = _cameraTransform.position;
        }
    }

    /// <summary>
    /// カメラシェイクを開始します。連続呼び出しにも対応。
    /// </summary>
    /// <param name="duration">シェイクの持続時間</param>
    /// <param name="magnitude">シェイクの強さ</param>
    public void ShakeCamera(float duration, float magnitude)
    {
        // 既存のシェイクを停止
        if (_currentShakeCoroutine != null)
        {
            StopCoroutine(_currentShakeCoroutine);
            _cameraTransform.position = _originalPosition; // 元の位置に戻す
        }

        // 新しいシェイクを開始
        _currentShakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // ランダムな揺れを計算
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // カメラの位置を揺らす
            _cameraTransform.position = _originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;

            // 次のフレームまで待機
            yield return null;
        }

        // カメラ位置を元に戻す
        _cameraTransform.position = _originalPosition;

        // シェイク終了を記録
        _currentShakeCoroutine = null;
    }
}