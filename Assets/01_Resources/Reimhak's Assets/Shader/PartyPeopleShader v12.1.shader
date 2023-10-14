//にゃん

//9.1 TextJackの処理方法変更→画面比に比例せず、そのままにした。処理変更β。 誤字修正。HUE実装。CSをgrabUV→defUVへ。
//10 TextJackの処理方法変更→視差計算
//11 Glitch1、2/ブラー/色収差/NoiseLine/明暗/グレースケール改良、intrangeの存在を知った。今更干渉対策を少しした（すっかり忘れてた）。初期値変更。
//11.2 TextJack、mono修正、改良、揺れ初期値修正、負荷軽減、スケール追加
//11.3 スケール視差修正、ビネット追加
//v12 販売用修正
//v12.1 ブラー修正
Shader "Reimhak's Shader/PartyPeopleShader v12.1"
{
	Properties{ 
        [Enum(UnityEngine.Rendering.CullMode)]
        		_Cull("Cull", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)]
				_Ztest ("ZTest", float) = 8
				_Length ("視認距離", float) = 250
		[Header(Paripi)]
				_Distortion ("ズレ", Range(-3,3)) = 0
				_yure ("揺れ" , Range(0,10))=10
				[Space(10)]
				_Distortionx ("xズレ", float) = 0
				_yurex ("x揺れ" , Range(0,10))=0
				[Space(10)]
				_Distortiony ("yズレ", float) = 0
				_yurey ("y揺れ" , Range(0,10))=0

				[Space(20)]
				_lerptextgl("歪み（ビネット、ブラー）",range(0,1))=1
				_lerptexx("x軸サイズ",float)=1.2
				_lerptexy("y軸サイズ",float)=1
				[IntRange]_DiffRange("ブラー回数",range(0,20))=10
				_Diff ("ブラー", Range(0,0.01)) = 0
				_vignette("ビネット",range(0,1))=0
				_vignetteColor("色", Color) = (0,0,0,1)

		[Space(10)]
				_Scale("スケール", Range (-.9, .9)) = 0
		[Space (20)]
		[Header(ChromaticAberration)]
		[Space (7)]
				_CHROMA("色収差機能", range(0,1)) = 0.0
		[Space (7)]
				_RedX("RedX",Range(-.1,0.1))=.01
				_RedY("RedY",Range(-.1,0.1))=-.01
		[Space (7)]
				_GreenX("GreenX",Range(-.1,0.1))=.01
				_GreenY("GreenY",Range(-.1,0.1))=-.01
		[Space (7)]
				_BlueX("BlueX",Range(-.1,0.1))=.01
				_BlueY("BlueY",Range(-.1,0.1))=-.01
		[Space (7)]
				[IntRange]_blur("色収差ブラー(1でオン)", range(0,1)) = 1
		[Space (30)]
		[Header(ColorChange)]
				_R("Red",Range(0,1))=0
				_G("Green",Range(0,1))=0
				_B("Blue",Range(0,1))=0
		[Space (7)]
				_nega ("色反転", Range(0., 1.)) = 0
		[Space (7)]
				_monno ("グレースケール",range(0,1))=0
		[Space (30)]
		[Header(DistortionMap)]
		[Space (7)]
				_NormalIntensity ("強さ", Range(0, .1)) = 0
				_ReflectionValue ("歪み", Range(-0.5, 1)) = 0.5
				_ReflectionMap ("BumpMap", 2D) = "bump" {}
				_Opacity ("Opacity", 2D) = "white" {}
		[Space (20)]
		[Header(Color)]
				_bw("明暗",range(-1,1))=0
				_rr("Red",Range(0,2))=1
				_gg("Green",Range(0,2))=1
				_bb("Blue",Range(0,2))=1
		[Space (20)]
		[Header(RainDrop)]
				_sz("サイズ",Range(0,1)) = 0
				_Intensity("強さ",Range(0,5)) = 1
				_SecondTex("水滴マップ", 2D) = "white"{}
		[Space (20)]
		[Header(Noise)]
				_RGBNoise("ノイズ", Range(0, 1)) = 0
		[Space (20)]
		[Header(ShadowPicuture_Edge)]
				_EdgeZ("太さ(0で無効化)",Range(0,5)) = 0
				_EdgeCol("縁の色", Color) = (1,1,1,1)
				_str("強さ",Range(0,20)) = 5
		[Space (20)]
		[Header(Rainbow)]
				_Rainbow("虹色",Range(0,1)) = 0
				_Thick("太さ",Range(.001,10)) = 1
				_sl("スライド",float) = 0
				_Autosl("スライド速度",float) = 20
				_Angle("角度",float) = 0
		[Space(20)]
		[Header(HUE)]
				[IntRange]_HUE("色相変化(1でオン)",range(0,1)) = 0
				_HueS("速度",float) = 2
				_Hue ("色相", Float) = 0
				_Sat ("彩度", Float) = 1
				_Val ("明度", Float) = 1  
		[Space (20)]
		[Header(NoiseLine)]
				_NoiseSize("線の太さ",Range(0,0.005)) = 0
				_NoiseAuto("スライド速度", range(0,1)) = 0
				_NColor("線の色",color) = (1,1,1,1)
				[IntRange]_RandLine("ランダム",range(0,1))=1
				[IntRange]_NAngle("角度", range(0,1)) = 1
				_NoisePos("線の位置",Range(0,1)) = 0
				_Npos("各線の間隔",vector) = (.2,.8,2,3)
				_Ndis("切り抜き",range(0,1)) = 1
		[Space (3)]
				[IntRange]_Nloop("ループ用(1でオン)", range(0,1)) = 0.0
				_Nw("線の間隔", Range(0,.5)) = 0
		[Header(NoiseLine2)]
				_LineAlpha("LineAlpha",range(0,1)) = 0
		[Space (20)]
		[Header(Circle)]
				_CUVPosx("X座標",range(-1,1)) = 0
				_CUVPosy("Y座標",range(-1,1)) = 0
				_CSizex("サイズ",range(0,1)) = 0
				_CSizey("切り抜き",range(0,1)) = 0
				_CColor("線の色",color) = (1,1,1,1)
		[Space (20)]
		[Header(Check)]
				_TAlpha("混ぜ具合",Range(0,1)) = 0
				_TSize("サイズ",float) = 50
				_TColor1("色1",color) = (1,1,1,1)
				_TColor2("色2",color) = (0,0,0,0)
				_TUVx("X座標",range(-1,1)) = 0
				_TUVy("X座標",range(-1,1)) = 0
		[Space(20)]
		[Header(TextJack)]
				_TextSW("テキスト", Range(0, 1)) = 0
				_Text("文字とか画像とか", 2D) = "white" {}
				_Cutoff("Cutoff", Range(-1, 1)) = 01
				_TxUVx("X座標",float) = 0
				_TxUVy("Y座標",float) = 0
				_TxSizex("Xサイズ",	float) = 1
				_TxSizey("Yサイズ", float) = 1
				[Space(10)]
				_TxSlider ("スライド", Range(0,1)) = 0
				_Txlength("幅", Range(0,.5)) = 0
				_TxSize("サイズ", Range(0,5)) = 1
				_prog("prog",range(0,1))=1
				_tColor("Color",color)=(1,1,1,0)
				_tAlpha("alpha",range(0,1))=1
		[Space(20)]
		[Header(Polygon)]
				_NPSize ("サイズ", Range(0, 2)) = 0
				_NPoly("多角形", Int) = 5
				_NPW("大きさ", Range(0, 2)) = 0
				_NPRotation ("回転", Range(0, 360)) = 0
				_NPFillColor("色（(0,0,0,0)で無色）", Color) = (1,1,1,1)
				_NPEdgeColor("縁の色（(0,0,0,0)で無色）", Color) = (0,0,0,1)
		[Space(20)]
		[Header(Glitch1)]
				_glitch1("グリッチ_1", Range(0, 1)) = 0
				_GlitchTex("グリッチ（必須）", 2D) = "white" {}
				_GUV("UV", int) = 0
				_UVScale("UVScale", range(0,2)) = .5
				_GIntensity("Intensity", range(0, 1)) =0.7
				_NoiseTex("タイミング", 2D) = "white" {}
		[Space(20)]
		[Header(Glitch2)]
				_glitch2("グリッチ_2", Range(0, 1)) = 0		
				_ChromAberrAmountX("色収差X", float) = 0
				_ChromAberrAmountY("色収差Y", float) = .1
				_RightStripesAmount("ストライプノイズ_1", float) = 5
				_RightStripesFill("乱数", range(0, 1)) = 0.7
				_LeftStripesAmount("ストライプノイズ_2", float) = 100
				_LeftStripesFill("乱数", range(0, 1)) = 0.7
				_DisplacementAmount("歪み", vector) = (2, -5, 5, 0)
				_WavyDisplFreq("強さ", float) = 1
		[Space(20)]
				_GlitchToggle("グリッチ切り替え", range(0,1)) = 1
				_GlitchSpeed("速度", float) = 2
		[Space(20)]
		[Header(CinemaScape)]
				_CSEnable("シネマスコープ",range(0,1))=0
				_CSColor("色",color) =(0,0,0,1)
				_CSWidth("幅",range(0,1))=.2
		[Header(UV)]
				_UVWidthy("UVWidth x",range(.01,1))=1
				_UVWidthx("UVWidth y",range(.01,1))=1
		[Space (100)]
		[Header(ShadowPicture)]
				[IntRange]_SP("影絵(0でオン※他機能無効化)", Range(0, 1)) = 1
				_SPTexture2 ("背景テクスチャ", 2D) = "white" {}
				_SPlerp2("テクスチャ混ぜ具合", Range(0, 1)) = 0
				_SPP("背景透明度", Range(0, 1)) = 1
				_SPTexture1 ("オブジェクトテクスチャ", 2D) = "white" {}
				_SPlerp1("テクスチャ混ぜ具合", Range(0, 1)) = 0
				_SPAlpha("オブジェクト透明度", Range(0, 1)) = 1
				_SPcol("シルエットカラー", Color) = (1,1,1,1)
				_SPColor("背景色", color) = (0,0,0,1)
		[Space (3)]
		[Header(ShadowPicture_Option)]
				_Rainbow2("虹色",Range(0,1)) = 0
				_Stlipe("ストライプ",Range(0,1)) = 0
				_Stlipe_c("ストライプ色1", color) = (0,0,0,1)
				_Stlipe_col("ストライプ色2", color) = (1,1,1,1)
		[Space (3)]
		[Header(ShadowPicture_UVOption)]
				_Thick2("太さ",Range(.001,10)) = 1
				_sl2("スライド",float) = 0
				_Autosl2("自動スライド",float) = 0
				_Angle2("角度",float) = 0
				[IntRange]_View("描画方法(1でオン)", range(0,1)) = 0.0
	}
	SubShader
	{
		Tags{
			"RenderType"="Transparent"
			"Queue"="Transparent+12000" 
			"DisableBatching"="True"
			"ForceNoShadowCasting" = "True"
		}        
		Cull [_Cull]


		GrabPass {
            "_Paripi"
			}

		Pass
		{
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest [_Ztest]
        ZWrite [_SP]


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#define PI 3.14159

			sampler2D _Paripi;
			fixed4 color;
			sampler2D _Texture;
			float4 _Texture_ST;
			float _Distortion;
			float _Distortionx,_Distortiony;
			float _Amount;
			float _yure;
			float _yurex,_yurey;
			float _Diff;
			half _RedX,_RedY;
			half _GreenX,_GreenY;
			half _BlueX,_BlueY;
			float _R;float _G;float _B;
			float _bw;
			float _nega;
			float _monno,_cutout;
			float _rr,_gg,_bb;
			sampler2D _ReflectionMap;float4 _ReflectionMap_ST;
            sampler2D _Opacity;float4 _Opacity_ST;
            float _RotationSpeed,_NormalIntensity,_ReflectionValue;
			fixed4 fragColor;
			sampler2D _SecondTex;
			float _Intensity,_sz,_RGBNoise,_SP,_Edge,_SPAlpha;
			float4 _SPcol;float4 _EdgeCol;float _EdgeZ;float _str;
			float _Rainbow;float _sl;float _Angle;float _Thick;float _Autosl;
			sampler2D _SPTexture1;float4 _SPTexture1_ST;float _SPlerp1,_Length;
			float _Scale;
			float _CHROMA,_blur,mono,_NoiseAuto,_Nloop,_NAngle,_View;
			struct appdata
			{
				float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
                float2 uv0 : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
				float4 posWorld : TEXCOORD2;
				float3 pos : TEXCOORD3;
			};
		 	uniform float _FadeIn, _FadeOut;

			half rand(half2 p) {
				return frac(sin(dot(p.xy, half2(12.9898, 78.233))) * 43758.5453);
			}
 
			float noise(fixed2 st)
			{
				fixed2 p = floor(st);
				return rand(p);
			}
			half2 mod(half2 a, half2 b)
			{
				return a - floor(a / b) * b;
			}

			float2 rotate(float angle,float2 uv){
				float2x2 rotate = float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
				float scale = 0.5;
				float2 pivot_uv = float2(0.5, 0.5);
				float2 r = (uv - pivot_uv) * (1 / scale);
				return mul(rotate, r) + pivot_uv;
			}

			float2 RainDrop(v2f i,float2 uv,float size){
				fixed2 u = (i.screenPos.xy / i.screenPos.w),
					 n = tex2D(_SecondTex, u).rg; 
    
				for (fixed r = 4 ; r > 0. ; r--) {
					fixed2 x = _ScreenParams.xy * r * .015;
					fixed2 p = 6.28 * u * x + (n - .5) * 2.,
						 s = sin(p)*size;

					fixed4 d = tex2D(_SecondTex, round(u*x-.25)/x);
					fixed t = (s.x+s.y) * max(0., 1. - frac(_Intensity *_Time.y * (d.b + .1) + d.g) * 2.);;

					if (d.r < (5.-r)*.08 && t > .5) {
						fixed3 v = normalize(-fixed3(cos(p), lerp(.2, 2., t-.5)));
						return u - v.xy * .3;
					}
				}
				return uv;
			}
			float4 Edge(float4 c,sampler2D _Paripi, float2 grabUV,float4 col,float z,float str){
				float zurex = z/_ScreenParams.xy;
                float zurey = z/_ScreenParams.xy;
                float3 xx = tex2D(_Paripi, grabUV  + float2(-zurex, -zurey)) - tex2D(_Paripi, grabUV  + float2(zurex, zurey));
                float3 yy = tex2D(_Paripi, grabUV  + float2(zurex, -zurey)) - tex2D(_Paripi, grabUV  + float2(-zurex, zurey));
			    float3 tex = min(sqrt(xx*xx+yy*yy),1);
				if(tex.r<0.1)tex.r=0;else tex.r=1;
				if(tex.g<0.1)tex.g=0;else tex.g=1;
				if(tex.b<0.1)tex.b=0;else tex.b=1;
				tex*=col.rgb;
				if(z!=0)return float4(tex*str, 1);
				else return c;
			}
			fixed3 shift_col(fixed3 RGB, half3 shift) //https://kido0617.github.io/unity/2015-01-03-shader-hue-shift/ 
													  //より
			{
				fixed3 RESULT = fixed3(RGB);
				float VSU = shift.z*shift.y*cos(shift.x*3.14159265/180);
				float VSW = shift.z*shift.y*sin(shift.x*3.14159265/180);
				
				RESULT.x = (.299*shift.z+.701*VSU+.168*VSW)*RGB.x
					+ (.587*shift.z-.587*VSU+.330*VSW)*RGB.y
					+ (.114*shift.z-.114*VSU-.497*VSW)*RGB.z;
				
				RESULT.y = (.299*shift.z-.299*VSU-.328*VSW)*RGB.x
					+ (.587*shift.z+.413*VSU+.035*VSW)*RGB.y
					+ (.114*shift.z-.114*VSU+.292*VSW)*RGB.z;
				
				RESULT.z = (.299*shift.z-.3*VSU+1.25*VSW)*RGB.x
					+ (.587*shift.z-.588*VSU-1.05*VSW)*RGB.y
					+ (.114*shift.z+.886*VSU-.203*VSW)*RGB.z;
				
				return (RESULT);
			}
			float _HUE,_HueS;
			half _Hue, _Sat, _Val;
			float4 Hue(float4 tex){
				half3 shift = half3(_Hue+_Time.y*_HueS*100, _Sat, _Val);
				tex=float4(shift_col(tex.rgb, shift),1);
				return tex;
			}
			float4 Rainbow(float2 uv,float sl,float angle,float thick){
				float4 c;

				uv=rotate(angle,uv);
				float x = uv.x*thick;
				float a = 3 / PI;
				float b = PI / 3;
				x+=sl;
				x=frac(x/10)*10;
				if (0 <= x && x <  PI / 3.0f) {
					c = float4(1, a * x, 0,1);
				} else if (PI / 3.0f <= x && x < 2 * PI / 3.0f) {
					c = float4(-a * (x - 2 * b), 1, 0,1);
				} else if (2 * PI / 3 <= x && x < PI) {
					c = float4(0, 1, a * (x - 2 * b),1);
				} else if (PI <= x && x < 4 * PI / 3.0f) {
					c = float4(0, -a * (x - 4 * b), 1,1);
				} else if (4 * PI / 3.0f <= x && x < 5 * PI / 3.0f) {
					c = float4(a * (x - 4 * b), 0, 1,1);
				} else {
					c = float4(1, 0, -a * (x - 6 * b),1);
				}
				return c;
			}

			float _NoisePos,_NoiseSize;
			float4 _Npos;float4 _NColor;float _Nw,_Ndis,_RandLine;

			float4 UVnoise(float4 tex,float2 uv){
				half4 col=tex;
				half4 pos = _NoisePos;
				float time=0;
				half duv = uv.x;

				if(_NAngle==1){
				duv=uv.y;
				uv.y=uv.x;
				}
				time=_Time.y*_NoiseAuto;


				fixed4 r=0;
				fixed4 Npos = fixed4(_Npos.x,_Npos.y,_Npos.z,_Npos.w);
				if(_NoiseSize!=0&&_RandLine){
					r.x=frac(noise(_Time.y*Npos.x));
					r.y=frac(noise(_Time.y*Npos.y));
					r.z=frac(noise(_Time.y*Npos.z));
					r.w=frac(noise(_Time.y*Npos.w));
				}
				pos.x+=r.x*frac(_Time.x*1)-frac(_Time.x*2)*r.y;
				pos.y+=r.y*frac(_Time.x*1.2)-frac(_Time.x*1.1)*r.z;
				pos.z+=r.z*frac(_Time.x*1.5)-frac(_Time.x)*r.w;
				pos.w+=r.w*frac(_Time.x*2)-frac(_Time.x)*r.x;
				pos=frac(pos);
				float4 posf = pos +_NoiseSize;
				half uvy=uv.y;
				
				if(_Nloop==1){
					uv.y+=time;
					uv.y=frac(uv.y-1);
					uv.y+=_NoisePos;
					if(uv.y%_Nw>0&& uv.y%_Nw<_NoiseSize)col=_NColor;
				}else{
					uv.y=frac(uvy+time*1);
					if(uv.y> pos.x && uv.y < posf.x)col=_NColor;
					uv.y=frac(uvy+time*1.2);
					if(uv.y> pos.y && uv.y < posf.y)col=_NColor;
					uv.y=frac(uvy+time*1.3);
					if(uv.y> pos.z && uv.y < posf.z)col=_NColor;
					uv.y=frac(uvy+time*1.4);
					if(uv.y> pos.w && uv.y < posf.w)col=_NColor;
				}
				if(duv<_Ndis)tex = lerp(tex,col,_NColor.a);
				return tex;
			}
			float _LineAlpha;
		float4 UVLINE(float4 tex,float2 uv){
				half4 col=tex;
				half4 pos = _NoisePos;
				half time=0;
				half duv = uv.x;

				
				time=_Time.y*_NoiseAuto;
				
				float r = rand(_Time.x);
				pos.x+=_Npos.x;
				pos.y+=_Npos.y;
				pos.z+=_Npos.z;
				pos.w+=_Npos.w;
				pos=frac(pos);
				half4 posf = pos +.5;
				half uvy=uv.y;
					uv.y=frac(uvy+time*1);
					if(uv.y> pos.x && uv.y < posf.x)col=float4(0,0,0,1);					
					uv.y=frac(uvy+time*1.2);
					if(uv.y> pos.y && uv.y < posf.y)col=float4(0,0,0,1);
					uv.y=frac(uvy+time*1.3);
					if(uv.y> pos.z && uv.y < posf.z)col=float4(0,0,0,1);
					uv.y=frac(uvy+time*1.4);
					if(uv.y> pos.w && uv.y < posf.w)col=float4(0,0,0,1);
				if(duv<_Ndis)tex = lerp(tex,col,_NColor.a);
				return tex;
			}


			float _CSizex, _CSizey, _CUVPosx, _CUVPosy;float4 _CColor;

			float4 circle(float2 uv,float4 tex){
				float4 col=tex;uv.y*=_ScreenParams.y/_ScreenParams.x;
	            float x = uv.x+_CUVPosx;
                float y = uv.y+_CUVPosy;
                float dis = sqrt(pow((0.5 - x), 2) + pow((0.5 - y), 2));
                if (dis >_CSizex*_CSizey && dis <_CSizex ) {
						 col = _CColor;
					}
				return lerp(tex,col,_CColor.a);
			}


			float _TSize, _TUVx, _TUVy, _TAlpha;
			float4 _TColor1,_TColor2;

			float4 check(float4 tex,float2 uv){
				uv.y*=_ScreenParams.y/_ScreenParams.x;
				uv+=float2(_TUVx,_TUVy);
				fixed2 v = step(0,sin(_TSize*uv))*.5;
				float4 col = frac(v.x+v.y)*2;
				if(col.r==1)col=_TColor1;
				else col=tex;
				return lerp(tex,col,_TAlpha);
			}
			float _TextSW,_TxUVx,_TxUVy,_Cutoff,_TxSizex,_TxSizey;sampler2D _Text;float4 _Text_ST;
			float _TxSlider,_TxSize,_Txlength;
			float _prog,_tAlpha;
			float4 _tColor;

			float4 Text(float4 tex,float2 uv){
				float scale = _Text_ST.x/_Text_ST.y;
				uv = float2(((_ScreenParams.b/_ScreenParams.a)*uv.x*scale)/_TxSizex,uv.y/_TxSizey)/-1.5 + 0.5;
				uv.x+=_TxUVx;uv.y+=_TxUVy;
				if(_TxSlider>=uv.x-_Txlength&&_TxSlider<=uv.x+_Txlength)uv.y-=length(length(_TxSlider-uv.x)-_Txlength)*_TxSize;
				float4 col = tex2D(_Text,TRANSFORM_TEX(uv,_Text));
				if(_Cutoff>=0){
					if(col.r<=abs(_Cutoff)&&col.g<=abs(_Cutoff)&&col.b<=abs(_Cutoff))tex=col;
				}else{
					if(col.r>=abs(_Cutoff)&&col.g>=abs(_Cutoff)&&col.b>=abs(_Cutoff))tex=col;
				}
				if(_tColor.a>0&&tex.a>0){
					tex.rgb=abs(tex.rgb-_tColor.rgb-1);
				}
					tex.a*=_tAlpha;
				return tex;
			}
			sampler2D _LerpTex;
			float4 _LerpTex_ST;
			fixed _lerptexx,_lerptexy;
			float2 LerpTex(float2 uv){
				return float2(((_ScreenParams.b/_ScreenParams.a)*uv.x)/_lerptexx,uv.y/_lerptexy)/-1.5 + 0.5;
			}

			int _NPoly;
			fixed _NPSize;
			fixed _NPW;
			float _NPRotation;

			fixed4 _NPFillColor;
			fixed4 _NPEdgeColor;

			float4 NPoly(float4 tex,float2 defuv){
				float2 uv = defuv - 0.5;
				uv.y*=_ScreenParams.y/_ScreenParams.x;
				float r = length(uv);
				float theta = atan2(uv.x, uv.y) + _NPRotation + 2.0 * PI;
				theta = theta % (2.0 * PI / _NPoly);

				float r2 = cos(PI / _NPoly) / cos (PI / _NPoly - theta);
				float step1 = step(r, r2 * _NPSize);
				float step2 = step(r, r2 * (_NPSize - _NPW));
				if(step1>0.001){
				tex = step1 * _NPEdgeColor + step2 * (_NPFillColor - _NPEdgeColor);
				}
				return tex;
			}
			float _GIntensity;sampler2D _GlitchTex;
			float4 _GlitchTex_ST;
			sampler2D _NoiseTex;float _glitch1,_UVScale;int _GUV;

			fixed4 glitch1(sampler2D _MainTex, float2 uvScreen,sampler2D _NoiseTex,sampler2D _GlitchTex,float4 tex){
				float2 uv = uvScreen;
				float2 pivot_uv = float2(0.5, 0.5); 
				uv = (uv.xy - pivot_uv) * (1 / _UVScale) + pivot_uv;
				uv.x*=_GlitchTex_ST.w+1;
				uv.y*=_GlitchTex_ST.z+1;
				
				float4 glitch = tex2D(_GlitchTex, uv+_GUV*.1+_GlitchTex_ST);
				uv=frac(uv);
				float thresh = 1.001 - _GIntensity * 1.001;
				float w_d = step(thresh, pow(glitch.z, 2.5)); 
				float w_b = step(thresh, pow(glitch.w, 2.5));
				float w_c = step(thresh, pow(glitch.z, 3.5)); 

				float3 noiseInput = tex2D(_NoiseTex, float2(_Time.x, _Time.x)).rgb;
				float2 redOffset = float2(sin(_Time.x * 0.1), 0);
				float2 greenOffset = float2(0, sin(_Time.x * -0.1));
				float2 blueOffset = float2(sin(_Time.x * -0.1), 0);
					float red=tex.r;
					float green=tex.g;
					float blue=tex.b;
					float2 fixr =  uvScreen + redOffset * step(0.2, noiseInput.r)*w_c;
					float2 fixg =  uvScreen + greenOffset * step(0.2, noiseInput.g)*w_c;
					float2 fixb =  uvScreen + blueOffset * step(0.2, noiseInput.b)*w_c;

				if(fixr.x!=uvScreen.x||fixr.y!=uvScreen.y)red = tex2D(_MainTex, uvScreen + redOffset * step(0.2, noiseInput.r)*w_c).r;
				if(fixg.x!=uvScreen.x||fixg.y!=uvScreen.y)green = tex2D(_MainTex, uvScreen + greenOffset * step(0.2, noiseInput.g)*w_c).g;
				if(fixb.x!=uvScreen.x||fixb.y!=uvScreen.y)blue = tex2D(_MainTex, uvScreen + blueOffset * step(0.2, noiseInput.b)*w_c).b;
				return fixed4(red, green, blue, 1);
			}

			float _glitch2,_glitch3,_ChromAberrAmountX,_ChromAberrAmountY,_DesaturationAmount,_RightStripesAmount,_RightStripesFill,_LeftStripesAmount,_LeftStripesFill,_WavyDisplFreq,_GlitchSpeed,_GlitchToggle;
            fixed4 _DisplacementAmount;
 
			fixed4 glitch2(sampler2D _MainTex, float2 uv,float4 tex){
                fixed2 _ChromAberrAmount = fixed2(_ChromAberrAmountX, _ChromAberrAmountY);
				
				
                fixed4 displAmount = fixed4(0, 0, 0, 0);
                fixed2 chromAberrAmount = fixed2(0, 0);
                float rightStripesFill = 0;
                float leftStripesFill = 0;
				float2 defuv=uv;
				float GlitchSpeed=_Time.y*_GlitchSpeed;
                if (frac(GlitchSpeed) < 0.8) {
                    rightStripesFill = lerp(0, _RightStripesFill, frac(GlitchSpeed) * 2);
                    leftStripesFill = lerp(0, _LeftStripesFill, frac(GlitchSpeed) * 2);
                }
                if (frac(GlitchSpeed) < 0.5) {
                    chromAberrAmount = lerp(fixed2(0, 0), _ChromAberrAmount.xy, frac(GlitchSpeed) * 2);
                }
                if (frac(GlitchSpeed) < 0.33) {
                    displAmount = lerp(fixed4(0,0,0,0), _DisplacementAmount, frac(GlitchSpeed) * 3);
                }
 
                float stripesRight = floor(uv.y * _RightStripesAmount);
                stripesRight = step(lerp(_RightStripesFill,rightStripesFill,_GlitchToggle), rand(float2(stripesRight, stripesRight)));
 
                float stripesLeft = floor(uv.y * _LeftStripesAmount);
                stripesLeft = step(lerp(_LeftStripesFill,leftStripesFill,_GlitchToggle), rand(float2(stripesLeft, stripesLeft)));
				
                fixed4 wavyDispl = lerp(fixed4(1,0,0,1), fixed4(0,1,0,1), (sin(uv.y * _WavyDisplFreq) + 1) / 2);
 
                fixed2 displUV = (lerp(_DisplacementAmount.xy,displAmount.xy,_GlitchToggle) * stripesRight) - (lerp(_DisplacementAmount.xy,displAmount.xy,_GlitchToggle) * stripesLeft);
                displUV += (lerp(_DisplacementAmount.zw,displAmount.zw,_GlitchToggle) * wavyDispl.r) - (lerp(_DisplacementAmount.zw,displAmount.zw,_GlitchToggle) * wavyDispl.g);
				uv+=displUV;
				uv=frac(uv);
				float chromR;
				float chromG;
				float chromB;
                if(defuv.x!=(uv + _ChromAberrAmount).x&&defuv.y!=(uv + _ChromAberrAmount).y)chromR = tex2D(_MainTex, uv + _ChromAberrAmount).r;
                else chromR=tex.r;
				if(defuv.x!=uv.x&&defuv.y!=uv.y)chromG = tex2D(_MainTex, uv).g;
                else chromG=tex.g;
				if(defuv.x!=(uv - _ChromAberrAmount).x&&defuv.y!=(uv - _ChromAberrAmount).y)chromB = tex2D(_MainTex, uv - _ChromAberrAmount).b;
                else chromB=tex.b;
				fixed4 finalCol = fixed4(chromR, chromG, chromB, 1);
                 
                return finalCol;
            }

			float _CSEnable,_CSWidth;float4 _CSColor;

			float4 CinemaScape(float4 tex,float2 uv){
				if(uv.y>1-(_CSWidth/2)){

					tex=_CSColor;

				}
				if(uv.y*2<_CSWidth){
					tex=_CSColor;

				}
				return tex;
			}

			int _DiffRange;
			float _UVWidthx,_UVWidthy;
			fixed _lerptextgl,_vignette;
			fixed4 _vignetteColor;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv0 =v.uv0;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.screenPos = ComputeGrabScreenPos(o.vertex);

				o.pos = mul(UNITY_MATRIX_V,float4((o.posWorld.xyz-_WorldSpaceCameraPos),0)).xyz;
				float4 grabPos = ComputeGrabScreenPos(UnityViewToClipPos(float4(0,0,1,1)));
				grabPos *= o.screenPos.w / grabPos.w;
				_Scale=max(-.999,_Scale);
				_Scale = 2-2/(_Scale+1);
				float2 UVWidth = float2(_UVWidthx,_UVWidthy);
				float2 r = (o.screenPos.xy - grabPos.xy) * (1 / UVWidth);
				o.screenPos.xy = r + grabPos.xy;
				o.screenPos.xy = lerp(o.screenPos.xy, grabPos.xy,_Scale);

				
				return o;
			}
			float4 frag (v2f i) : SV_Target
			{
				float2 Suv = i.screenPos.xy / i.screenPos.w;
				float2 uv = i.pos.xy/i.pos.z;
				float2 lerpTexUV=lerp(0,length(LerpTex(uv)-.5),_lerptextgl);
				fixed vignette=length(lerpTexUV.xy);
				Suv.y/=_ScreenParams.x/_ScreenParams.y;
				float2 grabUV = (i.screenPos.xy / i.screenPos.w);
				float2 defUV = grabUV;
				grabUV=RainDrop(i,grabUV,_sz);
				float4 nm = tex2D(_ReflectionMap,i.uv0);
				if(_NormalIntensity==0)nm=1;
                float3 _ReflectionMap_var = UnpackNormal(tex2D(_ReflectionMap,grabUV));
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(i.uv0, _Opacity));
				float3 normalLocal = lerp(float3(0,0,1),_ReflectionMap_var.rgb,_NormalIntensity);
				const float3 mono = float3(0.298912, 0.586611, 0.114478);
				float3 bw = _bw; 
				grabUV +=((_ReflectionMap_var.rgb.rg*(_NormalIntensity*_ReflectionValue))*_Opacity_var.r.r);
				_RedX*=nm.a;_RedX*=nm.a;
				_GreenX*=nm.a;_GreenY*=nm.a;
				_BlueX*=nm.a;_BlueY*=nm.a;
				_Distortion*=nm.a;
				_Diff*=nm.a;
				_R*=nm.a;_G*=nm.a;_B*=nm.a;
				_nega*=nm.a;bw*=nm.a;
				grabUV += _Distortion*0.005*cos(_Time*_yure*1000);
				grabUV.x +=_Distortionx*0.005*cos(_Time*_yurex*1000);
				grabUV.y +=_Distortiony*0.005*cos(_Time*_yurey*1000);
				grabUV +=  _Distortion*0.01;
				grabUV.x +=  _Distortionx*0.01;
				grabUV.y +=  _Distortiony*0.01;
				grabUV=frac(grabUV);
				float2 Diff = float2(_Diff,-_Diff);
				fixed4 tex;
				for(int j=0;j<=_DiffRange;j++){
					tex += tex2D(_Paripi, grabUV - _Diff*j*lerp(1,lerpTexUV,_lerptextgl)) + tex2D(_Paripi, grabUV + _Diff*j*lerp(1,lerpTexUV,_lerptextgl));
					tex += tex2D(_Paripi, grabUV - Diff*j*lerp(1,lerpTexUV,_lerptextgl)) + tex2D(_Paripi, grabUV + Diff*j*lerp(1,lerpTexUV,_lerptextgl));
				}
				tex/=j*2;
				tex.ra*=_rr;tex.ga*=_gg;tex.ba*=_bb;
				tex = Edge(tex,_Paripi,grabUV,_EdgeCol,_EdgeZ,_str);
			if(_CHROMA!=0){
                #define RED(x,y) tex2D(_Paripi,grabUV + float2(_RedX*x, _RedY*y)*_CHROMA).r
				#define GREEN(x,y) tex2D(_Paripi,grabUV + float2(_GreenX*x, _GreenY*y)*_CHROMA).g
                #define BLUE(x,y) tex2D(_Paripi,grabUV + float2(_BlueX*x, _BlueY*y)*_CHROMA).b

                tex.r += RED(1,-1);
                tex.r -= RED(-1,1);
                tex.g += GREEN(1,-1);
                tex.g -= GREEN(-1,1);
                tex.b += BLUE(1,-1);
                tex.b -= BLUE(-1,1);
				if(_blur==1){
					tex.r += RED(0.5,-0.5);
					tex.r -= RED(-0.5,0.5);
					tex.g += GREEN(0.5,-0.5);
					tex.g -= GREEN(-0.5,0.5);
					tex.b += BLUE(0.5,-0.5);
					tex.b -= BLUE(-0.5,0.5);
				}
			}
				tex.r+=_R*2;tex.g+=_G*2;tex.b+=_B*2;
				tex.rgb=abs(_nega*2 - tex.rgb);
				tex.r += rand(grabUV + float2(123 + _Time.y, 0))* _RGBNoise;
				tex.g += rand(grabUV + float2(123 + _Time.y, 1))* _RGBNoise;
				tex.b += rand(grabUV + float2(123 + _Time.y, 2))* _RGBNoise;

			
            tex = fixed4(lerp(tex.rgb, 0,(0.0*_Opacity_var.r)),2);
			tex.ra*=_rr;tex.ga*=_gg;tex.ba*=_bb;

			if(!_SP){
				tex.rgb=1;
			}

            UNITY_APPLY_FOG(i.fogCoord, tex);
			if(nm.a <0.1){
				discard;
			}
			tex/=2;	
			if(!_SP){
				if(_View){
					defUV =    normalize(i.posWorld-_WorldSpaceCameraPos);
				}
				fixed4 tex2 = tex2D(_SPTexture1,defUV);
				tex=lerp(_SPcol,tex2,_SPlerp1);
				tex.a=_SPAlpha;
			}else{
				if(_Rainbow>0)tex = lerp(tex,Rainbow(defUV,_sl+(_Autosl*_Time.y),_Angle,_Thick),_Rainbow);	
				if(_HUE>0)tex = lerp(tex,Hue(tex),_HUE);	
				tex=circle(grabUV,tex);
				tex=check(tex,grabUV);
				if(_NPSize!=0)tex=NPoly(tex,grabUV);
				if(_TextSW!=0)tex=lerp(tex,Text(tex,uv),_TextSW);
				if(_vignette!=0)tex.rgb=lerp(tex.rgb,vignette*_vignetteColor,vignette*_vignette);
				if(_glitch1!=0)tex=lerp(tex,glitch1(_Paripi,grabUV,_NoiseTex,_GlitchTex,tex),_glitch1);
				if(_glitch2!=0)tex=lerp(tex,glitch2(_Paripi,grabUV,tex),_glitch2);
				tex=UVnoise(tex,defUV);
				if(_LineAlpha!=0)tex=lerp(tex,UVLINE(tex,defUV),_LineAlpha);
				if(_CSEnable!=0)tex=lerp(tex,CinemaScape(tex,defUV),_CSEnable);
			}
			if(_bw>0)tex=lerp(tex,float4(1,1,1,1),_bw);
			if(_bw<0)tex=lerp(tex,float4(0,0,0,1),abs(_bw));
			if(_monno>0) tex.rgb=lerp(tex.rgb,dot(tex.rgb,mono),_monno);
			if(abs(length(mul(unity_ObjectToWorld,float4(0,0,0,1))-_WorldSpaceCameraPos))<_Length){
				return tex;
			}else{
				discard;
			return 0;
			}
		}
			ENDCG	
	}	
	Pass{
		Blend One OneMinusSrcAlpha 
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#define PI 3.141592	

		struct appdata
		{
			float4 vertex : POSITION;
		};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 pos : TEXCOORD2;
			};
				float _View;
				float4 _SPColor;float _SP;
				float _Rainbow2;float _sl2;float _Angle2;float _Thick2;float _Autosl2;
				float _Stlipe;float _Angle_stlipe;float4 _Stlipe_c;float4 _Stlipe_col;
				sampler2D _SPTexture2;float4 _SPTexture2_ST;float _SPlerp2;
				sampler2D _Paripi;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeGrabScreenPos(o.vertex);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.pos = v.vertex.xyz;
				return o;
			}

			float2 rotate(float angle,float2 uv){
				float2x2 rotate = float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
				float scale = 0.5;
				float2 pivot_uv = float2(0.5, 0.5);
				float2 r = (uv - pivot_uv) * (1 / scale);
				return mul(rotate, r) + pivot_uv;
			}

			float4 Rainbow(float2 uv,float sl,float angle,float thick){
				float4 c;
				uv=rotate(angle,uv);
				float x = uv.x*thick;
				float a = 3 / PI;
				float b = PI / 3;
				x+=sl;				
				x=frac(x/10)*10;
				if (0 <= x && x <  PI / 3.0f) {
					c = float4(1, a * x, 0,1);
				} else if (PI / 3.0f <= x && x < 2 * PI / 3.0f) {
					c = float4(-a * (x - 2 * b), 1, 0,1);
				} else if (2 * PI / 3 <= x && x < PI) {
					c = float4(0, 1, a * (x - 2 * b),1);
				} else if (PI <= x && x < 4 * PI / 3.0f) {
					c = float4(0, -a * (x - 4 * b), 1,1);
				} else if (4 * PI / 3.0f <= x && x < 5 * PI / 3.0f) {
					c = float4(a * (x - 4 * b), 0, 1,1);
				} else {
					c = float4(1, 0, -a * (x - 6 * b),1);
				}
				return c;
			}

			float4 stlipe(float2 uv,float angle,float4 c,float4 col,float thick,float slide){
				uv=rotate(angle,uv);
				uv.x+=slide;
				uv.x=frac(uv.x/10)*10;
				float4 tex = step(0, sin(thick*uv.x));
				tex.r=tex.r==1?c.r:col.r;
				tex.g=tex.g==1?c.g:col.g;
				tex.b=tex.b==1?c.b:col.b;
				return tex;
			}
			float _SPP,_Length;
			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 uv = (i.screenPos.xy / i.screenPos.w);
				if(_View==1){
				uv =    normalize(i.posWorld-_WorldSpaceCameraPos);
				}
				fixed4 tex = tex2D(_Paripi, uv);
				fixed4 tex2 = tex2D(_SPTexture2, uv);
				fixed4 col = _SPColor;
				col = lerp(col,Rainbow(uv,_sl2+(_Autosl2*_Time.y),_Angle2,_Thick2),_Rainbow2);	
				col = lerp(col,stlipe(uv,_Angle2,_Stlipe_c,_Stlipe_col,_Thick2*10,_sl2+(_Autosl2*_Time.y)),_Stlipe);
				col = lerp(tex,lerp(col,tex2,_SPlerp2),_SPP);
				if(!_SP&&(abs(length(mul(unity_ObjectToWorld,float4(0,0,0,1))-_WorldSpaceCameraPos))<_Length))return saturate(col);
				else{
					discard;
					return 0;
				}
			}
			ENDCG
		}
	}
        FallBack "Diffuse"
}
