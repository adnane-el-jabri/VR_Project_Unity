Shader "The Developer/SS All-in-One Outline"
{
    Properties
    {
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		//_OutlineColor("Outline Color", Color) = (1,1,1,1)
		//_OutlineWidth("Outline Width", Range(0.0004, 0.03)) = 0.0004
		_MainTex("Texture", 2D) = "white" {}
	//_OutlineColorStart("Outline Color Start", Color) = (1,1,1,1)
	//_OutlineColorEnd("Outline Color End", Color) = (0,0,0,1)
	_BinarizationThreshold("Binarization Threshold", Range(0.002, 0.2)) = 0.35 // Nouvelle propriété pour le seuil de binarisation
	}
		SubShader
	{
		Tags {"Queue" = "Overlay" "RenderType" = "Overlay"}
		//Tags {"Queue" = "Geometry+1" "RenderType" = "Opaque"}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			float4 _OutlineColor;
			//float4 _OutlineColorStart;
			//float4 _OutlineColorEnd;
			//float _OutlineWidth;
			float _OutlineRadius;
			sampler2D _MainTex;
			float _BinarizationThreshold;

			appdata_t vert(appdata_t v)
			{
				appdata_t o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}


			float4 frag(appdata_t v) : SV_Target
			{
				// Sample the main texture
				//float4 color = tex2D(_MainTex, v.texcoord);
				//float luminosity = 0.299 * color.r + 0.587 * color.g + 0.114 * color.b;

				float4 colorCenter = tex2D(_MainTex, v.texcoord);
				float luminosityCenter = 0.299 * colorCenter.r + 0.587 * colorCenter.g + 0.114 * colorCenter.b;

				//fixed4 binarizedColor = (luminosity >= _BinarizationThreshold) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);
				fixed4 binarizedColorCenter = (luminosityCenter >= _BinarizationThreshold) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);
				/*float4 colorRight = tex2D(_MainTex, v.texcoord + float2(_OutlineWidth, 0));
				float4 colorLeft = tex2D(_MainTex, v.texcoord + float2(-_OutlineWidth, 0));
				float4 colorUp = tex2D(_MainTex, v.texcoord + float2(0, _OutlineWidth));
				float4 colorDown = tex2D(_MainTex, v.texcoord + float2(0, -_OutlineWidth));
				*/
				_ScreenParams.x = 3000;
				_ScreenParams.y = 4000;

				float4 colorRight = tex2D(_MainTex, v.texcoord + float2(1.0 / _ScreenParams.x, 0));
				float4 colorLeft = tex2D(_MainTex, v.texcoord + float2(-1.0 / _ScreenParams.x, 0));
				float4 colorUp = tex2D(_MainTex, v.texcoord + float2(0, 1.0 / _ScreenParams.y));
				float4 colorDown = tex2D(_MainTex, v.texcoord + float2(0, -1.0 / _ScreenParams.y));

				float4 colorUpRight = tex2D(_MainTex, v.texcoord + float2(1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y));
				float4 colorUpLeft = tex2D(_MainTex, v.texcoord + float2(-1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y));
				float4 colorDownRight = tex2D(_MainTex, v.texcoord + float2(1.0 / _ScreenParams.x, -1.0 / _ScreenParams.y));
				float4 colorDownLeft = tex2D(_MainTex, v.texcoord + float2(-1.0 / _ScreenParams.x, -1.0 / _ScreenParams.y));

				/*float luminosityRight = 0.299 * colorRight.r + 0.587 * colorRight.g + 0.114 * colorRight.b;
				float luminosityLeft = 0.299 * colorLeft.r + 0.587 * colorLeft.g + 0.114 * colorLeft.b;
				float luminosityUp = 0.299 * colorUp.r + 0.587 * colorUp.g + 0.114 * colorUp.b;
				float luminosityDown = 0.299 * colorDown.r + 0.587 * colorDown.g + 0.114 * colorDown.b;

				fixed4 binarizedColorRight = (luminosityRight >= _BinarizationThreshold) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);
				fixed4 binarizedColorLeft = (luminosityLeft >= _BinarizationThreshold) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);
				fixed4 binarizedColorUp = (luminosityUp >= _BinarizationThreshold) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);
				fixed4 binarizedColorDown = (luminosityDown >= _BinarizationThreshold) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);
				*/

				//Passage en niveau de gris
				float luminosityRight = 0.299 * colorRight.r + 0.587 * colorRight.g + 0.114 * colorRight.b;
				float luminosityLeft = 0.299 * colorLeft.r + 0.587 * colorLeft.g + 0.114 * colorLeft.b;
				float luminosityUp = 0.299 * colorUp.r + 0.587 * colorUp.g + 0.114 * colorUp.b;
				float luminosityDown = 0.299 * colorDown.r + 0.587 * colorDown.g + 0.114 * colorDown.b;

				float luminosityUpRight = 0.299 * colorUpRight.r + 0.587 * colorUpRight.g + 0.114 * colorUpRight.b;
				float luminosityUpLeft = 0.299 * colorUpLeft.r + 0.587 * colorUpLeft.g + 0.114 * colorUpLeft.b;
				float luminosityDownRight = 0.299 * colorDownRight.r + 0.587 * colorDownRight.g + 0.114 * colorDownRight.b;
				float luminosityDownLeft = 0.299 * colorDownLeft.r + 0.587 * colorDownLeft.g + 0.114 * colorDownLeft.b;

				// Calculate the outline
				/*float4 outline = tex2D(_MainTex, v.texcoord + float2(_OutlineWidth, 0))
							   + tex2D(_MainTex, v.texcoord + float2(-_OutlineWidth, 0))
							   + tex2D(_MainTex, v.texcoord + float2(0, _OutlineWidth))
							   + tex2D(_MainTex, v.texcoord + float2(0, -_OutlineWidth));*/

							   /*float4 outline = binarizedColorRight + binarizedColorLeft + binarizedColorUp + binarizedColorDown;

							   outline /= 4;
							   //outline = (outline >= fixed4(1, 1, 1, 1)) ? fixed4(1, 1, 1, 1) : fixed4(0, 0, 0, 1);

							   // Check if the current pixel is inside the outline (not near the edges)
							   //float isInsideOutline = smoothstep(0.0, _OutlineWidth * 1.5, length(outline.rgb - .rgb));
							   float isInsideOutline = smoothstep(0.0, _OutlineWidth * 1.5, length(outline.rgb - binarizedColor.rgb));

							   // Calculate the color gradient for the outline
							   float4 outlineColor = lerp(_OutlineColorStart, _OutlineColorEnd, isInsideOutline);

							   // Mix the gradient outline color with the original color
							   float4 finalColor = lerp(color, outlineColor, isInsideOutline);

							   return finalColor;*/

							   //calcul du gradient horizontal de SOBEL
							   float Gx = -luminosityUpLeft - 2 * luminosityLeft - luminosityDownLeft + luminosityUpRight + 2 * luminosityRight + luminosityDownRight;
							   //calcul du gradient vertical de SOBEL
							   float Gy = -luminosityUpLeft - 2 * luminosityUp - luminosityUpRight + luminosityDownLeft + 2 * luminosityDown + luminosityDownRight;
							   //Calcul de la magnitude du gradient (sans la racine carrée pour gagner du temps)
							   float G = Gx * Gx + Gy * Gy;
							   //Impossible d'avoir le max pour "normaliser" => on utilise le max théorique (Gmax=32) transition noir/blanc
							   //G /= 32.0;

							   //On ramène à 0 toutes les valeurs de G<seuil et on garde les autres valeurs de G intactes (supprime le bruit)
							   float thresholdedG = (G >= _BinarizationThreshold) ? 1 : 0;
							   //thresholdedG = G;
							   //On crée une couleur de gradient plus ou moins forte en fonction de la magnitude de celui-ci
							   //float4 finalColor = _OutlineColor * thresholdedG;
							   //float4 finalColor = fixed4(thresholdedG, thresholdedG, thresholdedG, 1);

							   //float gris = 0.75;
							   //colorCenter = fixed4(gris, gris, gris, 1);

							   float4 finalColor = lerp(colorCenter, _OutlineColor, thresholdedG);
							   //Couleur du pixel renvoyée pour être superposée au rendu final de la scène
							   return finalColor;
						   }
						   ENDCG
					   }
	}
}
