Shader "Custom/Transition Polygon For Mobile"
{
	Properties
	{
		_Color ("Color", Color) = (0, 0, 0, 1)
        _Threshold ("Threshold", Range(-1.0, 2.0)) = -1.0
		_Strength ("Strength", int) = 10
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType"="Opaque"
		}
        Cull Front

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _Color;
            fixed _Threshold;
			int _Strength;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float2 id : TEXCOORD1;
				float3 center : TEXCOORD2;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			// ランダムな値を返す
			float rand(float2 seed)
			{
				return frac(sin(dot(seed.xy, float2(12.9898, 78.233))) * 43758.5453);
			}
			
			v2f vert (appdata v)
			{
				v2f o;

				float3 center = v.center;
				float3 normal = v.normal;

				float4 worldPos = mul(unity_ObjectToWorld, float4(center, 1.0));

				// 正面方向 : dot = 1.0, 真後方向 : dot = -1.0
				// これらが0.0から1.0に変化するように_Thresholdで値を加算する
                fixed destruction = dot(fixed3(0, 0, 1), normal) + _Threshold;

				// 加算の過程で値が0.0から1.0の範囲を外れるの防ぐためにclampでおさめる
                destruction = clamp(destruction, 0.0, 1.0);

				// 値の変化の調整として累乗を使用
                destruction = pow(destruction, _Strength);

				fixed random = rand(center.xy);
				fixed3 random3 = random.xxx;

				// 重心に向かってスケール
				v.vertex.xyz += (center - v.vertex.xyz) * destruction; 
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = lerp(_Color, _Color + fixed4(random3, 1.0), destruction);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = i.color;
				return col;
			}
			ENDCG
		}
	}
	FallBack "Unlit/Color"
}
