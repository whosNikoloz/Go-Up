using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FirstPersonController))]
public class FirstPersonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawCenteredHeader("Player Camera Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerCamera"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lockCursor"));

        DrawCenteredHeader("Crosshair Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("crosshair"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("crosshairImage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("crosshairColor"));

        DrawCenteredHeader("Zoom Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableZoom"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("holdToZoom"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("zoomKey"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("zoomFOV"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("zoomStepTime"));

        DrawCenteredHeader("Player Movement Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerCanMove"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("walkSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxVelocityChange"));

        DrawCenteredHeader("Sprint Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableSprint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("unlimitedSprint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintKey"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintDuration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintCooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintFOV"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintFOVStepTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("useSprintBar"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hideBarWhenFull"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintBarWidthPercent"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintBarHeightPercent"));

        DrawCenteredHeader("Jump Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableJump"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpKey"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpPower"));

        DrawCenteredHeader("Crouch Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCrouch"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("holdToCrouch"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("crouchKey"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("crouchHeight"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speedReduction"));

        DrawCenteredHeader("Head Bob Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enableHeadBob"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("joint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bobSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bobAmount"));

        DrawCenteredHeader("Mouse Settings");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mouseSensitivity"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxLookAngle"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultFOV"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraCanMove"));

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCenteredHeader(string header)
    {
        GUILayout.Space(10);
        GUILayout.Label(header, EditorStyles.boldLabel);
        var rect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(1));
        EditorGUI.DrawRect(rect, Color.black);
        GUILayout.Space(5);
    }
}
