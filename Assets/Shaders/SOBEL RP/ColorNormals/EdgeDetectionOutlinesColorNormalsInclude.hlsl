/* Contours SOBEL avec la couleur et les normales */

#ifndef SOBELOUTLINES_INCLUDED
#define SOBELOUTLINES_INCLUDED

#include "DecodeDepthNormals.hlsl"

TEXTURE2D(_DepthNormalsTexture); SAMPLER(sampler_DepthNormalsTexture);

// The sobel effect runs by sampling the texture around a point to see
// if there are any large changes. Each sample is multiplied by a convolution
// matrix weight for the x and y components seperately. Each value is then
// added together, and the final sobel value is the length of the resulting float2.
// Higher values mean the algorithm detected more of an edge

// These are points to sample relative to the starting point
static float2 sobelSamplePoints[9] = {
	float2(-1, 1), float2(0, 1), float2(1, 0),
	float2(-1, 0), float2(0, 0), float2(1, 0),
	float2(-1, -1), float2(0, -1), float2(1, -1),
};

// Weights for the x component
static float sobelXMatrix[9] = {
	1, 0, -1,
	2, 0, -2,
	1, 0, -1
};

// Weights for the y component
static float sobelYMatrix[9] = {
	1, 2, 1,
	0, 0, 0,
	-1, -2, -1
};

// This function runs the sobel algorithm over the opaque texture
void ColorSobel_float(float2 UV, float Thickness, out float Out) {
	// We have to run the sobel algorithm over the RGB channels separately
	float2 sobelR = 0;
	float2 sobelG = 0;
	float2 sobelB = 0;
	// We can unroll this loop to make it more efficient
	// The compiler is also smart enough to remove the i=4 iteration, which is always zero
	[unroll] for (int i = 0; i < 9; i++) {
		// Sample the scene color texture
		float3 rgb = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV + sobelSamplePoints[i] * Thickness);
		// Create the kernel for this iteration
		float2 kernel = float2(sobelXMatrix[i], sobelYMatrix[i]);
		// Accumulate samples for each color
		sobelR += rgb.r * kernel;
		sobelG += rgb.g * kernel;
		sobelB += rgb.b * kernel;
	}
	// Get the final sobel value
	// Combine the RGB values by taking the one with the largest sobel value
	Out = max(length(sobelR), max(length(sobelG), length(sobelB)));

}

// Sample the depth normal map and decode depth and normal from the texture
// nous => on ne r�cup�re pas la valeur de la profondeur (depth)
void GetNormal(float2 uv, out float3 normal) {
	float4 coded = SAMPLE_TEXTURE2D(_DepthNormalsTexture, sampler_DepthNormalsTexture, uv);
	DecodeDepthNormal(coded, normal);
}

// This function runs the sobel algorithm over the opaque texture
void NormalsSobel_float(float2 UV, float Thickness, out float Out) {
	// We have to run the sobel algorithm over the XYZ channels separately, like color
	float2 sobelX = 0;
	float2 sobelY = 0;
	float2 sobelZ = 0;
	// We can unroll this loop to make it more efficient
	// The compiler is also smart enough to remove the i=4 iteration, which is always zero
	[unroll] for (int i = 0; i < 9; i++) {
		float depth;
		float3 normal;
		GetNormal(UV + sobelSamplePoints[i] * Thickness, normal);
		// Create the kernel for this iteration
		float2 kernel = float2(sobelXMatrix[i], sobelYMatrix[i]);
		// Accumulate samples for each coordinate
		sobelX += normal.x * kernel;
		sobelY += normal.y * kernel;
		sobelZ += normal.z * kernel;
	}
	// Get the final sobel value
	// Combine the XYZ values by taking the one with the largest sobel value
	Out = max(length(sobelX), max(length(sobelY), length(sobelZ)));
}

#endif