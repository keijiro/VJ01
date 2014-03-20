Shader "Stencil/Stenciled Untextured Diffuse"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    CGINCLUDE

    half4 _Color;

    struct Input
    {
        float dummy;
    };

    void surf (Input IN, inout SurfaceOutput o)
    {
        o.Albedo = _Color.rgb;
        o.Alpha = _Color.a;
    }

    ENDCG

    // Flipped variant
    SubShader
    {
        LOD 1000
        Tags { "RenderType"="TransparentButHasDepth" "Queue"="Geometry+102" }
        Stencil { Ref 0 Comp Equal }
        CGPROGRAM
        #pragma surface surf Lambert
        ENDCG
    } 

    // Normal variant
    SubShader
    {
        LOD 0
        Tags { "Queue"="Geometry+102" }
        Stencil { Ref 0 Comp Less }
        CGPROGRAM
        #pragma surface surf Lambert
        ENDCG
    } 
}
