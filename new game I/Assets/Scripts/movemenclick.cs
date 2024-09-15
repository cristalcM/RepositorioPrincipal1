using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class movemenclick : MonoBehaviour
{



    //Atributos
    private float Velocidad = 5f;
    private Vector3 posicionDobjetivo;
    

   
    // Start is called before the first frame update
    void Start()
    {
        //ubica la posicion del objetivo
        posicionDobjetivo = this.transform.position;



       
    }

    // Update is called once per frame
    void Update()
    {

        //Mueve al personaje 
        if (Input.GetMouseButtonDown(0))
        {
            posicionDobjetivo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicionDobjetivo.z = this.transform.position.z;

        }
        this.transform.position = 
         Vector3.MoveTowards(current: this.transform.position, target: posicionDobjetivo, maxDistanceDelta: Velocidad * Time.deltaTime);



        

    }


}

     
    
 



