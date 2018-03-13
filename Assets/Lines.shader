Shader "Unlit/Lines"
{
	Properties
	{		
		/* colors */
		_ColorA	( "colorA", COLOR) = (0,0,0,1)
		_ColorB	( "colorB", COLOR) = (0,0,0,1)
		_ColorC	( "colorC", COLOR) = (0,0,0,1)
		/* data */
		_Length	( "Length", Range(0,10)) = 1
		_Sum	( "connection sum", Range(0, 100)) = 0
		_Values	( "values", Vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			fixed4 	_ColorA, _ColorB, _ColorC;
			fixed4 	_Values;
			half 	_Length, _Sum;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{				
				half _Sum = _Values.x + _Values.y + _Values.z;
				half a = 1 - (_Values.x / _Sum);
				half b = 1 - (_Values.y / _Sum);
				half c = 1 - _Values.z / _Sum;
				
				half sum = a + b + c;
				a /= sum;
				b /= sum;

				half uv = i.uv.x + ( i.uv.y / (7.5 * _Length ));
				fixed4 col = lerp( _ColorA, lerp(_ColorB, _ColorC, 1-step( uv, b + a)), 1-step( uv, a) );
				return col;
			}
			ENDCG
		}
	}
}
