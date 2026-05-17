using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaminiSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoSaltar;     
    public AudioClip sonidoHurt;


    public void playSaltar()
    {
        audioSource.PlayOneShot(sonidoSaltar);

    }

    public void playHurt()
    {
        audioSource.PlayOneShot(sonidoHurt);

    }


}
