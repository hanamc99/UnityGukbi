Shader "Custom/Toon2"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("NormalMap (RGB)", 2D) = "bump" {}
        _Thickness ("Border Thick", float) = 0.01
        _BorderTex ("Border Tex", 2D) = "white" {}
        _RampTex ("Ramp Tex", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        cull front

        CGPROGRAM
        #pragma surface surf _NoLight vertex:vert
        #pragma target 3.0

        float _Thickness;
        sampler2D _BorderTex;

        void vert(inout appdata_full v){
            v.vertex.xyz += v.normal.xyz * v.color.r * 0.1;
            //v.vertex.xyz += v.normal.xyz * _Thickness;
        }

        struct Input
        {
            float2 uv_BorderTex;
        };

        void surf(Input IN, inout SurfaceOutput o){
            fixed4 b = tex2D(_BorderTex, IN.uv_BorderTex + _Time.y);
            o.Albedo = b.rgb;
            o.Alpha = b.a;
        }

        float4 Lighting_NoLight(SurfaceOutput s, float3 lightDir, float atten){
            float4 final;
            final.rgb = s.Albedo;
            final.a = s.Alpha;
            return final;
        }
        ENDCG

        cull back
        CGPROGRAM

        #pragma surface surf _Toon

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _RampTex;

        struct Input
        {
            float4 color:COLOR;
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 d = tex2D(_BumpMap, IN.uv_BumpMap);
            float3 normal = UnpackNormal(d);
            o.Normal = normal;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        float4 Lighting_Toon (SurfaceOutput s, float3 lightDir, float atten){
            float4 final;

            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            if(ndotl > 0.75){
                ndotl = 1;
            }else if(ndotl > 0.39){
                ndotl = 0.4;
            } else {
                ndotl = 0;
            }
            //ndotl = ndotl * 5;
            //ndotl = ceil(ndotl) / 5;

            final.rgb = ndotl * s.Albedo;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
