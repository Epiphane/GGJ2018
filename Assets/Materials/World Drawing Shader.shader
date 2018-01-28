Shader "Unlit/World Drawing Shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SoundTex("SoundMap", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SoundTex;
			float4 _SoundTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 sound = tex2D(_SoundTex, i.uv);
				//return col;

				float brightness = max(max(sound.r, sound.g), sound.b);
				col.r *= brightness;
				col.g *= brightness;
				col.b *= brightness;
				return col;
			}
			ENDCG
		}
	}
}
