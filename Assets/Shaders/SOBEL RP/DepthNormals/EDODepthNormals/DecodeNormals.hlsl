#ifndef DECODENORMALS_INCLUDED
#define DECODENORMALS_INCLUDED

inline float DecodeFloatRG(float2 enc) {
	float2 kDecodeDot = float2(1.0, 1 / 255.0);
	return dot(enc, kDecodeDot);
}

inline float3 DecodeViewNormalStereo(float4 enc4) {
	float kScale = 1.7777;
	float3 nn = enc4.xyz * float3(2 * kScale, 2 * kScale, 0) + float3(-kScale, -kScale, 1);
	float g = 2.0 / dot(nn.xyz, nn.xyz);
	float3 n;
	n.xy = g * nn.xy;
	n.z = g - 1;
	return n;
}

inline void DecodeNormal(float4 enc, out float3 normal) {
	normal = DecodeViewNormalStereo(enc);
}

#endif