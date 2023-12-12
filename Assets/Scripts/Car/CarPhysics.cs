using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    // Stan pojazdu (zmienny)
    public GameInput CarInput { get; set; } = new();
    public bool IsControllable { get; private set; } = false; // pojazd mo�e przyspiesza�, warto�� pocz�tkowa to false poniewa� pojazd musi poczeka� na pocz�tek wy�cigu
    public float RotationAngle { get; private set; } = 0f;
    public float VelocityVsUp { get; private set; } = 0f;
    

    // Parametry pojazdu (niezmienne)
    [SerializeField] private float rotationFactor = 2.7f;
    [SerializeField] private float accelerationFactor = 8.0f;
    [SerializeField] private float slideFactor = 0.95f;
    [SerializeField] private float maxSpeed = 20.0f;
    [SerializeField] private float dragFactor = 3.0f;
    [SerializeField] private float allowTurnVelocityScale = 0.125f;
    [SerializeField] private float sideVelocityDriftThreshold = 7.0f;

    // Komponenty
    Rigidbody2D body;

    // Startowa pozycja pojazdu
    public Vector3 startPosition;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Fizyka i wp�yw sterowania
    private void FixedUpdate()
    {
        ApplyInputAndPhysics();
    }

    private void ApplyInputAndPhysics()
    {
        // Obliczenie jak bardzo "do przodu" pojazd si� przemieszcza (bior�c pod uwag� kierunek pr�dko�ci)
        VelocityVsUp = Vector2.Dot(transform.up, body.velocity);

        // Op�r gdy gracz nie przyspiesza, �eby pojazd zwalnia�
        bool noAcceleration = CarInput.accelerate <= PlayerCarController.InputRegisterThreshold;
        body.drag = noAcceleration ? Mathf.Lerp(body.drag, 1.0f, Time.fixedDeltaTime * dragFactor) : 0;

        // Przy�pieszenie
        if (VelocityVsUp <= maxSpeed && IsControllable)
            body.AddForce(CarInput.accelerate * accelerationFactor * transform.up, ForceMode2D.Force);

        // Tarcie boczne
        Vector2 forwardVelocity = transform.up * Vector2.Dot(body.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(body.velocity, transform.right);
        body.velocity = forwardVelocity + rightVelocity * slideFactor;

        // Rotacja (z redukcj� mo�liwo�ci skr�cania przy bardzo ma�ych pr�dko�ciach)
        // if (!IsControllable) return;
        float allowTurnFactor = Mathf.Clamp01(body.velocity.magnitude * allowTurnVelocityScale);
        RotationAngle -= CarInput.right * rotationFactor * allowTurnFactor;
        RotationAngle += CarInput.left * rotationFactor * allowTurnFactor;
        body.MoveRotation(RotationAngle);
    }

    public bool IsCarDrifting()
    {
        // Je�eli pr�dko�� boczna pojazdu (jednostki gry na sekund�) jest wi�ksza ni� pewna granica uznajemy �e pojazd jest w po�lizgu (driftuje)
        return Mathf.Abs(Vector2.Dot(transform.right, body.velocity)) > sideVelocityDriftThreshold;
    }

    // Ustawienie pozycji samochodu do zadanej pozycji i rotacji
    public void SetTransform(Vector3 position, float zRotation)
    {
        body.velocity = Vector2.zero;
        transform.position = position;
        //body.MoveRotation(zRotation);
        RotationAngle = zRotation;
    }

    public void ResetTransform()
    {
        SetTransform(startPosition, 0f);
    }

    public void MakeCarControllable()
    {
        IsControllable = true;
    }


    private void OnEnable() // Pojazd subskrybuje do wydarzenia rozpocz�cia wy�cigu
    {
        EventManager.RaceStart += MakeCarControllable; // Pojazd dostaje mo�liwo�� poruszania si� przy rozpocz�ciu wy�cigu
    }

    private void OnDisable() // Metoda usuwaj�ca subskrypcj� - dobra praktyka programistyczna
    {
        EventManager.RaceStart -= MakeCarControllable;
    }
}
