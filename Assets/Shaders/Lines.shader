Shader "Unlit/Lines"
{
	Properties
	{		
		_Noise ( "noise", 2D ) = "white"{}
		/* colors */
		_ColorA	( "colorA", COLOR) = (0,0,0,1)
		_ColorB	( "colorB", COLOR) = (0,0,0,1)
		_ColorC	( "colorC", COLOR) = (0,0,0,1)
		/* data */
		_Length	( "Length", Range(0,10)) = 1
		_Values	( "values", Vector) = (0,0,0,0)
		_Fade	( "fade",	Range(0,0.7)) = 0
	}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader
		{
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

				fixed4 	  _ColorA, _ColorB, _ColorC;
				fixed4 	  _Values;
				half 	  _Length, _Sum, _Fade;
				sampler2D _Noise;
				
				v2f vert (appdata v)
				{
					v2f o;
					o.uv = v.uv;
					o.vertex = UnityObjectToClipPos(v.vertex);
					return o;
				}
				
				fixed4 frag (v2f i) : SV_Target
				{	
					half uvY = i.uv.y - 0.5;
					half uv =  i.uv.x + 0.01 + (uvY * uvY * 0.03); //i.uv.x + ( i.uv.y / (7.5 * _Length ));
					fixed4 col = lerp( _ColorA, lerp(_ColorB, _ColorC, 1-step( uv, _Values.y + _Values.x)), 1-step( uv, _Values.x) );
															
					half power = (abs(0.5 - i.uv.x) - (_Fade - 0.2)) * 10;					
					col.a = tex2D(_Noise, i.uv).r * power;
					return col;
				}
				ENDCG
			}
		}
	}
}
