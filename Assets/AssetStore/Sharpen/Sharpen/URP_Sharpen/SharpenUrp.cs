namespace UnityEngine.Rendering.Universal
{
    public class SharpenUrp : ScriptableRendererFeature
    {
        [System.Serializable]
        public class SharpenSettings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;

            public Material blitMaterial = null;

            [Range(0, 1)]
            public float Sharpness = 1.0f;
        }

        public SharpenSettings settings = new SharpenSettings();

        SharpenUrpPass sharpenUrpPass;

        public override void Create()
        {
            sharpenUrpPass = new SharpenUrpPass(settings.Event, settings.blitMaterial, settings.Sharpness, this.name);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            sharpenUrpPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(sharpenUrpPass);
        }
    }
}

