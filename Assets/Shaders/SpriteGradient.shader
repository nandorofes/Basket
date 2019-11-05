// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33229,y:32719,varname:node_1873,prsc:2|emission-1749-OUT,alpha-603-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32580,y:32464,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5983,x:31309,y:33409,ptovrint:False,ptlb:StartColor,ptin:_StartColor,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32753,y:33218,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1749,x:33022,y:32792,cmnt:Premultiply Alpha,varname:node_1749,prsc:2|A-1928-OUT,B-603-OUT;n:type:ShaderForge.SFN_Multiply,id:603,x:32835,y:32991,cmnt:A,varname:node_603,prsc:2|A-2912-OUT,B-5444-OUT,C-3198-OUT;n:type:ShaderForge.SFN_Set,id:3795,x:32745,y:32539,varname:__mainTexA,prsc:2|IN-4805-A;n:type:ShaderForge.SFN_Set,id:1225,x:32745,y:32464,varname:__mainTexRGB,prsc:2|IN-4805-RGB;n:type:ShaderForge.SFN_Get,id:3619,x:32628,y:32721,varname:node_3619,prsc:2|IN-1225-OUT;n:type:ShaderForge.SFN_Get,id:2912,x:32628,y:32976,varname:node_2912,prsc:2|IN-3795-OUT;n:type:ShaderForge.SFN_TexCoord,id:1800,x:31342,y:32267,varname:node_1800,prsc:2,uv:0;n:type:ShaderForge.SFN_SwitchProperty,id:1017,x:31512,y:32277,ptovrint:False,ptlb:Vertical,ptin:_Vertical,varname:node_1017,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1800-U,B-1800-V;n:type:ShaderForge.SFN_RemapRange,id:6586,x:31703,y:32154,varname:node_6586,prsc:2,frmn:0,frmx:0.5,tomn:1,tomx:0|IN-1017-OUT;n:type:ShaderForge.SFN_RemapRange,id:2307,x:31703,y:32489,varname:node_2307,prsc:2,frmn:0.5,frmx:1,tomn:0,tomx:1|IN-1017-OUT;n:type:ShaderForge.SFN_Color,id:936,x:31633,y:33411,ptovrint:False,ptlb:MidColor,ptin:_MidColor,varname:node_936,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:5673,x:31935,y:33409,ptovrint:False,ptlb:EndColor,ptin:_EndColor,varname:node_5673,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Set,id:2867,x:31480,y:33488,varname:__col1A,prsc:2|IN-5983-A;n:type:ShaderForge.SFN_Set,id:7941,x:31480,y:33411,varname:__col1RGB,prsc:2|IN-5983-RGB;n:type:ShaderForge.SFN_Set,id:3158,x:31787,y:33409,varname:__col2RGB,prsc:2|IN-936-RGB;n:type:ShaderForge.SFN_Set,id:680,x:32107,y:33494,varname:__col3A,prsc:2|IN-5673-A;n:type:ShaderForge.SFN_Set,id:4188,x:32107,y:33409,varname:__col3RGB,prsc:2|IN-5673-RGB;n:type:ShaderForge.SFN_Set,id:9745,x:31787,y:33488,varname:__col2A,prsc:2|IN-936-A;n:type:ShaderForge.SFN_Set,id:938,x:32031,y:32487,varname:__endMap,prsc:2|IN-5964-OUT;n:type:ShaderForge.SFN_Set,id:7313,x:32031,y:32139,varname:__startMap,prsc:2|IN-1545-OUT;n:type:ShaderForge.SFN_Set,id:9240,x:32411,y:32188,varname:__midMap,prsc:2|IN-4084-OUT;n:type:ShaderForge.SFN_Multiply,id:4646,x:31474,y:32661,varname:node_4646,prsc:2|A-4488-OUT,B-7590-OUT;n:type:ShaderForge.SFN_Multiply,id:2414,x:31474,y:32804,varname:node_2414,prsc:2|A-4488-OUT,B-6915-OUT;n:type:ShaderForge.SFN_Get,id:4488,x:31273,y:32661,varname:node_4488,prsc:2|IN-7313-OUT;n:type:ShaderForge.SFN_Get,id:7590,x:31273,y:32720,varname:node_7590,prsc:2|IN-7941-OUT;n:type:ShaderForge.SFN_Get,id:6915,x:31273,y:32777,varname:node_6915,prsc:2|IN-2867-OUT;n:type:ShaderForge.SFN_Set,id:4243,x:31654,y:32804,varname:__result1A,prsc:2|IN-2414-OUT;n:type:ShaderForge.SFN_Set,id:7822,x:31654,y:32661,varname:__result1RGB,prsc:2|IN-4646-OUT;n:type:ShaderForge.SFN_Get,id:1316,x:32427,y:32773,varname:node_1316,prsc:2|IN-7822-OUT;n:type:ShaderForge.SFN_Get,id:3025,x:32448,y:33049,varname:node_3025,prsc:2|IN-4243-OUT;n:type:ShaderForge.SFN_Multiply,id:4331,x:31501,y:33029,varname:node_4331,prsc:2|A-8351-OUT,B-8690-OUT;n:type:ShaderForge.SFN_Multiply,id:3525,x:31501,y:33162,varname:node_3525,prsc:2|A-8351-OUT,B-2019-OUT;n:type:ShaderForge.SFN_Get,id:8351,x:31266,y:32997,varname:node_8351,prsc:2|IN-9240-OUT;n:type:ShaderForge.SFN_Get,id:8690,x:31271,y:33088,varname:node_8690,prsc:2|IN-3158-OUT;n:type:ShaderForge.SFN_Get,id:2019,x:31275,y:33174,varname:node_2019,prsc:2|IN-9745-OUT;n:type:ShaderForge.SFN_Set,id:7932,x:31686,y:33162,varname:__result2A,prsc:2|IN-3525-OUT;n:type:ShaderForge.SFN_Set,id:3572,x:31686,y:33029,varname:__result2RGB,prsc:2|IN-4331-OUT;n:type:ShaderForge.SFN_Get,id:7484,x:31894,y:32876,varname:node_7484,prsc:2|IN-938-OUT;n:type:ShaderForge.SFN_Get,id:4197,x:31894,y:32931,varname:node_4197,prsc:2|IN-4188-OUT;n:type:ShaderForge.SFN_Get,id:6021,x:31894,y:32994,varname:node_6021,prsc:2|IN-680-OUT;n:type:ShaderForge.SFN_Multiply,id:2437,x:32080,y:32876,varname:node_2437,prsc:2|A-7484-OUT,B-4197-OUT;n:type:ShaderForge.SFN_Multiply,id:1867,x:32080,y:32994,varname:node_1867,prsc:2|A-7484-OUT,B-6021-OUT;n:type:ShaderForge.SFN_Set,id:4544,x:32247,y:32876,varname:__result3RGB,prsc:2|IN-2437-OUT;n:type:ShaderForge.SFN_Set,id:1116,x:32247,y:32994,varname:__result3A,prsc:2|IN-1867-OUT;n:type:ShaderForge.SFN_Get,id:668,x:32427,y:32825,varname:node_668,prsc:2|IN-3572-OUT;n:type:ShaderForge.SFN_Get,id:1602,x:32427,y:32876,varname:node_1602,prsc:2|IN-4544-OUT;n:type:ShaderForge.SFN_Get,id:1730,x:32448,y:33100,varname:node_1730,prsc:2|IN-7932-OUT;n:type:ShaderForge.SFN_Get,id:3059,x:32448,y:33153,varname:node_3059,prsc:2|IN-1116-OUT;n:type:ShaderForge.SFN_Clamp01,id:1545,x:31876,y:32139,varname:node_1545,prsc:2|IN-6586-OUT;n:type:ShaderForge.SFN_Clamp01,id:5964,x:31873,y:32487,varname:node_5964,prsc:2|IN-2307-OUT;n:type:ShaderForge.SFN_Add,id:7123,x:32052,y:32318,varname:node_7123,prsc:2|A-1545-OUT,B-5964-OUT;n:type:ShaderForge.SFN_OneMinus,id:5862,x:32227,y:32318,varname:node_5862,prsc:2|IN-7123-OUT;n:type:ShaderForge.SFN_Clamp01,id:4084,x:32248,y:32188,varname:node_4084,prsc:2|IN-5862-OUT;n:type:ShaderForge.SFN_Add,id:7051,x:32638,y:32789,varname:node_7051,prsc:2|A-1316-OUT,B-668-OUT,C-1602-OUT;n:type:ShaderForge.SFN_Multiply,id:1928,x:32835,y:32766,varname:node_1928,prsc:2|A-3619-OUT,B-7051-OUT,C-174-OUT;n:type:ShaderForge.SFN_Add,id:5444,x:32654,y:33035,varname:node_5444,prsc:2|A-3025-OUT,B-1730-OUT,C-3059-OUT;n:type:ShaderForge.SFN_Set,id:5061,x:32938,y:33218,varname:__vertRGB,prsc:2|IN-5376-RGB;n:type:ShaderForge.SFN_Set,id:9080,x:32938,y:33287,varname:__vertA,prsc:2|IN-5376-A;n:type:ShaderForge.SFN_Get,id:3198,x:32767,y:33122,varname:node_3198,prsc:2|IN-9080-OUT;n:type:ShaderForge.SFN_Get,id:174,x:32786,y:32898,varname:node_174,prsc:2|IN-5061-OUT;proporder:4805-5983-936-5673-1017;pass:END;sub:END;*/

Shader "Shader Forge/SpriteGradient" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _StartColor ("StartColor", Color) = (1,0,0,1)
        _MidColor ("MidColor", Color) = (0,1,0,1)
        _EndColor ("EndColor", Color) = (0,0,1,1)
        [MaterialToggle] _Vertical ("Vertical", Float ) = 0
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
            uniform float4 _StartColor;
            uniform fixed _Vertical;
            uniform float4 _MidColor;
            uniform float4 _EndColor;
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
                float3 __mainTexRGB = _MainTex_var.rgb;
                float3 node_3619 = __mainTexRGB;
                float _Vertical_var = lerp( i.uv0.r, i.uv0.g, _Vertical );
                float node_6586 = (_Vertical_var*-2.0+1.0);
                float node_1545 = saturate(node_6586);
                float __startMap = node_1545;
                float node_4488 = __startMap;
                float3 __col1RGB = _StartColor.rgb;
                float3 __result1RGB = (node_4488*__col1RGB);
                float3 node_1316 = __result1RGB;
                float node_2307 = (_Vertical_var*2.0+-1.0);
                float node_5964 = saturate(node_2307);
                float __midMap = saturate((1.0 - (node_1545+node_5964)));
                float node_8351 = __midMap;
                float3 __col2RGB = _MidColor.rgb;
                float3 __result2RGB = (node_8351*__col2RGB);
                float3 node_668 = __result2RGB;
                float __endMap = node_5964;
                float node_7484 = __endMap;
                float3 __col3RGB = _EndColor.rgb;
                float3 __result3RGB = (node_7484*__col3RGB);
                float3 node_1602 = __result3RGB;
                float3 __vertRGB = i.vertexColor.rgb;
                float __mainTexA = _MainTex_var.a;
                float __col1A = _StartColor.a;
                float __result1A = (node_4488*__col1A);
                float __col2A = _MidColor.a;
                float __result2A = (node_8351*__col2A);
                float __col3A = _EndColor.a;
                float __result3A = (node_7484*__col3A);
                float __vertA = i.vertexColor.a;
                float node_603 = (__mainTexA*(__result1A+__result2A+__result3A)*__vertA); // A
                float3 emissive = ((node_3619*(node_1316+node_668+node_1602)*__vertRGB)*node_603);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_603);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
