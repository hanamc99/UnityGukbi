//****************************************************

//이름: 하민석

//과목: 게임 그래픽 프로그래밍

//날짜: 2022-03-08

//****************************************************

Shader "Custom/Robot2Pass"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        cull front
        CGPROGRAM
        #pragma surface surf _NoLight vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;

        void vert(inout appdata_full v){
            v.vertex.xyz += v.normal.xyz * 0.007;
        }

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = 0;
            o.Alpha = 1;
        }

        float4 Lighting_NoLight(SurfaceOutput s, float3 lightDir, float atten){
            return float4(0,0,0,1);
        }
        ENDCG

        cull back
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
