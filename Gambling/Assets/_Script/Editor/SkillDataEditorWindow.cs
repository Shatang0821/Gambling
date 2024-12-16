using UnityEditor;
using UnityEngine;
using Game.SkillSystem; // SkillDataのnamespace

public class SkillDataEditorWindow : EditorWindow
{
    private string skillName = "NewSkill";
    private int skillID = 1001;
    private float cooldown = 1.0f;
    private float manaCost = 10.0f;
    private Animator animator;
    private string selectedAnimationName;
    private TargetType targetType = TargetType.Enemy;
    private ColliderType colliderType = ColliderType.Sphere;

    [MenuItem("Tools/Skill Data Editor")]
    public static void ShowWindow()
    {
        GetWindow<SkillDataEditorWindow>("Skill Data Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Skill Data Editor", EditorStyles.boldLabel);

        skillName = EditorGUILayout.TextField("Skill Name", skillName);
        skillID = EditorGUILayout.IntField("Skill ID", skillID);
        cooldown = EditorGUILayout.FloatField("Cooldown", cooldown);
        manaCost = EditorGUILayout.FloatField("Mana Cost", manaCost);

        animator = (Animator)EditorGUILayout.ObjectField("Animator", animator, typeof(Animator), true);
        
        // アニメーション選択
        if (animator != null)
        {
            string[] animationClips = AnimatorUtility.GetAnimationClips(animator);
            int selectedIndex = System.Array.IndexOf(animationClips, selectedAnimationName);
            selectedIndex = EditorGUILayout.Popup("Animation Clip", selectedIndex, animationClips);

            if (selectedIndex >= 0)
            {
                selectedAnimationName = animationClips[selectedIndex];
            }
        }

        targetType = (TargetType)EditorGUILayout.EnumPopup("Target Type", targetType);
        colliderType = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", colliderType);

        // ScriptableObject生成ボタン
        if (GUILayout.Button("Create Skill Data"))
        {
            CreateSkillData();
        }
    }

    private void CreateSkillData()
    {
        // ScriptableObjectを生成
        SkillData newSkillData = ScriptableObject.CreateInstance<SkillData>();

        // データを設定
        newSkillData.SkillName = skillName;
        newSkillData.SkillID = skillID;
        newSkillData.Cooldown = cooldown;
        newSkillData.ManaCost = manaCost;
        newSkillData.AnimationName = selectedAnimationName;
        newSkillData.TargetType = targetType;
        newSkillData.ColliderType = colliderType;

        // 保存パスを選択
        string path = EditorUtility.SaveFilePanelInProject("Save Skill Data", skillName, "asset", "Please enter a file name to save the skill data.");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newSkillData, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newSkillData;
            Debug.Log($"SkillData {skillName} was created at {path}");
        }
    }
}
