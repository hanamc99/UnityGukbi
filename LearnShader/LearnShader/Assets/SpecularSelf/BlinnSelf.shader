Shader "Custom/BlinnSelf"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Inten("Intensity", float) = 0.5
        _Width("Width", float) = 40
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _BlinnSelf
        #pragma target 3.0

        sampler2D _MainTex;
        float _Inten;
        float _Width;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = 0;
            o.Alpha = 1;
            //o.Albedo = c.rgb;
            //o.Alpha = c.a;
        }

        float4 Lighting_BlinnSelf(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            //Blinn-Phong °ø½Ä
            float3 speColor;
            float3 h = normalize(lightDir + viewDir);
            float ndoth = saturate(dot(s.Normal, h));
            speColor = pow(ndoth, _Width);

            final.rgb = (speColor.rgb * _Inten) + s.Albedo;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
