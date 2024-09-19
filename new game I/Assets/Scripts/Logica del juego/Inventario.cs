using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventario : MonoBehaviour
{
    public int monedas = 0;

    public void AñadirMoneda()
    {
        monedas++;
        Debug.Log("Tienes " + monedas + " monedas.");
    }
}


