using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceResultManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject carResultEntryPrefab;
    [SerializeField] private float spaceBetweenEntries;

    //private TimeManager timeManager;
    private int NOLeaderboardEntries = 0;
    
    private void Start()
    {
        //timeManager = GetComponent<TimeManager>();
        leaderboard.SetActive(false);
    }

    public void ShowResultPanel()
    {
        leaderboard.SetActive(true);
    }

    public void AddCarToLeaderboard(GameObject car)
    {
        // Stworzenie nowej instancji rezultatu i przesuniêcie jej pod poprzedni¹
        GameObject newResultEntry = Instantiate(carResultEntryPrefab, leaderboard.transform);
        newResultEntry.GetComponent<RectTransform>().anchoredPosition += NOLeaderboardEntries * spaceBetweenEntries * Vector2.down;

        NOLeaderboardEntries++;

        // Podmiana tekstu
        string resultEntryText = NOLeaderboardEntries.ToString() + " - " + car.name + ": " + string.Format("{0:f2}", TimeManager.RaceTimer);
        newResultEntry.GetComponentInChildren<TextMeshProUGUI>().text = resultEntryText;

        // Podmiana grafiki pojazdu
        newResultEntry.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = car.GetComponentInChildren<SpriteRenderer>().sprite;
    }


    private void OnEnable() // Menad¿er rezultatów wyœcigu subskrybuje do wydarzenia zakoñczenia wyœcigu i ukoñczenia wyœcigu przez pojazd
    {
        EventManager.RaceEnd += ShowResultPanel; // Rezultaty pokazywane s¹ przy zakoñczeniu wyœcigu
        EventManager.CarReachedGoal += AddCarToLeaderboard; // Pojazd który ukoñczy³ wyœcig dodawany jest do rezultatów
    }

    private void OnDisable() // Metoda usuwaj¹ca subskrypcjê - dobra praktyka programistyczna
    {
        EventManager.RaceEnd -= ShowResultPanel;
        EventManager.CarReachedGoal -= AddCarToLeaderboard;
    }
}
