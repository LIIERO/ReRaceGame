using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    // Stan pojazdu (zmienny)
    public GameInput CarInput { get; set; } = new();
    public bool IsControllable { get; private set; } = false; // pojazd mo¿e przyspieszaæ, wartoœæ pocz¹tkowa to false poniewa¿ pojazd musi poczekaæ na pocz¹tek wyœcigu
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

    // Fizyka i wp³yw sterowania
    private void FixedUpdate()
    {
        ApplyInputAndPhysics();
    }

    private void ApplyInputAndPhysics()
    {
        // Obliczenie jak bardzo "do przodu" pojazd siê przemieszcza (bior¹c pod uwagê kierunek prêdkoœci)
        VelocityVsUp = Vector2.Dot(transform.up, body.velocity);

        // Opór gdy gracz nie przyspiesza, ¿eby pojazd zwalnia³
        bool noAcceleration = CarInput.accelerate <= PlayerCarController.InputRegisterThreshold;
        body.drag = noAcceleration ? Mathf.Lerp(body.drag, 1.0f, Time.fixedDeltaTime * dragFactor) : 0;

        // Przyœpieszenie
        if (VelocityVsUp <= maxSpeed && IsControllable)
            body.AddForce(CarInput.accelerate * accelerationFactor * transform.up, ForceMode2D.Force);

        // Tarcie boczne
        Vector2 forwardVelocity = transform.up * Vector2.Dot(body.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(body.velocity, transform.right);
        body.velocity = forwardVelocity + rightVelocity * slideFactor;

        // Rotacja (z redukcj¹ mo¿liwoœci skrêcania przy bardzo ma³ych prêdkoœciach)
        // if (!IsControllable) return;
        float allowTurnFactor = Mathf.Clamp01(body.velocity.magnitude * allowTurnVelocityScale);
        RotationAngle -= CarInput.right * rotationFactor * allowTurnFactor;
        RotationAngle += CarInput.left * rotationFactor * allowTurnFactor;
        body.MoveRotation(RotationAngle);
    }

    public bool IsCarDrifting()
    {
        // Je¿eli prêdkoœæ boczna pojazdu (jednostki gry na sekundê) jest wiêksza ni¿ pewna granica uznajemy ¿e pojazd jest w poœlizgu (driftuje)
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


    private void OnEnable() // Pojazd subskrybuje do wydarzenia rozpoczêcia wyœcigu
    {
        EventManager.RaceStart += MakeCarControllable; // Pojazd dostaje mo¿liwoœæ poruszania siê przy rozpoczêciu wyœcigu
    }

    private void OnDisable() // Metoda usuwaj¹ca subskrypcjê - dobra praktyka programistyczna
    {
        EventManager.RaceStart -= MakeCarControllable;
    }
}
