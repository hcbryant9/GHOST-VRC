Shader "Pya/StencilWindow"
{

    Properties
    {
       _Mask ("Mask", Int) = 1
       [Space (7)]
       [MaterialToggle] _IsDistance ("Enable Rendering Distance", Float) = 0 
       _Distance("Rendering Distance",  float) = 0.5
    }

    SubShader
    {

        Tags { "RenderType"="Opaque" "Queue"="Geometry-2"}

        ColorMask 0 //色の出力をOFF
        Cull Front
        ZWrite Off
        Stencil 
        {
            Ref [_Mask]
            Comp Always
            Pass Replace
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float _IsDistance;
            float _Distance;

            //カメラとオブジェクトの距離を計算
			float CalcDistance() {
				float3 objPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
				return abs(length(objPos - _WorldSpaceCameraPos));
			}

            fixed4 frag (v2f i) : SV_Target
            {
                if (_IsDistance) {
				    // オブジェクトとの距離が_Distance以下で描画
				    if (CalcDistance() <= _Distance) {
					    return 0;
				    } else {
					    discard;
					    return 0;
				    }
                } else {
                    return 0;        
				}
            }
            ENDCG
        }

    }

}