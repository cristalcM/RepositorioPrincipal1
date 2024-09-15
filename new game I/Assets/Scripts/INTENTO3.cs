using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTENTO3 : MonoBehaviour
{
    
        private float velocidad = 4f;
        private Vector3 posicionDobjetivo;
        private bool hasNewClick = false; // Para controlar si ha habido un nuevo clic
        private bool isColliding = false; // Para saber si el personaje est� colisionando

        void Start()
        {
            // Inicializa la posici�n del objetivo como la posici�n actual del personaje
            posicionDobjetivo = this.transform.position;
        }

        void Update()
        {
            // Detectar nuevo clic
            if (Input.GetMouseButtonDown(0))
            {
                // Obtener la posici�n del clic
                posicionDobjetivo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                posicionDobjetivo.z = this.transform.position.z;

                // Registrar que hubo un nuevo clic, permitiendo el movimiento
                hasNewClick = true;
                isColliding = false; // Reiniciar el estado de colisi�n
            }

            MoverHaciaObjetivo();
        }

        private void MoverHaciaObjetivo()
        {
            // Si el personaje est� colisionando, detener el movimiento
            if (isColliding)
            {
                hasNewClick = false; // Al colisionar, esperar un nuevo clic
                return; // Detener el movimiento al colisionar
            }

            // Mover solo si hubo un nuevo clic y no est� colisionando
            if (hasNewClick)
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionDobjetivo, velocidad * Time.deltaTime);

                // Si llega a la posici�n objetivo, dejar de mover
                if (Vector3.Distance(transform.position, posicionDobjetivo) < 0.1f)
                {
                    hasNewClick = false; // Ya lleg� al objetivo, no necesita seguir movi�ndose
                }
            }
        }

        // Detectar cuando colisiona con algo
        private void OnCollisionEnter2D(Collision2D collision)
        {
            isColliding = true; // Marca que est� colisionando
        }
    }


