Shader "Custom/WaveformCenterLineVisualizer" {
    Properties {
        _WaveformTex ("Waveform Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (1,0,0,1)
        _LineHeight ("Line Height", Float) = 1
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }
    SubShader {
        Tags { "Queue"="Transparent" }

        // Unity UI often uses premultiply alpha blending.
        Blend SrcAlpha OneMinusSrcAlpha 
        ZWrite Off
        Cull Off
        Fog { Mode Off }

    
        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }
            ColorMask[_ColorMask]
     

            
    Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _WaveformTex;
            float4 _LineColor;
            float _LineHeight;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float amplitude = tex2D(_WaveformTex, float2(i.uv.x, 0.5)).r;
                float waveformHeight = _LineHeight * amplitude;
                float distanceFromCenterY = abs(i.uv.y - 0.5);
                float alpha = distanceFromCenterY < waveformHeight ? _LineColor.a : 0.0;
                return float4(_LineColor.rgb, alpha);
            }
            ENDCG
        }
    }
}
