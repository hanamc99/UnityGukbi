Shader "Custom/Holo"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        float4 _RimColor;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            //o.Albedo = 1;
            o.Emission = _RimColor.rgb;
            float holoLine = pow(frac(IN.worldPos.y * 3 - _Time.y * 2), 1);
            //o.Emission = holoLine;
            float ndotv = saturate(dot(o.Normal, IN.viewDir));
            ndotv = pow(1 - ndotv, 2);
            //o.Alpha = 1;
            o.Alpha = (ndotv * holoLine) * pow(abs(sin(_Time.y)), 1);
            //o.Alpha = (ndotv * pow(abs(sin(_Time.y)), 1)) + holoLine;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
