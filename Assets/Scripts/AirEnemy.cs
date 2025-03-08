using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    public float waitTime = 2f;
    bool canGo = true;
    int index = 1;
    // Стрельба
    public Transform shootPosition;
    public GameObject bomb;
    public float timeShoot = 4f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
        shootPosition.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {   if (canGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);
        }

    if (transform.position == points[index].position)
        {
            if (index < points.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            canGo = false;
            StartCoroutine(Waiting());
        }

        
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        canGo = true;

    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(timeShoot);
        Instantiate(bomb, shootPosition.transform.position, transform.rotation);
        StartCoroutine(Shooting());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine(Death());
        }
    }

    public IEnumerator Death()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
