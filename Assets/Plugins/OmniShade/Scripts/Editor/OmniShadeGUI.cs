//------------------------------------
//             OmniShade
//     Copyright© 2022 OmniShade     
//------------------------------------

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/**
 * This class manages the GUI for the shader, automatically enabling/disabling keywords when values change.
 */
public class OmniShadeGUI : ShaderGUI {
	public const string SHADER_NAME = "OmniShade";

	enum PropertyType {
		Float, Vector, Texture
	};
	int prevPreset = -1;
	List<Material> prevSelectedMats = new List<Material>();

	// Shader keywords to detect to automatically enable/disable
	List<(string keyword, string name, PropertyType type, Vector4 defaultValue)> props = 
		new List<(string keyword, string name, PropertyType type, Vector4 defaultValue)>(){
		("AMBIENT", "_AmbientBrightness", PropertyType.Float, Vector4.zero),
		("TOP_TEX", "_TopTex", PropertyType.Texture, Vector4.one),							// Triplanar
		("TRIPLANAR_SHARPNESS", "_TriplanarSharpness", PropertyType.Float, Vector4.one),	// Triplanar
		("BASE_CONTRAST", "_Contrast", PropertyType.Float, Vector4.one),
		("EMISSIVE_MAP", "_EmissiveTex", PropertyType.Texture, Vector4.one),
		("VERTEX_COLORS_CONTRAST", "_VertexColorsContrast", PropertyType.Float, Vector4.one),
		("DETAIL", "_DetailTex", PropertyType.Texture, Vector4.one),
		("DETAIL_CONTRAST", "_DetailContrast", PropertyType.Float, Vector4.one),
		("LIGHT_MAP", "_LightmapTex", PropertyType.Texture, Vector4.one),
		("LIGHT_MAP_DIR", "_LightmapDirectionTex", PropertyType.Texture, Vector4.one),
		("MATCAP", "_MatCapTex", PropertyType.Texture, Vector4.one),	
		("MATCAP_CONTRAST", "_MatCapContrast", PropertyType.Float, Vector4.one),
		("NORMAL_MAP", "_NormalTex", PropertyType.Texture, Vector4.one),
		("NORMAL_MAP_TOP", "_NormalTopTex", PropertyType.Texture, Vector4.one),				// Triplanar
		("RIM_DIRECTION", "_RimDirection", PropertyType.Vector, Vector4.zero),
		("REFLECTION", "_ReflectionTex", PropertyType.Texture, Vector4.one),
		("SPECULAR_MAP", "_SpecularTex", PropertyType.Texture, Vector4.one),
		("LAYER1", "_Layer1Tex", PropertyType.Texture, Vector4.one),
		("LAYER2", "_Layer2Tex", PropertyType.Texture, Vector4.one),
		("LAYER3", "_Layer3Tex", PropertyType.Texture, Vector4.one),
		("TRANSPARENCY_MASK", "_TransparencyMaskTex", PropertyType.Texture, Vector4.one),
		("TRANSPARENCY_MASK_CONTRAST", "_TransparencyMaskContrast", PropertyType.Float, Vector4.one),
		("HEIGHT_COLORS_TEX", "_HeightColorsTex", PropertyType.Texture, Vector4.one),
        ("SHADOW_OVERLAY", "_ShadowOverlayTex", PropertyType.Texture, Vector4.one),
		("ANIME_SOFT", "_AnimeSoftness", PropertyType.Float, Vector4.zero),
		("MATCAP_CONTRAST", "_MatCapContrast", PropertyType.Float, Vector4.one),
		("ZOFFSET", "_ZOffset", PropertyType.Float, Vector4.zero),
	};

	// Parameters that are ON by default
	List<(string keyword, string name)> defaultOnParams = new List<(string keyword, string name)>() {
		("MATCAP_PERSPECTIVE", "_MatCapPerspective" ),
		("SHADOWS_ENABLED", "_ShadowsEnabled" ),
		("DIFFUSE", "_Diffuse" ),
		("FOG", "_Fog" ),
		("AMBIENT", "_Ambient" ),
	};

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) {
		base.OnGUI(materialEditor, properties);
		
		var mat = materialEditor.target as Material;

		// Multi-selection
		var mats = new List<Material>();
		if (mat != null)
			mats.Add(mat);
		foreach (var selected in Selection.objects) {
			if (selected.GetType() == typeof(Material)) {
				var selectedMat = selected as Material;
				if (selectedMat != mat && selectedMat != null &&
					selectedMat.shader.name.Contains(SHADER_NAME))
					mats.Add(selectedMat);
			}
		}

		// Loop selected materials
		foreach (var material in mats) {
			this.AutoEnableShaderKeywords(material);
			this.SetPresetIfChanged(material);
			if (mats.Count > 1)
				this.prevPreset = -1;
		}

		// Reset preset if selection changed
		if (mats.Count == 1 && this.prevSelectedMats.Count == 1 &&
			mats[0] != null && this.prevSelectedMats[0] != null && mats[0].name != this.prevSelectedMats[0].name)
			this.prevPreset = -1;
		this.prevSelectedMats = mats;
	}

	public void AutoEnableShaderKeywords(Material mat) {
		foreach (var prop in this.props) {
			if (!mat.HasProperty(prop.name))
				continue;

			// Check if property value is being used (not set to default)
			bool isInUse = false;
			switch (prop.type) {
				case PropertyType.Float:
					isInUse = mat.GetFloat(prop.name) != prop.defaultValue.x;
					break;
				case PropertyType.Vector:
					isInUse = mat.GetVector(prop.name) != prop.defaultValue;
					break;
				case PropertyType.Texture:
					isInUse = mat.GetTexture(prop.name) != null;
					break;
				default:
					break;
			}

			// Enable or disable shader keyword
			if (isInUse) {
				if (!mat.IsKeywordEnabled(prop.keyword))
					mat.EnableKeyword(prop.keyword);
			} 
			else if (mat.IsKeywordEnabled(prop.keyword))
				mat.DisableKeyword(prop.keyword);
		}

		// Set keywords for parameters that are ON by default
		foreach (var defaultOnParam in this.defaultOnParams) {
			if (mat.HasProperty(defaultOnParam.name) && mat.GetFloat(defaultOnParam.name) == 1)
				mat.EnableKeyword(defaultOnParam.keyword);
		}

		// MatCap Static Rotation default angle points to camera
		if (mat.IsKeywordEnabled("MATCAP_STATIC") && mat.HasProperty("_MatCapRot") && 
			mat.GetVector("_MatCapRot") == Vector4.zero) {
			var cam = GameObject.FindObjectOfType<Camera>();
			if (cam != null) {
				var matCapRot = Vector4.zero;
				matCapRot = -cam.transform.rotation.eulerAngles * Mathf.PI / 180;
				mat.SetVector("_MatCapRot", matCapRot);
			}
		}

		// Enable GPU instancing automatically for certain keywords
		bool forceSave = false;
		var instancingParams = new List<string>() { 
			"_Plant",
		};
		if (!mat.enableInstancing) {
			foreach (var instancingParma in instancingParams) {
				if ((mat.HasProperty(instancingParma) && mat.GetFloat(instancingParma) == 1)) {
					mat.enableInstancing = true;
					forceSave = true;
				}
			}
		}
		if (forceSave)
			AssetDatabase.SaveAssets();
	}

	void SetPresetIfChanged(Material mat) {
		int preset = (int)mat.GetFloat("_Preset");
		if (this.prevPreset == -1) {  // Initialize preset value
			if (mat.GetFloat("_ZTest") == 4 && mat.GetFloat("_BlendOp") == 0) {
				var cull = mat.GetFloat("_Cull");
				var ZWrite = mat.GetFloat("_ZWrite");
				var srcBlend = mat.GetFloat("_SourceBlend");
				var destBlend = mat.GetFloat("_DestBlend");
				if (cull == 2 && ZWrite == 1 && srcBlend == 5 && destBlend == 10 && mat.renderQueue < 2450)
					mat.SetFloat("_Preset", 0);         // Opaque
				else if (ZWrite == 0 && mat.renderQueue >= 2450) {
					if (srcBlend == 5 && destBlend == 10)
						mat.SetFloat("_Preset", 1);     // Transparent
					if (srcBlend == 1 && destBlend == 1)
						mat.SetFloat("_Preset", 2);     // Transparent Additive
					if (srcBlend == 5 && destBlend == 1)
						mat.SetFloat("_Preset", 3);     // Transparent Additive Alpha
				}
			}
		}
		else if (this.prevPreset != preset) {  // New preset selected - set values
			mat.SetFloat("_ZTest", 4);					// LessEqual
			mat.SetFloat("_BlendOp", 0);				// Add

			switch (preset) {
				case 0:		// Opaque
					mat.SetFloat("_Cull", 2);			// Back
					mat.SetFloat("_ZWrite", 1);
					mat.SetFloat("_SourceBlend", 5);	// SrcAlpha
					mat.SetFloat("_DestBlend", 10);		// OneMinusSrcAlpha
					if (mat.renderQueue >= 2450)
						mat.renderQueue = 2000;
					break;

				case 1:		// Transparent
					mat.SetFloat("_ZWrite", 0);
					mat.SetFloat("_SourceBlend", 5);	// SrcAlpha
					mat.SetFloat("_DestBlend", 10);		// OneMinusSrcAlpha
					if (mat.renderQueue < 2450)
						mat.renderQueue = 3000;
					break;

				case 2:		// Transparent Additive
					mat.SetFloat("_ZWrite", 0);
					mat.SetFloat("_SourceBlend", 1);	// One
					mat.SetFloat("_DestBlend", 1);		// One
					if (mat.renderQueue < 2450)
						mat.renderQueue = 3000;
					break;

				case 3:		// Transparent Additive Alpha
					mat.SetFloat("_ZWrite", 0);
					mat.SetFloat("_SourceBlend", 5);	// SrcAlpha
					mat.SetFloat("_DestBlend", 1);		// One
					if (mat.renderQueue < 2450)
						mat.renderQueue = 3000;
					break;

				default:
					Debug.LogError("Unrecognized Preset (" + preset +")");
					break;
			}
		}
		
		this.prevPreset = preset;
	}
}
