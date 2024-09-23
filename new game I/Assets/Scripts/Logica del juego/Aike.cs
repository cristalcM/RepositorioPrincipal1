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
    private bool jugadorEnRango = false;  // Para detectar si el jugador est� cerca
    public DialogoNPC Dialogo;

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E)) // Si el jugador est� en rango y presiona E
        {
             Dialogo.MostrarDialogo(AikeDialogoSinSombra);
            Player player = FindFirstObjectByType<Player>();
            DarSombrilla(player.TieneSombra());  // Llama a DarSomvrilla si el jugador est� cerca
            
        }
    }

    //----------------------------------------
    // Buscar nombre y lo declara
    private void Start()
    {
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
        nombre + ": �Necesitas ayuda?",
        "Aike: S�, por favor, no logro alcanzar mi sombrilla y la necesito para protegerme del sol cuando vaya a mi sal�n.",
        nombre + ": Claro, yo te ayudo."
    };

    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoConSombra=
    {
       nombre + ": Aqu� tienes",
       "Aike: �Muchas gracias! Ahora podr� salir sin problemas.",
       nombre + ": Me alegra haber podido ayudarte.",
       "Aike: Ten, es un peque�o obsequio. Note que has estado ayudando al gatito, as� que espero que te sea de utilidad.",
       "*Le entrega una taza en agradecimiento.",
       nombre + ": �Oh, una taza! Muchas gracias, ahora ya no tendr� que hacer dos viajes para poder darle de comer."
    };

    [SerializeField, TextArea(4, 6)]
    private string[] AikeDialogoConAyuda =
    {
       "Aike: �Hey! Es bueno volver a verte; gracias a ti llegu� a tiempo a mi clase.",
       nombre + ": No hay de qu�, s� que habr�as hecho lo mismo por m�.",
       "Aike:  :)"
    };






}
