using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;



public class Eloy : MonoBehaviour
{
    public GameObject player;
    public Vector2 coordenadasCorrectas = new Vector2(5, 3);  // Coordenadas correctas del edificio E
    public GameObject llaveroObjeto;  // Llavero que recibirá el jugador
    private bool haRecibidoLlavero = false;
    private bool esperandoRespuesta = false;
    
    //Canvas y sus elementos
    public InputField inputX;
    public InputField inputY;
    public Button enviarButton;
    public DialogoNPC Dialogo;
    public GameObject panelPregunta;
    

    void Start()
    {
        // Ocultar el Canvas al inicio
        panelPregunta.gameObject.SetActive(false);

        // Asignar la función al botón
        enviarButton.onClick.AddListener(EnviarCoordenadas);
    }

    void Update()
    {
        if (esperandoRespuesta && Input.GetKeyDown(KeyCode.E))
        {
            IniciarInteraccion();
        }
    }

    void IniciarInteraccion()
    {
        if (!haRecibidoLlavero)
        {
            Debug.Log("Eloy: ¡Oye, asere! Soy de intercambio y no entiendo el mapa del CUC. ¿Sabes cuáles son las coordenadas del edificio E?");
            Dialogo.MostrarDialogo(EloyDialogoSinAyuda);
            esperandoRespuesta = true;

            // Mostrar el Canvas para ingresar las coordenadas
            panelPregunta.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Eloy: Gracias nuevamente por ayudarme, asere.");
            Dialogo.MostrarDialogo(EloyDialogoFinal);
        }
    }

    public void EnviarCoordenadas()
    {
        // Obtener las coordenadas ingresadas por el jugador
        float x, y;
        Dialogo.MostrarDialogo(EloydialogoEsperando);
        // Verificar si las coordenadas son válidas
        if (float.TryParse(inputX.text, out x) && float.TryParse(inputY.text, out y))
        {
            Vector2 coordenadasIngresadas = new Vector2(x, y);
            VerificarCoordenadas(coordenadasIngresadas);
        }
        else
        {
            Debug.Log("Eloy: Por favor, ingresa valores numéricos válidos.");
        }
    }

    void VerificarCoordenadas(Vector2 coordenadasIngresadas)

    {
        Dialogo.MostrarDialogo(EloyDialogoSinAyuda);
        if (coordenadasIngresadas == coordenadasCorrectas)
        {
            Debug.Log("Eloy: ¡Gracias, asere! ¡Esas son las coordenadas correctas! Aquí tienes un llavero.");
            DarLlavero();

            Dialogo.MostrarDialogo(EloyDialogoConAyuda);
        }
        else
        {
            Debug.Log("Incorrectas");
        }

        // Ocultar el Canvas después de verificar
        esperandoRespuesta = false;
        panelPregunta.gameObject.SetActive(false);
    }

    void DarLlavero()
    {
        if (!haRecibidoLlavero)
        {
            //player.GetComponent<Player>().RecibirLlavero();  // Método en el script del jugador
            llaveroObjeto.SetActive(true);  // Mostrar el llavero
            haRecibidoLlavero = true;
            Debug.Log("Jugador: Este llavero está genial, tu trabajo es increíble.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            esperandoRespuesta = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            esperandoRespuesta = false;
        }
    }


    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________

    [SerializeField, TextArea(4, 6)]
    private string[] EloyDialogoSinAyuda =
    {
       "Eloy: ¡Oye, asere! Soy de intercambio y no entiendo el mapa del CUC. ¿Sabes cuáles son las coordenadas del edificio E?"


    };
    [SerializeField, TextArea(4, 6)]
    private string[] EloydialogoEsperando =
   {
    
        "Jugador: ammm las cordenadas son...",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] EloyDialogoConAyuda =
    {

        "Eloy: ¡Gracias, asere! Te tengo un regalito.",
        "*le entrega un llavero.",
        "Jugador: Está playera esta hermosa, tu trabajo es increíble.",
         "Eloy: Nuevamente, gracias mi asere, nos vemos.",
        "Jugador: ¡Adiós!",


    };
    [SerializeField, TextArea(4, 6)]
    private string[] EloyDialogoFinal =
   {

        "Eloy: ¡Asere, ¿qué bolá?! Me alegra ver que uses mi regalo.",
        "Jugador: Me alegra verte. ¿Y cómo no usarla si tu trabajo es magnífico?"
    };

   
    
}


