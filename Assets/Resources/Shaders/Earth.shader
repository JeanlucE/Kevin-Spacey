Shader "Custom/Earth" {

Properties {
	_Cloud ("Cloudmap", 2D) = "white" {}
	_CloudAlpha ("CloudAlpha", Float) = 0.5
	_Cube ("Cubemap", CUBE) = "" {}
	_Fog ("Fog", Color) = (1,1,1,1)
	_FogAmp ("Fog Amplifier", Float) = 1
	_Tint ("Tint", Color) = (1,1,1,1)
	_FogExp ("Fog Exponent", Float) = 1
}

SubShader { 
	Tags { "Queue" = "Background+1"} 

	Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back
	
		CGPROGRAM
		struct v2f {
			float4 position_clip: SV_POSITION;
			float3 position_local: TEXCOORD0;
			float2 uv : TEXCOORD1;
			float3 nor : NORMAL;
		};

		#pragma vertex main_vertex
		v2f main_vertex(float4 position: position, float2 uv : TEXCOORD0, float3 nor : NORMAL) {
			v2f v2f;
			v2f.position_clip = mul(UNITY_MATRIX_MVP, position);
			v2f.position_local = position;
			v2f.uv = uv;
			v2f.nor = normalize(mul(float4(nor, 0.0), _World2Object).xyz);
			return v2f;
		}
		
		#pragma fragment main_fragment
		samplerCUBE _Cube;
		sampler2D _Cloud;
		fixed4 _Cloud_ST;
		fixed _CloudAlpha;
		fixed4 _Fog;
		fixed _FogAmp;
		fixed4 _Tint;
		fixed _FogExp;
		float4 _CamDir;
			
		half4 main_fragment(v2f i): COLOR {
			float p = pow(saturate(dot(normalize(_CamDir).xyz, normalize(i.nor)) * _FogAmp), _FogExp);
			return fixed4(p * (texCUBE(_Cube, i.position_local).xyz + _CloudAlpha * tex2D(_Cloud, i.uv * _Cloud_ST.xy + _Cloud_ST.zw).xyz) + (1-p) * _Fog.xyz, 1.f) * _Tint;
		}
		ENDCG
	} 
}

}