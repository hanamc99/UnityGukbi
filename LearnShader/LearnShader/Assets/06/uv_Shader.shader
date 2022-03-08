Shader "Custom/uv_Shader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MoveX ("MoveX", Range(-1, 1)) = 0
        _MoveY ("MoveY", Range(-1, 1)) = 0
        _Speed ("Speed", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _MainTex;
        float _MoveX;
        float _MoveY;
        float _Speed;

        struct Input
        {
            float2 uv_MainTex;
        };
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //텍스쳐의 좌표를 이동시킴.
            IN.uv_MainTex.x += _MoveX;
            IN.uv_MainTex.y += _MoveY;

            //텍스쳐에서 어느 좌표의 칼라값을 가져올건지 정함
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            //fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x, _MoveY));
            fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + (_Time.y * _Speed)));
            o.Emission = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
