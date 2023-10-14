Shader "Pya/StencilObject" {

	Properties
	{
        _Mask ("Mask", Int) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _Comp("Comp", Float) = 3 //Equal
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)
	}

    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" "LightMode" = "ForwardBase"}

        Stencil {
            Ref [_Mask]
            Comp [_Comp]
        }

        Pass {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct v2f {
                float4 pos    : SV_POSITION;
                float3 normal : TEXCOORD0;
            };

            float4 _MainColor;
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                float3 normal = normalize(i.normal);
                float3 light  = normalize(_WorldSpaceLightPos0.xyz);

                float  diffuse = saturate(dot(normal, light));
                float3 ambient = ShadeSH9(half4(normal, 1));

                float4 col = diffuse * _MainColor * _LightColor0;
                        col.rgb += ambient * _MainColor;
                return col;
            }
            ENDCG
        }
    } 
}
