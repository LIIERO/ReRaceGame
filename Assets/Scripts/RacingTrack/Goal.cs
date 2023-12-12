using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject mainCar; // Pojazd kt�ry po dotarciu do mety aktywuje wydarzenie zako�czenia wy�cigu

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car")) // Je�eli obiekt, kt�ry osi�gn�� met� jest pojazdem
        {
            // Wydarzenie dotarcia pojazdu do mety aktywuje si� z pojazdem, kt�ry j� osi�gn��
            EventManager.InvokeCarReachedGoalEvent(collision.gameObject);

            if (ReferenceEquals(collision.gameObject, mainCar)) // Je�eli pojazd, kt�ry uko�czy� wy�cig jest g��wnym pojazdem (pojazdem gracza)
            {
                EventManager.InvokeRaceEndEvent(); // Wydarzenie zako�czenia wy�cigu aktywuje si�
            }

            collision.gameObject.SetActive(false); // Pojazd znika po osi�gni�ciu mety
        }
    }
}
