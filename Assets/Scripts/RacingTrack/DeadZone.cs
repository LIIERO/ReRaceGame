using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour // Skrypt odbijacza ograniczaj¹cego tor dla pojazdów
{
    public static bool ResetCarTransformOnDeadZone {  get; set; }

    [SerializeField] private Transform respawnTransform;
    private float respawnZAngle;

    private void Start()
    {
        respawnZAngle = respawnTransform.transform.eulerAngles.z;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car")) // Je¿eli pojazd wjedzie w strefê poza torem
        {
            CarPhysics carScript = collision.gameObject.GetComponent<CarPhysics>();

            if (ResetCarTransformOnDeadZone) carScript.ResetTransform();
            else carScript.SetTransform(respawnTransform.position, respawnZAngle);
        }
    }

    /*private IEnumerator DisableCarControlForSeconds(GameObject car, float time)
    {
        CarPhysics carScript = car.GetComponent<CarPhysics>();
        carScript.IsControllable = false;

        yield return new WaitForSeconds(time);
        carScript.IsControllable = true;
    }*/
}
