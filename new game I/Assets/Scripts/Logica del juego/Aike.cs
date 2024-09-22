using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Aike : MonoBehaviour
{

    public bool necesitaAyuda = true;
    public GameObject sombrilla;
    public GameObject tazaPrefab;
    private bool jugadorEnRango = false;  // Para detectar si el jugador está cerca
    public DialogoNPC Dialogo;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E)) // Si el jugador está en rango y presiona E
        {
            Player player = FindFirstObjectByType<Player>();
            DarSombrilla(player.TieneSombra());  // Llama a DarSomvrilla si el jugador está cerca
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador cerca de Aike.");
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jugador fuera del rango de Aike.");
            jugadorEnRango = false;
        }
    }

    public void DarSombrilla(bool JugadorTieneSombra)
    {

        Debug.Log("Texto de conversaion");
        if (necesitaAyuda)
        {
            Dialogo.MostrarDialogo(AikeDialogoSinSombra);

            if ( JugadorTieneSombra)
            {
                Debug.Log("Gracias por traerme la sombrilla.");
                Dialogo.MostrarDialogo(AikeDialogoConSombra);
                necesitaAyuda = false;
                CrearTaza();  // Crear taza en el mundo();
            }
            else
            {
                Debug.Log("Necesito una sombrilla.");
            }
          
        }
        else
        {
            Debug.Log("Ya no necesito ayuda.");
            Dialogo.MostrarDialogo(AikeDialogoConAyuda);
        }
    }

   
        private void CrearTaza()
        {
            Debug.Log("Aike ha dejado una taza.");
            Instantiate(tazaPrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);  // Aparece la taza
        }



    //__________________________________________
    //SUS DIALOGOS 
    //_________________________________________
    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoSinSombra =
    {
        "Jugador: ¿Necesitas ayuda?",
        "Aike: Sí, por favor, no logro alcanzar mi sombrilla y la necesito para protegerme del sol cuando vaya a mi salón.",
        "Jugador: Claro, yo te ayudo."
    };

    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoConSombra=
    { 
       "Jugador: Aquí tienes",
       "Aike: ¡Muchas gracias! Ahora podré salir sin problemas.",
       "Jugador: Me alegra haber podido ayudarte.",
       "Aike: Ten, es un pequeño obsequio. Note que has estado ayudando al gatito, así que espero que te sea de utilidad.",
       "*Le entrega una taza en agradecimiento.",
       "Jugador: ¡Oh, una taza! Muchas gracias, ahora ya no tendré que hacer dos viajes para poder darle de comer."
    };

    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoConAyuda =
    {
       "Aike: ¡Hey! Es bueno volver a verte; gracias a ti llegué a tiempo a mi clase.",
       "Jugador: No hay de qué, sé que habrías hecho lo mismo por mí.",
       "Aike:  :)"
    };






}
