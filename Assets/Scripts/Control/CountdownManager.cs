using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private GameObject countdownObject; // Obiekt UI wy�wietlaj�cy odliczanie do pocz�tku wy�cigu
    private TextMeshProUGUI countdownTextComponent; // Komponent odpowiedzialny za odliczanie powy�szego obiektu
    private int currentCountdownNumber; // Aktualna cyfra wy�wietlana na ekranie
    private float countdownTimer; // Licznik czasu

    [SerializeField] int raceStartCountdownTime; // Czas odliczany do rozpocz�cia wy�cigu
    [SerializeField] float countdownDisableTime; // Czas po kt�rym sygna� do startu znika
    [SerializeField] string raceStartText; // Tekst wy�wietlany po zako�czeniu odliczania

    private void Start()
    {
        countdownTextComponent = countdownObject.GetComponent<TextMeshProUGUI>(); // Pobranie komponentu TextMeshPro licznika aby zmieni� wy�wietlan� cyfr�

        // Rozpocz�cie odliczania czasu do pocz�tku wy�cigu
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
        if (countdownTimer <= 0.0f) return; // Czas jest odliczany dop�ki nie osi�gnie warto�ci 0


        countdownTimer -= Time.deltaTime; // Odliczanie czasu kt�ry min�� od ostatniej klatki gry

        if (countdownTimer <= currentCountdownNumber) // Kiedy licznik osi�gnie kolejn� warto�� ca�kowit�
        {
            countdownTextComponent.text = currentCountdownNumber.ToString(); // Zmiana wy�wietlanej cyfry na ekranie

            // Je�eli licznik osi�gnie 0, wy�wietlany na liczniku jest tekst rozpocz�cia wy�cigu, a licznik ukrywany po zadanym czasie
            if (currentCountdownNumber == 0)
            {
                countdownTextComponent.text = raceStartText; // Zmiana tekstu odliczania na sygna� do startu
                StartCoroutine(DisableCountdownAfterSeconds(countdownDisableTime)); // Sygna� do startu ga�nie po danym czasie
                EventManager.InvokeRaceStartEvent(); // Wydarzenie rozpocz�cia wy�cigu aktywuje si�
            }

            currentCountdownNumber--; // Dekrementacja aktualnej cyfry
        }
    }
}
