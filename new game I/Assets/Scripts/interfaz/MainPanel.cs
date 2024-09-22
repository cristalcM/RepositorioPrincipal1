using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class MainPanel : MonoBehaviour
{
    public TMP_Text Prueba1;
    public TMP_Text Prueba2;
    public GameObject VisibleP1;
    public GameObject VisibleP2;

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
    public GameObject CustomizacionPanel;
    public GameObject ControlesPanel;

    //------------------------
    [Header("Customización")]
    //---------------------------
    //Para el nombre y carrera
    public TMP_InputField NombrePlayer;
    public TMP_Text TextaNema;
    public TMP_InputField Career;
    public TMP_Text TextCarrera;
    public GameObject BSalir;
    //---------------------------

    private void Awake()
    {
        VolumenFX.onValueChanged.AddListener(ChangeVolumenFX);
        volumenMaster.onValueChanged.AddListener(ChangeVolumenmaster);

        LoadVolumeSettings();
    }


    void Start()
    {
        if (PlayerPrefs.HasKey("NamePLayer"))
        {
            TextaNema.text = PlayerPrefs.GetString("NamePLayer");
            NombrePlayer.text = TextaNema.text;
        }

        if (PlayerPrefs.HasKey("Career"))
        {
            TextCarrera.text = PlayerPrefs.GetString("Career");
            Career.text = TextCarrera.text;
        }
    }

    public void UpdateInfo()
    {

        PlayerPrefs.SetString("NamePLayer", NombrePlayer.text);
        PlayerPrefs.SetString("Career", Career.text);

        //Prueba1.text = PlayerPrefs.GetString("NamePLayer", NombrePlayer.text);
        //Prueba2.text = PlayerPrefs.GetString("Career", Career.text);

        //VisibleP1.SetActive(true);
        //VisibleP2.SetActive(true);        
    }

    private void Update()
    {
        //Si es menor el texto a un caracter no se activara el juego
        if (TextaNema.text.Length < 1 && TextCarrera.text.Length < 1)
        {
            BSalir.SetActive(false);
        }
        if (TextaNema.text.Length > 1 && TextCarrera.text.Length > 1)
        {
            BSalir.SetActive(true);
        }
    }



    //---------------------------------------
    //Metodo para  abrir y cambiar de panel
    //---------------------------------------
    public void OpenPanel(GameObject panel)
    {
        //iniciaizar
        mainPanel.SetActive(false);
        ajustesPanel.SetActive(false);
        CustomizacionPanel.SetActive(false);
        ControlesPanel.SetActive(false);

        panel.SetActive(true);

        PLaySoundButton();      
    }

    //---------------------------------------
    //Método para cambiar de escena al presionar el botón Play
    //---------------------------------------
    public void ChangeScene(string sceneName)
    {
        if (TextaNema.text.Length > 1 && TextCarrera.text.Length > 1)
        {
            SceneManager.LoadScene(sceneName);
        }

        ////activar depúes de la prueba
        PlayerPrefs.SetString("NamePLayer", NombrePlayer.text);
        PlayerPrefs.SetString("Career", Career.text);
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

    //---------------------------------------
    //Método para salir del juego
    //---------------------------------------
    public void SalirJuego()
    {
        //Application.Quit();
    }
}
