Shader "Custom/Fire"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo (RGB)", 2D) = "white" {}
        _Speed ("Speed", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _MainTex;
        sampler2D _MainTex2;
        float _Speed;

        struct Input //x,y 값으로 매핑된 텍스쳐를 받아온다.
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex.x + _Time.y, IN.uv_MainTex2.y + _Time.y));
            fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2);
            //여기서 더하는 _Time은 순수값. 텍스쳐의 위치를 이동시켜서 Repeat되게 한다.
            
            d.r *= 100;

            //빨강색은 (1, 0, 0)이므로 흰색부분의 빨강요소는 1이다.
            //색깔을 일종의 굴곡진 고지대 기준으로 보자.
            //fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x - d.x, IN.uv_MainTex.y - d.x)); //float값, float2 / d.r값은 계속 바뀐다.
            //fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y + (_Time.y * _Speed)));
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + d);// float2 - float2
            o.Emission = c.rgb;
            //o.Emission = c.rgb * d.rgb; //어두워짐
            //o.Alpha = d.a * c.a;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
