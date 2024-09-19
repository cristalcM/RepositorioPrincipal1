using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gato : MonoBehaviour
{
    public bool tieneHambre = true;
    public GameObject monedaPrefab;
    public float tiempoParaHambre = 60f;
    private int comidaRecibida = 0;
    private bool jugadorEnRango = false;  // Para detectar si el jugador est� cerca
    private Player Player;
    public Dialogue gatoDialogue;
    private bool ComidaEntregada = false;


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
    private string[] gatoDialogoConcomida =
    {
    // Despu�s de dar la primera comida
        "Jugador: Aqu� tienes.",
        "Gato: Mrauu",
        "Jugador: Parece que a�n tienes hambre, supongo que tendr� que traerte un poco m�s.",
    };
    [SerializeField, TextArea(4, 6)]
    private string[] gatoDialogoFinal=
   {
     // Despu�s de la segunda comida
        "Jugador: Ahora s�, provecho Bigotes.",
        "Gato: Miau",
        "*El gato le da una moneda en agradecimiento.*",
        "Jugador: Gracias amigo, regresar� a visitarte m�s tarde por si vuelves a tener hambre.",
        "Gato: �Miau!"
    };

    private void Update()
    {
        
            if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))  // Si el jugador est� en rango y presiona E
            {
                Player = FindFirstObjectByType<Player>();
                if (Player != null && Player.TieneComida())  // Solo dar comida si el jugador tiene comida
                {
                    DarComida(Player.TieneTaza());  // Llama a DarComida si el jugador est� cerca y tiene comida
                    Player.RecibirTaza();  // Despu�s de dar comida, el jugador pierde su comida
                }
                else
                {
                    Debug.Log("No tienes comida para el gato.");
                IniciarGatoDialogo(false);
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
        if (tieneHambre)
        {
            ComidaEntregada = true;
            if (jugadorTieneTaza)
            {
                Debug.Log("El gato est� siendo alimentado en un solo viaje.");
                LlenarHambre();
                IniciarGatoDialogo(true);  // Continuar con el di�logo
            }
            else
            {
                comidaRecibida++;
                Debug.Log("El gato ha recibido algo de comida.");

                if (comidaRecibida >= 3)
                {
                    LlenarHambre();
                }
                else
                {
                    Debug.Log("El gato sigue con hambre. Necesitas traer m�s comida.");
                    IniciarGatoDialogo(true);  // Continuar con el di�logo
                }
            }
        }
        else
        {
            Debug.Log("El gato ya est� satisfecho.");
            IniciarGatoDialogo(true );  // Continuar con el di�logo
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
        Instantiate(monedaPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
    }

    private IEnumerator TemporizadorHambre()
    {
        yield return new WaitForSeconds(tiempoParaHambre);
        tieneHambre = true;
        Debug.Log("El gato tiene hambre nuevamente.");
    }

    private int currentDialogueIndex = 0;


    //______________________________
    //INICIAR DIALOGO CON EL GATO
    //________________________________
    private void IniciarGatoDialogo(bool TieneComida)
    {
        string[] dialogo = TieneComida ? gatoDialogoSincomida : gatoDialogoConcomida;
        if (currentDialogueIndex < dialogo.Length)
        {
            gatoDialogue.MostrarTexto(dialogo[currentDialogueIndex]);
            currentDialogueIndex++;  // Avanzar al siguiente di�logo en cada interacci�n
        }
        else if (ComidaEntregada && currentDialogueIndex < gatoDialogoFinal.Length)
        {
            //si ya entrego la comida muestra el dialogo final
            gatoDialogue.MostrarTexto(gatoDialogoFinal[currentDialogueIndex]);
            currentDialogueIndex++;
        }
        else
        {
            gatoDialogue.CerrarDialogo();
            Debug.Log("Di�logo finalizado.");
            currentDialogueIndex = 0;  // Reiniciar el di�logo si es necesario
        }
    }
}
