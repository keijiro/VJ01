Shader "Custom/Diffuse Cutout In Geometry Queue"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _MainTex ("Base Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range (0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" }
        
        CGPROGRAM

        #pragma surface surf Lambert alphatest:_Cutoff

        fixed4 _Color;
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        ENDCG
    } 
    Fallback "Transparent/Cutout/Diffuse"
}
