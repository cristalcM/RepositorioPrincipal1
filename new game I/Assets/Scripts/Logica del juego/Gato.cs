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
    private bool jugadorEnRango = false;  // Para detectar si el jugador est� cerca
    private Player Player;
    private bool ComidaEntregada = false;

    private bool escapando = false;       // Para evitar que se escapen varias veces


    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� en rango y presiona E
        {
            Player = FindFirstObjectByType<Player>();
            if (Player != null && Player.croquetas > 0)  // Solo dar comida si el jugador tiene comida
            {
                DarComida(Player.TieneTaza());  // Llama a DarComida si el jugador est� cerca y tiene comida
                Player.RecibirTaza();  // Despu�s de dar comida, el jugador pierde su comida
            }
            else
            {
                Debug.Log("No tienes comida para el gato.");
           
                Dialogo.MostrarDialogo(gatoDialogoSincomida);
            }
        }

    }

    // Detecta cuando el jugador entra en la zona de interacci�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Aseg�rate de que el jugador tenga la etiqueta "Jugador"
        {
            Debug.Log("Jugador cerca del gato.");
            jugadorEnRango = true;
        }
    }

    // Detecta cuando el jugador sale de la zona de interacci�n
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
                Debug.Log("El gato est� siendo alimentado en un solo viaje.");
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
                    Debug.Log("El gato sigue con hambre. Necesitas traer m�s comida.");
                   
                }
            }
        }
        else
        {
            Debug.Log("El gato ya est� satisfecho.");
            Dialogo.MostrarDialogo(gatoDialogoSatisfecho);
        }
    }


    private void LlenarHambre()
    {
        Debug.Log("El gato est� satisfecho.");
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
            Debug.Log("El gato se est� escapando...");
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
        "Gato: �MIAUURR!",
        "Jugador: �Te encuentras bien, amiguito?",
        "Gato: MRAUU",
        "Jugador: Mmm� pareces tener hambre, d�jame buscarte algo de comer.",


    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoContaza =
   {
    // Despu�s de dar la primera comida
        "Jugador: Aqu� tienes.",
        "Gato: Mrauu",
        "Jugador: Me alegra que fuera suficiente.",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoConcomida =
    {
    // Despu�s de dar la primera comida
        "Jugador: Aqu� tienes.",
        "Gato: Mrauu",
        "Jugador: Parece que a�n tienes hambre, supongo que tendr� que traerte un poco m�s.",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoFinal =
   {
     // Despu�s de la segunda comida
        "Jugador: Ahora s�, provecho Bigotes.",
        "Gato: Miau",
        "*El gato le da una moneda en agradecimiento.*",
        "Jugador: Gracias amigo, regresar� a visitarte m�s tarde por si vuelves a tener hambre.",
        "Gato: �Miau!"
    };

    private string[] gatoDialogoSatisfecho =
    {
        // Despu�s de dar la primera comida
        "Gato: Rrrrrrr",
        "Jugador: Es bueno ver que est�s bien. Nos vemos m�s tarde."
,
    };


    //--------------------------
    //Notificaciones
    //___________________

    private string[]  notificacion1 =
   {
      "Bigotes tiene hambre, alim�ntalo antes de que se escape"
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
      "Bigotes fue a buscar comida, volver�... pronto...                Quizas"
    };




}
