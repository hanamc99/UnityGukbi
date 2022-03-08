Shader "Custom/Barb"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RealMainTex ("Real", 2D) = "white" {}
        _BumpMap ("NormalMap", 2D) = "bump"{}
        _RampMap ("RampMap", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        cull front
        CGPROGRAM
        #pragma surface surf _NoLight vertex:vert

        sampler2D _MainTex;

        void vert(inout appdata_full v){
            v.vertex.xyz += v.normal.xyz * 0.0075 * v.color.g;
        }

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 m = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = 0;
            o.Alpha = 1;
        }

        float4 Lighting_NoLight(SurfaceOutput s, float3 lightDir, float atten){
            return float4(0, 0, 0, 1);
        }
        ENDCG

        cull back
        CGPROGRAM
        
        #pragma surface surf _Barb
        #pragma target 3.0

        sampler2D _RealMainTex;
        sampler2D _BumpMap;
        sampler2D _RampMap;

        struct Input{
            float2 uv_RealMainTex;
            float2 uv_BumpMap;
            float4 color:COLOR;
        };

        void surf(Input IN, inout SurfaceOutput m){
            fixed4 c = tex2D(_RealMainTex, IN.uv_RealMainTex);
            fixed4 n = tex2D(_BumpMap, IN.uv_BumpMap);
            m.Albedo = c.rgb;
            m.Gloss = IN.color.r;
            m.Normal = UnpackNormal(n);
            m.Alpha = c.a;
        }

        float4 Lighting_Barb(SurfaceOutput t, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            float ndotl = dot(t.Normal, lightDir) * 0.5 + 0.5;
            /*if(ndotl > 0.9){
                ndotl = 1;
            } else if(ndotl >= 0.7){
                ndotl = 0.7;
            } else if(ndotl >= 0.4){
                ndotl = 0.4;
            } else{
                ndotl = 0.3;
            }*/
            ndotl = ndotl * 5;
            ndotl = ceil(ndotl) / 5;

            fixed4 ramp = tex2D(_RampMap, float2(ndotl, 0.5));

            float3 rimColor;
            float rim = abs(dot(t.Normal, viewDir));
            float invertRim = 1 - rim;
            rimColor = pow(invertRim, 5) * 0.3 * t.Gloss;

            float3 fakeSpecColor;
            fakeSpecColor = pow(rim, 100) * t.Gloss;

            final.rgb = ((t.Albedo.rgb * ndotl) + rimColor + fakeSpecColor) * 3;
            final.a = t.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
