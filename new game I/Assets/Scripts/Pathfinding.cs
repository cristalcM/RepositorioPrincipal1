using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{

    public LayerMask capatransitable;
    private Rigidbody2D   rb;
    private Ray miRayo;

    public float DetectionRange = 5f;
    public float minDistance = 2f;
    public float dodgeForce = 2f;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction  = mousepos- transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, DetectionRange);

            if (hit.collider != null)
                {
                rb.position = hit.point;
            }
           
            if (hit.collider != null && Vector2.Distance(transform.position, hit.point) < minDistance)
            {
                // Calcular dirección de esquiva (por ejemplo, hacia la izquierda)
                Vector2 dodgeDirection = -transform.right;

                // Aplicar fuerza para esquivar
                rb.AddForce(dodgeDirection * dodgeForce, ForceMode2D.Impulse);
            }

        }

    }
}
