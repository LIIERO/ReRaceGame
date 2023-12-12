using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSkidTrail : MonoBehaviour
{
    CarPhysics carScript; // Skrypt odpowiedzialny za fizykê i aplikowanie sterowania pojazdu
    TrailRenderer trailRenderer; // Projektor œladu opon na nawierzchni

    private void Awake()
    {
        // Pozyskanie komponentów
        carScript = GetComponentInParent<CarPhysics>(); // Obiekty œladu opon s¹ dzieæmi pojazdu
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        // Œlady opon pokazywane s¹ kiedy pojazd jest w poœlizgu (driftuje)
        trailRenderer.emitting = carScript.IsCarDrifting();
    }
}
