Shader "Custom/customLight"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("normalMap", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _MyLambert// noambient
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 n = tex2D(_BumpMap, IN.uv_BumpMap);
            o.Albedo = c.rgb;
            float3 normal = UnpackNormal(n);
            o.Normal = normal;
            o.Alpha = c.a;
        }
        
        float4 Lighting_MyLambert(SurfaceOutput s, float3 LightDir, float atten){
            float nDot = dot(s.Normal, LightDir);
            //return nDot;
            float n = pow(nDot * 0.5f + 0.5f, 3);
            
            float4 final;
            final.rgb = n * s.Albedo * _LightColor0.rgb * atten;
            final.a = s.Alpha;
            return final;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
