using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Aduio
{
    public class AudioManager : UnityPersistentSingleton<AudioManager>
    {
        [SerializeField] AudioSource sFXPlayer;

        /// <summary>
        /// 音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        public void PlaySFX(AudioData audioData)
        {
            sFXPlayer.PlayOneShot(audioData.AudioClip, audioData.Volueme);
        }

        /// <summary>
        /// Pitchをランダムに変更して音を出す
        /// </summary>
        /// <param name="audioData">音データ</param>
        public void PlayRandomSFX(AudioData audioData)
        {
            sFXPlayer.pitch = Random.Range(audioData.MinPitch, audioData.MaxPitch);
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