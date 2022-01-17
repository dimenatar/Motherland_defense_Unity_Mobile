Shader "Custom/Snow"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_AlphaThreshold("Alpha Threshold", Range(0, 10)) = 0.01
		_SnowLevel("Snow Level", Range(0, 7)) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma exclude_renderers d3d11_9x
			#pragma exclude_renderers d3d9
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			float _AlphaThreshold;
			int _SnowLevel;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float4 _MainTex_TexelSize;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			int isSnow (float2 texcoord, fixed4 col){
				[loop]
				for (int i = 1; i <= _SnowLevel; i++)
                {
					//int d = 1;
					//if (random(texcoord)>0.5) d = -1;
					float2 pixelDownTexCoord = texcoord - float2(0, i*_MainTex_TexelSize.y);
					//if (i==_SnowLevel && i > 1 && random(texcoord)>0.5) pixelDownTexCoord.x += _MainTex_TexelSize.x;
					fixed pixelDownAlpha = tex2Dgrad(_MainTex, pixelDownTexCoord, ddx(texcoord), ddy(texcoord)).a;
					//if (i==_SnowLevel && i > 1 && random(texcoord)>0.5) return 0;
					if (pixelDownAlpha==0) return 1;
				}
				return 0;
			}

			int isSnow2 (float2 texcoord){
				[loop]
				for (int i = 1; i <= 7; i++)
                {
					float2 pixelDownTexCoord = texcoord - float2(0, i*_MainTex_TexelSize.y);
					fixed pixelDownAlpha = tex2Dgrad(_MainTex, pixelDownTexCoord, ddx(texcoord), ddy(texcoord)).a;
					if (i>_SnowLevel) break;
					if (pixelDownAlpha==1) return 1;
				}
				return 0;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				//if (c.a==0) c.a = 1;
                if (c.a == 0 && isSnow2(IN.texcoord) == 1){
					c.rgb = 1;
					c.a = 1;
				}
				//c.r = IN.texcoord.y;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
