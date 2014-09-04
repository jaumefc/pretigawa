Shader "Custom/NewShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BrightColor("Bright Color", Color) = (1,0,0)
		_Threshold1("Threshold Bright to Dark", range(0,1)) = 0.2
		_DarkColor("Dark Color", Color) = (0,1,0)
		_Threshold2("Threshold Middle to Dark", range(0,1)) = 0.9
		_TransitionTexture("Transition Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
		#pragma surface surf Cartoon
		sampler2D _MainTex;
		sampler2D _TransitionTexture;
		half4 _BrightColor;
		half4 _DarkColor;
		half _Threshold1;
		half _Threshold2;
		
		struct Input {
			float2 uv_MainTex;
		};
		
		struct SurfaceOutputCustom{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
			fixed viewFallof;
			half2 UV;
		};
		
		half4 LightingCartoon(SurfaceOutputCustom s, half3 dir, half attend){
			dir = normalize(dir);
			half NdotL = dot (-s.Normal, dir);
			
			//half3 ShadowColor = NdotL < _Threshold1 ? _DarkColor : NdotL < _Threshold2 ? lerp(_DarkColor, _BrightColor, tex2D(_TransitionTexture, s.UV)) : _BrightColor;
			half4 c;
			c.rgb = ((NdotL * 0.4f)+0.6f) * s.Albedo * _LightColor0;
			c.a = s.Alpha;
			return c;
		}
		
		void surf (Input IN, inout SurfaceOutputCustom o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.UV = IN.uv_MainTex;
			o.Albedo = c;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
