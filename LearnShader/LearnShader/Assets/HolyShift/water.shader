Shader "Custom/water"
{
    Properties
    {
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Cube("Cube", Cube) = ""{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        #pragma target 3.0

        sampler2D _BumpMap;
        samplerCUBE _Cube;

        struct Input
        {
            float3 viewDir;
            float2 uv_BumpMap;
            float3 worldRefl;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            float3 refColor = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal));
            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, 1.5);
            o.Emission = rim * refColor * 5;
            o.Alpha = rim + 0.3;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
