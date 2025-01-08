using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

namespace Framework.Aduio
{
    public class AudioManager : UnityPersistentSingleton<AudioManager>
    {
        [SerializeField] AudioSource sFXPlayer;

        [SerializeField] float minPitch = 0.9f;

        [SerializeField] float maxPitch = 1.1f;

        public AudioData Attack;
        public AudioData Hit;

        /// <summary>
        /// 音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        public void PlaySFX(AudioData audioData)
        {
            sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volueme);
        }

        /// <summary>
        /// Pitchをランダムに変更して音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        public void PlayRandomSFX(AudioData audioData)
        {
            sFXPlayer.pitch = Random.Range(minPitch, maxPitch);
            PlaySFX(audioData);
        }

        /// <summary>
        /// Pitchをランダムに変更して音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        /// <param name="minPitch"></param>
        /// <param name="maxPitch"></param>
        public void PlayRandomSFX(AudioData audioData,float minPitch,float maxPitch)
        {
            sFXPlayer.pitch = Random.Range(minPitch, maxPitch);
            PlaySFX(audioData);
        }

        /// <summary>
        /// いくつかの音源をランダムに流す
        /// </summary>
        /// <param name="audioData">音データ配列</param>
        public void PlayRandomSFX(AudioData[] audioData)
        {
            PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
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
        public AudioClip audioClip;

        /// <summary>
        /// 音量
        /// </summary>
        public float volueme;
    }
}