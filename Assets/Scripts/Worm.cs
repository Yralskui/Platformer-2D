using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// программируй

public class Worm : MonoBehaviour
{
    public float speed = 3f;
    public bool moveLeft = true;
    public Transform groundDetector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 1f);

        if (groundInfo.collider == false)
        {
            if (moveLeft==true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                moveLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveLeft = true;
            }
        }

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
