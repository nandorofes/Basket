// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33229,y:32719,varname:node_1873,prsc:2|emission-1749-OUT,alpha-603-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32551,y:32729,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1086,x:32812,y:32818,cmnt:RGB,varname:node_1086,prsc:2|A-4805-RGB,B-5983-RGB,C-5376-RGB;n:type:ShaderForge.SFN_Color,id:5983,x:32551,y:32915,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32551,y:33079,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1749,x:33025,y:32818,cmnt:Premultiply Alpha,varname:node_1749,prsc:2|A-1086-OUT,B-603-OUT;n:type:ShaderForge.SFN_Multiply,id:603,x:32812,y:32992,cmnt:A,varname:node_603,prsc:2|A-4805-A,B-5983-A,C-5376-A,D-7669-OUT;n:type:ShaderForge.SFN_TexCoord,id:8953,x:31450,y:32496,varname:node_8953,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:3145,x:31626,y:32496,varname:node_3145,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8953-UVOUT;n:type:ShaderForge.SFN_Distance,id:9048,x:31645,y:32667,varname:node_9048,prsc:2|A-5247-OUT,B-1789-OUT;n:type:ShaderForge.SFN_Vector2,id:1789,x:31472,y:32731,varname:node_1789,prsc:2,v1:0,v2:0;n:type:ShaderForge.SFN_OneMinus,id:7411,x:31826,y:32667,cmnt:Circle area,varname:node_7411,prsc:2|IN-9048-OUT;n:type:ShaderForge.SFN_ArcTan2,id:5600,x:31426,y:32852,varname:node_5600,prsc:2,attp:2|A-9894-G,B-9894-R;n:type:ShaderForge.SFN_ComponentMask,id:9894,x:31241,y:32852,varname:node_9894,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5923-OUT;n:type:ShaderForge.SFN_Set,id:2083,x:31826,y:32496,varname:__uvRemap,prsc:2|IN-3145-OUT;n:type:ShaderForge.SFN_Get,id:5247,x:31440,y:32667,varname:node_5247,prsc:2|IN-2083-OUT;n:type:ShaderForge.SFN_Get,id:5923,x:31184,y:32779,varname:node_5923,prsc:2|IN-2083-OUT;n:type:ShaderForge.SFN_Frac,id:8648,x:31813,y:32988,varname:node_8648,prsc:2|IN-8642-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:8642,x:31648,y:32988,varname:node_8642,prsc:2|IN-5600-OUT,IMIN-4160-OUT,IMAX-6168-OUT,OMIN-6975-OUT,OMAX-263-OUT;n:type:ShaderForge.SFN_ValueProperty,id:263,x:31241,y:33138,ptovrint:False,ptlb:StreakCount,ptin:_StreakCount,varname:node_263,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Negate,id:6975,x:31456,y:33165,varname:node_6975,prsc:2|IN-263-OUT;n:type:ShaderForge.SFN_Vector1,id:4160,x:31426,y:33006,varname:node_4160,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:6168,x:31426,y:33055,varname:node_6168,prsc:2,v1:1;n:type:ShaderForge.SFN_Step,id:6258,x:31994,y:32988,cmnt: Streak area,varname:node_6258,prsc:2|A-8648-OUT,B-5821-OUT;n:type:ShaderForge.SFN_Slider,id:5821,x:31648,y:33141,ptovrint:False,ptlb:WidthRatio,ptin:_WidthRatio,varname:node_5821,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Set,id:8982,x:32351,y:32667,varname:__circleArea,prsc:2|IN-448-OUT;n:type:ShaderForge.SFN_Set,id:510,x:32164,y:33024,varname:__streakArea,prsc:2|IN-6258-OUT;n:type:ShaderForge.SFN_Get,id:5099,x:31894,y:33297,varname:node_5099,prsc:2|IN-8982-OUT;n:type:ShaderForge.SFN_Get,id:192,x:31894,y:33351,varname:node_192,prsc:2|IN-510-OUT;n:type:ShaderForge.SFN_Multiply,id:9919,x:32096,y:33315,varname:node_9919,prsc:2|A-5099-OUT,B-192-OUT;n:type:ShaderForge.SFN_Set,id:6963,x:32278,y:33315,varname:__streakMask,prsc:2|IN-9919-OUT;n:type:ShaderForge.SFN_Get,id:7669,x:32551,y:33227,varname:node_7669,prsc:2|IN-6963-OUT;n:type:ShaderForge.SFN_Power,id:448,x:32189,y:32667,varname:node_448,prsc:2|VAL-7628-OUT,EXP-6565-OUT;n:type:ShaderForge.SFN_Slider,id:6565,x:31648,y:32834,ptovrint:False,ptlb:Softness,ptin:_Softness,varname:node_6565,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:10;n:type:ShaderForge.SFN_Clamp01,id:7628,x:32003,y:32667,varname:node_7628,prsc:2|IN-7411-OUT;proporder:4805-5983-263-5821-6565;pass:END;sub:END;*/

Shader "Shader Forge/SpriteStreakization" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _StreakCount ("StreakCount", Float ) = 10
        _WidthRatio ("WidthRatio", Range(0, 1)) = 0.5
        _Softness ("Softness", Range(0, 10)) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _StreakCount;
            uniform float _WidthRatio;
            uniform float _Softness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 __uvRemap = (i.uv0*2.0+-1.0);
                float __circleArea = pow(saturate((1.0 - distance(__uvRemap,float2(0,0)))),_Softness);
                float2 node_9894 = __uvRemap.rg;
                float node_4160 = 0.0;
                float node_6975 = (-1*_StreakCount);
                float __streakArea = step(frac((node_6975 + ( (((atan2(node_9894.g,node_9894.r)/6.28318530718)+0.5) - node_4160) * (_StreakCount - node_6975) ) / (1.0 - node_4160))),_WidthRatio);
                float __streakMask = (__circleArea*__streakArea);
                float node_603 = (_MainTex_var.a*_Color.a*i.vertexColor.a*__streakMask); // A
                float3 emissive = ((_MainTex_var.rgb*_Color.rgb*i.vertexColor.rgb)*node_603);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_603);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
