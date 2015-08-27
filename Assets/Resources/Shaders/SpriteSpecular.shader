Shader "Custom/SpriteSpecular" {
	Properties {
		_MainTex ("MainTexture (Alpha)", 2D) = "white" {}
		_NormalTex ("NormalMap", 2D) = "bump" {}
		_SpecIndex ("Specular Index", Float) = 1
		_AmbientFactor ("Ambient Factor", Float) = 1
		_DiffuseFactor ("Diffuse Factor", Float) = 1
		_SpecularFactor ("Specular Factor", Float) = 1
	}
	SubShader {		
		Tags { "Queue" = "Transparent" }
	
		Pass {
			Tags { "LightMode" = "ForwardBase"} 
		
			ZWrite Off

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct VertIn {
				float4 pos : POSITION;
				float4 col : COLOR;
				float2 tex : TEXCOORD0;
			};

			struct FragIn {
				float4 pos : SV_POSITION;
				float4 col : COLOR;
				float2 tex : TEXCOORD0;
			};

			FragIn vert(VertIn input) 
			{
				FragIn output;

				output.pos = mul(UNITY_MATRIX_MVP, input.pos);
				output.tex = input.tex;
				output.col = input.col;

				return output;
			}

			sampler2D _MainTex;
			fixed _AmbientFactor;

			float4 frag(FragIn input) : COLOR
			{
				float4 tex = tex2D(_MainTex, input.tex);
				return fixed4(UNITY_LIGHTMODEL_AMBIENT.rgb * tex.rgb * input.col.rgb * _AmbientFactor, tex.a);
			}
			ENDCG
		}
		
		Pass {
			Tags { "LightMode" = "ForwardAdd"} 
		
			ZWrite Off

			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct VertIn {
				float4 pos : POSITION;
				float4 col : COLOR;
				float2 tex : TEXCOORD0;
			};

			struct FragIn {
				float4 pos : SV_POSITION;
				float4 col : COLOR;
				float2 tex : TEXCOORD0;
				float3 posWorld : TEXCOORD1;
			};

			FragIn vert(VertIn input) 
			{
				FragIn output;

				output.pos = mul(UNITY_MATRIX_MVP, input.pos);
				output.tex = input.tex;
				output.col = input.col;
				output.posWorld = mul(_Object2World, input.pos);

				return output;
			}

			sampler2D _MainTex;
			sampler2D _NormalTex;
			float4 _LightColor0;
			float _SpecIndex;
			fixed _DiffuseFactor;
			fixed _SpecularFactor;

			float4 frag(FragIn input) : COLOR
			{
				float4 tex = tex2D(_MainTex, input.tex);
				
				float3 n = (tex2D(_NormalTex, input.tex).rgb - 0.5f) * 2.f;
				n = mul(fixed4(n,1.f), _World2Object).rgb;
				n.z *= -1.f;
				n = normalize(n);
				
				float3 l = (_WorldSpaceLightPos0 - input.posWorld).rgb;
				float attentuation = pow(1.f / length(l), 2.f);
				l = normalize(l);
				float dotNormalLight = dot(n,l);
				float3 diff = saturate(dotNormalLight) * tex.rgb * input.col.rgb * attentuation * _LightColor0.rgb;

				float3 spec;
				if (dotNormalLight < 0.f)
				{
					spec = fixed3(0.f,0.f,0.f);
				}
				else
				{
					float3 r = reflect(-l, n);
					//float3 v = normalize(_WorldSpaceCameraPos.xyz - input.posWorld.xyz);
					float3 v = fixed3(0.f, 0.f, -1.f);
				
					spec = pow(saturate(dot(v,r)), _SpecIndex) * _LightColor0.rgb * attentuation * input.col.rgb;
				}
				
				return fixed4(diff * _DiffuseFactor + spec * _SpecularFactor, tex.a);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
