using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventario : MonoBehaviour
{
    public int monedas = 0;
    public int croquetas = 0;
    public int piezas = 0;

    //Monedas
    public void AñadirMoneda()
    {
        monedas++;
        Debug.Log("Tienes " + monedas + " monedas.");
    }


    //croquetas
    public void RecogerCroq()
    {
        croquetas++;
        Debug.Log("Tienes " + croquetas + " croquetas.");
    }
   public void RestrarCroq()
    {
        croquetas--;
        Debug.Log("Tienes " + croquetas + " croquetas.");
    }


    //piezas

    public void RecojerPieza()
    {
        piezas++;
        Debug.Log("Tienes " + piezas + " croquetas.");
    }
    public void UsarPiezas()
    {
        piezas -= piezas; 
    }
}


