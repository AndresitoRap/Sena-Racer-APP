using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(AROcclusionManager))]
public class DepthOcclusion : MonoBehaviour
{
    private AROcclusionManager arOcclusionManager;
    private Material material;

    void Start()
    {
        arOcclusionManager = GetComponent<AROcclusionManager>();
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (arOcclusionManager.enabled && arOcclusionManager.TryAcquireEnvironmentDepthCpuImage(out XRCpuImage image))
        {
            // Aquí es donde manejarías la lógica de la oclusión.
            // Por ejemplo, podrías comprobar si el objeto 3D está detrás de un objeto del mundo real y, de ser así, ocultarlo.
            // Esto es solo un ejemplo y necesitarás adaptarlo para que funcione con tu aplicación específica.
            Vector2Int dimensions = new Vector2Int(image.width, image.height);
            int bufferSize = image.GetConvertedDataSize(dimensions, TextureFormat.R16);
            material.SetFloat("_Depth", bufferSize);
        }
    }
}
