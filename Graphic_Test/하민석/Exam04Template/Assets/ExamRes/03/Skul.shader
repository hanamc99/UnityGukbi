//****************************************************

//이름: 하민석

//과목: 게임 그래픽 프로그래밍

//날짜: 2022-03-08

//****************************************************

Shader "Custom/Skul"
{
    Properties
    {
        _RimColor ("Rim Color", Color) = (0, 0, 0, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        

        zwrite on
        ColorMask 0
        CGPROGRAM
        #pragma surface surf _NoLight noambient
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
        }

        float4 Lighting_NoLight(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            return float4(0,0,0,0);
        }
        ENDCG

        zwrite off
        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        float4 _RimColor;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);


            float ndotv = saturate(dot(o.Normal, IN.viewDir));
            ndotv = pow(1 - ndotv, 3);

            o.Emission = _RimColor;
            o.Alpha = ndotv;
        }
        ENDCG
    }
    FallBack "Transparent"
}
