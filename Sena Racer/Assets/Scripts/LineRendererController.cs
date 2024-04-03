using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    public Transform targetObject; // El objeto 3D al que quieres conectar la línea
    public Color lineColor = Color.blue; // Color de la línea

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    private void Update()
    {
        // Actualiza la posición de la línea para que siempre esté unida a tu posición
        lineRenderer.SetPosition(0, transform.position);

        // Actualiza la posición de la otra punta de la línea para que esté unida al objeto 3D
        if (targetObject != null)
        {
            lineRenderer.SetPosition(1, targetObject.position);
        }
    }
}
