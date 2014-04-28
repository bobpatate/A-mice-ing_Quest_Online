using UnityEngine;

/// <summary>
/// Permet de déplacer un objet avec des touches de façon basique (gauche/droite, avancer/reculer).
/// </summary>
public class MovementController : MonoBehaviour
{
    public string movementAxis = "Vertical";
    public string rotationAxis = "Horizontal";

    public float angularSpeed = 90.0f;
    public float movementSpeed = 1.0f;

    void Update()
    {
        float movementDirection = Input.GetAxis(movementAxis);
        float rotationDirection = Input.GetAxis(rotationAxis);

        transform.Rotate(new Vector3(0, Time.deltaTime * angularSpeed * rotationDirection, 0));
        transform.position += Time.deltaTime * movementSpeed * movementDirection * (transform.rotation * Vector3.forward);
    }
}