Shader "Custom/Blend 2 Textures" { 
 
	Properties {
		_Blend ("Blend", Range (0, 1) ) = 0.5 
		_MainTex ("Texture 1", 2D) = "" 
		_Texture2 ("Texture 2", 2D) = ""
	}
	 
	SubShader {	
		Tags { "RenderType"="Opaque"}
//		Pass {
//			SetTexture[_MainTex]
//			SetTexture[_Texture2] { 
//				ConstantColor (0,0,0, [_Blend]) 
//				Combine texture Lerp(constant) previous
//			}
//			
		CGPROGRAM
			
			#pragma surface surf Lambert
			
			struct Input {
				float2 uv_MainTex;
				float2 uv_Texture2;
			};
			
			sampler2D _MainTex;
			sampler2D _Texture2;
			float _Blend;
			
			void surf (Input IN, inout SurfaceOutput o){
				o.Albedo = tex2D(_MainTex,IN.uv_MainTex).rgb*_Blend+tex2D(_Texture2,IN.uv_Texture2).rgb*(1-_Blend);
			}
		ENDCG	
				
	}
	FallBack "Diffuse" 
 
}