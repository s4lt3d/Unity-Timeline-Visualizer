Shader "Custom/WaveformCenterLineVisualizer"
{
    Properties
    {
        _WaveformTex ("Waveform Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (1,0,0,1) // Default to red
        _LineWidth ("Line Width", Float) = 0.01 // Width of the line in UV space
        _LineHeight ("Line Height", Float) = 0.2 // Max height of the waveform in UV space
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Enable blending for transparency

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _WaveformTex;
            float4 _LineColor;
            float _LineWidth;
            float _LineHeight;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float amplitude = tex2D(_WaveformTex, float2(i.uv.x, 0.5)).r; // Sample amplitude from texture
                float centerDistance = abs(i.uv.x - 0.5);
                bool isWithinLineWidth = centerDistance < _LineWidth;
                float alpha = 0.0;

                // Determine if within vertical bounds of the modulated waveform
                if (isWithinLineWidth)
                {
                    float waveformHeight = _LineHeight * amplitude; // Modulate line height based on waveform
                    float distanceFromCenterY = abs(i.uv.y - 0.5);
                    alpha = distanceFromCenterY < waveformHeight ? _LineColor.a : 0.0;
                }

                return float4(_LineColor.rgb, alpha);
            }
            ENDCG
        }
    }
}
