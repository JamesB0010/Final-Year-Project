Shader "Unlit/CrtScreen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Curvature ("Curvature", Float) = 0
        _VignetteWidth ("VignetteWidth", Float) = 0
        [HDR] _Color ("Color", Color) = (1,1,1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Curvature;
            float _VignetteWidth;
            float4 _Color;

            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            //credit acerola https://github.com/GarrettGunnell/CRT-Shader/blob/main/Assets/CRT.shader
            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * 2.0f - 1.0f;
                float2 offset = uv.yx / _Curvature;
                uv = uv + uv * offset * offset;
                uv = uv * 0.5f + 0.5f;

                float4 col = tex2D(_MainTex, uv);
                if (uv.x <= 0.0f || 1.0f <= uv.x || uv.y <= 0.0f || 1.0f <= uv.y)
                    col = 0;

                uv = uv * 2.0f - 1.0f;
                float2 vignette = _VignetteWidth / _ScreenParams.xy;
                vignette = smoothstep(0.0f, vignette, 1.0f - abs(uv));
                vignette = saturate(vignette);

                col.g *= (sin((i.uv.y * _ScreenParams.y * 2.0f) * _Time * 10) + 1.0f) * 0.15f + 1.0f;
                col.rb *= (cos((i.uv.y * _ScreenParams.y * 2.0f)* _Time * 10) + 1.0f) * 0.135f + 1.0f; 

                return (saturate(col) * vignette.x * vignette.y ) * _Color;
            }
            ENDHLSL
        }
    }
}
