using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaySoundOnClick : MonoBehaviour
{
    //-----------------------------
    //Componentes
    //----------------------------

    public AudioSource audioSource;





    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }



    //-----------------------------
    //Metodo para reproducir un sonido al tocar un objeto
    //----------------------------

    void OnMouseDown()
    {
        // Reproduce el sonido si el AudioSource no está ya reproduciendo
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }



}
