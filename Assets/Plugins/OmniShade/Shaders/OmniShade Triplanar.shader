//------------------------------------
//             OmniShade
//     Copyright© 2022 OmniShade     
//------------------------------------

Shader "OmniShade/Triplanar" {
	Properties {	
		[Header(SHADOWS AND ENVIRONMENT)]
		[Toggle(SHADOWS_ENABLED)] _ShadowsEnabled ("Receive Shadows", Float) = 1
		[HDR] _ShadowColor ("Shadow Color", Color) = (0.3, 0.3, 0.3, 1)
		[Toggle(FOG)] _Fog ("Fog", Float) = 1
		_AmbientBrightness ("Ambient Brightness", range(0, 25)) = 1	
		[Toggle(FLAT)] _Flat ("Flat Shading", Float) = 0
		_TriplanarSharpness ("Triplanar Blend Sharpness", range(0.01, 30)) = 1

		[Header(MAIN TEXTURE)]
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_Brightness ("Brightness", range(0, 25)) = 1
		_Contrast ("Contrast", range(0, 25)) = 1
		[Toggle] _IgnoreMainTexAlpha ("Ignore Main Texture Alpha", Float) = 0

		[Header(TRIPLANAR TOP)]
		_TopTex ("Top Texture", 2D) = "white" {}
		_TopColor ("Top Color", Color) = (1, 1, 1, 1)
		_TopBrightness ("Top Brightness", range(0, 25)) = 1

		[Header(VERTEX COLORS)]
		[Toggle(VERTEX_COLORS)] _VertexColor ("Enable Vertex Colors", Float) = 0
		_VertexColorContrast ("Vertex Color Contrast", range(0, 25)) = 1

		[Header(DETAIL MAP)]
		_DetailTex ("Detail Map", 2D) = "white" {}
		_DetailColor ("Detail Color", Color) = (1, 1, 1, 1)
		_DetailBrightness ("Detail Brightness", range(0, 25)) = 1
		_DetailContrast ("Detail Contrast", range(0, 25)) = 1
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten)] _DetailBlend ("Detail Blend Mode", Float) = 2
		[Toggle(DETAIL_LIGHTING)] _DetailLighting ("Apply To Lighting", Float) = 0
		[Toggle(DETAIL_VERTEX_COLORS)] _DetailVertexColors ("Mask With Vertex Color (A)", Float) = 0

		[Header(DIFFUSE)]
		[Toggle(DIFFUSE)] _Diffuse ("Enable Diffuse", Float) = 1
		_DiffuseWrap ("Diffuse Softness", range(-1, 1)) = 0
		_DiffuseBrightness ("Diffuse Brightness", range(0, 25)) = 1
		_DiffuseContrast ("Diffuse Contrast", range(0.01, 25)) = 1
		[Toggle(DIFFUSE_PER_PIXEL)] _DiffusePerPixel ("Per-Pixel Point Lights", Float) = 0

		[Header(SPECULAR)]
		[Toggle(SPECULAR)] _Specular ("Enable Specular", Float) = 0
		_SpecularBrightness ("Specular Brightness", range(0, 25)) = 1
		_SpecularSmoothness ("Specular Smoothness", range(1, 100)) = 20
		[Toggle(SPECULAR_HAIR)] _SpecularHair ("Specular Hair", Float) = 0

		[Header(EMISSIVE)]
		[HDR] _Emissive ("Emissive Color", Color) = (0, 0, 0, 0)
		_EmissiveTex ("Emissive Map", 2D) = "white" {}

		[Header(LIGHTMAP)]
		_LightmapTex ("Lightmap", 2D) = "white" {}
		_LightmapColor ("Lightmap Color", Color) = (1, 1, 1, 1)
		_LightmapBrightness ("Lightmap Brightness", range(0, 25)) = 1
		[Toggle(LIGHT_MAP_HDR)] _LightmapHDR ("Is HDR", Float) = 0
		[NoScaleOffset] _LightmapDirectionTex ("Lightmap Direction", 2D) = "white" {}

		[Header(MATCAP)]
		_MatCapTex ("MatCap Texture", 2D) = "black" {}
		_MatCapColor ("MatCap Color", Color) = (1, 1, 1, 1)
		_MatCapBrightness ("MatCap Brightness", range(0, 25)) = 1
		_MatCapContrast ("MatCap Contrast", range(0, 25)) = 1
		[KeywordEnum(Multiply, Multiply Lighten)] _MatCapBlend ("MatCap Blend Mode", Float) = 0
        [Toggle(MATCAP_PERSPECTIVE)] _MatCapPerspective ("Perspective Correction", Float) = 1
		[Toggle(MATCAP_STATIC)] _MatCapStatic ("Use Static Rotation", Float) = 0
		_MatCapRot ("MatCap Static Rotation", Vector) = (0, 0, 0, 0)

		[Header(NORMAL MAP)]
		[Normal] _NormalTex ("Normal Map", 2D) = "bump" {}
		[Normal] _NormalTopTex ("Normal Map Top", 2D) = "bump" {}
		_NormalStrength ("Normal Strength", range(0, 5)) = 1

		[Header(RIM LIGHT)]
		[Toggle(RIM)] _Rim ("Enable Rim Light", Float) = 0
		[HDR] _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
		_RimAmount ("Rim Amount", range(0, 25)) = 1
		_RimContrast ("Rim Contrast", range(0, 50)) = 5
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten, Transparency)] _RimBlend ("Rim Blend Mode", Float) = 1
		[Toggle] _RimInverse ("Rim Invert", Float) = 0
		_RimDirection ("Rim Direction", Vector) = (0, 0, 0, 0)

		[Header(REFLECTION (Requires Rim Light))]
		[NoScaleOffset] _ReflectionTex ("Reflection Cubemap", Cube) = "" {}
		_ReflectionBrightness ("Reflection Brightness", range(0, 50)) = 1

		[Header(HEIGHT BASED COLORS)]
		[Toggle(HEIGHT_COLORS)] _HeightColors ("Enable Height Based Colors", Float) = 0
		_HeightColorsColor ("Color", Color) = (1, 1, 1, 1)
		_HeightColorsAlpha ("Alpha", range(0, 25)) = 1
		_HeightColorsHeight ("Height", range(-100, 100)) = 0
		_HeightColorsEdgeThickness ("Edge Thickness", range(0.001, 100)) = 1
		_HeightColorsThickness ("Thickness", range(0, 100)) = 0
		[Enum(World, 0, Local, 1)] _HeightColorsSpace ("Coordinate Space", Float) = 0
		[KeywordEnum(Alpha Blend, Additive, Lit)] _HeightColorsBlend ("Height Colors Blend Mode", Float) = 0

		[Header(SHADOW OVERLAY)]
		_ShadowOverlayTex ("Shadow Overlay Tex", 2D) = "white" {}
		_ShadowOverlayBrightness ("Shadow Brightness", range(0, 2)) = 1
		_ShadowOverlaySpeedU ("Shadow Speed U", range(-5, 5)) = 0.1
		_ShadowOverlaySpeedV ("Shadow Speed V", range(-5, 5)) = 0.03
		_ShadowOverlaySwayAmount ("Shadow Sway Amount", range(0, 0.01)) = 0.01
		[KeywordEnum(Scroll, Sway)] _ShadowOverlayAnimation ("Animation Type", Float) = 0

		[Header(PLANT SWAY)]
		[Toggle(PLANT_SWAY)] _Plant ("Enable Grass Sway", Float) = 0
		_PlantSwayAmount ("Sway Amount", range(0, 10)) = 0.15
		_PlantSwaySpeed ("Sway Speed", range(0, 10)) = 1
		_PlantPhaseVariation ("Phase Variation", range(0, 100)) = 0.3
		_PlantBaseHeight ("Base Height", range(-100, 100)) = 0
		[KeywordEnum(Plant, Leaf, Vertex Color Alpha)] _PlantType ("Plant Type", Float) = 0

		[Header(OUTLINE)]
		[Toggle(OUTLINE)] _Outline ("Enable Outline", Float) = 0
		_OutlineWidth ("Outline Width", range(0, 0.1)) = 0.002
		_OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)

		[Header(ANIME)]
		[Toggle(ANIME)] _Anime ("Enable Anime", Float) = 0
		[HDR] _AnimeColor1 ("Color 1", Color) = (1, 1, 1, 1)
		_AnimeThreshold1 ("Luminance Threshold 1", range(0, 3)) = 0.45
		[HDR] _AnimeColor2 ("Color 2", Color) = (1.35, 1.35, 1.35, 1)
		_AnimeThreshold2 ("Luminance Threshold 2", range(0, 3)) = 0.85
		[HDR] _AnimeColor3 ("Color 3", Color) = (2, 2, 2, 1)
		_AnimeSoftness ("Softness", range(0, 0.25)) = 0.01

		[Header(PRESETS FOR CULLING AND BLEND SETTINGS)]
		[Space] [Enum(Solid, 0, Transparent, 1, Transparent Additive, 2, Transparent Additive Alpha, 3)] _Preset ("", Float) = 0

		[Header(CULLING AND DEPTH)]
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Culling", Float) = 2 				// Back
		[Enum(Off, 0, On, 1)] _ZWrite ("Z Write", Float) = 1.0								// On
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("Z Test", Float) = 4 			// LessEqual
		_ZOffset ("Depth Offset", range(-5, 5)) = 0

		[Header(BLEND)]
		[Enum(UnityEngine.Rendering.BlendMode)] _SourceBlend ("Source Blend", Float) = 5 	// SrcAlpha
		[Enum(UnityEngine.Rendering.BlendMode)] _DestBlend ("Dest Blend", Float) = 10 		// OneMinusSrcAlpha
		[Enum(UnityEngine.Rendering.BlendOp)] _BlendOp ("Blend Mode", Float) = 0  			// "Add"

		[Header(OPTIMIZATION FLAGS (advanced))]
		[KeywordEnum(All, Disabled, Enabled Only)] _OptShadow ("Shadows", Float) = 0
		[KeywordEnum(All, Disabled)] _OptPointLights ("Point Lights", Float) = 0
		[KeywordEnum(All, Disabled, Enabled Only)] _OptFog ("Fog", Float) = 0
		[KeywordEnum(All, Disabled, Enabled Only)] _OptLightmapping ("Lightmapping", Float) = 0
		[KeywordEnum(All, Disabled)] _OptFallback ("Fallback (OpenGL ES 2)", Float) = 0
	}

	Subshader {
		Pass {
			name "Forward"
			Tags { "LightMode" = "ForwardBase" }
			Cull [_Cull]
			ZWrite [_ZWrite]
			ZTest [_ZTest]
			Blend [_SourceBlend][_DestBlend]
			BlendOp [_BlendOp]

			CGPROGRAM
			#define TRIPLANAR 1
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "AutoLight.cginc"
			#include "OmniShadeCore.cginc"
			#pragma target 3.5
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile_instancing
			#pragma multi_compile _ VERTEXLIGHT_ON
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ SHADOWS_SCREEN
			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma shader_feature SHADOWS_ENABLED
			#pragma shader_feature FOG
			#pragma shader_feature AMBIENT
			#pragma shader_feature FLAT
			#pragma shader_feature TRIPLANAR_SHARPNESS
			#pragma shader_feature BASE_CONTRAST
			#pragma shader_feature TOP_TEX
			#pragma shader_feature DIFFUSE
			#pragma shader_feature DIFFUSE_PER_PIXEL
			#pragma shader_feature SPECULAR
			#pragma shader_feature SPECULAR_HAIR
			#pragma shader_feature EMISSIVE_MAP
			#pragma shader_feature VERTEX_COLORS
			#pragma shader_feature VERTEX_COLORS_CONTRAST
			#pragma shader_feature DETAIL
			#pragma shader_feature DETAIL_CONTRAST
			#pragma shader_feature _DETAILBLEND_ALPHA_BLEND _DETAILBLEND_ADDITIVE _DETAILBLEND_MULTIPLY _DETAILBLEND_MULTIPLY_LIGHTEN
			#pragma shader_feature DETAIL_LIGHTING
			#pragma shader_feature DETAIL_VERTEX_COLORS
			#pragma shader_feature LIGHT_MAP
			#pragma shader_feature LIGHT_MAP_HDR
			#pragma shader_feature LIGHT_MAP_DIR
			#pragma shader_feature MATCAP
			#pragma shader_feature MATCAP_CONTRAST
			#pragma shader_feature _MATCAPBLEND_MULTIPLY _MATCAPBLEND_MULTIPLY_LIGHTEN
			#pragma shader_feature MATCAP_PERSPECTIVE
			#pragma shader_feature MATCAP_STATIC
			#pragma shader_feature NORMAL_MAP
			#pragma shader_feature NORMAL_MAP_TOP
			#pragma shader_feature RIM
			#pragma shader_feature _RIMBLEND_ALPHA_BLEND _RIMBLEND_ADDITIVE _RIMBLEND_MULTIPLY _RIMBLEND_MULTIPLY_LIGHTEN _RIMBLEND_TRANSPARENCY
			#pragma shader_feature RIM_DIRECTION
			#pragma shader_feature REFLECTION
			#pragma shader_feature HEIGHT_COLORS
			#pragma shader_feature _HEIGHTCOLORSBLEND_ALPHA_BLEND _HEIGHTCOLORSBLEND_ADDITIVE _HEIGHTCOLORSBLEND_LIT
			#pragma shader_feature HEIGHT_COLORS_TEX
			#pragma shader_feature SHADOW_OVERLAY
			#pragma shader_feature _SHADOWOVERLAYANIMATION_SCROLL _SHADOWOVERLAYANIMATION_SWAY
			#pragma shader_feature PLANT_SWAY
			#pragma shader_feature _PLANTTYPE_PLANT _PLANTTYPE_LEAF _PLANTTYPE_VERTEX_COLOR_ALPHA
			#pragma shader_feature ZOFFSET
			#pragma shader_feature ANIME
			#pragma shader_feature ANIME_SOFT
			#pragma shader_feature _OPTSHADOW_ALL _OPTSHADOW_DISABLED _OPTSHADOW_ENABLED_ONLY
			#pragma shader_feature _OPTPOINTLIGHTS_ALL _OPTPOINTLIGHTS_DISABLED
			#pragma shader_feature _OPTFOG_ALL _OPTFOG_DISABLED _OPTFOG_ENABLED_ONLY
			#pragma shader_feature _OPTLIGHTMAPPING_ALL _OPTLIGHTMAPPING_DISABLED _OPTLIGHTMAPPING_ENABLED_ONLY
			ENDCG
		}

		Pass {
			Name "Outline"
			Tags { "Render Queue" = "Transparent" }
			Cull Front
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#define TRIPLANAR 1
			#define OUTLINE_PASS 1
			#include "UnityCG.cginc"
			#include "OmniShadeCore.cginc"
			#pragma target 3.5
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma shader_feature PLANT_SWAY
			#pragma shader_feature _PLANTTYPE_PLANT _PLANTTYPE_LEAF _PLANTTYPE_VERTEX_COLOR_ALPHA
			#pragma shader_feature OUTLINE
			ENDCG
		}

		Pass {
			name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
			Cull [_Cull]
			ZWrite [_ZWrite]
			ZTest [_ZTest]
			Blend [_SourceBlend][_DestBlend]
			BlendOp [_BlendOp]

			CGPROGRAM
			#define TRIPLANAR 1
			#define SHADOW_CASTER 1
			#include "UnityCG.cginc"
			#include "OmniShadeCore.cginc"
			#pragma target 3.5
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma shader_feature PLANT_SWAY
			#pragma shader_feature _PLANTTYPE_PLANT _PLANTTYPE_LEAF _PLANTTYPE_VERTEX_COLOR_ALPHA
			ENDCG
		}

		Pass {
			Name "Lightmap Meta"
			Tags { "LightMode" = "Meta" }
			Cull [_Cull]
			ZWrite [_ZWrite]
			ZTest [_ZTest]
			Blend [_SourceBlend][_DestBlend]
			BlendOp [_BlendOp]

			CGPROGRAM
			#define TRIPLANAR 1
			#define META 1
			#include "UnityCG.cginc"
			#include "UnityMetaPass.cginc"
			#include "OmniShadeCore.cginc"
			#pragma target 3.5
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma shader_feature TRIPLANAR_SHARPNESS
			#pragma shader_feature BASE_CONTRAST
			#pragma shader_feature TOP_TEX
			#pragma shader_feature EMISSIVE_MAP
			#pragma shader_feature VERTEX_COLORS
			#pragma shader_feature VERTEX_COLORS_CONTRAST
			#pragma shader_feature DETAIL
			#pragma shader_feature DETAIL_CONTRAST
			#pragma shader_feature _DETAILBLEND_ALPHA_BLEND _DETAILBLEND_ADDITIVE _DETAILBLEND_MULTIPLY _DETAILBLEND_MULTIPLY_LIGHTEN
			#pragma shader_feature DETAIL_VERTEX_COLORS
			#pragma shader_feature MATCAP
			#pragma shader_feature MATCAP_CONTRAST
			#pragma shader_feature _MATCAPBLEND_MULTIPLY _MATCAPBLEND_MULTIPLY_LIGHTEN
			#pragma shader_feature HEIGHT_COLORS
			#pragma shader_feature _HEIGHTCOLORSBLEND_ALPHA_BLEND _HEIGHTCOLORSBLEND_ADDITIVE _HEIGHTCOLORSBLEND_LIT
			#pragma shader_feature HEIGHT_COLORS_TEX
			ENDCG
		}
	}

	Subshader {
		name "Fallback Shader"		

		Pass {
			Name "Fallback"
			Tags { "LightMode" = "ForwardBase" }
			Cull [_Cull]
			ZWrite [_ZWrite]
			ZTest [_ZTest]
			Blend [_SourceBlend][_DestBlend]
			BlendOp [_BlendOp]

			CGPROGRAM
			#define FALLBACK_PASS 1
			#define TRIPLANAR 1
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "OmniShadeCore.cginc"
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma shader_feature FOG
			#pragma shader_feature AMBIENT
			#pragma shader_feature FLAT
			#pragma shader_feature TRIPLANAR_SHARPNESS
			#pragma shader_feature TOP_TEX
			#pragma shader_feature DIFFUSE
			#pragma shader_feature SPECULAR
			#pragma shader_feature VERTEX_COLORS
			#pragma shader_feature LIGHT_MAP
			#pragma shader_feature LIGHT_MAP_HDR
			#pragma shader_feature MATCAP
			#pragma shader_feature _MATCAPBLEND_MULTIPLY _MATCAPBLEND_MULTIPLY_LIGHTEN
			#pragma shader_feature RIM
			#pragma shader_feature _RIMBLEND_ALPHA_BLEND _RIMBLEND_ADDITIVE _RIMBLEND_MULTIPLY _RIMBLEND_MULTIPLY_LIGHTEN _RIMBLEND_TRANSPARENCY
			#pragma shader_feature ZOFFSET
			#pragma shader_feature ANIME
			#pragma shader_feature ANIME_SOFT
			#pragma shader_feature _OPTFOG_ALL _OPTFOG_DISABLED _OPTFOG_ENABLED_ONLY
			#pragma shader_feature _OPTLIGHTMAPPING_ALL _OPTLIGHTMAPPING_DISABLED _OPTLIGHTMAPPING_ENABLED_ONLY
			#pragma shader_feature _OPTFALLBACK_ALL _OPTFALLBACK_DISABLED
			ENDCG
		}
	}
	
	Fallback "Diffuse"
	CustomEditor "OmniShadeGUI"
}
