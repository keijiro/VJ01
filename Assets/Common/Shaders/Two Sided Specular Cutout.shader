Shader "Custom/Two Sided Specular Cutout"
{
	Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
        _Gloss ("Gloss", Range (0.0, 2.0)) = 1
        _MainTex ("Base Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range (0, 1)) = 0.5
    }
	SubShader
    {
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutoutTwoSided" }
        Cull Off
		
		CGPROGRAM

		#pragma surface surf BlinnPhong alphatest:_Cutoff

        half4 _Color;
        half _Shininess;
        half _Gloss;
		sampler2D _MainTex;

		struct Input
        {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o)
        {
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
            o.Specular = _Shininess;
            o.Gloss = _Gloss;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Transparent/Cutout/Diffuse"
}
