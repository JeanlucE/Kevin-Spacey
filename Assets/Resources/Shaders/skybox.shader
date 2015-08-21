Shader "Custom/Skybox" {
	Properties {
		_MainTex ("MainTexture (Alpha)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "Background"} 
		LOD 200
		
		Pass {
			ZWrite On
			Cull Front
			Blend One Zero

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

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
				return tex2D(_MainTex, _MainTex_ST.xy * input.tex.xy + _MainTex_ST.zw);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
