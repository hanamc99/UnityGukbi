//****************************************************

//이름: 하민석

//과목: 게임 그래픽 프로그래밍

//날짜: 2022-03-08

//****************************************************

Shader "Custom/Robot1Pass"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _1PassOutline
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

        float4 Lighting_1PassOutline(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            ndotl = pow(ndotl, 3);

            float ndotv = saturate(dot(s.Normal, viewDir));
            if(ndotv > 0.4){
                ndotv = 1;
            } else{
                ndotv = -1;
            }

            final.rgb = ndotl * s.Albedo * ndotv;
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
