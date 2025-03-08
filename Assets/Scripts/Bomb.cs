using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// будущего:
public class Bomb : MonoBehaviour
{
    public float bombSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * bombSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    //}
}
