using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Car")) // Je¿eli pojazd wjedzie w strefê poza torem
        {
            collision.gameObject.GetComponent<CarPhysics>().ResetTransform();
        }*/
    }
}
