using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraDampTime; // Czas reakcji kamery na zmian� pozycji pojazdu
    [SerializeField] private float cameraRotationDampTime; // Reakcja kamery na zmian� rotacji pojazdu
    [SerializeField] private float carVelocityFactor; // Wp�yw pr�dko�ci pojazdu na ruch kamery

    // Pojazd kt�ry ma �ledzi� kamera
    [SerializeField] private GameObject carToFollow;
    private Rigidbody2D carBody;
    private CarPhysics carScript;

    // Punkt, do kt�rego ma d��y� kamera po uko�czeniu przez gracza wy�cigu
    [SerializeField] private Transform raceEndFocusPoint;
    private bool raceEndCamera = false;

    private Vector3 cameraVelocity = Vector3.zero; // Aktualna pr�dko�� kamery

    private void Start()
    {
        ChangeCarToFollow(carToFollow); // Ustawienie pojazdu za kt�rym ma pod��a� kamera
        transform.position = new Vector3(carScript.startPosition.x, carScript.startPosition.y, transform.position.z); // Ustawienie startowej pozycji kamery na pozycj� pojazdu
    }

    public void ChangeCarToFollow(GameObject car) // Zmie� �ledzony samoch�d
    {
        carToFollow = car;
        carBody = carToFollow.GetComponent<Rigidbody2D>();
        carScript = carToFollow.GetComponent<CarPhysics>();
    }

    private Vector3 CalculateNewCameraPosition(Transform focusPoint)
    {
        // Pozycja do kt�rej d��y kamera jest przed pojazdem i zale�na od jego pr�dko�ci aby by�o dobrze wida� co jest przed pojazdem
        Vector3 carVelocityVector = new Vector3(carBody.velocity.x, carBody.velocity.y, 0f);
        Vector3 desiredPosition = focusPoint.position + (carVelocityVector * carVelocityFactor);

        // Dodanie inercji do pozycji kamery
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, cameraDampTime);
        return smoothedPosition;
    }

    private void LateUpdate()
    {
        // Je�eli wy�cig trwa to kamera pod��a za pojazdem, a je�li si� sko�czy� to za specyficznym punktem
        Transform focusPoint = raceEndCamera ? raceEndFocusPoint : carToFollow.transform;

        Vector3 newCameraPosition = CalculateNewCameraPosition(focusPoint);

        // Aplikacja nowej pozycji kamery (wszystkie wymiary opr�cz z, poniewa� nie jest istotny)
        transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.position.z);
    }

    public void SwitchToRaceEndCamera()
    {
        raceEndCamera = true;
    }


    private void OnEnable() // Kamera subskrybuje do wydarzenia zako�czenia wy�cigu
    {
        EventManager.RaceEnd += SwitchToRaceEndCamera; // Kamera przemieszcza si� do okre�lonego punktu przy zako�czeniu wy�cigu
    }

    private void OnDisable() // Metoda usuwaj�ca subskrypcj� - dobra praktyka programistyczna
    {
        EventManager.RaceEnd -= SwitchToRaceEndCamera;
    }
}
