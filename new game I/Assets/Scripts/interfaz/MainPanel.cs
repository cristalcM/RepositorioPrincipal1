using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{

    //--------------------------
    [Header("Ajustes")]
    //---------------------------
    public Slider VolumenFX;
    public Slider volumenMaster;
    public AudioMixer mixer;

    public AudioSource fxsourse;
    public AudioClip clicksound;

    //------------------------
    [Header("Panels")]
    //---------------------------
    public GameObject mainPanel;
    public GameObject ajustesPanel;

    //---------------------------
    //Para el nombre
    public InputField inputText;
    public Text TextaNema;
    //---------------------------

    private void Awake()
    {
        VolumenFX.onValueChanged.AddListener(ChangeVolumenFX);
        volumenMaster.onValueChanged.AddListener(ChangeVolumenmaster);


        LoadVolumeSettings();
    }


    //---------------------------------------
    //Metodo para  abrir y cambiar de panel
    //---------------------------------------
    public void OpenPanel(GameObject panel)
    {
        //iniciaizar
        mainPanel.SetActive(false);
        ajustesPanel.SetActive(false);

        panel.SetActive(true);

        PLaySoundButton();
        
    }
    //---------------------------------------
    //Método para cambiar de escena al presionar el botón Play
    //---------------------------------------
    public void ChangeScene(string sceneName)
    {   
        //Si es menor el texto a un caracter no se activara el juego
        if (TextaNema.text.Length < 1)
        {
          //Avisa al jugador que debe llevar su NameTag  
        }

        //Si es menor el texto a un caracter no se activara el juego
        if (TextaNema.text.Length > 1)
        {  
            //Guarda y activa la Escena
            SceneManager.LoadScene(sceneName);
            PlayerPrefs.SetString("NamePLayer", inputText.text);
        }        
    }

    //---------------------------------------
    //Metodo para ajustar los volumenes 
    //---------------------------------------
    public void ChangeVolumenmaster(float V)
    {
        mixer.SetFloat("VolMaster", V);
        PlayerPrefs.SetFloat("VolMaster", V);
    }

    public void ChangeVolumenFX(float V)
    {
        mixer.SetFloat("VolFX", V);
        PlayerPrefs.SetFloat("VolFX", V);  // Guardar el valor de volumen FX
    }

    public void PLaySoundButton()
    {
        fxsourse.PlayOneShot(clicksound);
    }
    //---------------------------------------
    //Método para cargar los valores de volumen guardados
    //---------------------------------------
    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("VolMaster"))
        {
            float volMaster = PlayerPrefs.GetFloat("VolMaster");
            volumenMaster.value = volMaster;
            mixer.SetFloat("VolMaster", volMaster);
        }

        if (PlayerPrefs.HasKey("VolFX"))
        {
            float volFX = PlayerPrefs.GetFloat("VolFX");
            VolumenFX.value = volFX;
            mixer.SetFloat("VolFX", volFX);
        }
    }


}
