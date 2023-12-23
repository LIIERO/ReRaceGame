using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInitializationManager : MonoBehaviour
{
    // Skrypt odpowiedzialny za instancjowanie pojazdów AI przed rozpoczêciem wyœcigu

    [SerializeField] private GameObject AICarPrefab; // Prefabrykat pojazdu AI
    [SerializeField] private string AICarBaseName; // Bazowa nazwa pojazdu AI
    [SerializeField] private Sprite[] carSprites; // Grafiki pojazdów
    [SerializeField] private Transform[] carPositions; // Miejsca w których maj¹ siê pojawiæ

    [SerializeField] private GameObject easyTrack; // Prefabrykat ³atwiejszego toru
    [SerializeField] private GameObject difficultTrack; // Prefabrykat trudniejszego toru
    [SerializeField] private GameObject easyTrackRewardPoints; // Prefabrykat punktów nagrody dla AI ³atwiejszego toru
    [SerializeField] private GameObject difficultTrackRewardPoints; // Prefabrykat punktów nagrody dla AI trudniejszego toru

    private void Awake()
    {
        // 0 -> ³atwiejszy tor, 1 -> trudniejszy tor
        if (GlobalGameManagerSingleton.SelectedRacingTrack == 0) { 
            Instantiate(easyTrack);
            Instantiate(easyTrackRewardPoints);
        }
        else if (GlobalGameManagerSingleton.SelectedRacingTrack == 1) { 
            Instantiate(difficultTrack);
            Instantiate(difficultTrackRewardPoints);
        }

        if (GlobalGameManagerSingleton.NumberOfAICars == 0) return; // Je¿eli nie ma byæ pojazdów AI to pomijamy dalsz¹ czêœæ

        // U¿ywamy liczby pojazdów AI maj¹cych pojawiæ siê na scenie, ustawionej w g³ównym menu
        for (int i = 0; i < GlobalGameManagerSingleton.NumberOfAICars; i++) 
        {
            GameObject newCar = Instantiate(AICarPrefab, carPositions[i]); // Tworzymy nowy pojazd w nowym miejscu
            newCar.name = AICarBaseName + i.ToString(); // Zmieniamy jego nazwê
            newCar.GetComponent<SpriteRenderer>().sprite = carSprites[i]; // Zmieniamy jego grafikê
        }
    }
}
