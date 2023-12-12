using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            float angle = Vector3.Angle(collision.contacts[0].normal, transform.up);

            if (Mathf.Approximately(angle, 0f)) // Pojazd uderza od przodu
            {
                //Nagroda
                //Debug.Log("Nagroda");
            }
            if (Mathf.Approximately(angle, 180f)) // Pojazd uderza od ty³u
            {
                // Kara - pojazd zostaje ustawiony na starcie 
                collision.gameObject.GetComponent<CarPhysics>().ResetTransform();
            }

        }
    }
}
