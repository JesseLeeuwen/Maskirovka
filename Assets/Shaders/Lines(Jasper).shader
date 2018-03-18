Shader "Unlit/Lines(Jasper)"
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
		_LineWidth ("Width of Line", float) = 0
		_LineColor	( "Line Color", COLOR) = (0,0,0,1)
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

			fixed4 	_ColorA, _ColorB, _ColorC, _LineColor;
			fixed4 	_Values;
			half 	_Length, _Sum;
			half	_LineWidth;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{	
				if (i.uv.y<_LineWidth || i.uv.y>1-_LineWidth){
					return _LineColor;
				};	
				half uv = i.uv.x + ( i.uv.y / (7.5 * _Length ));
				fixed4 col = lerp( _ColorA, lerp(_ColorB, _ColorC, 1-step( uv, _Values.y + _Values.x)), 1-step( uv, _Values.x) );	
				return col;
			}
			ENDCG
		}
	}
}
