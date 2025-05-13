using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] private Yandex _ya;

    [SerializeField] private PlayerSettings _player;
    [SerializeField] private Transform _parent;

    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _pause;

    [SerializeField] private GameObject _restart;
    [SerializeField] private Text _restartText;

    [SerializeField] private List<GameObject> _soundButtons;
    [SerializeField] private GameObject _soundManager;

    private bool _ad = false;

    public void Play() {
        _play.SetActive(true);
        _pause.SetActive(false);
    }

    public void Pause() {
        _play.SetActive(false);
        _pause.SetActive(true);
        _ya.GetLangNow();
    }

    public void AdSoundOn()
    {
        _ad = false ;
        if (_player.sound)
        {
            _soundManager.SetActive(true);
        }
    }
    public void AdSoundOff()
    {
        _ad = true;
        _soundManager.SetActive(false);
    }

    public void SoundControl(bool value)
    {
        _soundManager?.SetActive(value);
        _player.sound = value;
        _soundButtons[0].SetActive(!_soundButtons[0].activeSelf);
        _soundButtons[1].SetActive(!_soundButtons[1].activeSelf);
    }

    public void RestartButton()
    {
        _restart.SetActive(true);
        if (_ya.lang == "en")
        {
            _restartText.text = "Do you want to restart the game?";
        }
        else if (_ya.lang == "tr")
        {
            _restartText.text = "Oyuna yeniden başlamak ister misiniz?";
        }
    }

    public void RestartGame()
    {
        foreach (Transform child in _parent)
        {
            Destroy(child.gameObject);
        }
        _player.ResetData();
        BackToPause();
        Play(); 
    }

    public void BackToPause()
    {
        _restart.SetActive(false);       
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (_player.sound && !_ad)
            {
                _soundManager.SetActive(true);
            }
        }
        else
        {
            _soundManager.SetActive(false);
        }
    }
}
