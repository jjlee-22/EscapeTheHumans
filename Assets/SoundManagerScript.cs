using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHurtSound, collectSound, transitionSound;
    static AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        collectSound = Resources.Load<AudioClip>("Money");
        transitionSound = Resources.Load<AudioClip>("Stairs");
        playerHurtSound = Resources.Load<AudioClip>("Hero_Hurt");

        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "money":
                audiosource.PlayOneShot(collectSound);
                break;
            case "door":
                audiosource.PlayOneShot(transitionSound);
                break;
            case "hurt":
                audiosource.PlayOneShot(playerHurtSound);
                break;
        }
    }
}
