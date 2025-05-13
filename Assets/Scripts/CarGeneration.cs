using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CarGeneration : MonoBehaviour
{
    [SerializeField] private SavingData _save;

    [SerializeField] private GameObject _parent;
    [SerializeField] private PlayerSettings _player;

    [SerializeField] private Vector3 _firstCorner;
    [SerializeField] private Vector3 _secondCorner;

    [SerializeField] private List<GameObject> _prefabs;

    [SerializeField] private int _parkingSize = 10;

    [SerializeField] private float _timer;
    [SerializeField] private float time;

    [SerializeField] private Text _parkingText;



    private int _maxLvl = 8;
    private void Start() {
        time = _timer;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        _parkingText.text = ((_parkingSize + _player.parkingLvl * 5) - _parent.transform.childCount).ToString();
        if (time < 0)
        {
            time = _timer;
            RandomGenerate();
        }
    }

    public bool RandomGenerate()
    {
        return (Generate(UnityEngine.Random.Range(0, _player.carLvl), new Vector3(UnityEngine.Random.Range(_firstCorner.x, _secondCorner.x), 10, UnityEngine.Random.Range(_firstCorner.z, _secondCorner.z))));
    }
    public bool Generate(int lvl, Vector3 pos) {
        if (_parent.transform.childCount < _parkingSize + _player.parkingLvl * 5)
        {
            if (lvl <= _maxLvl)
            {
                var Obj = GameObject.Instantiate(_prefabs[lvl], pos, _parent.transform.rotation);
                Obj.transform.parent = _parent.transform;
                Obj.transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0);
            }
            else
            {
                _player.AddScore(200);
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
