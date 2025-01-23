using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
 using FrameWork.UI;
 using Game.Core;
 using UnityEngine.EventSystems;


 public class UIGameCtrl : UICtrl
{
	private Text _timer;
	private Button _startButton;
	
	private bool flag = false;
	public override void Awake() {

		base.Awake();
		_timer = View["Timer"].GetComponent<Text>();
	}

	void Start()
	{
		View["WaveUI"].SetActive(false);
		_startButton = View["StartPanel/Start_Button"].GetComponent<Button>();
		UIInput.Instance.SelectUI(_startButton);
		AddButtonListener("StartPanel/Start_Button",StartGame);
	}

	private void Update()
	{
		// 何も選択していないときにデフォルト(startボタンを選択)
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			UIInput.Instance.SelectUI(_startButton);
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
		}
	}

	private void StartGame()
	{
		View["StartPanel"].SetActive(false);
		GameManager.Instance.ChangeState(GameState.Countdown);
		View["WaveUI"].SetActive(true);
		TimeManager.Instance.StartCountdown(3.0f,()=>GameManager.Instance.ChangeState(GameState.InGame));
	}
	
	
	
	/// <summary>
	/// タイマーの更新
	/// </summary>
	private void UpdateTimer()
	{
		_timer.text = TimeManager.Instance.GetElapsedTime().ToString("F2");
	}
}
