//****************************************************

//이름: 하민석

//과목: 게임 그래픽 프로그래밍

//날짜: 2022-03-08

//****************************************************

﻿

Shader "Custom/Fire"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EfecTex ("Effect Tex", 2D) = "white" {}
        _Speed("Speed", float) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" ="Transparent" }

        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _EfecTex;
        float _Speed;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_EfecTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 e = tex2D(_EfecTex, IN.uv_EfecTex - float2(0, _Time.y * _Speed));
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + (e / 20));
            o.Albedo = c.rgb;
            o.Alpha = c.a * (e.r * 20);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
