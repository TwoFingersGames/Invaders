using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingletonClass<UIManager>
{
    [SerializeField] private Text _seedText, _startButtonText;
    [SerializeField] private InputField _inputField;
    private void SetSeedText(string text)
    {
        _seedText.text = "Seed: " + text;
    }

    public void StartOrStopGame()
    {
        switch (GameManager.Instance.GameStates)
        {
            case GameStates.Start:
                {
                    GameManager.Instance.GameStates = GameStates.Play;
                    _startButtonText.text = "Stop";
                    return;
                }
            case GameStates.Play:
                {
                    GameManager.Instance.GameStates = GameStates.Stop;
                    _startButtonText.text = "Play";
                    return;
                }
            case GameStates.Stop:
                {
                    GameManager.Instance.GameStates = GameStates.Play;
                    _startButtonText.text = "Stop";
                    return;
                }
            default:
                {
                    GameManager.Instance.GameStates = GameStates.Start;
                    _startButtonText.text = "Start";
                    return;
                }
        }
    }
    public void SetRandomSeed()
    {
        SetSeedText(LevelCreator.Instance.SetSeed(UnityEngine.Random.Range(0, 999999999)).ToString());
    }

    public void HideSeedText()
    {
        _inputField.image.color = new Vector4(0.4f, 0.4f, 0.4f, 1);
    }
    public void InputText(string value)
    {
        SetSeedText(LevelCreator.Instance.SetSeed(Convert.ToInt32(value)).ToString());
        _inputField.text = "";
        _inputField.image.color = new Vector4(0.4f, 0.4f, 0.4f, 0.4f);
    }
}
