// Upgrade NOTE: upgraded instancing buffer 'FrequencyBar' to new syntax.

// Made with Amplify Shader Editor v1.9.3.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FrequencyBar"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

        [HDR]_Color0("Color0", Color) = (0.07772847,1.498039,0.1408624,1)
        [HDR]_Color1("Color1", Color) = (1.498039,0.1724159,0.07772846,1)
        _Peak("Peak", Float) = 0.86
        _Height("Height", Float) = 0.81

    }

    SubShader
    {
		LOD 0

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

        Stencil
        {
        	Ref [_Stencil]
        	ReadMask [_StencilReadMask]
        	WriteMask [_StencilWriteMask]
        	Comp [_StencilComp]
        	Pass [_StencilOp]
        }


        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        
        Pass
        {
            Name "Default"
        CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #pragma multi_compile_instancing


            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            UNITY_INSTANCING_BUFFER_START(FrequencyBar)
            	UNITY_DEFINE_INSTANCED_PROP(float4, _Color0)
#define _Color0_arr FrequencyBar
            	UNITY_DEFINE_INSTANCED_PROP(float4, _Color1)
#define _Color1_arr FrequencyBar
            	UNITY_DEFINE_INSTANCED_PROP(float, _Height)
#define _Height_arr FrequencyBar
            	UNITY_DEFINE_INSTANCED_PROP(float, _Peak)
#define _Peak_arr FrequencyBar
            UNITY_INSTANCING_BUFFER_END(FrequencyBar)

            
            v2f vert(appdata_t v )
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                

                v.vertex.xyz +=  float3( 0, 0, 0 ) ;

                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                OUT.texcoord = v.texcoord;
                OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN ) : SV_Target
            {
                //Round up the alpha color coming from the interpolator (to 1.0/256.0 steps)
                //The incoming alpha could have numerical instability, which makes it very sensible to
                //HDR color transparency blend, when it blends with the world's texture.
                const half alphaPrecision = half(0xff);
                const half invAlphaPrecision = half(1.0/alphaPrecision);
                IN.color.a = round(IN.color.a * alphaPrecision)*invAlphaPrecision;

                float4 _Color0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color0_arr, _Color0);
                float4 _Color1_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color1_arr, _Color1);
                float2 texCoord1 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
                float clampResult15 = clamp( ( -0.27 + ( 1.5 * texCoord1.y ) ) , 0.0 , 1.0 );
                float4 lerpResult5 = lerp( _Color0_Instance , _Color1_Instance , clampResult15);
                float _Height_Instance = UNITY_ACCESS_INSTANCED_PROP(_Height_arr, _Height);
                float _Peak_Instance = UNITY_ACCESS_INSTANCED_PROP(_Peak_arr, _Peak);
                float4 color21 = IsGammaSpace() ? float4(0.572549,0.5803922,0.5568628,1) : float4(0.2874408,0.2961383,0.2704979,1);
                

                half4 color = ( ( lerpResult5 * ( texCoord1.y < _Height_Instance ? 1.0 : 0.0 ) ) + ( (( texCoord1.y >= _Peak_Instance && texCoord1.y <= ( _Peak_Instance + 0.05 ) ) ? 1.0 :  0.0 ) * color21 ) );

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                return color;
            }
        ENDCG
        }
    }
    CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19302
Node;AmplifyShaderEditor.RangedFloatNode;14;-951.8284,-318.7596;Inherit;False;Constant;_Float3;Float 2;2;0;Create;True;0;0;0;False;0;False;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1050,-181;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-950.1266,-415.2598;Inherit;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;0;False;0;False;-0.27;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-734.1284,-317.6597;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-476.2281,-408.26;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1059.321,49.70219;Inherit;False;Constant;_Width;Width;2;0;Create;True;0;0;0;False;0;False;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1057.514,-27.98584;Inherit;False;InstancedProperty;_Peak;Peak;2;0;Create;True;0;0;0;False;0;False;0.86;0.86;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;15;-315.3428,-426.7541;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-856.5144,31.01416;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-830.4441,173.2813;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-808.0001,-860.7997;Inherit;False;InstancedProperty;_Color0;Color0;0;1;[HDR];Create;True;0;0;0;False;0;False;0.07772847,1.498039,0.1408624,1;1,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;3;-809.6,-668.8;Inherit;False;InstancedProperty;_Color1;Color1;1;1;[HDR];Create;True;0;0;0;False;0;False;1.498039,0.1724159,0.07772846,1;1,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-545.0965,20.06065;Inherit;False;InstancedProperty;_Height;Height;3;0;Create;True;0;0;0;False;0;False;0.81;0.81;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;5;-144,-656;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Compare;7;-299.2793,-21.81654;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareWithRange;16;-447.0945,-236.8427;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;21;-8.609584,-121.7172;Inherit;False;Constant;_Color2;Color 2;2;1;[HDR];Create;True;0;0;0;False;0;False;0.572549,0.5803922,0.5568628,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;207.5723,-703.3605;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;211.0907,-284.2173;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;495.7904,-624.8167;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;824.1995,-628.6996;Float;False;True;-1;2;ASEMaterialInspector;0;3;FrequencyBar;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;13;0;14;0
WireConnection;13;1;1;2
WireConnection;11;0;12;0
WireConnection;11;1;13;0
WireConnection;15;0;11;0
WireConnection;19;0;18;0
WireConnection;19;1;17;0
WireConnection;5;0;2;0
WireConnection;5;1;3;0
WireConnection;5;2;15;0
WireConnection;7;0;1;2
WireConnection;7;1;6;0
WireConnection;7;2;8;0
WireConnection;16;0;1;2
WireConnection;16;1;18;0
WireConnection;16;2;19;0
WireConnection;16;3;8;0
WireConnection;10;0;5;0
WireConnection;10;1;7;0
WireConnection;22;0;16;0
WireConnection;22;1;21;0
WireConnection;20;0;10;0
WireConnection;20;1;22;0
WireConnection;0;0;20;0
ASEEND*/
//CHKSM=0F1F27879FF9B262FC37668B611E5E3B6B5606AD