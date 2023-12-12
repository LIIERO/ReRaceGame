using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject mainCar; // Pojazd który po dotarciu do mety aktywuje wydarzenie zakoñczenia wyœcigu

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car")) // Je¿eli obiekt, który osi¹gn¹³ metê jest pojazdem
        {
            // Wydarzenie dotarcia pojazdu do mety aktywuje siê z pojazdem, który j¹ osi¹gn¹³
            EventManager.InvokeCarReachedGoalEvent(collision.gameObject);

            if (ReferenceEquals(collision.gameObject, mainCar)) // Je¿eli pojazd, który ukoñczy³ wyœcig jest g³ównym pojazdem (pojazdem gracza)
            {
                EventManager.InvokeRaceEndEvent(); // Wydarzenie zakoñczenia wyœcigu aktywuje siê
            }

            collision.gameObject.SetActive(false); // Pojazd znika po osi¹gniêciu mety
        }
    }
}
