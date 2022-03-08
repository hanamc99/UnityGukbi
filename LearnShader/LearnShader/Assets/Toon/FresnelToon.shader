Shader "Custom/FresnelToon"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf _Toon

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        float4 Lighting_Toon(inout SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            if(ndotl > 0.75){
                ndotl = 1;
            }else if(ndotl > 0.39){
                ndotl = 0.4;
            } else {
                ndotl = 0;
            }

            float ndotv = abs(dot(s.Normal, viewDir));
            if(ndotv > 0.3){
                ndotv = 1;
            } else{
                ndotv = -1;
            }
            
            final.rgb = ndotl * ndotv * s.Albedo;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
