using UnityEngine;

[ExecuteInEditMode]
public class Sharpen : MonoBehaviour
{
    [Range(0, 1)]
    [Tooltip("Sharpness")]
    public float Sharpness = 0.0f;

    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_CentralFactor", 1.0f + (3.2f * Sharpness));
        material.SetFloat("_SideFactor", 0.8f * Sharpness);
        Graphics.Blit(source, destination, material, 0);
    }
}
