// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/GlobalFog" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "black" {}
}

CGINCLUDE

	#include "UnityCG.cginc"
	#include "Lighting.cginc"

	uniform sampler2D _MainTex;

	uniform sampler2D_float _CameraDepthTexture;
	
	// x = fog height
	// y = FdotC (CameraY-FogHeight)
	// z = k (FdotC > 0.0)
	// w = a/2
	uniform float4 _HeightParams;
	
	// x = start distance
	uniform float4 _DistanceParams;
	
	int4 _SceneFogMode; // x = fog mode, y = use radial flag
	float4 _SceneFogParams;
	#ifndef UNITY_APPLY_FOG
	half4 unity_FogColor;
	half4 unity_FogDensity;
	#endif	

	uniform float4 _MainTex_TexelSize;
	
	// for fast world space reconstruction
	uniform float4x4 _FrustumCornersWS;
	uniform float4 _CameraWS;

//	uniform sampler2D _Skybox;
	uniform float _Size;
	uniform float _Intensity;
	uniform float _Falloff;
	uniform float _ColBoost;

	//preetham properties
	uniform float turbid, worldscale, reileigh;
	uniform float luminance, bias, lumamount, contrast;
	/*
	float mieCoefficient = 0.053;
	float mieDirectionalG = 0.75;

	// constants for atmospheric scattering
	const float e = 2.71828182845904523536028747135266249775724709369995957;
	const float pi = 3.14159265358979;

	const float n = 1.0003; // refractive index of air
	const float N = 2.545E25; // number of molecules per unit volume for air at
						// 288.15K and 1013mb (sea level -45 celsius)
	const float pn = 0.035;	// depolatization factor for standard air

	// wavelength of used primaries, according to preetham
	const float3 lambda = float3(680E-9, 550E-9, 450E-9);

	// mie stuff
	// K coefficient for the primaries
	const float3 K = float3(0.686, 0.678, 0.666);
	const float v = 4.0;

	// optical length at zenith for molecules
	const float rayleighZenithLength = 8.4E3;
	const float mieZenithLength = 1.25E3;
	const float3 up = float3(0.0, 0.0, 1.0);

	const float E = 1000.0;
	const float sunAngularDiameterCos = 0.999956676946448443553574619906976478926848692873900859324;

	// earth shadow hack
	const float cutoffAngle = 3.141592653589793238462643383279502884197169/2.0;
	const float steepness = 0.5;
	*/

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 uv_depth : TEXCOORD1;
		float4 interpolatedRay : TEXCOORD2;
	};
	
	v2f vert (appdata_img v)
	{
		v2f o;
		half index = v.vertex.z;
		v.vertex.z = 0.1;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		o.uv_depth = v.texcoord.xy;
		
		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			o.uv.y = 1-o.uv.y;
		#endif				
		
		o.interpolatedRay = _FrustumCornersWS[(int)index];
		o.interpolatedRay.w = index;
		
		return o;
	}
	
	// Applies one of standard fog formulas, given fog coordinate (i.e. distance)
	half ComputeFogFactor (float coord)
	{
		float fogFac = 0.0;
		if (_SceneFogMode.x == 1) // linear
		{
			// factor = (end-z)/(end-start) = z * (-1/(end-start)) + (end/(end-start))
			fogFac = coord * _SceneFogParams.z + _SceneFogParams.w;
		}
		if (_SceneFogMode.x == 2) // exp
		{
			// factor = exp(-density*z)
			fogFac = _SceneFogParams.y * coord; fogFac = exp2(-fogFac);
		}
		if (_SceneFogMode.x == 3) // exp2
		{
			// factor = exp(-(density*z)^2)
			fogFac = _SceneFogParams.x * coord; fogFac = exp2(-fogFac*fogFac);
		}
		return saturate(fogFac);
	}

	// Distance-based fog
	float ComputeDistance (float3 camDir, float zdepth)
	{
		float dist; 
		if (_SceneFogMode.y == 1)
			dist = length(camDir);
		else
			dist = zdepth * _ProjectionParams.z;
		// Built-in fog starts at near plane, so match that by
		// subtracting the near value. Not a perfect approximation
		// if near plane is very large, but good enough.
		dist -= _ProjectionParams.y;
		return dist;
	}

	// Linear half-space fog, from https://www.terathon.com/lengyel/Lengyel-UnifiedFog.pdf
	float ComputeHalfSpace (float3 wsDir)
	{
		float3 wpos = _CameraWS + wsDir;
		float FH = _HeightParams.x;
		float3 C = _CameraWS;
		float3 V = wsDir;
		float3 P = wpos;
		float3 aV = _HeightParams.w * V;
		float FdotC = _HeightParams.y;
		float k = _HeightParams.z;
		float FdotP = P.y-FH;
		float FdotV = wsDir.y;
		float c1 = k * (FdotP + FdotC);
		float c2 = (1-2*k) * FdotP;
		float g = min(c2, 0.0);
		g = -length(aV) * (c1 - g * g / abs(FdotV+1.0e-5f));
		return g;
	}

	float3 totalRayleigh(float3 lambda)
	{
		return (8.0 * pow(3.14159265358979, 3.0) * pow(pow(1.0003, 2.0) - 1.0, 2.0) * (6.0 + 3.0 * 0.035)) / (3.0 * 2.545E25 * pow(lambda, float3(4.0,4.0,4.0)) * (6.0 - 7.0 * 0.035));
	}

	float rayleighPhase(float cosTheta)
	{	 
	//	return (3.0 / (16.0*pi)) * (1.0 + pow(cosTheta, 2.0));
	//	return (1.0 / (3.0*pi)) * (1.0 + pow(cosTheta, 2.0));
		return (3.0 / 4.0) * (1.0 + pow(cosTheta, 2.0));
	}

	float3 totalMie(float3 lambda, float3 K, float T)
	{
		float c = (0.2 * T ) * 10E-18;
		return 0.434 * c * 3.14159265358979 * pow((2.0 * 3.14159265358979) / float3(0.000005804542996261093,0.000013562911419845635,0.00003026590629238531), float3(4.0 - 2.0,4.0 - 2.0,4.0 - 2.0)) * K;
	}

	float hgPhase(float cosTheta, float g)
	{
	        return (1.0 / (4.0 * 3.14159265358979)) * ((1.0 - pow(g, 2.0)) / pow(1.0 - 2.0 * g * cosTheta + pow(g, 2.0), 1.5));
	}

	float sunIntensity(float zenithAngleCos)
	{
		return 1000.0 * max(0.0, 1.0 - exp(-(((3.14159265358979/2.0) - acos(zenithAngleCos))/0.5)));
	}

	float logLuminance(float3 c)
	{
		return log(c.r * 0.2126 + c.g * 0.7152 + c.b * 0.0722);
	}

	float3 tonemap(float3 hdr)
	{
    float Y = logLuminance(hdr);

	float low = exp(((Y*lumamount+(1.0-lumamount))*luminance) - bias - contrast/2.0);
	float high = exp(((Y*lumamount+(1.0-lumamount))*luminance) - bias + contrast/2.0);

	float3 ldr = (hdr.rgb - low) / (high - low);
	return float3(ldr);
	}


	half4 ComputeFog (v2f i, bool distance, bool height, bool AffectSkybox, bool Scattering) : SV_Target
	{
		half4 sceneColor = tex2D(_MainTex, i.uv);
//		half4 Sky = tex2D(_Skybox, i.uv);
		
		// Reconstruct world space position & direction
		// towards this screen pixel.
		float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv_depth);
		float dpth = Linear01Depth(rawDepth);
		float4 wsDir = dpth * i.interpolatedRay;
		float4 wsPos = _CameraWS + wsDir;

		float3 sunDir = normalize(-_WorldSpaceLightPos0.xyz);
		
		float cosTheta = dot(normalize(wsPos - _CameraWS), sunDir);
		float mPhase = hgPhase(cosTheta, -_Size)*_Intensity;

		
		//preetham 
		float reileighCoefficient = (reileigh*worldscale)+1.0-clamp((wsPos.z+50.0)/20.0,0.0,1.0)*5.0;
		float turbidity = (turbid*worldscale)+1.0-clamp((wsPos.z+50.0)/20.0,0.0,1.0)*5.0;
		float3 lambda = float3(0.000005804542996261093,0.000013562911419845635,0.00003026590629238531);
		float3 K = float3(0.686, 0.678, 0.666);
		float mieCoefficient = 0.053;
		float3 up = float3(0.0,0.0,1.0);
		float rayleighZenithLength = 8.4E3;
		float mieZenithLength = 1.25E3;

		// extinction (absorbtion + out scattering)
		// rayleigh coefficients
		float3 betaR = totalRayleigh(lambda) * reileighCoefficient;

		// mie coefficients
		float3 betaM = totalMie(lambda, K, turbidity) * mieCoefficient;

		// optical length
		// cutoff angle at 90 to avoid singularity in next formula.
		float sunAngle = acos(max(0.0, dot(up, sunDir)));
		// FIXME: remove code duplication, extract function.

		float sR = rayleighZenithLength / (cos(sunAngle) + 0.15 * pow(93.885 - ((sunAngle * 180.0) / 3.14159265358979), -1.253));
		float sM = mieZenithLength / (cos(sunAngle) + 0.15 * pow(93.885 - ((sunAngle * 180.0) / 3.14159265358979), -1.253));

		// combined extinction factor	
		float3 Fex = exp(-(betaR * sR + betaM * sM));

		//// in scattering

		//float cosTheta = 1.0; // i.e. starring at the sun

		float rPhase = rayleighPhase(cosTheta);
		float3 betaRTheta = float3(betaR * rPhase).xyz;

		//float mPhase = hgPhase(cosTheta, -_Size);
		float3 betaMTheta = float3(betaM * mPhase).xyz;
		float SD = dot(sunDir, up);
		//float3 Lin = float3(sunIntensity(dot(sunDir, up))) * ((betaRTheta + betaMTheta) / (betaR + betaM)) * (1.0 - Fex);
		float3 Lin = float3((betaRTheta + betaMTheta) / (betaR + betaM)).xyz * (1.0 - Fex);
		float3 sunI = (sunIntensity(SD));
		float3 sunEGround = tonemap((Fex + Lin));


		// Compute fog distance
		float g = _DistanceParams.x;
		if (distance)
			g += ComputeDistance (wsDir, dpth);
		if (height)
			g += ComputeHalfSpace (wsDir);

		// Compute fog amount
		half fogFac = ComputeFogFactor (max(0.0,g));
		half fogFacSky = ComputeFogFactor (max(0.0,g));
		// Do not fog skybox
		if (rawDepth >= 0.999999)
			fogFac = 1.0;
		if (AffectSkybox)
			fogFac = fogFacSky;
		
//		return fogFac; // for debugging
		
		float4 fogCol = unity_FogColor;
		float4 boost = (_LightColor0 * _ColBoost) ;
		if(Scattering)
			//fogCol = (((_LightColor0 + boost * boost)*mPhase)*_Intensity)+unity_FogColor;
			fogCol.xyz = fogCol+sunEGround;
//			fogCol += (Sky*((1.0*mPhase)*_Intensity)); // comment this if you want to remove the skybox color
		// Lerp between fog color & original scene color
		// by fog amount
		float4 FinalColor;
		FinalColor = lerp (fogCol, sceneColor, fogFac );
//		return lerp (unity_FogColor, sceneColor, fogFac); // original from unity
//		return lerp (((unity_FogColor*mPhase)*_Intensity)+unity_FogColor, sceneColor, fogFac);
//		return fogCol + ((Sky*mPhase)*_Intensity); // debug color
		//return fogCol;
		return FinalColor;
	}

ENDCG

SubShader
{
	ZTest Always Cull Off ZWrite Off Fog { Mode Off }

	// 0: distance + height
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, true, false, false); }
		ENDCG
	}
	// 1: distance
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, false, false, false); }
		ENDCG
	}
	// 2: height
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, false, true, false, false); }
		ENDCG
	}
	// 3: distance + height + sky + scattering
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, true, true, true); }
		ENDCG
	}
	// 4: distance + sky + scattering
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, false, true, true); }
		ENDCG
	}
	// 5: height + sky + scattering
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, false, true, true, true); }
		ENDCG
	}
	// 6: distance + height + sky
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, true, true, false); }
		ENDCG
	}
	// 7: distance + sky
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, false, true, false); }
		ENDCG
	}
	// 8: height + sky
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, false, true, true, false); }
		ENDCG
	}
	// 9: distance + height + scattering
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, true, false, true); }
		ENDCG
	}
	// 10: distance + scattering
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, true, false, false, true); }
		ENDCG
	}
	// 11: height + scattering
	Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		half4 frag (v2f i) : SV_Target { return ComputeFog (i, false, true, false, true); }
		ENDCG
	}
}

Fallback off

}
