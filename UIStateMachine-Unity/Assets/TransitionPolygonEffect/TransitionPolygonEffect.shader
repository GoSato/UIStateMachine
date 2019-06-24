Shader "Custom/Transition Polygon Effect"
{
	Properties
	{
		_Color ("Color", Color) = (0, 0, 0, 1)
        _Threshold ("Thresold", Range(-1.0, 2.0)) = -1.0
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
			#pragma geometry geom
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _Color;
            fixed _Threshold;
			int _Strength;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct g2f
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
			
			appdata vert (appdata v)
			{
				return v;
			}

			// ジオメトリシェーダー
			[maxvertexcount(3)]
			void geom (triangle appdata input[3], inout TriangleStream<g2f> stream)
			{
				// カメラとポリゴンの重心の距離
				float3 center = (input[0].vertex + input[1].vertex + input[2].vertex) / 3;
				float4 worldPos = mul(unity_ObjectToWorld, float4(center, 1.0));
				float3 dist = length(_WorldSpaceCameraPos - worldPos);

				// 法線を計算
				float3 vec1 = input[1].vertex - input[0].vertex;
				float3 vec2 = input[2].vertex - input[0].vertex;
				float3 normal = normalize(cross(vec1, vec2));
	
				// 正面方向 : dot = 1.0, 真後方向 : dot = -1.0
				// これらが0.0から1.0に変化するように_Thresholdで値を加算する
                fixed destruction = dot(fixed3(0, 0, 1), normal) + _Threshold;

				// 加算の過程で値が0.0から1.0の範囲を外れるの防ぐためにclampでおさめる
                destruction = clamp(destruction, 0.0, 1.0);

				// 値の変化の調整として累乗を使用
                destruction = pow(destruction, _Strength);

				fixed random = rand(center.xy);
				fixed3 random3 = random.xxx;

				[unroll]
				for(int i = 0; i < 3; i++)
				{
					appdata v = input[i];
					g2f o;
					// 重心に向かってスケール
				    v.vertex.xyz += (center - input[i].vertex.xyz) * destruction; 
                    o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.color = lerp(_Color, _Color + fixed4(random3, 1.0), destruction);
					stream.Append(o);
				}
				stream.RestartStrip();
			}
			
			fixed4 frag (g2f i) : SV_Target
			{
				fixed4 col = i.color;
				return col;
			}
			ENDCG
		}
	}
	FallBack "Unlit/Color"
}
