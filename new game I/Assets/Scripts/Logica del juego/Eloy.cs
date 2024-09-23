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
    public Vector2 coordenadasCorrectas = new(5, 3);  // Coordenadas correctas del edificio E
    public GameObject llaveroObjeto;  // Llavero que recibir� el jugador
    private bool haRecibidoLlavero = false;
    private bool esperandoRespuesta = false;
    
    //Canvas y sus elementos
    public InputField inputX;
    public InputField inputY;
    public Button enviarButton;
    public DialogoNPC Dialogo;
    public GameObject panelPregunta;

   static Text TextNombre;
    //public static Text TextCarrera;


    void Start()
    {
        // Ocultar el Canvas al inicio
        panelPregunta.gameObject.SetActive(false);

        // Asignar la funci�n al bot�n
        enviarButton.onClick.AddListener(EnviarCoordenadas);

        if (PlayerPrefs.HasKey("NamePLayer"))
        {
            string playerName = PlayerPrefs.GetString("NamePLayer");
            // Aqu� puedes usar "playerName" en tus di�logos o donde sea necesario
            Debug.Log("Se busca nombre");
        }

        if (PlayerPrefs.HasKey("Career"))
        {
            string playerCareer = PlayerPrefs.GetString("Career");
            // Aqu� puedes usar "playerCareer" en tus di�logos o donde sea necesario
            Debug.Log("Se busca carrera");
        }

        TextNombre.text = PlayerPrefs.GetString("NamePLayer");
        //TextCarrera.text = PlayerPrefs.GetString("Career");
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
            Debug.Log("Eloy: �Oye, asere! Soy de intercambio y no entiendo el mapa del CUC. �Sabes cu�les son las coordenadas del edificio E?");
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
        
        // Verificar si las coordenadas son v�lidas
        if (float.TryParse(inputX.text, out x) && float.TryParse(inputY.text, out y))
        {
            Vector2 coordenadasIngresadas = new(x, y);
            VerificarCoordenadas(coordenadasIngresadas);
        }
        else
        {
            Debug.Log("Eloy: Por favor, ingresa valores num�ricos v�lidos.");
        }
    }

    void VerificarCoordenadas(Vector2 coordenadasIngresadas)

    {
        Dialogo.MostrarDialogo(EloyDialogoSinAyuda);
        if (coordenadasIngresadas == coordenadasCorrectas)
        {
            Debug.Log("Eloy: �Gracias, asere! �Esas son las coordenadas correctas! Aqu� tienes un llavero.");
            DarLlavero();

            Dialogo.MostrarDialogo(EloyDialogoConAyuda);
        }
        else
        {
            Debug.Log("Incorrectas");
        }

        // Ocultar el Canvas despu�s de verificar
        esperandoRespuesta = false;
        panelPregunta.gameObject.SetActive(false);
    }

    void DarLlavero()
    {
        if (!haRecibidoLlavero)
        {
            //player.GetComponent<Player>().RecibirLlavero();  // M�todo en el script del jugador
            llaveroObjeto.SetActive(true);  // Mostrar el llavero
            haRecibidoLlavero = true;
            Debug.Log("Jugador: Este llavero est� genial, tu trabajo es incre�ble.");
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
            panelPregunta.gameObject.SetActive(false);
        }
    }



    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________

    [SerializeField, TextArea(4, 6)]
    private string[] EloyDialogoSinAyuda =
    {
       "Eloy: �Oye, asere! Soy de intercambio y no entiendo el mapa del CUC. �Sabes cu�les son las coordenadas del edificio E?",
       TextNombre +": ammm las cordenadas son..."

    };


    [SerializeField, TextArea(4, 6)]
    private string[] EloyDialogoConAyuda =
    {

        "Eloy: �Gracias, asere! Te tengo un regalito.",
        "*le entrega un llavero.",
        TextNombre +": Est� playera esta hermosa, tu trabajo es incre�ble.",
         "Eloy: Nuevamente, gracias mi asere, nos vemos.",
        TextNombre +": �Adi�s!",


    };
    [SerializeField, TextArea(4, 6)]
    private string[] EloyDialogoFinal =
   {

        "Eloy: �Asere, �qu� bol�?! Me alegra ver que uses mi regalo.",
        TextNombre +": Me alegra verte. �Y c�mo no usarla si tu trabajo es magn�fico?"
    };


}


