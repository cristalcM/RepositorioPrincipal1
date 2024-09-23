using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Aike : MonoBehaviour
{
    static string nombre;

    public bool necesitaAyuda = true;
    public GameObject sombrilla;
    public GameObject tazaPrefab;
    private bool jugadorEnRango = false;  // Para detectar si el jugador está cerca
    public DialogoNPC Dialogo;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E)) // Si el jugador está en rango y presiona E
        {
             Dialogo.MostrarDialogo(AikeDialogoSinSombra);
            Player player = FindFirstObjectByType<Player>();
            DarSombrilla(player.TieneSombra());  // Llama a DarSomvrilla si el jugador está cerca
            
        }
    }

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
        nombre + ": ¿Necesitas ayuda?",
        "Aike: Sí, por favor, no logro alcanzar mi sombrilla y la necesito para protegerme del sol cuando vaya a mi salón.",
        nombre + ": Claro, yo te ayudo."
    };

    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoConSombra=
    {
       nombre + ": Aquí tienes",
       "Aike: ¡Muchas gracias! Ahora podré salir sin problemas.",
       nombre + ": Me alegra haber podido ayudarte.",
       "Aike: Ten, es un pequeño obsequio. Note que has estado ayudando al gatito, así que espero que te sea de utilidad.",
       "*Le entrega una taza en agradecimiento.",
       nombre + ": ¡Oh, una taza! Muchas gracias, ahora ya no tendré que hacer dos viajes para poder darle de comer."
    };

    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoConAyuda =
    {
       "Aike: ¡Hey! Es bueno volver a verte; gracias a ti llegué a tiempo a mi clase.",
       nombre + ": No hay de qué, sé que habrías hecho lo mismo por mí.",
       "Aike:  :)"
    };






}
