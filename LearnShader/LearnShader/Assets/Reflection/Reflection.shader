Shader "Custom/Reflection"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Cube ("CubeMap", Cube) = "" {}
        _BumpMap("Normal", 2D) = "bump" {}
        _MaskMap("Mask", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert noambient
        #pragma target 3.0

        sampler2D _BumpMap; 
        sampler2D _MainTex;
        samplerCUBE _Cube;
        sampler2D _MaskMap;

        struct Input
        {
            float2 uv_MaskMap;
            float2 uv_BumpMap;
            float2 uv_MainTex;
            float3 worldNormal;
            float3 worldRefl; INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 m = tex2D(_MaskMap, IN.uv_MaskMap);
            fixed4 n = tex2D(_BumpMap, IN.uv_BumpMap);
            float3 normal = UnpackNormal(n);
            o.Normal = normal;

            float3 worldRefl = WorldReflectionVector(IN, o.Normal);
            float3 worldNormal = WorldNormalVector(IN, o.Normal);

            float4 refl = texCUBE(_Cube, worldRefl);

            o.Albedo = c.rgb * (1 - m.r);
            o.Emission = refl.rgb * m.r;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
