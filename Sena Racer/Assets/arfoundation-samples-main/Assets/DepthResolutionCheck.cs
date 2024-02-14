using UnityEngine;
using TMPro; // Importa el espacio de nombres TMPro
using UnityEngine.XR.ARFoundation;

public class DepthResolutionCheck : MonoBehaviour
{
    public TextMeshProUGUI resolutionText; // Usa TextMeshProUGUI en lugar de Text
    private AROcclusionManager occlusionManager;

    void Start()
    {
        occlusionManager = GetComponent<AROcclusionManager>();
    }

    void Update()
    {
        if (occlusionManager && occlusionManager.environmentDepthTexture)
        {
            resolutionText.text = "Depth texture resolution: " + occlusionManager.environmentDepthTexture.width + "x" + occlusionManager.environmentDepthTexture.height;
        }
    }
}
