using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    [SerializeField] private Yandex _ya;

    [SerializeField] private AudioSource _sound;
    [SerializeField] private SavingData _save;
    [SerializeField] private PlayerSettings _player;
    [SerializeField] private CarGeneration _generator;
    private List<GameObject[]> _collisions = new List<GameObject[]>();
    private List<int> _lvls = new List<int>();
    private List<Vector3> _positions = new List<Vector3>();

    private int _count = 10;

    // Update is called once per frame
    void Update()
    {
        if (_lvls.Count > 0)
        {
            for (int i = _lvls.Count - 1; i>=0; i--)
            {
                _generator.Generate(_lvls[i] + 1, _positions[i]);               
                _player.AddCoins(25 + _lvls[i] * 10);
                _player.AddScore(5 + _lvls[i] * 2);
                
                if (_count > 0)
                {
                    _count--;
                }
                else
                {
                    _ya.Rate();
                }
            }
            _lvls.Clear();
            _positions.Clear();
        }
        if (_collisions.Count > 0)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (GameObject[] obj in _collisions)
            {
                if (!list.Contains(obj[0]))
                {
                    int lvl = obj[0].GetComponent<CarLvl>().lvl;
                    list.Add(obj[1]);
                    Destroy(obj[0]);
                    Destroy(obj[1]);
                    _lvls.Add(lvl);
                    _positions.Add(obj[0].transform.position);
                    _sound.Play();
                }
            }
            _collisions.Clear();
        }
    }

    public void AddCollision(GameObject obj1, GameObject obj2)
    {
        GameObject[] array = { obj1, obj2 };
        _collisions.Add(array);
    }
}
