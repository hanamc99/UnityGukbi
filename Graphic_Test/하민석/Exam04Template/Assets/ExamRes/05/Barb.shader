//****************************************************

//이름: 하민석

//과목: 게임 그래픽 프로그래밍

//날짜: 2022-03-08

//****************************************************

Shader "Custom/Barb"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RampTex("RampTex", 2D) = "white"{}
        _BumpTex("BumpTex", 2D) = "bump"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _Barb
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpTex;
        sampler2D _RampTex;

        struct Input
        {
            float2 uv_BumpTex;
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 b = tex2D(_BumpTex, IN.uv_BumpTex);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal(b);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        float4 Lighting_Barb(SurfaceOutput s, float3 lightDir, float atten){
            float4 final;

            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            ndotl *= 5;
            ndotl = ceil(ndotl) / 5;

            fixed4 r = tex2D(_RampTex, float2(ndotl, 0.5));

            final.rgb = (s.Albedo * r.rgb) * 1.2;
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
