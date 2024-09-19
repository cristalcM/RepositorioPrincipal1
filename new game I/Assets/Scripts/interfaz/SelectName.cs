using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public InputField inputText;
    public Text TextaNema;
    public GameObject BotonPlay;

    private void Update()
    {   //Si es menor el texto a un caracter no se activara el juego
       if (TextaNema.text.Length < 1)
       {
            BotonPlay.SetActive(false);
       }

       //Si es menor el texto a un caracter no se activara el juego
       if (TextaNema.text.Length > 1)
       {
            BotonPlay.SetActive(true);
       }
    }

    //Guarda y activa la Escena
    public void Cheked()
    {
        PlayerPrefs.SetString("NamePLayer", inputText.text);
        SceneManager.LoadScene("");

    }
}
