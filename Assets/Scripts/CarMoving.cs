using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoving : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _car;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _layerMask))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.tag == "car")
            {
                _car = hit.transform;
                _car.position = new Vector3(_car.position.x, 10, _car.position.z);
                _car.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (Input.GetMouseButton(0) && _car != null)
            {
                _car.position = hit.point + Vector3.up * 10;
            }
        }

        if (Input.GetMouseButtonUp(0) && _car != null)
        {
            _car.GetComponent<Rigidbody>().isKinematic = false;
            _car = null;
        }
    }
}
