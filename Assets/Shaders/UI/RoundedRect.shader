// Upgrade NOTE: upgraded instancing buffer 'RoundedRect' to new syntax.

// Made with Amplify Shader Editor v1.9.3.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RoundedRect"
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

        [HDR]_ColorOn("Color On", Color) = (0.4784314,0.9843138,0.9647059,1)
        [HDR]_ColorOff("Color Off", Color) = (0.572549,0.5803922,0.5568628,1)
        [HDR]_Color0("Color 0", Color) = (0.2077697,0.2735849,0.2710339,1)

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

            UNITY_INSTANCING_BUFFER_START(RoundedRect)
            	UNITY_DEFINE_INSTANCED_PROP(float4, _ColorOff)
#define _ColorOff_arr RoundedRect
            	UNITY_DEFINE_INSTANCED_PROP(float4, _ColorOn)
#define _ColorOn_arr RoundedRect
            	UNITY_DEFINE_INSTANCED_PROP(float4, _Color0)
#define _Color0_arr RoundedRect
            UNITY_INSTANCING_BUFFER_END(RoundedRect)

            
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

                float4 _ColorOff_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorOff_arr, _ColorOff);
                float4 _ColorOn_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorOn_arr, _ColorOn);
                float4 lerpResult20 = lerp( _ColorOff_Instance , _ColorOn_Instance , 1.0);
                float2 texCoord2 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
                float2 appendResult10_g2 = (float2(0.17 , 1.0));
                float2 temp_output_11_0_g2 = ( abs( (texCoord2*2.0 + -1.0) ) - appendResult10_g2 );
                float2 break16_g2 = ( 1.0 - ( temp_output_11_0_g2 / fwidth( temp_output_11_0_g2 ) ) );
                float4 _Color0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color0_arr, _Color0);
                float temp_output_2_0_g1 = 1.0;
                float temp_output_3_0_g1 = 1.0;
                float2 appendResult21_g1 = (float2(temp_output_2_0_g1 , temp_output_3_0_g1));
                float Radius25_g1 = max( min( min( abs( ( 0.23 * 2 ) ) , abs( temp_output_2_0_g1 ) ) , abs( temp_output_3_0_g1 ) ) , 1E-05 );
                float2 temp_cast_0 = (0.0).xx;
                float temp_output_30_0_g1 = ( length( max( ( ( abs( (texCoord2*2.0 + -1.0) ) - appendResult21_g1 ) + Radius25_g1 ) , temp_cast_0 ) ) / Radius25_g1 );
                

                half4 color = ( ( lerpResult20 * saturate( min( break16_g2.x , break16_g2.y ) ) ) + ( _Color0_Instance * saturate( ( ( 1.0 - temp_output_30_0_g1 ) / fwidth( temp_output_30_0_g1 ) ) ) ) );

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
Node;AmplifyShaderEditor.RangedFloatNode;7;-1299.722,-16.23076;Inherit;False;Constant;_Float7;Float 7;0;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1228.221,167.0693;Inherit;False;Constant;_Float9;Float 7;0;0;Create;True;0;0;0;False;0;False;0.23;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1281.522,66.96923;Inherit;False;Constant;_Float8;Float 7;0;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1614.112,-258.6715;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-1247.149,-587.3441;Inherit;False;Constant;_Float10;Float 7;0;0;Create;True;0;0;0;False;0;False;0.17;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;-1135.524,-903.6616;Inherit;False;InstancedProperty;_ColorOn;Color On;0;1;[HDR];Create;True;0;0;0;False;0;False;0.4784314,0.9843138,0.9647059,1;0.4784314,0.9843138,0.9647059,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;-1132.516,-1160.255;Inherit;False;InstancedProperty;_ColorOff;Color Off;1;1;[HDR];Create;True;0;0;0;False;0;False;0.572549,0.5803922,0.5568628,1;0.4784314,0.9843138,0.9647059,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-812.8605,-803.3651;Inherit;False;Constant;_ColorLerp;ColorLerp;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-1020.5,-74.99998;Inherit;False;Rounded Rectangle;-1;;1;8679f72f5be758f47babb3ba1d5f51d3;0;4;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;10;-911.5223,-562.8903;Inherit;False;Rectangle;-1;;2;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;0.5;False;3;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-1067.527,-350.9462;Inherit;False;InstancedProperty;_Color0;Color 0;2;1;[HDR];Create;True;0;0;0;False;0;False;0.2077697,0.2735849,0.2710339,1;0.2077697,0.2735849,0.2710339,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;20;-639.0483,-976.5455;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-624.8278,-119.6462;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-312.4424,-488.7365;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;31.24311,-401.0886;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;478.1215,-182.5898;Float;False;True;-1;2;ASEMaterialInspector;0;3;RoundedRect;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;1;1;2;0
WireConnection;1;2;7;0
WireConnection;1;3;8;0
WireConnection;1;4;9;0
WireConnection;10;1;2;0
WireConnection;10;2;14;0
WireConnection;10;3;7;0
WireConnection;20;0;18;0
WireConnection;20;1;16;0
WireConnection;20;2;21;0
WireConnection;5;0;6;0
WireConnection;5;1;1;0
WireConnection;15;0;20;0
WireConnection;15;1;10;0
WireConnection;17;0;15;0
WireConnection;17;1;5;0
WireConnection;0;0;17;0
ASEEND*/
//CHKSM=ED1248175C5A3AC2BC0B6FA5517523977AA5C6EF