using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraDampTime; // Czas reakcji kamery na zmianê pozycji pojazdu
    [SerializeField] private float cameraRotationDampTime; // Reakcja kamery na zmianê rotacji pojazdu
    [SerializeField] private float carVelocityFactor; // Wp³yw prêdkoœci pojazdu na ruch kamery

    // Pojazd który ma œledziæ kamera
    [SerializeField] private GameObject carToFollow;
    private Rigidbody2D carBody;
    private CarPhysics carScript;

    // Punkt, do którego ma d¹¿yæ kamera po ukoñczeniu przez gracza wyœcigu
    [SerializeField] private Transform raceEndFocusPoint;
    private bool raceEndCamera = false;

    private Vector3 cameraVelocity = Vector3.zero; // Aktualna prêdkoœæ kamery

    private void Start()
    {
        ChangeCarToFollow(carToFollow); // Ustawienie pojazdu za którym ma pod¹¿aæ kamera
        transform.position = new Vector3(carScript.startPosition.x, carScript.startPosition.y, transform.position.z); // Ustawienie startowej pozycji kamery na pozycjê pojazdu
    }

    public void ChangeCarToFollow(GameObject car) // Zmieñ œledzony samochód
    {
        carToFollow = car;
        carBody = carToFollow.GetComponent<Rigidbody2D>();
        carScript = carToFollow.GetComponent<CarPhysics>();
    }

    private Vector3 CalculateNewCameraPosition(Transform focusPoint)
    {
        // Pozycja do której d¹¿y kamera jest przed pojazdem i zale¿na od jego prêdkoœci aby by³o dobrze widaæ co jest przed pojazdem
        Vector3 carVelocityVector = new Vector3(carBody.velocity.x, carBody.velocity.y, 0f);
        Vector3 desiredPosition = focusPoint.position + (carVelocityVector * carVelocityFactor);

        // Dodanie inercji do pozycji kamery
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, cameraDampTime);
        return smoothedPosition;
    }

    private void LateUpdate()
    {
        // Je¿eli wyœcig trwa to kamera pod¹¿a za pojazdem, a jeœli siê skoñczy³ to za specyficznym punktem
        Transform focusPoint = raceEndCamera ? raceEndFocusPoint : carToFollow.transform;

        Vector3 newCameraPosition = CalculateNewCameraPosition(focusPoint);

        // Aplikacja nowej pozycji kamery (wszystkie wymiary oprócz z, poniewa¿ nie jest istotny)
        transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.position.z);
    }

    public void SwitchToRaceEndCamera()
    {
        raceEndCamera = true;
    }


    private void OnEnable() // Kamera subskrybuje do wydarzenia zakoñczenia wyœcigu
    {
        EventManager.RaceEnd += SwitchToRaceEndCamera; // Kamera przemieszcza siê do okreœlonego punktu przy zakoñczeniu wyœcigu
    }

    private void OnDisable() // Metoda usuwaj¹ca subskrypcjê - dobra praktyka programistyczna
    {
        EventManager.RaceEnd -= SwitchToRaceEndCamera;
    }
}
