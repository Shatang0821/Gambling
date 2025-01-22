using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Aduio
{
    public class AudioManager : UnityPersistentSingleton<AudioManager>
    {
        [SerializeField] AudioSource sFXPlayer;
        [SerializeField] AudioSource BGMPlayer;
        public float fadeDuration = 2.0f; // フェードイン・アウトの時間
        private readonly float DefaultPitch = 1;
        /// <summary>
        /// 音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        public void PlaySFX(AudioData audioData)
        {
            sFXPlayer.volume = audioData.Volueme;
            if (audioData.IsPlayRandomPitch)
            {
                PlayRandomSFX(audioData);
            }
            else
            {
                sFXPlayer.pitch = DefaultPitch;
                sFXPlayer.PlayOneShot(audioData.AudioClip, audioData.Volueme);
            }
        }

        /// <summary>
        /// Pitchをランダムに変更して音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        private void PlayRandomSFX(AudioData audioData)
        {
            sFXPlayer.pitch = Random.Range(audioData.MinPitch, audioData.MaxPitch);
            sFXPlayer.PlayOneShot(audioData.AudioClip, audioData.Volueme);
        }

        /// <summary>
        /// いくつかの音源をランダムに流す
        /// </summary>
        /// <param name="audioData">音データ配列</param>
        public void PlayRandomSFX(AudioData[] audioData)
        {
            PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
        }

        /// <summary>
        /// BGMをフェードインしながら再生する
        /// </summary>
        /// <param name="audioData">BGM音データ</param>
        public IEnumerator FadeInBGM(AudioData audioData)
        {
            if (audioData == null || audioData.AudioClip == null) yield break;

            float currentTime = 0f;
            BGMPlayer.clip = audioData.AudioClip;
            BGMPlayer.volume = 0f;
            BGMPlayer.Play();

            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                BGMPlayer.volume = Mathf.Lerp(0f, audioData.Volueme, currentTime / fadeDuration);
                yield return null;
            }

            BGMPlayer.volume = audioData.Volueme;
        }

        /// <summary>
        /// BGMをフェードアウトする
        /// </summary>
        /// <param name="onFadeComplete">フェード完了時のコールバック</param>
        public IEnumerator FadeOutBGM(System.Action onFadeComplete = null)
        {
            float currentTime = 0f;
            float startVolume = BGMPlayer.volume;

            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                BGMPlayer.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeDuration);
                yield return null;
            }

            BGMPlayer.volume = 0f;
            BGMPlayer.Stop();
            onFadeComplete?.Invoke();
        }
    }

    /// <summary>
    /// AudioClipとvoluemeをまとめるクラス
    /// </summary>
    [System.Serializable]
    public class AudioData
    {
        /// <summary>
        /// 音源
        /// </summary>
        [FormerlySerializedAs("audioClip")] public AudioClip AudioClip;

        /// <summary>
        /// 音量
        /// </summary>
        [FormerlySerializedAs("volueme")] public float Volueme;

        public bool IsPlayRandomPitch;
        
        public float MinPitch;

        public float MaxPitch;
    }
}