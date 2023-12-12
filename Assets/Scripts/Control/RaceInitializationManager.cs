using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInitializationManager : MonoBehaviour
{
    // Skrypt odpowiedzialny za instancjowanie pojazdów AI przed rozpoczêciem wyœcigu

    [SerializeField] private GameObject AICarPrefab; // Prefabrykat pojazdu AI
    [SerializeField] private Sprite[] carSprites; // Grafiki pojazdów
    [SerializeField] private Transform[] carPositions; // Miejsca w których maj¹ siê pojawiæ

    [SerializeField] private GameObject easyTrack; // Prefabrykat ³atwiejszego toru
    [SerializeField] private GameObject difficultTrack; // Prefabrykat trudniejszego toru

    private void Awake()
    {
        // 0 -> ³atwiejszy tor, 1 -> trudniejszy tor
        if (GlobalGameManagerSingleton.SelectedRacingTrack == 0) { Instantiate(easyTrack); }
        else if (GlobalGameManagerSingleton.SelectedRacingTrack == 1) { Instantiate(difficultTrack); }


        if (GlobalGameManagerSingleton.NumberOfAICars == 0) return; // Je¿eli nie ma byæ pojazdów AI to pomijamy dalsz¹ czêœæ

        // U¿ywamy liczby pojazdów AI maj¹cych pojawiæ siê na scenie, ustawionej w g³ównym menu
        for (int i = 0; i < GlobalGameManagerSingleton.NumberOfAICars; i++) 
        {
            GameObject newCar = Instantiate(AICarPrefab, carPositions[i]); // Tworzymy nowy pojazd w nowym miejscu
            newCar.GetComponent<SpriteRenderer>().sprite = carSprites[i]; // Zmieniamy jego grafikê
        }
    }


}
