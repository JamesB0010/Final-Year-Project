Shader "Unlit/DirectionArrow_Transparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _Alignment ("Alignment", Float) = 0
        
        _NotAlignedColor ("not aligned color", Color) = (0,0,0,0)
        
        _AlignedColor ("Aligned COlor", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
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
            float _Alignment;
            float4 _NotAlignedColor;
            float4 _AlignedColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                
                // Ensure transparency is respected
                clip(col.a - 0.01);

                col *= lerp(_NotAlignedColor, _AlignedColor, _Alignment);
                
                return saturate(col);
            }
            ENDHLSL
        }
    }
}