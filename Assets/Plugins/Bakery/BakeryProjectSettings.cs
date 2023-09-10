#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class BakeryProjectSettings : ScriptableObject
{
    // Affects texture import settings for lightmaps
    [SerializeField]
    public bool mipmapLightmaps = false;

    // Use PNG instead of TGA for shadowmasks, directions and L1 maps
    public enum FileFormat
    {
        TGA = 0,
        PNG = 1
    }
    [SerializeField]
    public FileFormat format8bit = FileFormat.TGA;

    // Padding values for atlas packers
    [SerializeField]
    public int texelPaddingForDefaultAtlasPacker = 3;
    [SerializeField]
    public int texelPaddingForXatlasAtlasPacker = 1;

    // Scales resolution for alpha Meta Pass maps
    [SerializeField]
    public int alphaMetaPassResolutionMultiplier = 2;

    // Render mode for all volumes in the scene. Defaults to Auto, which uses global scene render mode.
    [SerializeField]
    public int volumeRenderMode = 1000;//BakeryLightmapGroup.RenderMode.Auto;

    // Should previously rendered Bakery lightmaps be deleted before the new bake?
    // Turned off by default because I'm scared of deleting anything
    [SerializeField]
    public bool deletePreviousLightmapsBeforeBake = false;

    // Print information about the bake process to console?
    [System.FlagsAttribute]
    public enum LogLevel
    {
        Nothing = 0,
        Info = 1,   // print to Debug.Log
        Warning = 2 // print to Debug.LogWarning
    }
    [SerializeField]
    public int logLevel = (int)(LogLevel.Info | LogLevel.Warning);

    // Make it work more similar to original Unity behaviour
    [SerializeField]
    public bool alternativeScaleInLightmap = false;

    // Should we adjust sample positions to prevent incorrect shadowing on very low-poly meshes with smooth normals?
    [SerializeField]
    public bool generateSmoothPos = true;
}

#endif
