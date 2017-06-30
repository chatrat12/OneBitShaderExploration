Shader "Hidden/OneBitShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off 
		ZWrite Off 
		ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _PixelSize;

			float2 GetScreenCoords(float2 UVs)
			{
				float2 pixelDelta = _ScreenParams.zw - 1;
				return floor(UVs / pixelDelta);
			}

			float GetDither(float spacing, float2 screenCoords)
			{
				float result;
				float x = fmod(screenCoords.x, spacing * _PixelSize + _PixelSize) >= _PixelSize ? 0 : 1;
				float y = fmod(screenCoords.y, spacing * _PixelSize + _PixelSize) >= _PixelSize ? 0 : 1;
				return !(x && y);
			}

			float GetGrayscale(float3 color)
			{
				const static float oneOverThree = 1.0 / 3.0;
				return (color.r + color.g + color.b) * oneOverThree;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 screenCoords = GetScreenCoords(i.uv);

				float gray = GetGrayscale(tex2D(_MainTex, i.uv));
				float spacing = floor(gray * 8);
				float dither = GetDither(spacing, screenCoords);

				float3 color = dither;
				
				return float4(color, 1);
			}
			ENDCG
		}
	}
}
