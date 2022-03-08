Shader "Custom/holo"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}

        CGPROGRAM
        #pragma surface surf _NoLight alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = 0;
            float holo = pow(frac(IN.worldPos.g * 2.5 - _Time.y), 30);
            o.Emission = holo;
            //o.Emission = float4(1, 0.5, 0.8, 1);
            float rim = saturate(dot(o.Normal, IN.viewDir));
            o.Alpha = pow(1 - rim, 3) + holo;
            //o.Alpha = pow(1 - rim, 3) * sin(_Time.y * 0.5 + 0.5);
        }

        float4 Lighting_NoLight(SurfaceOutput s, float3 lightDir, float atten){
            return float4(0, 0, 0, s.Alpha);
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
