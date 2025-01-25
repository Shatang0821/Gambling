using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using FrameWork.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using Framework.Aduio;
using FrameWork.EventCenter;
using Game.Entity;
using Game.Event;
using Game.Input;
using UnityEngine.InputSystem.UI;


public class UITitleCtrl : UICtrl
{
	/// <summary>
	/// UIの入力を管理するコンポーネント
	/// </summary>
	[SerializeField] InputSystemUIInputModule UIInputModule;
	private Button _startButton;
	private Button _tutorialButton;
	private Button _exitButton;

	public GameObject Tutorial;
	public Player player;
	public AudioData bgm;
    public Color loadToColor = Color.black;

    public override void Awake() {

		base.Awake();
		_startButton = View["Button Container/Start_Button"].GetComponent<Button>();
        _tutorialButton = View["Button Container/Tutorial_Button"].GetComponent<Button>();
        AddButtonListener("Button Container/Start_Button",GameStart);
        AddButtonListener("Button Container/Tutorial_Button",OnTutorial);
	}

	void Start()
	{
		UIInput.Instance.SelectUI(_startButton);
		//UIInput.Instance.SelectUI(_tutorialButton);
        StartCoroutine(AudioManager.Instance.FadeInBGM(bgm));
        Tutorial.SetActive(false);
    }

	private void OnEnable()
	{
		EventCenter.AddListener(TitleEvents.OnClosePanel, CloseTutorial);
	}

	private void OnDisable()
	{
		EventCenter.RemoveListener(TitleEvents.OnClosePanel, CloseTutorial);
	}
	
	/// <summary>
	/// アプリケーションを開いたら
	/// </summary>
	/// <param name="hasFocus"></param>
	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			// 現在選択中のボタンが消えたら選択できるように
			if (EventSystem.current?.currentSelectedGameObject == null)
			{
				UIInput.Instance.SelectUI(_startButton);
			}
		}
	}

	private void Update()
	{
		// 何も選択していないときにデフォルト(startボタンを選択)
		if (EventSystem.current?.currentSelectedGameObject == null)
		{
			UIInput.Instance.SelectUI(_startButton);
		}

		if (player.isActiveAndEnabled)
		{
			player.LogicUpdate();
		}
	}

	private void FixedUpdate()
	{
		if (player.isActiveAndEnabled)
		{
			player.PhysicsUpdate();
		}
	}

	public void GameStart()
	{
		Debug.Log("Click Start");

        StartCoroutine(AudioManager.Instance.FadeOutBGM(() => {
            // フェードアウト後の処理
            Debug.Log("BGM Faded Out. Starting Game...");
			// ここでシーン切り替えなどを行う
			Initiate.Fade("MainGame", loadToColor, 1.0f);
        }));
    }

	public void OnTutorial()
	{
		Tutorial.SetActive(true);
		UIInputModule.enabled = false;
		InputManager.Instance.EnableTitleInput();
	}

	private void CloseTutorial()
	{
		Debug.Log("Close Tutorial");
		player.gameObject.SetActive(false);
		Tutorial.SetActive(false);
		UIInputModule.enabled = true;
		UIInput.Instance.SelectUI(_startButton);
		InputManager.Instance.DisableAllInputs();
	}
	
}
