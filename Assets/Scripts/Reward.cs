using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private Yandex _ya;
    [SerializeField] private PlayerSettings _playerSettings;

    [SerializeField] private GameObject _rewButton;
    [SerializeField] private GameObject _rewAnim;

    private bool _rewarded = false;

    private float _rewTimer = 30f;
    private float _timer = 300f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else if (!_rewButton.activeSelf)
        {
            _rewButton.SetActive(true);
        }
        if (_rewarded)
        {
            if (_rewTimer > 0) 
            {
                _rewTimer -= Time.deltaTime; 
            }
            else
            {
                _playerSettings.coinsX = 1;
                _rewAnim.SetActive(false);
                _rewarded = false;
            }
        }
        
    }

    public void Watch()
    {
        _ya.RewardAd();
        _rewButton.SetActive(false);
        _timer = 300f;
        //
    }

    public void IsWatched()
    {
            _rewTimer = 30f;
            _playerSettings.coinsX = 2;
            _rewAnim.SetActive(true);
            _rewarded = true;
    }
}
