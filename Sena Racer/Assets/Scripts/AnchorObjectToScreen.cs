using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AnchorObjectToScreen : MonoBehaviour
{
    public GameObject objectToAnchor; // El prefab del objeto 3D que deseas anclar
    public float distanceFromCamera = 1.0f; // La distancia entre la cámara y el objeto anclado
    public bool anchorToCenter = true; // Indica si el objeto se anclará al centro de la pantalla o a una posición específica

    private Camera mainCamera;
    private Vector3 screenCenter;
    private RaycastHit hit;

    void Start()
    {
        mainCamera = Camera.main;
        screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
    }

    void Update()
    {
        if (objectToAnchor == null)
            return;

        Vector3 desiredPosition = Vector3.zero; // Initialize to a default value

        if (anchorToCenter)
        {
            desiredPosition = mainCamera.ViewportToWorldPoint(screenCenter) + mainCamera.transform.forward * distanceFromCamera;
        }
        else
        {
            // Implementar la lógica para calcular la posición deseada en función del toque en la pantalla
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    desiredPosition = hit.point;
                }
            }
        }

        // Ensure desiredPosition is always assigned before using it
        objectToAnchor.transform.position = desiredPosition;

        // Mantener la rotación del objeto 3D perpendicular a la cámara
        objectToAnchor.transform.LookAt(mainCamera.transform.position, Vector3.up);
    }
}
