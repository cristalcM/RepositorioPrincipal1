using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTENTO3 : MonoBehaviour
{
    
        private float velocidad = 4f;
        private Vector3 posicionDobjetivo;
        private bool hasNewClick = false; // Para controlar si ha habido un nuevo clic
        private bool isColliding = false; // Para saber si el personaje está colisionando

        void Start()
        {
            // Inicializa la posición del objetivo como la posición actual del personaje
            posicionDobjetivo = this.transform.position;
        }

        void Update()
        {
            // Detectar nuevo clic
            if (Input.GetMouseButtonDown(0))
            {
                // Obtener la posición del clic
                posicionDobjetivo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                posicionDobjetivo.z = this.transform.position.z;

                // Registrar que hubo un nuevo clic, permitiendo el movimiento
                hasNewClick = true;
                isColliding = false; // Reiniciar el estado de colisión
            }

            MoverHaciaObjetivo();
        }

        private void MoverHaciaObjetivo()
        {
            // Si el personaje está colisionando, detener el movimiento
            if (isColliding)
            {
                hasNewClick = false; // Al colisionar, esperar un nuevo clic
                return; // Detener el movimiento al colisionar
            }

            // Mover solo si hubo un nuevo clic y no está colisionando
            if (hasNewClick)
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionDobjetivo, velocidad * Time.deltaTime);

                // Si llega a la posición objetivo, dejar de mover
                if (Vector3.Distance(transform.position, posicionDobjetivo) < 0.1f)
                {
                    hasNewClick = false; // Ya llegó al objetivo, no necesita seguir moviéndose
                }
            }
        }

        // Detectar cuando colisiona con algo
        private void OnCollisionEnter2D(Collision2D collision)
        {
            isColliding = true; // Marca que está colisionando
        }
    }


