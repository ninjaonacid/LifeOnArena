using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.EditorWindows
{
    public class AnimationEventSetup : EditorWindow
    {
        [MenuItem("Tools/Setup Animation Events")]
        public static void SetupAnimationEvents()
        {
            AnimationClip[] animationClips = GetAllAnimationClips();

            foreach (AnimationClip clip in animationClips)
            {
                AddAnimationEvents(clip);
            }

            Debug.Log("Animation events setup complete.");
        }
        
        [MenuItem("Tools/Remove Animation Events")]
        public static void RemoveAnimationEvents()
        {
            AnimationClip[] animationClips = GetAllAnimationClips();

            foreach (AnimationClip clip in animationClips)
            {
                RemoveSpecificAnimationEvents(clip, "AnimationStartEvent", "AnimationEndEvent");
            }

            Debug.Log("Animation events removal complete.");
        }


        private static AnimationClip[] GetAllAnimationClips()
        {
            string[] guids = AssetDatabase.FindAssets("t:AnimationClip");
            return guids.Select(guid => AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath(guid))).ToArray();
        }

        private static void AddAnimationEvents(AnimationClip clip)
        {
            AnimationEvent startEvent = new AnimationEvent
            {
                functionName = "AnimationStartEvent",
                time = 0f
            };

            AnimationEvent endEvent = new AnimationEvent
            {
                functionName = "AnimationEndEvent",
                time = clip.length
            };

            AnimationEvent[] existingEvents = AnimationUtility.GetAnimationEvents(clip);

      
            System.Array.Resize(ref existingEvents, existingEvents.Length + 2);
            existingEvents[^2] = startEvent;
            existingEvents[^1] = endEvent;
      
            AnimationUtility.SetAnimationEvents(clip, existingEvents);

           
            EditorUtility.SetDirty(clip);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.SetDirty(clip);
            AssetDatabase.SaveAssets();
        }
        
        private static void RemoveSpecificAnimationEvents(AnimationClip clip, params string[] eventNames)
        {
            AnimationEvent[] existingEvents = AnimationUtility.GetAnimationEvents(clip);
            var newEvents = existingEvents.Where(e => !eventNames.Contains(e.functionName)).ToArray();

            AnimationUtility.SetAnimationEvents(clip, newEvents);

            EditorUtility.SetDirty(clip);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
