using UnityEditor;
using UnityEngine;

namespace YAMG
{

    [CustomEditor(typeof(PlayerInput))]
    public class PlayerInputEditor : DataPersisterEditor
    {
        private bool m_IsPrefab = false;
        private bool m_IsNotInstance = false;

        SerializedProperty m_MeleeAttackEnabled;
        SerializedProperty m_RangeAttackEnabled;
        SerializedProperty m_DashEnabled;
        SerializedProperty m_WallJumpEnabled;

        GUIContent m_MeleeContent;
        GUIContent m_RangeContent;
        GUIContent m_DashContent;
        GUIContent m_WallJumpContent;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_IsPrefab = AssetDatabase.Contains(target);
            m_IsNotInstance = PrefabUtility.GetCorrespondingObjectFromSource(target) == null;

            m_MeleeAttackEnabled = serializedObject.FindProperty("MeleeAttack.m_Enabled");
            m_RangeAttackEnabled = serializedObject.FindProperty("RangedAttack.m_Enabled");
            m_DashEnabled = serializedObject.FindProperty("Dash.m_Enabled");
            m_WallJumpEnabled = serializedObject.FindProperty("isWallJumpEnabled");

            m_MeleeContent = new GUIContent("Melee attack enabled");
            m_RangeContent = new GUIContent("Range attack enabled");
            m_DashContent = new GUIContent("Dash enabled");
            m_WallJumpContent = new GUIContent("WallJump enabled");
        }

        public override void OnInspectorGUI()
        {
            if (m_IsPrefab || m_IsNotInstance)
                base.OnInspectorGUI();
            else
            {
                EditorGUILayout.PropertyField(m_MeleeAttackEnabled, m_MeleeContent);
                EditorGUILayout.PropertyField(m_RangeAttackEnabled, m_RangeContent);
                EditorGUILayout.PropertyField(m_DashEnabled, m_DashContent);
                EditorGUILayout.PropertyField(m_WallJumpEnabled, m_WallJumpContent);

                EditorGUILayout.HelpBox("Modify the prefab and not this instance", MessageType.Warning);
                if (GUILayout.Button("Select Prefab"))
                {
                    Selection.activeObject = PrefabUtility.GetCorrespondingObjectFromSource(target);
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}