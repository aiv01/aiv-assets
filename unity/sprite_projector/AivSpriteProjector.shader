Shader "AIV/SpriteProjector" {
	Properties {
		_Sprite ("Sprite", 2D) = "white" {}
	}
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvSprite : TEXCOORD0;
				float4 pos : SV_POSITION;
			};
			
			float4x4 _Projector;

			sampler2D _Sprite;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvSprite = mul (_Projector, vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 color = tex2Dproj (_Sprite, UNITY_PROJ_COORD(i.uvSprite));
				return color;
			}
			ENDCG
		}
	}
}
