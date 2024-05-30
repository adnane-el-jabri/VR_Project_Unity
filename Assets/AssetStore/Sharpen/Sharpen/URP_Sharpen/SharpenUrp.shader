Shader "SupGames/Shaders/SharpenURP"
{
	Properties
	{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
	}
		HLSLINCLUDE

	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

	TEXTURE2D_X(_MainTex);
	SAMPLER(sampler_MainTex);

	half _CentralFactor;
	half _SideFactor;
	half4 _MainTex_TexelSize;

	struct AttributesDefault
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		half4 pos : SV_POSITION;
		half2 uv  : TEXCOORD0;
		half4 uv1  : TEXCOORD1;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2f vert(AttributesDefault v)
	{
		v2f o = (v2f)0;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(v.vertex.xyz, 1.0h)));
		o.uv = UnityStereoTransformScreenSpaceTex(v.uv);
		o.uv1 = half4(o.uv - _MainTex_TexelSize.xy, o.uv + _MainTex_TexelSize.xy);
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		half4 c = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv)*_CentralFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.xy)*_SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.xw)*_SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.zy)*_SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.zw)*_SideFactor;
		return c;

	}
		ENDHLSL

		Subshader
	{
		Pass
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma vertex vert
		  #pragma fragment frag
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}
	}
}
