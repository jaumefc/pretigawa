Shader "Custom/CartoonShader2" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BrightColor("Bright Color", Color) = (1,0,0)
		_Threshold1("Threshold Bright to Dark", range(0,1)) = 0.2
		_DarkColor("Dark Color", Color) = (0,1,0)
		_Threshold2("Threshold Middle to Dark", range(0,1)) = 0.9
		_TransitionTexture("Transition Texture", 2D) = "white" {}
		_TransitionTextureSize("Transition Texture Size", range(0.1, 50)) = 1
		_BrightTexture("Bright Color Texture", 2D) = "white"{}
		_BrightTextureSize("Bright Texture Size", range(0.1,50)) = 1
		_BrightTextureIntensity("Bright Texture Intensity", range(0.0,1)) = 0.5
		_DarkTexture("Dark Color Texture", 2D) = "white"{}
		_DarkTextureSize("Dark Texture Size", range(0.1,50)) = 1
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
		half _TransitionTextureSize;
		half _DarkTextureSize;
		half _BrightTextureSize;
		half _BrightTextureIntensity;
		sampler2D _BrightTexture;
		sampler2D _DarkTexture;
		
		struct Input {
			float2 uv_MainTex;
		};
		
		struct SurfaceOutputCustom {
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
			//Normalitza el vector dir (direccio de la llum)
			dir = normalize(dir);
			//Calcula el producte entre la normal de la superficie i la direccio de la llum.Saturate fa q tots els valors estiguin entre 0 i 1.
			half NdotL = saturate( dot (s.Normal, dir));
			
			half4 darkColor = _DarkColor * tex2D(_DarkTexture, s.UV * _DarkTextureSize);
			half4 brightColor = _BrightColor * ( tex2D(_BrightTexture, s.UV * _BrightTextureSize) * _BrightTextureIntensity + (1- _BrightTextureIntensity));
			half3 ShadowColor = NdotL < _Threshold1 ? darkColor : NdotL < _Threshold2 ? lerp(darkColor, brightColor, tex2D(_TransitionTexture, s.UV * _TransitionTextureSize)) : brightColor;
			
			half4 c;
			c.rgb = ((NdotL * 0.4f)+0.6f) * s.Albedo * _LightColor0 * ShadowColor;
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
