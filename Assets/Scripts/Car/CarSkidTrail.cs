using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSkidTrail : MonoBehaviour
{
    CarPhysics carScript; // Skrypt odpowiedzialny za fizyk� i aplikowanie sterowania pojazdu
    TrailRenderer trailRenderer; // Projektor �ladu opon na nawierzchni

    private void Awake()
    {
        // Pozyskanie komponent�w
        carScript = GetComponentInParent<CarPhysics>(); // Obiekty �ladu opon s� dzie�mi pojazdu
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        // �lady opon pokazywane s� kiedy pojazd jest w po�lizgu (driftuje)
        trailRenderer.emitting = carScript.IsCarDrifting();
    }
}
