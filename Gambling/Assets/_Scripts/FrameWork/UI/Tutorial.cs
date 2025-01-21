using Framework.Entity;
using Game.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : EntityObject
{
    private UITutorial _uITutorial;
    [SerializeField]
    private Sprite[] _PressedButton;
    [SerializeField] 
    private Sprite[] _ReleasedButton;
    [SerializeField]
    private SpriteRenderer[] _Buttons;
    public GameObject player;

    private void Awake()
    {
        _uITutorial = AddEntityComponent(new UITutorial());
        for (int i = 0; i >_Buttons.Length; i++)
        {
            _Buttons[i] = gameObject.GetComponent<SpriteRenderer>();
        }
    }
    private void OnEnable()
    {
        _uITutorial.OnEnable();
    }

    private void OnDisable()
    {
        _uITutorial.OnDisable();
    }

    private void Update()
    {
         if (_uITutorial.DirectionlInput.y > 0)
         {
            _Buttons[0].sprite = _PressedButton[0];
         }
         else
        {
            _Buttons[0].sprite = _ReleasedButton[0];
        }

        if (_uITutorial.DirectionlInput.x < 0)
        {
            _Buttons[1].sprite = _PressedButton[1];
        }
        else
        {
            _Buttons[1].sprite = _ReleasedButton[1];
        }

        if (_uITutorial.DirectionlInput.x > 0)
        {
            _Buttons[2].sprite = _PressedButton[2];
        }
        else
        {
            _Buttons[2].sprite = _ReleasedButton[2];
        }

        if (_uITutorial.AttackInput)
        {
            _Buttons[3].sprite = _PressedButton[3];
        }
        else
        {
            _Buttons[3].sprite = _ReleasedButton[3];
        }

        if (_uITutorial.DefenceInput)
        {
            _Buttons[4].sprite = _PressedButton[4];
        }
        else
        {
            _Buttons[4].sprite = _ReleasedButton[4];
        }

        if (_uITutorial.DashInput)
        {
            for(int i = 0;i<3;i++)
            {
                _Buttons[5 + i].sprite = _PressedButton[5 + i];
            }
            
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                _Buttons[5 + i].sprite = _ReleasedButton[5 + i];
            }
        }
    }

    public void ActivePlayer()
    {
        player.SetActive(true);
    }

}
