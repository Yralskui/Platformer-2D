using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    float speed = 10f;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionCamera = target.position;
        positionCamera.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, positionCamera, speed * Time.deltaTime);
    }
}
