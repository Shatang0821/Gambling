using System.Collections;
using FrameWork.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : UnityPersistentSingleton<SceneLoader>
{
    /// <summary>
    /// 指定したシーンを即時切り替えします。
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void LoadScene(string sceneName)
    {
        if (IsSceneValid(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"シーン '{sceneName}' はビルド設定に存在しません。");
        }
    }

    /// <summary>
    /// 指定した時間後にシーンを切り替えます。
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    /// <param name="delay">遅延時間（秒）</param>
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        if (IsSceneValid(sceneName))
        {
            StartCoroutine(LoadSceneWithDelayCoroutine(sceneName, delay));
        }
        else
        {
            Debug.LogError($"シーン '{sceneName}' はビルド設定に存在しません。");
        }
    }

    /// <summary>
    /// フェードイン/フェードアウトを伴うシーン切り替え。
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    /// <param name="fadeDuration">フェード時間（秒）</param>
    /// <param name="fadeCanvas">フェード用のCanvas</param>
    public void LoadSceneWithFade(string sceneName, float fadeDuration, CanvasGroup fadeCanvas)
    {
        if (IsSceneValid(sceneName))
        {
            StartCoroutine(LoadSceneWithFadeCoroutine(sceneName, fadeDuration, fadeCanvas));
        }
        else
        {
            Debug.LogError($"シーン '{sceneName}' はビルド設定に存在しません。");
        }
    }

    /// <summary>
    /// 指定時間後にシーンをロードするコルーチン。
    /// </summary>
    private IEnumerator LoadSceneWithDelayCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// フェードイン/フェードアウト付きシーン切り替えのコルーチン。
    /// </summary>
    private IEnumerator LoadSceneWithFadeCoroutine(string sceneName, float fadeDuration, CanvasGroup fadeCanvas)
    {
        if (fadeCanvas != null)
        {
            // フェードアウト
            yield return FadeCanvas(fadeCanvas, true, fadeDuration);
        }

        SceneManager.LoadScene(sceneName);

        if (fadeCanvas != null)
        {
            // フェードイン
            yield return FadeCanvas(fadeCanvas, false, fadeDuration);
        }
    }

    /// <summary>
    /// CanvasGroupを使用したフェードイン/フェードアウト。
    /// </summary>
    private IEnumerator FadeCanvas(CanvasGroup canvas, bool fadeOut, float duration)
    {
        float start = fadeOut ? 0 : 1;
        float end = fadeOut ? 1 : 0;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        canvas.alpha = end;
    }

    /// <summary>
    /// 指定されたシーンがビルド設定に含まれているかをチェック。
    /// </summary>
    private bool IsSceneValid(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuild = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameInBuild == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
