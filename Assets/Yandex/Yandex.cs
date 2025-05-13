using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [SerializeField] private CanvasControl _cc;
    [SerializeField] private Reward _rew;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void Ready();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void WatchAd();

    [DllImport("__Internal")]
    private static extern void WatchReward();

    [DllImport("__Internal")]
    private static extern void GetLang();
    public string lang;

    private void Start()
    {
        Ready();
        LoadExtern();
        Ad();
    }
    public void Save(string data)
    {
        SaveExtern(data);
    }

    public void Rate()
    {
        RateGame();
    }
    public void Ad()
    {
        _cc.AdSoundOff();
        WatchAd();
    }

    public void EndOfAd()
    {
        _cc.AdSoundOn();
    }

    public void RewardAd()
    {
        WatchReward();
        _cc.AdSoundOff();
    }

    public void Reward()
    {
        _rew.IsWatched();
        _cc.AdSoundOn();
    }

    public void NotReward()
    {
        _cc.AdSoundOn();
    }

    public void GetLangNow()
    {
        GetLang();
    }
    public void SetLang(string _lang)
    {
        lang = _lang;
    }
}
