using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventario : MonoBehaviour
{
    public int monedas = 0;
    public int croquetas = 0;
    public void A�adirMoneda()
    {
        monedas++;
        Debug.Log("Tienes " + monedas + " monedas.");
    }
    public void RecogerCroq()
    {
        croquetas++;
        Debug.Log("Tienes " + monedas + " monedas.");
    }
}


