Shader "Custom/PhongSelf"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _PhongSelf
        #pragma target 3.0

        sampler2D _MainTex;

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

        float4 Lighting_PhongSelf(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            //Phong °ø½Ä
            float3 speColor;
            float3 reflectDir = reflect(-lightDir, s.Normal);
            float rdotv = dot(reflectDir, viewDir);
            speColor = pow(rdotv, 10);

            final.rgb = speColor.rgb + s.Albedo;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
