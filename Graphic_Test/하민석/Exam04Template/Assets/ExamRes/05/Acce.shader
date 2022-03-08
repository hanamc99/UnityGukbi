Shader "Custom/Acce"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex("BumpTex", 2D) = "bump"{}
        _AxeTex ("Axe Albedo (RGB)", 2D) = "white" {}
        _AxeBumpTex("Axe BumpTex", 2D) = "bump"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf _Acce
        #pragma target 3.0
        sampler2D _MainTex;
        sampler2D _AxeTex;
        sampler2D _BumpTex;
        sampler2D _AxeBumpTex;
        struct Input
        {
            float2 uv_BumpTex;
            float2 uv_MainTex;
            float2 uv_AxeTex;
            float2 uv_AxeBumpTex;
            float4 color:COLOR;
        };
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 d = tex2D(_AxeTex, IN.uv_AxeTex);
            fixed4 b = tex2D(_BumpTex, IN.uv_BumpTex);
            fixed4 e = tex2D(_AxeBumpTex, IN.uv_AxeBumpTex);
            float3 normal = UnpackNormal(b); 
            float3 axeNormal = UnpackNormal(e);
            o.Normal = lerp(normal, axeNormal, IN.color.r);
            o.Gloss = IN.color.g;
            o.Albedo = lerp(c, d, IN.color.r);
            o.Alpha = c.a;
        }
        float4 Lighting_Acce(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            ndotl *= 5;
            ndotl = ceil(ndotl) / 5;
            float h = normalize(lightDir + viewDir);
            float ndoth = dot(s.Normal, h) * 0.5 + 0.5;
            ndoth = pow(ndoth, 2);
            final.rgb = s.Albedo * ndotl * 1.2 + (ndoth * s.Gloss);
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
