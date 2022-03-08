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

        struct Input //x,y ������ ���ε� �ؽ��ĸ� �޾ƿ´�.
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex.x + _Time.y, IN.uv_MainTex2.y + _Time.y));
            fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2);
            //���⼭ ���ϴ� _Time�� ������. �ؽ����� ��ġ�� �̵����Ѽ� Repeat�ǰ� �Ѵ�.
            
            d.r *= 100;

            //�������� (1, 0, 0)�̹Ƿ� ����κ��� ������Ҵ� 1�̴�.
            //������ ������ ������ ������ �������� ����.
            //fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x - d.x, IN.uv_MainTex.y - d.x)); //float��, float2 / d.r���� ��� �ٲ��.
            //fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y + (_Time.y * _Speed)));
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + d);// float2 - float2
            o.Emission = c.rgb;
            //o.Emission = c.rgb * d.rgb; //��ο���
            //o.Alpha = d.a * c.a;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
