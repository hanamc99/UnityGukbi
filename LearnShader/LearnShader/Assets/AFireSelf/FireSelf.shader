Shader "Custom/FireSelf"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EfecTex("Effect", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _EfecTex;

        struct Input
        {
            float2 uv_EfecTex;
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 e = tex2D(_EfecTex, (IN.uv_EfecTex * float2(1, 1.1)) + float2(0, 1 - _Time.y));
            fixed4 c = tex2D (_MainTex, (IN.uv_MainTex * float2(1, 1.1)) + (e.r * 0.1) - float2(0, 0.1));
            o.Albedo = c.rgb;
            o.Alpha = c.a * e.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
