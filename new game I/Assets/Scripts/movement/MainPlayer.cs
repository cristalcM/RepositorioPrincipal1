using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class MainPlayer : MonoBehaviour
{

    //------------------------
    // atributos para el player
    // -------------------------
    public  float velocidad = 4f;
    private Vector3 posicionDobjetivo;
    private bool hasNewClick = false; // Nuevo clic
    private GameObject objetoAgarrado = null; // Referencia al objeto agarrado
    [SerializeField] float radioDeDeteccion = 1f; // Radio de detección para el CircleCast
    [SerializeField] LayerMask layerDeColision;
    public Transform mano; // Donde se colocará el objeto agarrado
    private GameObject objetoObjetivo = null; // Referencia al objeto que el personaje va a agarrar

    //-----------------------------
    //Animacion.
    //-------------------------
    private Animator animator;

    void Start()
    {
        posicionDobjetivo = this.transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //Ver la posicion del raton
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = this.transform.position.z;



            //selecciona al objeto que quiere agarrar
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Agarrar"))
            {
                GameObject objetoClickeado = hit.collider.gameObject;

                if (objetoAgarrado != null && objetoClickeado == objetoAgarrado)
                {
                    Soltar(); // Soltar el objeto
                }
                else if (objetoAgarrado == null)
                {
                    objetoObjetivo = objetoClickeado;
                    posicionDobjetivo = objetoObjetivo.transform.position;
                    hasNewClick = true;
                }
            }
            else
            {
                posicionDobjetivo = mousePos;
                hasNewClick = true;
            }
        }


        MoverHaciaObjetivo();
       
    }

    //mover el personaje hacia donde quiere ir
    private void MoverHaciaObjetivo()
    {
        bool isColliding = Physics2D.OverlapCircle(transform.position, radioDeDeteccion, layerDeColision);

        // Calcular la dirección hacia el objetivo
        Vector3 direccion = (posicionDobjetivo - transform.position).normalized;
        if (hasNewClick)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionDobjetivo, velocidad * Time.deltaTime);

            
            // Actualizar los parámetros de animación
            animator.SetFloat("MovimientoX", direccion.x);
            animator.SetFloat("MovimientoY", direccion.y);
            //animacion
            if (direccion.x != 0 || direccion.y != 0)
            {
                animator.SetFloat("UltimoX", direccion.x);
                animator.SetFloat("UltimoY", direccion.y);
            }

          

            if (objetoObjetivo != null && Vector3.Distance(transform.position, objetoObjetivo.transform.position) <= 3f)
            {
                Agarrar(objetoObjetivo);
                hasNewClick = false;
            }

            if (Vector3.Distance(transform.position, posicionDobjetivo) <= 0.1f)
            {
                hasNewClick = false;
            }
        }
        else
        {

            // Si no hay clics, y no se está moviendo, cambiar a la animación de idle
            animator.SetFloat("MovimientoX", 0);
            animator.SetFloat("MovimientoY", 0);
        }

        //Si coliciona con
        if (isColliding)
        {
            hasNewClick = false;
            return;
        }

        // Animación de idle si no se está moviendo
        if (!hasNewClick && (direccion.x == 0 && direccion.y == 0))
        {
            // Usar la última dirección para mostrar la animación idle
            animator.SetFloat("MovimientoX", 0);
            animator.SetFloat("MovimientoY", 0);
            }
      }

    //Metodo para agarrar el objeto
    void Agarrar(GameObject objeto)
    {
        objetoAgarrado = objeto;
        Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Desactivar el Rigidbody2D del personaje para evitar efectos físicos no deseados
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }

        objetoAgarrado.transform.position = mano.position;
        objetoAgarrado.transform.SetParent(mano);
        objetoObjetivo = null;
    }


    //Metodo para soltar el objeto
    void Soltar()
    {
        if (objetoAgarrado != null)
        {
            Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // Restaurar el Rigidbody2D del personaje
            if (GetComponent<Rigidbody2D>() != null)
            {
                GetComponent<Rigidbody2D>().simulated = true;
            }

            objetoAgarrado.transform.SetParent(null);
            objetoAgarrado = null;
        }
    }


    //------------------------------
    //metodo para darle potenciacion ap personaje
    //-----------------------------------

    public void PowerOp()
    {
        velocidad = 6;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDeteccion);
    }
}










