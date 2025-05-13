using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLvl : MonoBehaviour
{
    [SerializeField] private int _lvl;
    [SerializeField] private CarCollision _carCollision;
    public int lvl => _lvl;

    private void Start()
    {
        _carCollision = GameObject.Find("Generator").GetComponent<CarCollision>();
    }

    private void Update()
    {
        if (transform.position.y < -1) { 
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "car" && obj.GetComponent<CarLvl>().lvl == lvl) 
        {
            _carCollision.AddCollision(obj, this.gameObject);
        }
    }


}
