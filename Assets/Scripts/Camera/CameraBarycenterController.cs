using UnityEngine;

/// <summary>
/// Place l'objet auquel ce script est attaché à égale distance de toutes les caméras dans la scène.
/// </summary>
public class CameraBarycenterController : MonoBehaviour
{
    private Camera[] cameras;

    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
    }

    void Update()
    {
        Vector3 barycenter = Vector3.zero;
        foreach (Camera camera in cameras) barycenter += camera.transform.position;
        if (cameras.Length > 0) barycenter /= cameras.Length;
        transform.position = barycenter;
    }
}