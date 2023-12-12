using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private GameObject countdownObject; // Obiekt UI wyœwietlaj¹cy odliczanie do pocz¹tku wyœcigu
    private TextMeshProUGUI countdownTextComponent; // Komponent odpowiedzialny za odliczanie powy¿szego obiektu
    private int currentCountdownNumber; // Aktualna cyfra wyœwietlana na ekranie
    private float countdownTimer; // Licznik czasu

    [SerializeField] int raceStartCountdownTime; // Czas odliczany do rozpoczêcia wyœcigu
    [SerializeField] float countdownDisableTime; // Czas po którym sygna³ do startu znika
    [SerializeField] string raceStartText; // Tekst wyœwietlany po zakoñczeniu odliczania

    private void Start()
    {
        countdownTextComponent = countdownObject.GetComponent<TextMeshProUGUI>(); // Pobranie komponentu TextMeshPro licznika aby zmieniæ wyœwietlan¹ cyfrê

        // Rozpoczêcie odliczania czasu do pocz¹tku wyœcigu
        EnableCountdown(raceStartCountdownTime);
    }

    public void EnableCountdown(int countdownTimeSeconds)
    {
        countdownTextComponent.text = countdownTimeSeconds.ToString();
        countdownObject.SetActive(true);
        currentCountdownNumber = countdownTimeSeconds;
        countdownTimer = countdownTimeSeconds;
    }

    public void DisableCountdown()
    {
        countdownObject.SetActive(false);
        currentCountdownNumber = 0;
        countdownTimer = 0.0f;
    }

    public IEnumerator DisableCountdownAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        DisableCountdown();
    }

    private void Update()
    {
        if (countdownTimer <= 0.0f) return; // Czas jest odliczany dopóki nie osi¹gnie wartoœci 0


        countdownTimer -= Time.deltaTime; // Odliczanie czasu który min¹³ od ostatniej klatki gry

        if (countdownTimer <= currentCountdownNumber) // Kiedy licznik osi¹gnie kolejn¹ wartoœæ ca³kowit¹
        {
            countdownTextComponent.text = currentCountdownNumber.ToString(); // Zmiana wyœwietlanej cyfry na ekranie

            // Je¿eli licznik osi¹gnie 0, wyœwietlany na liczniku jest tekst rozpoczêcia wyœcigu, a licznik ukrywany po zadanym czasie
            if (currentCountdownNumber == 0)
            {
                countdownTextComponent.text = raceStartText; // Zmiana tekstu odliczania na sygna³ do startu
                StartCoroutine(DisableCountdownAfterSeconds(countdownDisableTime)); // Sygna³ do startu gaœnie po danym czasie
                EventManager.InvokeRaceStartEvent(); // Wydarzenie rozpoczêcia wyœcigu aktywuje siê
            }

            currentCountdownNumber--; // Dekrementacja aktualnej cyfry
        }
    }
}
