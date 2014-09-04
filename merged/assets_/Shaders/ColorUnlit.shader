Shader "Custom/ColorUnlit" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1)
	}
	SubShader {
//		Color[_Color]
//		Pass { }
		Tags{"RenderType" = "Opaque"}
		CGPROGRAM
			#pragma surface surf Lambert
			struct Input {
				float2 uv_MainTex;
			};
			
			float3 _Color;
			
			void surf (Input IN, inout SurfaceOutput o){
				o.Albedo = _Color.rgb;
			}
		ENDCG

	}
	FallBack "Diffuse"
}
