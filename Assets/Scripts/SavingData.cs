using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public int wallet;
    public int score;
    public int parking;
    public int buy;
    public int car;
    public List<int> carsLvls = new List<int>();
    public List<float> carsPosX = new List<float>();
    public List<float> carsPosY = new List<float>();
    public List<float> carsPosZ = new List<float>();
}

public class SavingData : MonoBehaviour
{
    [SerializeField] private Yandex _ya;

    [SerializeField] private CarGeneration _generator;
    [SerializeField] private PlayerSettings _player;
    [SerializeField] private Transform _parent;
    private float _timer = 5f;

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        else
        {
            Save();
            _timer = 5f;
            _timer = 5f;
        }
    }
    public void Save()
    {
        Data data = new Data();
        data.wallet = _player.wallet;
        data.score = _player.score;
        data.parking = _player.parkingLvl;
        data.buy = _player.buyLvl;
        data.car = _player.carLvl;

        for (int i = _parent.childCount; i > 0; i--)
        {
            Transform child = _parent.GetChild(i-1);
            int lvl = child.gameObject.GetComponent<CarLvl>().lvl;
            data.carsLvls.Add(lvl);
            data.carsPosX.Add(child.position.x);
            data.carsPosY.Add(child.position.y);
            data.carsPosZ.Add(child.position.z);
        }

        string d = JsonUtility.ToJson(data);
        _ya.Save(d);
    }

    public void Load(string newdata)
    {
        Data data = JsonUtility.FromJson<Data>(newdata);
        
        for (int i = data.parking; i > 0; i--)
        {
            _player.ParkingLvlUp(true);
        }
        for (int i = data.car; i > 0; i--)
        {
            _player.CarLvlUp(true);
        }
        _player.SetBuyPrice(data.buy);
        _player.AddCoins(data.wallet);
        _player.AddScore(data.score);

        for (int i = data.carsLvls.Count; i>0; i--)
        {
            _generator.Generate(data.carsLvls[i-1], new Vector3(data.carsPosX[i - 1], data.carsPosY[i - 1], data.carsPosZ[i - 1]));
        }
    }


}

