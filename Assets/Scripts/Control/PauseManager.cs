using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen; // Ekran pojawiaj�cy si� podczas pauzy
    [SerializeField] private KeyCode pauseButton = KeyCode.Escape; // Przycisk pauzuj�cy gr�

    public bool GamePaused { get; private set; } = false; // flaga pauzy gry

    private void Start()
    {
        ResumeGame(); // Gra nie jest na pocz�tku zapauzowana
    }

    public void PauseGame() // Metoda pauzuj�ca gr�
    {
        GamePaused = true;
        Time.timeScale = 0; // Ustawienie skali czasu na 0 w rezultacie zapauzuje gr�
        pauseScreen.SetActive(true); // W��czamy ekran pauzy
    }

    public void ResumeGame()
    {
        GamePaused = false;
        Time.timeScale = 1; // Ustawienie skali czasu na 1 w rezultacie wznowi gr�
        pauseScreen.SetActive(false); // Wy��czamy ekran pauzy
    }

    private void Update()
    {
        if (Input.GetKeyUp(pauseButton)) // Po wzi�ni�ciu klawisza ESCAPE pausujemy/wznawiamy gr�
        {
            if (!GamePaused) { PauseGame(); } // Je�eli gra wznowiona to pauzujemy
            else { ResumeGame(); }
        }
    }
}
