using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.UI;

public class Nadia : MonoBehaviour
{
    static string nombre;

    //-----------------------------
    //Atributos publicos.
    //---------------------------
    public bool Necesitaayuda = true;
    public GameObject RampaPrefab;
    public GameObject Gato;
    public GameObject Pescadodoprefab;
    public int PiezasNecesarias = 5;
    public Transform rampaPosicion;  // La posición donde se colocará la rampa

    //______________________________
    //atributos privados
    //-----------------------------
    private bool jugadorenrango = false;
    private bool rampaConstruida = false;
    private bool haRecibidoPescado = false;

    //referencia el sistema de inventario 
    public Inventario inventario;
    public DialogoNPC dialogo;



    //----------------------------------------
    // Buscar nombre y lo declara
    private void Start()
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

        nombre = PlayerPrefs.GetString("NamePLayer");
    }
    //----------------------------------------

    private void Update()
    {
        if (jugadorenrango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está en rango y presiona E
        {
            IniciarInteraccion();
        }
    }

    void IniciarInteraccion()
    {
        if (!rampaConstruida)
        {
            if (inventario.piezas == PiezasNecesarias)
            {
                ContruirRampa();
            }
            else
            {
                dialogo.MostrarDialogo(NadiaDialogoSinAyuda);
                Debug.Log("No puede pasar porque falta una rampa.");
                Debug.Log("Mmm… voy a buscar una solución. ");
            }
        }
        else if (!haRecibidoPescado)
        {
            DarPescado();
        }
        else
        {
            SegundaInteraccion();
        }

    }

    public void ContruirRampa()
    {

        Debug.Log("Jugador: Fue un poco difícil, pero aquí está la rampa.");
        dialogo.MostrarDialogo(NadiaDialogoRampa);
        Instantiate(RampaPrefab, rampaPosicion.position, Quaternion.identity);  // Coloca la rampa en la posición indicada
        rampaConstruida = true;
        inventario.UsarPiezas();  // Remueve las piezas del inventario
        StartCoroutine(TeletransportarNadia());
    }

    void DarPescado()
    {

        if (!haRecibidoPescado)
        {
            Debug.Log("¡Gracias, ahora podré continuar! Estoy estudiando cultura física y deporte, y he notado que Bigotes es bastante activo");
            dialogo.MostrarDialogo(NadiaDialogoConAYUDA);
           

            CrearPezcado();
            haRecibidoPescado = true;
        }
    }

    private void CrearPezcado()
    {
        Debug.Log("Aike ha dejado un pez.");
        Instantiate(Pescadodoprefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);  // Aparece el pescado
    }

    //-----------------
    //Metodo para teletranportar a nadia
    //-----------------
    IEnumerator TeletransportarNadia()
    {
        yield return new WaitForSeconds(10);  // Espera 10 segundos antes de teletransportar

        Debug.Log("Nadia: ¡Gracias, ahora podré continuar!");
        Debug.Log("Jugador: ¡Gracias, Nadia!");
        transform.position = new Vector3(-16, -25, 0); //POsicion a la que se movera nadia
    }
    void SegundaInteraccion()
    {
        dialogo.MostrarDialogo(NadiaDialogofinal);
        Debug.Log("Nadia: ¡Vaya, parece que nunca paras!");
        Debug.Log("Jugador: Hay mucho por hacer.");
        Debug.Log("Nadia: Recuerda descansar e hidratarte.");
    }



    //-----------------
    //Coliciones
    //-----------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorenrango = true;
            Debug.Log("Jugador: Hola, ¿tienes algún problema?");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugadorenrango = false;
        }
    }


    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________
    [ TextArea(4, 6)]
    private string[] NadiaDialogoSinAyuda=
     {
        "Yo: Hola, ¿tienes algún problema?",
        "Nadia: Sí, no puedo pasar, hace falta una rampa.",
        "Yo: Mmm… voy a buscar una solución. Quizás logre encontrar unas piezas para improvisar una."
    };
    [ TextArea(4, 6)]
    private string[] NadiaDialogoRampa =
    {
        "Yo: Fue un poco difícil, pero aquí está la rampa."
    };
    [ TextArea(4, 6)]
    private string[] NadiaDialogoConAYUDA=
    {
        "Nadia: ¡Gracias, ahora podré continuar! Estoy estudiando cultura física y deporte, " +
            "y he notado que Bigotes es bastante activo, especialmente cuando lo alimentas. " +
            "He escuchado que cuando tiene mucha hambre suele desaparecer por un tiempo. No sé mucho de animales, " +
            "pero tal vez este pescado dorado le ayude a quedarse contigo por más tiempo, quizá incluso de forma indefinida.",
        "Yo: ¡Gracias, Nadia! Es justo lo que necesitaba. ¡Eres verdaderamente increíble!"
    };
    [ TextArea(4, 6)]
    private string[] NadiaDialogogracias =
    {
       
    };
    [ TextArea(4, 6)]
    private string[] NadiaDialogofinal =
    {
       "Nadia: ¡Vaya, parece que nunca paras!" ,
       "Yo: Hay mucho por hacer." ,
       "Nadia: Recuerda que es importante descansar e hidratarte bien, sobre todo con este calor infernal. " ,
       "Yo: Gracias, lo tendré en cuenta. Nos vemos por ahí." ,
       "Nadia: Cuídate, y recuerda no sobrepasarte."
    };
}
