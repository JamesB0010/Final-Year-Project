Shader "Unlit/RippleWater"
{
    Properties
    {
        _Frequency ("Frequency", Float) = 10
        _Speed ("Speed", Float) = 0
        _RippleWidth ("Ripple Width", Float) = 0
        _RippleHeight ("Ripple Height", Float) = 0
        _FalloffExponent ("Fallof Exponent", Float) = 0
        _Color ("Color", Color) = (1,1,1,1)
        _RippleColor ("Ripple Color", Color) = (1,1,1,1)
        _RippleColorStrength ("Ripple Color Strength", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
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

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float rippleValue : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Frequency;
            float _Speed;
            float _RippleWidth;
            float2 _RippleOffset[20];
            int _rippleCount;
            float _RippleHeight;
            float _FalloffExponent;
            float4 _Color;
            float4 _RippleColor;
            float _RippleColorStrength;



            Interpolators vert(appdata v)
            {
                Interpolators o;
                o.worldPos = TransformObjectToWorld(v.vertex);
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = v.uv;
                float highestRippleStrength = 0;
                for(int i = 0; i < _rippleCount; ++i)
                {
                    float2 offsettedUvs = o.uv + -.5f + _RippleOffset[i];

                    float distanceFromCenter = length(offsettedUvs) * _RippleWidth;

                    float rippleStrength = pow(saturate(1 - distanceFromCenter), _FalloffExponent);

                    if(rippleStrength > highestRippleStrength)
                    {
                        highestRippleStrength = rippleStrength;

                    o.rippleValue = sin(distanceFromCenter * _Frequency - _Time.y * _Speed);

                    o.rippleValue *= pow(saturate(1 - distanceFromCenter), _FalloffExponent);

                    o.vertex.y += _RippleHeight * o.rippleValue;
                    }

                }
                return o;
            }

            float4 frag(Interpolators i) : SV_Target
            {
                return saturate(lerp(_Color, _RippleColor, i.rippleValue * _RippleColorStrength));
            }
            ENDHLSL
        }
    }
}