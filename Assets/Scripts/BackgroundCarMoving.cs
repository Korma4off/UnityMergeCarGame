using static System.Math;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCarMoving : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private Vector3 _dir;

    private void Update()
    {
        transform.position = transform.position + _speed * _dir;
        if (Abs(transform.position.z) > 130 || Abs(transform.position.x) > 130)
        {
            transform.position -= _dir * 250;
        }
    }
}
