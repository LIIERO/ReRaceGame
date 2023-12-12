using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen; // Ekran pojawiaj¹cy siê podczas pauzy
    [SerializeField] private KeyCode pauseButton = KeyCode.Escape; // Przycisk pauzuj¹cy grê

    public bool GamePaused { get; private set; } = false; // flaga pauzy gry

    private void Start()
    {
        ResumeGame(); // Gra nie jest na pocz¹tku zapauzowana
    }

    public void PauseGame() // Metoda pauzuj¹ca grê
    {
        GamePaused = true;
        Time.timeScale = 0; // Ustawienie skali czasu na 0 w rezultacie zapauzuje grê
        pauseScreen.SetActive(true); // W³¹czamy ekran pauzy
    }

    public void ResumeGame()
    {
        GamePaused = false;
        Time.timeScale = 1; // Ustawienie skali czasu na 1 w rezultacie wznowi grê
        pauseScreen.SetActive(false); // Wy³¹czamy ekran pauzy
    }

    private void Update()
    {
        if (Input.GetKeyUp(pauseButton)) // Po wziœniêciu klawisza ESCAPE pausujemy/wznawiamy grê
        {
            if (!GamePaused) { PauseGame(); } // Je¿eli gra wznowiona to pauzujemy
            else { ResumeGame(); }
        }
    }
}
