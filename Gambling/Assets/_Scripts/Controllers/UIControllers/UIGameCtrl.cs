using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Framework.Aduio;
using FrameWork.EventCenter;
using FrameWork.UI;
 using Game.Core;
 using UnityEngine.EventSystems;


 public class UIGameCtrl : UICtrl
{
	private Text _timer;
	private Button _startButton;
	private GameObject _resultPanel;
	
	private GameObject _currentPanel;
	private bool flag = false;
	
	public AudioData bgm;
	public Color loadToColor = Color.black;
	public override void Awake() {

		base.Awake();
		_startButton = View["StartPanel/Start_Button"].GetComponent<Button>();
		
		AddButtonListener("ResultPanel/Start_Button",StartGame);
		AddButtonListener(
			"ResultPanel/Return_Button", () => StartCoroutine(
				AudioManager.Instance.FadeOutBGM
				(
					() =>
						{
							// フェードアウト後の処理
							Debug.Log("BGM Faded Out. Starting Game...");
							// ここでシーン切り替えなどを行う
							Initiate.Fade("Title", loadToColor, 1.0f);
						}
				)
			)
		);
		_timer = View["Timer"].GetComponent<Text>();
		_resultPanel = View["ResultPanel"];
	}

	private void Start()
	{
		StartCoroutine(AudioManager.Instance.FadeInBGM(bgm));
		OnIdle();
	}

	private void OnEnable()
	{
		EventCenter.AddListener(GameState.Idle, OnIdle);
		EventCenter.AddListener(GameState.Result, OnResult);
	}

	private void OnDisable()
	{
		EventCenter.RemoveListener(GameState.Idle, OnIdle);
		EventCenter.RemoveListener(GameState.Result, OnResult);
	}


	private void Update()
	{
		// 何も選択していないときにデフォルト(startボタンを選択)
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			UIInput.Instance.SelectUI(_startButton);
		}

		if (_resultPanel.activeSelf && EventSystem.current.currentSelectedGameObject == null)
		{
			UIInput.Instance.SelectUI(View["ResultPanel/Start_Button"].GetComponent<Button>());
		}
		UpdateTimer();
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
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				UIInput.Instance.SelectUI(_startButton);
			}
			
			if (_resultPanel.activeSelf && EventSystem.current.currentSelectedGameObject == null)
			{
				UIInput.Instance.SelectUI(View["ResultPanel/Start_Button"].GetComponent<Button>());
			}
		}
	}
	/// <summary>
	/// アイドル状態の処理
	/// </summary>
	private void OnIdle()
	{
		ShowPanel(View["StartPanel"]);
		_resultPanel.SetActive(false);
		View["Timer"].SetActive(false);
		View["WaveUI"].SetActive(false);
		_startButton = View["StartPanel/Start_Button"].GetComponent<Button>();
		UIInput.Instance.SelectUI(_startButton);
		AddButtonListener("StartPanel/Start_Button",StartGame);
	}

	private void StartGame()
	{
		HideCurrentPanel();
		GameManager.Instance.ChangeState(GameState.Countdown);
		
		View["WaveUI"].SetActive(true);
		TimeManager.Instance.StartCountdown(3.0f,OnStart);
	}

	private void OnStart()
	{
		GameManager.Instance.ChangeState(GameState.InGame);
		View["Timer"].SetActive(true);
		View["WaveUI"].SetActive(false);
	}
	
	private void OnResult()
	{
		View["Timer"].SetActive(false);
		ShowPanel(_resultPanel);
		var text = View["ResultPanel/Timer"].GetComponent<Text>();
		// 結果表示
		if (GameManager.Instance.IsGameWin)
		{
			text.color = Color.black;
			text.text = TimeManager.Instance.GetElapsedTime().ToString("F2");
		}
		else
		{
			text.color = Color.red;
			text.text = "Lose";
		}
		UIInput.Instance.SelectUI(View["ResultPanel/Start_Button"].GetComponent<Button>());
	}
	
	/// <summary>
	/// タイマーの更新
	/// </summary>
	private void UpdateTimer()
	{
		_timer.text = TimeManager.Instance.GetElapsedTime().ToString("F2");
	}
	
	private void ShowPanel(GameObject panel)
	{
		if (_currentPanel && _currentPanel.activeSelf)
		{
			_currentPanel.SetActive(false);
		}

		_currentPanel = panel;
		_currentPanel.SetActive(true);
	}

	private void HideCurrentPanel()
	{
		if(!_currentPanel && !_currentPanel.activeSelf) return;
		
		_currentPanel.SetActive(false);
	}
}
