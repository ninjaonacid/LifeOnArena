
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

/**
 * This class strips shader variants at build time to reduce the shader size and memory.
 * Note that you should adjust the project-wide Shader Stripping settings in Project Settings->Graphics first.
 * It is completely optional to toggle the optimization flags in the material.
 */
public class AniShaderPreprocess : IPreprocessShaders {
    // PROJECT-WIDE SETTINGS
    // Set these values to always strip certain variants
    const bool ALWAYS_STRIP_SHADOWS = false;
    const bool ALWAYS_STRIP_POINT_LIGHTS = false;
    const bool ALWAYS_STRIP_FALLBACK = false;

    // Shadows
    ShaderKeyword SHADOWS_SHADOWMASK = new ShaderKeyword("SHADOWS_SHADOWMASK");
    ShaderKeyword SHADOWS_SCREEN = new ShaderKeyword("SHADOWS_SCREEN");
    ShaderKeyword SHADOWS_SOFT = new ShaderKeyword("_SHADOWS_SOFT");
    ShaderKeyword MAIN_LIGHT_SHADOWS = new ShaderKeyword("_MAIN_LIGHT_SHADOWS");
    ShaderKeyword MAIN_LIGHT_SHADOWS_CASCADE = new ShaderKeyword("_MAIN_LIGHT_SHADOWS_CASCADE");
    ShaderKeyword SHADOWS_ENABLED = new ShaderKeyword("SHADOWS_ENABLED");
    ShaderKeyword OPTSHADOW_DISABLED = new ShaderKeyword("_OPTSHADOW_DISABLED");
    ShaderKeyword OPTSHADOW_ENABLED_ONLY = new ShaderKeyword("_OPTSHADOW_ENABLED_ONLY");

    // Point light
    ShaderKeyword VERTEXLIGHT_ON = new ShaderKeyword("VERTEXLIGHT_ON");
    ShaderKeyword ADDITIONAL_LIGHTS = new ShaderKeyword("_ADDITIONAL_LIGHTS");
    ShaderKeyword DIFFUSE = new ShaderKeyword("DIFFUSE");
    ShaderKeyword OPTPOINTLIGHTS_DISABLED = new ShaderKeyword("_OPTPOINTLIGHTS_DISABLED");

    // Fog
    ShaderKeyword FOG_LINEAR = new ShaderKeyword("FOG_LINEAR");
    ShaderKeyword FOG_EXP = new ShaderKeyword("FOG_EXP");
    ShaderKeyword FOG_EXP2 = new ShaderKeyword("FOG_EXP2");
    ShaderKeyword FOG = new ShaderKeyword("FOG");
    ShaderKeyword OPTFOG_DISABLED = new ShaderKeyword("_OPTFOG_DISABLED");
    ShaderKeyword OPTFOG_ENABLED_ONLY = new ShaderKeyword("_OPTFOG_ENABLED_ONLY");

    // Lightmapping
    ShaderKeyword LIGHTMAP_ON = new ShaderKeyword("LIGHTMAP_ON");
    ShaderKeyword DIRLIGHTMAP_COMBINED = new ShaderKeyword("DIRLIGHTMAP_COMBINED");
    ShaderKeyword OPTLIGHTMAPPING_DISABLED = new ShaderKeyword("_OPTLIGHTMAPPING_DISABLED");
    ShaderKeyword OPTLIGHTMAPPING_ENABLED_ONLY = new ShaderKeyword("_OPTLIGHTMAPPING_ENABLED_ONLY");

    // Fallback
    ShaderKeyword OPTFALLBACK_DISABLED = new ShaderKeyword("_OPTFALLBACK_DISABLED");
    const string FALLBACK_PASS_NAME = "Fallback";

    // Outline
    ShaderKeyword OUTLINE = new ShaderKeyword("OUTLINE");
    const string OUTLINE_PASS_NAME = "Outline";

    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data) {
        if (!shader.name.Contains(AniShaderGUI.SHADER_NAME))
            return;

        for (int i = data.Count - 1; i >= 0; --i) {
            var keys = data[i].shaderKeywordSet;

            // Strip shadows
            bool isShadowVariant = keys.IsEnabled(SHADOWS_SHADOWMASK) || keys.IsEnabled(SHADOWS_SCREEN) || keys.IsEnabled(SHADOWS_SOFT) ||
                keys.IsEnabled(MAIN_LIGHT_SHADOWS) || keys.IsEnabled(MAIN_LIGHT_SHADOWS_CASCADE);
            bool isShadowDisabled = !keys.IsEnabled(SHADOWS_ENABLED) || keys.IsEnabled(OPTSHADOW_DISABLED) || ALWAYS_STRIP_SHADOWS;
            bool isShadowEnabledOnly = keys.IsEnabled(SHADOWS_ENABLED) && keys.IsEnabled(OPTSHADOW_ENABLED_ONLY);
            if ((isShadowVariant && isShadowDisabled) || (!isShadowVariant && isShadowEnabledOnly)) {
                data.RemoveAt(i);
                continue;
            }

            // Strip shadow caster pass
            bool isShadowCasterPass = snippet.passType == PassType.ShadowCaster;
            bool isShadowCasterDisabled = keys.IsEnabled(OPTSHADOW_DISABLED) || ALWAYS_STRIP_SHADOWS;
            bool isShadowCasterEnabledOnly = keys.IsEnabled(OPTSHADOW_ENABLED_ONLY);
            if ((isShadowCasterPass && isShadowCasterDisabled) || (!isShadowCasterPass && isShadowCasterEnabledOnly)) {
                data.RemoveAt(i);
                continue;
            }

            // Strip point lights
            bool isPointLightVariant = keys.IsEnabled(VERTEXLIGHT_ON) || keys.IsEnabled(ADDITIONAL_LIGHTS);
            bool isPointLightDisabled = !keys.IsEnabled(DIFFUSE) || !keys.IsEnabled(OPTPOINTLIGHTS_DISABLED) || ALWAYS_STRIP_POINT_LIGHTS;
            if (isPointLightVariant && isPointLightDisabled) {
                data.RemoveAt(i);
                continue;
            }

            // Strip fog
            bool isFogVariant = keys.IsEnabled(FOG_LINEAR) || keys.IsEnabled(FOG_EXP) || keys.IsEnabled(FOG_EXP2);
            bool isFogDisabled = !keys.IsEnabled(FOG) || keys.IsEnabled(OPTFOG_DISABLED);
            bool isFogEnabledOnly = keys.IsEnabled(FOG) && keys.IsEnabled(OPTFOG_ENABLED_ONLY);
            if ((isFogVariant && isFogDisabled) || (!isFogVariant && isFogEnabledOnly)) {
                data.RemoveAt(i);
                continue;
            }

            // Strip lightmapping
            bool isLightmappingVariant = keys.IsEnabled(LIGHTMAP_ON) || keys.IsEnabled(DIRLIGHTMAP_COMBINED);
            bool isLightmappingDisabled = keys.IsEnabled(OPTLIGHTMAPPING_DISABLED);
            bool isLightmappingEnableOnly = keys.IsEnabled(OPTLIGHTMAPPING_ENABLED_ONLY);
            if ((isLightmappingVariant && isLightmappingDisabled) || (!isLightmappingVariant && isLightmappingEnableOnly)) {
                data.RemoveAt(i);
                continue;
            }

            // Strip fallback
            bool isFallbackPass = snippet.passName == FALLBACK_PASS_NAME;
            bool isFallbackDisabled = keys.IsEnabled(OPTFALLBACK_DISABLED) || ALWAYS_STRIP_FALLBACK;
            if (isFallbackPass && isFallbackDisabled) {
                data.RemoveAt(i);
                continue;
            }

            // Strip outline
            bool isOutlinePass = snippet.passName == OUTLINE_PASS_NAME;
            bool isOutlineDisabled = !keys.IsEnabled(OUTLINE);
            if (isOutlinePass && isOutlineDisabled) {
                data.RemoveAt(i);
                continue;
            }
        }
    }

    public int callbackOrder { get { return 0; } }
}
