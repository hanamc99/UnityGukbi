Shader "Custom/BlinnPhong"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SpecCol ("Specular Color", Color) = (1, 1, 1, 1)
        _GlossTex ("Gloss Tex", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _Lambert noambient

        sampler2D _MainTex;
        sampler2D _GlossTex;
        float4 _SpecCol;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_GlossTex;
        };

        struct SurfaceOutputCustom{
            fixed3 Albedo;
            fixed3 Normal;
            fixed3 Emission;
            half Specular;
            half Gloss;
            fixed Alpha;
            float CustomData;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 d = tex2D(_GlossTex, IN.uv_GlossTex);
            o.Albedo = c.rgb;
            o.Gloss = d.a;
            o.Alpha = c.a;
        }

        float4 Lighting_Lambert(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            //diffuse
            float3 diffuseColor;
            float nDotl = saturate(dot(s.Normal, lightDir));
            diffuseColor = nDotl * s.Albedo * _LightColor0.rgb * atten;
            
            //spec
            float3 h = normalize(lightDir + viewDir);
            float spec = saturate(dot(h, s.Normal));
            spec = pow(spec, 100);
            float3 specColor = spec * _SpecCol.rgb * s.Gloss;

            //rim
            float3 rimColor;
            float rim = abs(dot(viewDir, s.Normal));
            float invertRim = 1 - rim;
            rimColor = pow(invertRim, 6) * float3(0.5, 0.5, 0.5);

            //fake spec
            float3 fakeSpecColor;
            fakeSpecColor = pow(rim, 50) * float3(0.2, 0.2, 0.2) * s.Gloss;

            //final
            final.rgb = diffuseColor.rgb + specColor + rimColor + fakeSpecColor;
            final.a = s.Alpha;
            return pow(rim, 200);
            //return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
