using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using FrameWork.UI;
using UnityEngine.InputSystem.HID;


public class UITitleCtrl : UICtrl
{
	private Button _startButton;
	private Button _tutorialButton;
	private HID.Button _exitButton;
	public GameObject Tutorial;
	public GameObject player;
    public AudioSource bgmAudioSource; // BGMのAudioSource
    public float fadeDuration = 2.0f; // フェードイン・アウトの時間
    public override void Awake() {

		base.Awake();
		_startButton = View["Buttons/Start_Button"].GetComponent<Button>();
        _tutorialButton = View["Buttons/Tutorial_Button"].GetComponent<Button>();
        AddButtonListener("Buttons/Start_Button",Test);
        AddButtonListener("Buttons/Tutorial_Button",OnTutorial);
	}

	void Start() {
		UIInput.Instance.SelectUI(_startButton);
		//UIInput.Instance.SelectUI(_tutorialButton);
        StartCoroutine(FadeInBGM());
    }

	public void Test()
	{
		Debug.Log("Click Start");

        StartCoroutine(FadeOutBGM(() => {
            // フェードアウト後の処理
            Debug.Log("BGM Faded Out. Starting Game...");
            // ここでシーン切り替えなどを行う
        }));
    }

	public void OnTutorial()
	{
		if (Tutorial.activeSelf)
		{
			Tutorial.SetActive(false);
			player.SetActive(false);
		}
		else
		{
            Tutorial.SetActive(true);
        }
	}

    // BGMをフェードイン
    private IEnumerator FadeInBGM()
    {
        float currentTime = 0f;
        float startVolume = 0f;

        bgmAudioSource.volume = startVolume;
        bgmAudioSource.Play();

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            bgmAudioSource.volume = Mathf.Lerp(startVolume, 0.3f, currentTime / fadeDuration);
            yield return null;
        }

        bgmAudioSource.volume = 0.3f;
    }

    // BGMをフェードアウト
    private IEnumerator FadeOutBGM(System.Action onFadeComplete = null)
    {
        float currentTime = 0f;
        float startVolume = bgmAudioSource.volume;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            bgmAudioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeDuration);
            yield return null;
        }

        bgmAudioSource.volume = 0f;
        bgmAudioSource.Stop();

        onFadeComplete?.Invoke();
    }
}
