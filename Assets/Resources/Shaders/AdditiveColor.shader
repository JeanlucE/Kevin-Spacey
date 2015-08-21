Shader "Custom/AdditiveColor" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("MainTexture (Alpha)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "Overlay"} 
		LOD 200
		
		Pass {
			ZWrite Off

			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			sampler2D _MainTex;

			struct VertIn {
				float4 pos : POSITION;
				float2 tex : TEXCOORD0;
			};

			struct FragIn {
				float4 pos : SV_POSITION;
				float2 tex : TEXCOORD0;
			};

			FragIn vert(VertIn input) 
			{
				FragIn output;

				output.pos = mul(UNITY_MATRIX_MVP, input.pos);
				output.tex = input.tex;

				return output;
			}

			float4 frag(FragIn input) : COLOR
			{
				float4 c = fixed4(1, 1, 1, tex2D(_MainTex, input.tex).w ) * _Color;
				return c;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
