using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInitializationManager : MonoBehaviour
{
    // Skrypt odpowiedzialny za instancjowanie pojazd�w AI przed rozpocz�ciem wy�cigu

    [SerializeField] private GameObject AICarPrefab; // Prefabrykat pojazdu AI
    [SerializeField] private Sprite[] carSprites; // Grafiki pojazd�w
    [SerializeField] private Transform[] carPositions; // Miejsca w kt�rych maj� si� pojawi�

    [SerializeField] private GameObject easyTrack; // Prefabrykat �atwiejszego toru
    [SerializeField] private GameObject difficultTrack; // Prefabrykat trudniejszego toru

    private void Awake()
    {
        // 0 -> �atwiejszy tor, 1 -> trudniejszy tor
        if (GlobalGameManagerSingleton.SelectedRacingTrack == 0) { Instantiate(easyTrack); }
        else if (GlobalGameManagerSingleton.SelectedRacingTrack == 1) { Instantiate(difficultTrack); }


        if (GlobalGameManagerSingleton.NumberOfAICars == 0) return; // Je�eli nie ma by� pojazd�w AI to pomijamy dalsz� cz��

        // U�ywamy liczby pojazd�w AI maj�cych pojawi� si� na scenie, ustawionej w g��wnym menu
        for (int i = 0; i < GlobalGameManagerSingleton.NumberOfAICars; i++) 
        {
            GameObject newCar = Instantiate(AICarPrefab, carPositions[i]); // Tworzymy nowy pojazd w nowym miejscu
            newCar.GetComponent<SpriteRenderer>().sprite = carSprites[i]; // Zmieniamy jego grafik�
        }
    }


}
