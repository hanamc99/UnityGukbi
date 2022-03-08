Shader "Custom/Warped"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RampTex ("RampMap", 2D) = "white" {}
        _BumpTex ("NormalMap", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _Warp noambient
        #pragma target 3.0

        sampler2D _BumpTex;
        sampler2D _MainTex;
        sampler2D _RampTex;

        struct Input
        {
            float2 uv_BumpTex;
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 n = tex2D(_BumpTex, IN.uv_BumpTex);
            o.Normal = UnpackNormal(n);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        float4 Lighting_Warp(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            ndotl = ndotl * 10;
            ndotl = ceil(ndotl) / 10;

            float3 h = normalize(lightDir + viewDir);
            float ndoth = saturate(dot(s.Normal, h));

            fixed4 r = tex2D(_RampTex, float2(ndotl, ndoth));

            final.rgb = s.Albedo * r.rgb;
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
