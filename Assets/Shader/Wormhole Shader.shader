Shader "Unlit/Wormhole"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float) = 1.0
        _Strength ("Strength", Float) = 1.0
    }
 
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert
 
        sampler2D _MainTex;
        float _Speed;
        float _Strength;
 
        struct Input
        {
            float2 uv_MainTex;
        };
 
        void surf(Input IN, inout SurfaceOutput o)
        {
            float2 warp = sin(IN.uv_MainTex * _Strength + _Time.y * _Speed) * _Strength;
            float2 uv = IN.uv_MainTex + warp;
            fixed4 c = tex2D(_MainTex, uv);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
 
    Fallback "Diffuse"
}
