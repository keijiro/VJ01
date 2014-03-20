Shader "Stencil/Stencil Increment No Color Alpha Test"
{
	Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _Cutoff ("Cutoff", Range (0, 1)) = 0.5
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    struct v2f
    {
        float4 position : SV_POSITION;
        float2 texcoord : TEXCOORD0;
    };

    sampler2D _MainTex;
    float4 _MainTex_ST;
    fixed _Cutoff;

    v2f vert (appdata_base v)
    {
        v2f o;
        o.position = mul (UNITY_MATRIX_MVP, v.vertex);
        o.texcoord = TRANSFORM_TEX (v.texcoord, _MainTex);
        return o;
    }

    fixed4 frag (v2f i) : COLOR
    {
        fixed4 tc = tex2D (_MainTex, i.texcoord);
        clip (tc.a - _Cutoff);
        return fixed4 (0, 0, 0, 0);
    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="TransparentCutoutWithoutDepthNormal" "Queue"="Geometry+100" }
        Pass
        {
            ColorMask 0
            Stencil { Pass IncrSat }
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    } 
}
