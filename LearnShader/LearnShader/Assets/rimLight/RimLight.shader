Shader "Custom/RimLight"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimColor ("Rim color", Color) = (1, 1, 1, 1)
        _RimPow ("Rim Power", Range(1, 10)) = 3
        _BumpMap ("Bump Map", 2D) = "Bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        //#pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float4 _RimColor;
        float _RimPow;

        struct Input
        {
            float2 uv_BumpMap;
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 n = tex2D (_BumpMap, IN.uv_BumpMap);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            float3 normal = UnpackNormal(n);
            o.Normal = normal;
            o.Albedo = c.rgb;
            float rim = saturate(dot(o.Normal, IN.viewDir));
            o.Emission = pow(1 - rim, _RimPow) * _RimColor.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
