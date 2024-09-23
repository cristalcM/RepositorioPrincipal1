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

    // Información y variables
    public Inventario inventario;
 
    int monedas;
    int croquetas;
    int piezas;

    //----------------------------------------
    // Buscar nombre y carrera para declaralo al inicio
    //----------------------------------------
    void Start()
    {
       
        if (PlayerPrefs.HasKey("NamePLayer"))
        {
            string playerName = PlayerPrefs.GetString("NamePLayer");
        }

        if (PlayerPrefs.HasKey("Career"))
        {
            string playerCareer = PlayerPrefs.GetString("Career");
        }

        TextNombre.text = PlayerPrefs.GetString("NamePLayer");
        TextCarrera.text = PlayerPrefs.GetString("Career");
    }

    //----------------------------------------
    // Se muestra la información del inventario
    //----------------------------------------
    void Update()
    {
        monedas = inventario.monedas;
        croquetas = inventario.croquetas;
        piezas = inventario.piezas;

        TextComida.text = "Croquetas: " + croquetas;
        TextMonedas.text = "Monedas: " + monedas;
        TextPiezas.text = "Piezas: " + piezas;
    }

    public void ChangeScene(string sceneName)
    {
            SceneManager.LoadScene(sceneName);
    }
}
