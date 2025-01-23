using System;
using System.Collections.Generic;
using FrameWork.EventCenter;
using Game.Entity;
using Game.Event;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("入力項目リスト")]
    [SerializeField] private List<InputTutorialItem> tutorialItems;
    [SerializeField] private Player _player;
    [SerializeField] private InputTutorialItem right;
    [SerializeField] private InputTutorialItem left;
    
    [SerializeField] private Image lStickImage;
    [SerializeField] private Sprite[] lStick;
    private void OnEnable()
    {
        // 各アイテムのイベントを初期化してバインド
        foreach (var item in tutorialItems)
        {
            BindInputEvent(item);
        }
        EventCenter.AddListener<float>(InputEvents.OnHorizontal, value =>UpdateHorizontalSprite(right,left,value));
        InputManager.Instance.CurrentDevice.Register(new Action<InputDevice>(OnDeviceChanged));
    }

    private void OnDisable()
    {
        // イベントバインドを解除
        foreach (var item in tutorialItems)
        {
            EventCenter.RemoveListener<bool>(InputEvents.OnJump, state => UpdateSprite(item, state));
            EventCenter.RemoveListener<bool>(InputEvents.OnAttack, state => UpdateSprite(item, state));
            EventCenter.RemoveListener<bool>(InputEvents.OnDefence, state => UpdateSprite(item, state));
            EventCenter.RemoveListener<bool>(InputEvents.OnDash, state => UpdateSprite(item, state));
        }
        EventCenter.RemoveListener<float>(InputEvents.OnHorizontal, value =>UpdateHorizontalSprite(right,left,value));
        InputManager.Instance.CurrentDevice.UnRegister(new Action<InputDevice>(OnDeviceChanged));
        
    }
    
    private void BindInputEvent(InputTutorialItem item)
    {
        // イベントセンターを使用して対応するイベントをバインド
        switch (item.EventName)
        {
            case "Jump":
                EventCenter.AddListener<bool>(InputEvents.OnJump, state => UpdateSprite(item, state));
                break;
            case "Attack":
                EventCenter.AddListener<bool>(InputEvents.OnAttack, state => UpdateSprite(item, state));
                break;
            case "Defence":
                EventCenter.AddListener<bool>(InputEvents.OnDefence, state => UpdateSprite(item, state));
                break;
            case "Dash":
                EventCenter.AddListener<bool>(InputEvents.OnDash, state => UpdateSprite(item, state));
                break;
        }
    }

    
    protected void OnDeviceChanged(InputDevice device)
    {
        foreach (var item in tutorialItems)
        {
            UpdateSprite(item,false);
        }

        UpdateHorizontal(device);

    }

    private void UpdateHorizontal(InputDevice device)
    {
        switch (device)
        {
            case Keyboard:
                lStickImage.enabled = false;
                right.Image.enabled = true;
                left.Image.enabled = true;
                break;
            case Gamepad:
                lStickImage.enabled = true;
                right.Image.enabled = false;
                left.Image.enabled = false;
                break;
        }
    }

    private void UpdateSprite(InputTutorialItem item, bool isPressed)
    {
        // 入力デバイスタイプに応じたスプライトを更新
        var spriteSet = item.GetCurrentSpriteSet();
        item.Image.sprite = isPressed ? spriteSet.PressedSprite : spriteSet.ReleasedSprite;
    }

    private void UpdateHorizontalSprite(InputTutorialItem right,InputTutorialItem left,float value)
    {
        SpriteSet spriteSet;
        var currentDevice = InputManager.Instance.CurrentDevice.Value;
        switch (currentDevice)
        {
            case Keyboard:
                if (value > 0)
                {
                    spriteSet = this.right.GetCurrentSpriteSet();
                    right.Image.sprite = spriteSet.PressedSprite; // 右方向入力
                }
                else if (value < 0)
                {
                    spriteSet = this.left.GetCurrentSpriteSet();
                    left.Image.sprite = spriteSet.PressedSprite; // 左方向入力
                }
                else
                {
                    // デフォルト状態に戻す
                    spriteSet = this.right.GetCurrentSpriteSet();
                    right.Image.sprite = spriteSet.ReleasedSprite; 
                    spriteSet = this.left.GetCurrentSpriteSet();
                    left.Image.sprite = spriteSet.ReleasedSprite;
                }
                //Debug.Log(_currentDevice);
                break;
            case Gamepad:
                if (value > 0)
                {
                    lStickImage.sprite = lStick[1];
                }
                else if (value < 0)
                {
                    lStickImage.sprite = lStick[2];
                }
                else
                {
                    lStickImage.sprite = lStick[0];
                }
                break;
        }
        
    }

    public void ActivePlayer()
    {
        _player.gameObject.SetActive(true);
    }
}

[System.Serializable]
public class InputTutorialItem
{
    public string InputName;              // 入力の名前（例："Jump"）
    public SpriteSet KeyboardSprites;     // キーボード用スプライトセット
    public SpriteSet GamepadSprites;      // コントローラー用スプライトセット
    public string EventName;              // イベント名
    public Image Image;       // スプライト表示用コンポーネント

    // 現在のデバイスタイプに応じたスプライトセットを取得
    public SpriteSet GetCurrentSpriteSet()
    {
        return InputManager.Instance.CurrentDevice.Value == Keyboard.current ? KeyboardSprites : GamepadSprites;
    }
}

[System.Serializable]
public class SpriteSet
{
    public Sprite PressedSprite;   // 押下時のスプライト
    public Sprite ReleasedSprite;  // 通常時のスプライト
}

public enum InputDeviceType
{
    Keyboard,  // キーボード
    Gamepad    // コントローラー
}
