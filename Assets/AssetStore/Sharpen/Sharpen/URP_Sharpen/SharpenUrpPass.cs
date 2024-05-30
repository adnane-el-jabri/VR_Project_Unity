namespace UnityEngine.Rendering.Universal
{
    internal class SharpenUrpPass : ScriptableRenderPass
    {
        public Material material;

        private readonly string tag;
        private readonly float sharpness;

        static readonly int centralString = Shader.PropertyToID("_CentralFactor");
        static readonly int sideString = Shader.PropertyToID("_SideFactor");
        static readonly int tempCopyString = Shader.PropertyToID("_TempCopy");

        private RenderTargetIdentifier source;
        private RenderTargetIdentifier tempCopy = new RenderTargetIdentifier(tempCopyString);

        public SharpenUrpPass(RenderPassEvent renderPassEvent, Material material, float sharpness, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.tag = tag;
            this.material = material;
            this.sharpness = sharpness;
        }

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(tag);
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            material.SetFloat(centralString, 1.0f + (3.2f * sharpness));
            material.SetFloat(sideString, 0.8f * sharpness);

            cmd.GetTemporaryRT(tempCopyString, opaqueDesc, FilterMode.Bilinear);
            cmd.CopyTexture(source, tempCopy);
            cmd.Blit(tempCopy, source, material, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempCopyString);
        }
    }
}
