using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
public class Gato : MonoBehaviour
{
    //-----------------------------
    //Atributos publicos.
    //---------------------------
    public bool tieneHambre = true;
    public GameObject monedaPrefab;
    public int tiempoParaHambre = 120;


    //Otras clases.
    public DialogoNPC Dialogo;
    public Notification notification;

    public int tiempoParaEscapar = 240; 
   
    //-----------------------------
    //Atributos privados.
    //---------------------------
    private int comidaRecibida = 0;
    private bool jugadorEnRango = false;  // Para detectar si el jugador está cerca
    private Player Player;
    private bool ComidaEntregada = false;

    private bool escapando = false;       // Para evitar que se escapen varias veces


    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador está en rango y presiona E
        {
            Player = FindFirstObjectByType<Player>();
            if (Player != null && Player.croquetas > 0)  // Solo dar comida si el jugador tiene comida
            {
                DarComida(Player.TieneTaza());  // Llama a DarComida si el jugador está cerca y tiene comida
                Player.RecibirTaza();  // Después de dar comida, el jugador pierde su comida
            }
            else
            {
                Debug.Log("No tienes comida para el gato.");
           
                Dialogo.MostrarDialogo(gatoDialogoSincomida);
            }
        }

    }

    // Detecta cuando el jugador entra en la zona de interacción
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Asegúrate de que el jugador tenga la etiqueta "Jugador"
        {
            Debug.Log("Jugador cerca del gato.");
            jugadorEnRango = true;
        }
    }

    // Detecta cuando el jugador sale de la zona de interacción
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador fuera del rango del gato.");
            jugadorEnRango = false;
        }
    }


    //___________________________
    //Metodo para darle comida al gato.
    //_________________________________
    public void DarComida(bool jugadorTieneTaza)
    {
        if (tieneHambre == true)
        {
            ComidaEntregada = true;
            if (jugadorTieneTaza)
            {
                Debug.Log("El gato está siendo alimentado en un solo viaje.");
                LlenarHambre();
                Dialogo.MostrarDialogo(gatoDialogoContaza);
            }
            else
            {
                comidaRecibida++;
                Debug.Log("El gato ha recibido algo de comida.");
                Dialogo.MostrarDialogo(gatoDialogoConcomida);
                if (comidaRecibida == 3)
                {
                    LlenarHambre();
                    Dialogo.MostrarDialogo(gatoDialogoFinal);
                }
                else
                {
                    Debug.Log("El gato sigue con hambre. Necesitas traer más comida.");
                   
                }
            }
        }
        else
        {
            Debug.Log("El gato ya está satisfecho.");
            Dialogo.MostrarDialogo(gatoDialogoSatisfecho);
        }
    }


    private void LlenarHambre()
    {
        Debug.Log("El gato está satisfecho.");
        tieneHambre = false;
        comidaRecibida = 0;
        DarRecompensa();
        StartCoroutine(TemporizadorHambre());


    }

    private void DarRecompensa()
    {
        Debug.Log("El gato te ha dado una moneda.");
        notification.MostrarNotification(notificacion2);
        Instantiate(monedaPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);

    }

    private IEnumerator TemporizadorHambre()
    {
        yield return new WaitForSeconds(tiempoParaHambre);
        tieneHambre = true;
        Debug.Log("El gato tiene hambre nuevamente.");
        notification.MostrarNotification(notificacion1);
        StartCoroutine(TemporizadorEscape());

    }

    private IEnumerator TemporizadorEscape()
    {
        yield return new WaitForSeconds(tiempoParaEscapar);

       
            escapando = true;
            Debug.Log("El gato se está escapando...");
        notification.MostrarNotification(notificacion3);
           
            yield return new WaitForSeconds(2f);  // Espera antes de desaparecer
            Debug.Log("El gato ha desaparecido.");
        notification.MostrarNotification(notificacion4);
        gameObject.SetActive(false);  // El gato desaparece
        
    }



    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoSincomida =
     {
        "Gato: ¡MIAUURR!",
        "Jugador: ¿Te encuentras bien, amiguito?",
        "Gato: MRAUU",
        "Jugador: Mmm… pareces tener hambre, déjame buscarte algo de comer.",


    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoContaza =
   {
    // Después de dar la primera comida
        "Jugador: Aquí tienes.",
        "Gato: Mrauu",
        "Jugador: Me alegra que fuera suficiente.",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoConcomida =
    {
    // Después de dar la primera comida
        "Jugador: Aquí tienes.",
        "Gato: Mrauu",
        "Jugador: Parece que aún tienes hambre, supongo que tendré que traerte un poco más.",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoFinal =
   {
     // Después de la segunda comida
        "Jugador: Ahora sí, provecho Bigotes.",
        "Gato: Miau",
        "*El gato le da una moneda en agradecimiento.*",
        "Jugador: Gracias amigo, regresaré a visitarte más tarde por si vuelves a tener hambre.",
        "Gato: ¡Miau!"
    };

    private string[] gatoDialogoSatisfecho =
    {
        // Después de dar la primera comida
        "Gato: Rrrrrrr",
        "Jugador: Es bueno ver que estés bien. Nos vemos más tarde."
,
    };


    //--------------------------
    //Notificaciones
    //___________________

    private string[]  notificacion1 =
   {
      "Bigotes tiene hambre, aliméntalo antes de que se escape"
    };
    private string[] notificacion2 =
   {
      "Bigotes te ah dado una moneda"
    };

    private string[] notificacion3 =
  {
      "Bigotes se esta escapando"
    };


    private string[] notificacion4 =
  {
      "Bigotes fue a buscar comida, volverá... pronto...                Quizas"
    };




}
