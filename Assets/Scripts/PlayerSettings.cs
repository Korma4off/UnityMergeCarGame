using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Yandex _ya;

    [SerializeField] private AudioSource _moneySound;
    [SerializeField] private AudioSource _errorSound;

    [SerializeField] private CarGeneration _generator;
    [SerializeField] private SavingData _save;

    [SerializeField] private int _wallet = 1234;
    [SerializeField] private Text _walletText;

    [SerializeField] private int _score;
    [SerializeField] private Text _scoreText;

    [SerializeField] private int _parkingLvl = 0;
    [SerializeField] private int _parkingLvlPrice = 70;
    [SerializeField] private Animator _parkingLvlAnimator;
    [SerializeField] private Text _parkingLvlPriceText;
    [SerializeField] private Button _parkingLvlButton;

    [SerializeField] private int _buyLvl = 0;
    [SerializeField] private Animator _buyAnimator;
    [SerializeField] private Text _buyPriceText;
    [SerializeField] private Button _buyButton;

    [SerializeField] private int _carLvl = 0;
    [SerializeField] private int _carLvlPrice = 70;
    [SerializeField] private Animator _carLvlAnimator;
    [SerializeField] private Text _carLvlPriceText;
    [SerializeField] private Button _carLvlButton;

    public int coinsX = 1;

    public bool sound;
    public int parkingLvl => _parkingLvl;
    public int buyLvl => _buyLvl;
    public int carLvl => _carLvl;

    public int wallet => _wallet;
    public int score => _score;

    private float _timer = 61f;

    private void Start()
    {
        _parkingLvlPriceText.text = StrFormat(_parkingLvlPrice);

        _buyPriceText.text = StrFormat(20 + _buyLvl * 10);

        _carLvlPriceText.text = StrFormat(_carLvlPrice);

        _walletText.text = StrFormat(_wallet);
        _scoreText.text = StrFormat(_score);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }

    public void ParkingLvlUp(bool val = false)
    {
        if (val || Buy(_parkingLvlPrice))
        {
            _parkingLvl += 1;
            _parkingLvlPrice += 30;
            _parkingLvlPriceText.text = StrFormat(_parkingLvlPrice);
            if (_carLvl == 8)
            {
                _parkingLvlButton.enabled = false;
            }
        }
        else
        {
            _errorSound.Play();
            _parkingLvlAnimator.SetTrigger("Anim");
        }
    }

    public void buyLvlUp()
    {
        if (_wallet >= (20 + _buyLvl * 10) && _generator.RandomGenerate())
        {
            if (Buy(20 + _buyLvl * 10))
            {
                _buyLvl += 1;
                int buyPrice = 20 + _buyLvl * 10;
                _buyPriceText.text = StrFormat(buyPrice);
            }
        }
        else
        {
            _errorSound.Play();
            _buyAnimator.SetTrigger("Anim");
        }

    }
    public void CarLvlUp(bool val = false)
    {

        if (val || _carLvl < 9 && Buy(_carLvlPrice))
        {
            _carLvl += 1;
            _carLvlPrice += _carLvlPrice;
            _carLvlPriceText.text = StrFormat(_carLvlPrice);
            if (_carLvl == 8)
            {
                _carLvlButton.gameObject.SetActive(false);
            }
        }
        else
        {
            _errorSound.Play();
            _carLvlAnimator.SetTrigger("Anim");
        }
    }

    public bool Buy(int price)
    {
        if (_wallet >= price)
        {
            _wallet -= price;
            _walletText.text = StrFormat(_wallet);
            _moneySound.Play();
            if (_timer < 0)
            {
                _timer = 61f;
                _ya.Ad();
            }
            return true;
        }
        else
        {
            _errorSound.Play();
            return false;
        }
    }
    public void SetBuyPrice(int lvl)
    {
        _buyLvl = lvl;
        _buyPriceText.text = StrFormat(20 + _buyLvl * 10);
    }

    public void AddCoins(int coins)
    {
        _wallet += coins * coinsX;
        _walletText.text = StrFormat(_wallet);
    }
    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = StrFormat(_score);
    }

    public void ResetData()
    {
        _carLvl = 0;
        _buyLvl = 0;
        _parkingLvl = 0;
        _carLvlPrice = 70;
        _parkingLvlPrice = 70;

        _score = 0;
        _wallet = 0;

        _parkingLvlPriceText.text = StrFormat(_parkingLvlPrice);

        _buyPriceText.text = StrFormat(20 + _buyLvl * 10);

        _carLvlPriceText.text = StrFormat(_carLvlPrice);

        _walletText.text = StrFormat(_wallet);
        _scoreText.text = StrFormat(_score);
    }

    private string StrFormat(int i)
    {
        string str = i.ToString();
        if (i > 1000000)
        {
            str = str.Substring(0, str.Length - 6) + "m" + str[str.Length - 6];
        }
        else if (i > 1000)
        {
            str = str.Substring(0, str.Length - 3) + "k" + str[str.Length - 3];
        }
        return str;
    }
}
