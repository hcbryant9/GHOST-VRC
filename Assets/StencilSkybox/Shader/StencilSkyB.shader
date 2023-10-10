Shader "Pya/StencilSkyB"
{
    Properties {
        _Mask ("Mask", Int) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _Comp("Comp", Float) = 3 //Equal
        _SkyboxSize ("Skybox Size", Range(0.1, 100)) = 10
        [NoScaleOffset] _Tex ("Cubemap", Cube) = "grey" {}
    }

    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        Cull Front

        Stencil {
            Ref [_Mask]
            Comp [_Comp]
        }

        Pass {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            samplerCUBE _Tex;
            float _SkyboxSize;

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD;
            };

            v2f vert (appdata_base v)
            {
                v.vertex.xyz *= _SkyboxSize; //Skyboxのサイズ変更
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.vertex;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 tex = texCUBE (_Tex, i.texcoord);
                return tex;
            }
            ENDCG
        }
    }

Fallback Off

}