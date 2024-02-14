using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(AROcclusionManager))]
public class DepthOcclusion : MonoBehaviour
{
    private AROcclusionManager arOcclusionManager;
    private Material material;
    public GameObject objectToHide; // El objeto 3D que quieres ocultar
    public float hideDistance = 3.0f; // La distancia a la que quieres ocultar el objeto

    void Start()
    {
        arOcclusionManager = GetComponent<AROcclusionManager>();
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (arOcclusionManager.enabled && arOcclusionManager.TryAcquireEnvironmentDepthCpuImage(out XRCpuImage image))
        {
            Vector2Int dimensions = new Vector2Int(image.width, image.height);
            int bufferSize = image.GetConvertedDataSize(dimensions, TextureFormat.R16);
            material.SetFloat("_Depth", bufferSize);

            // Comprueba si el objeto 3D está a menos de 'hideDistance' metros de un objeto del mundo real
            RaycastHit hit;
            if (Physics.Raycast(objectToHide.transform.position, -Vector3.up, out hit, hideDistance))
            {
                // Si es así, oculta el objeto 3D
                objectToHide.SetActive(false);
            }
            else
            {
                // Si no, muestra el objeto 3D
                objectToHide.SetActive(true);
            }
        }
    }
}
