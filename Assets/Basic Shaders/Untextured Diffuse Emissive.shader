Shader "Custom/Untextured Diffuse Emissive"
{
    Properties
    {
        _Color ("Color", Color) = (0.5, 0.5, 0.5, 1)
        _Emission ("Emission", Color) = (0.5, 0.5, 0.5, 0)
    }

    CGINCLUDE

    half4 _Color;
    half4 _Emission;

    struct Input
    {
        float dummy;
    };

    void surf (Input IN, inout SurfaceOutput o)
    {
        o.Albedo = _Color.rgb;
        o.Emission = _Emission.rgb;
        o.Alpha = _Color.a;
    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert
        ENDCG
    } 
    FallBack "Diffuse"
}
