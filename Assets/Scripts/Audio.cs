using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip jumpSound, coinSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void CoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }
}
