Shader "Sprites/SnowLit"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_SnowLevel("Snow Level", Range(0, 7)) = 1
		_SnowColor ("Snow Color", Color) = (1,1,1,1)
		_Color ("Tint", Color) = (1,1,1,1)
		_AlphaThreshold("Alpha Threshold", Range(0, 10)) = 0.01
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

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
        #pragma multi_compile_local _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #include "UnitySprites.cginc"

        struct Input
        {
            float2 uv_MainTex;
            fixed4 color;
        };

		float4 _MainTex_TexelSize;
		fixed4 _SnowColor;
		float _AlphaThreshold;
		int _SnowLevel;

		float random (float2 uv)
		{
			return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
		}

		int isSnow2 (float2 texcoord, float col){
			[loop]
			for (int i = 1; i <= 7; i++)
			{
				float2 pixelDownTexCoord = texcoord - float2(0, (i-step(0.5, col))*_MainTex_TexelSize.y);
				fixed pixelDownAlpha = tex2Dgrad(_MainTex, pixelDownTexCoord, ddx(texcoord), ddy(texcoord)).a;
				if (i>_SnowLevel) break;
				if (pixelDownAlpha==1) return 1;
			}
			return 0;
		}

        void vert (inout appdata_full v, out Input o)
        {
            v.vertex = UnityFlipSprite(v.vertex, _Flip);

            #if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = v.color * _Color * _RendererColor;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = SampleSpriteTexture (IN.uv_MainTex) * IN.color;
			if (c.a == 0 && isSnow2(IN.uv_MainTex, c.r) == 1){
				c = _SnowColor;
			}
            o.Albedo = c.rgb * c.a;
            o.Alpha = c.a;
        }
        ENDCG
    }

Fallback "Transparent/VertexLit"
}
