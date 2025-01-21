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
	public override void Awake() {

		base.Awake();
		_startButton = View["Buttons/Start_Button"].GetComponent<Button>();
        _tutorialButton = View["Buttons/Tutorial_Button"].GetComponent<Button>();
        AddButtonListener("Buttons/Start_Button",Test);
        AddButtonListener("Buttons/Tutorial_Button",OnTutorial);
	}

	void Start() {
		UIInput.Instance.SelectUI(_startButton);
		UIInput.Instance.SelectUI(_tutorialButton);
	}

	public void Test()
	{
		Debug.Log("Click Start");
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

}
