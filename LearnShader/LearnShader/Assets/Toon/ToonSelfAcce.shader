Shader "Custom/ToonSelfAcce"
{
    Properties
    {
        _MainTex("Albedo", 2D) = "white" {}
        _NormalMap("NormalMap", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        cull front
        CGPROGRAM
        #pragma surface surf _NoLight vertex:MyVert

        sampler2D _MainTex;

        void MyVert(inout appdata_full v){
            //v.vertex.xyz += v.normal.xyz * 0.013;
            v.vertex.xyz += v.normal.xyz * 0.013 * v.color.g;
        }

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = 0;
            o.Alpha = 1;
        }

        float4 Lighting_NoLight(SurfaceOutput s, float3 lightDir, float atten){
            return 0;
        }
        ENDCG

        cull back
        CGPROGRAM
        #pragma surface surf _MyLight

        sampler2D _MainTex;
        sampler2D _NormalMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 color:COLOR;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 n = tex2D(_NormalMap, IN.uv_NormalMap);
            o.Normal = UnpackNormal(n);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Gloss = IN.color.r;
        }

        float4 Lighting_MyLight(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten){
            float4 final;

            //≤˜æÓ¡ˆ¥¬ ¿Ωøµ
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            //float ndotl = dot(s.Normal, lightDir);
            
            if(ndotl > 0.7){
                ndotl = 1;
            } else if(ndotl > 0.5){
                ndotl = 0.7;
            } else{
                ndotl = 0.3;
            }

            //ndotl *= 5;
            //ndotl = ceil(ndotl) / 5;
            
            //«¡∑π≥⁄ ø‹∞˚º±
            /*float rim = abs(dot(s.Normal, viewDir));
            if(rim > 0.5){
                rim = 1;
            } else{
                rim = -0.5;
            }*/

            //Ω∫∆‰≈ß∂Û
            float3 h = normalize(lightDir + viewDir);
            float ndoth = dot(h, s.Normal);
            ndoth = pow(ndoth, 250);
            float3 speColor = smoothstep(0.005, 0.01, ndoth);


            final.rgb = (s.Albedo * ndotl) + (speColor * s.Gloss);
            //final.rgb = s.Albedo * ndotl;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
