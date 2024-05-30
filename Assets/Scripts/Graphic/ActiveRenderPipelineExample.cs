using UnityEngine;
using UnityEngine.Rendering;

public class ActiveRenderPipelineExample : MonoBehaviour
{
    public RenderPipelineAsset defaultRenderPipelineAsset;
    public RenderPipelineAsset overrideRenderPipelineAsset;

    void Awake()
    {
        GraphicsSettings.defaultRenderPipeline = defaultRenderPipelineAsset;
        QualitySettings.renderPipeline = overrideRenderPipelineAsset;

        DisplayCurrentRenderPipeline();
    }

    void SwitchDefaultRenderPipeline()
    {
        if (GraphicsSettings.defaultRenderPipeline == defaultRenderPipelineAsset)
        {
            GraphicsSettings.defaultRenderPipeline = null;
        }
        else
        {
            GraphicsSettings.defaultRenderPipeline = defaultRenderPipelineAsset;
        }
    }

    void SwitchOverrideRenderPipeline()
    {
        if (QualitySettings.renderPipeline == overrideRenderPipelineAsset)
        {
            QualitySettings.renderPipeline = null;
        }
        else
        {
           QualitySettings.renderPipeline = overrideRenderPipelineAsset;
        }
    }

    void DisplayCurrentRenderPipeline()
    {
        if (GraphicsSettings.defaultRenderPipeline != null)
        {
            Debug.Log("The default render pipeline is defined by " + GraphicsSettings.defaultRenderPipeline.name);
        }
        else
        {
            Debug.Log("The default render pipeline is the Built-in Render Pipeline");
        }

        if (QualitySettings.renderPipeline != null)
        {
            Debug.Log("The override render pipeline for the current quality level is defined by " + QualitySettings.renderPipeline.name);
        }
        else
        {
            Debug.Log("No override render pipeline exists for the current quality level");
        }

        if (QualitySettings.renderPipeline != null)
        {
            Debug.Log("The active render pipeline is the override render pipeline");
        }
        else
        {
            Debug.Log("The active render pipeline is the default render pipeline");
        }

        if (GraphicsSettings.currentRenderPipeline != null)
        {
            Debug.Log("The active render pipeline is defined by " +GraphicsSettings.currentRenderPipeline.name);
        }
        else
        {
            Debug.Log("The active render pipeline is the Built-in Render Pipeline");
        }
    }
}
