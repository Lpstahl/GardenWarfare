using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint; // Define o Waypoint como o alvo.

    private void OnSceneGUI()
    {
        Handles.color = Color.red; // Define a cor do Handle como vermelho.
        if (Waypoint.Points != null)
        {
            for (int i = 0; i < Waypoint.Points.Length; i++)
            {
                EditorGUI.BeginChangeCheck(); // Inicia a verificação de mudanças.

                Vector3 currentWaypointPoint = Waypoint.CurrentPos + Waypoint.Points[i]; // Cria um Handle para o ponto atual.

                var fmh_20_85_638622600260536811 = Quaternion.identity; Vector3 newwaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap); // Cria um Handle para o novo ponto.

                GUIStyle textstyle = new GUIStyle(); // Cria um novo GUIStyle.
                textstyle.fontStyle = FontStyle.Bold; // Define o estilo da fonte como negrito.
                textstyle.fontSize = 16; // Define o tamanho da fonte como 16.
                textstyle.normal.textColor = Color.yellow; // Define a cor da fonte como amarela.
                Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f; // Define o alinhamento do texto.
                Handles.Label(Waypoint.CurrentPos + Waypoint.Points[i] + textAlligment, $"{i + 1}", textstyle); // Cria um label para o ponto.

                EditorGUI.EndChangeCheck(); // Finaliza a verificação de mudanças.

                if (EditorGUI.EndChangeCheck()) // Se houve mudanças,
                {
                    Undo.RecordObject(Waypoint, "Free Move Handle"); // Registra a mudança no objeto.
                    Waypoint.Points[i] = newwaypointPoint - Waypoint.CurrentPos; // Atualiza o ponto.
                }
            }
        }
    }
}
