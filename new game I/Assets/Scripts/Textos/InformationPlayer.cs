using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InformationPlayer : MonoBehaviour
{
    //--------------------------
    [Header("Objetos")]
    //---------------------------
    //Objetos para la información
    public GameObject NombreJugador;
    public GameObject CarreraJugador;

    public GameObject ComidaGato;
    public GameObject Monedas;
    public GameObject Piezas;

    //---------------------------
    [Header("Textos")]
    //---------------------------
    //Texto que muestra
    public TMP_Text TextNombre;
    public TMP_Text TextCarrera;

    public TMP_Text TextComida;
    public TMP_Text TextMonedas;
    public TMP_Text TextPiezas;


    int monedas;
    int croquetas;
    int piezas;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("NamePLayer"))
        {
            string playerName = PlayerPrefs.GetString("NamePLayer");
            // Aquí puedes usar "playerName" en tus diálogos o donde sea necesario
            Debug.Log("Se busca nombre");
        }

        if (PlayerPrefs.HasKey("Career"))
        {
            string playerCareer = PlayerPrefs.GetString("Career");
            // Aquí puedes usar "playerCareer" en tus diálogos o donde sea necesario
            Debug.Log("Se busca carrera");
        }

        TextNombre.text = PlayerPrefs.GetString("NamePLayer");
        TextCarrera.text = PlayerPrefs.GetString("Career");
    }

    // Update is called once per frame
    void Update()
    {
        TextComida.text = "Croquetas obtenidas: " + croquetas;
        TextComida.text = "Monedas obtenidas: " + monedas;
        TextComida.text = "Piezas obtenidas: " + piezas;
    }

    public void ChangeScene(string sceneName)
    {
            SceneManager.LoadScene(sceneName);
    }
}
