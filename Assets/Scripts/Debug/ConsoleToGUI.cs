using EditorCustom.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DebugStuff
{
    [Todo("Release => Disable")]
    public class ConsoleToGUI : MonoBehaviour
    {
#if true
        #region fields & properties
        private const int maxChars = 10000;
        private string logInfo = "*";
        private string fileName = "";
        string startTimeFormatted = "";

        [SerializeField] private bool drawField = true;
        [SerializeField] private bool enableFileLog = false;
        [SerializeField] private bool ignoreEditor = true;
        [SerializeField] private Vector2 consoleScale = new(1200, 800);
        [SerializeField] private Vector2 textArea = new(540, 370);
        private static string SaveDirectory => System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/AS_Logs";
        #endregion fields & properties

        #region methods
        private void Awake()
        {
            Log("Start Log", null, LogType.Log);
            if (enableFileLog)
            {
                Log("File log enabled", null, LogType.Log);
            }
        }
        private void OnEnable()
        {
            Application.logMessageReceived += Log;
        }
        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        private void Log(string logString, string stackTrace, LogType type)
        {
#if UNITY_EDITOR
            if (ignoreEditor) return;
#endif
            logInfo = logInfo + "\n" + logString;

            FixField();
            TryDraw(type);

            if (!enableFileLog) return;
            SaveFileLog(logString, stackTrace, type);
        }
        private void FixField()
        {
            if (logInfo.Length > maxChars)
                logInfo = logInfo.Substring(logInfo.Length - maxChars);
        }
        private void TryDraw(LogType type)
        {
            if (type == LogType.Assert || type == LogType.Exception || type == LogType.Error)
                drawField = true;
        }

        private void SaveFileLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Warning) return;
            string currentTime = $"{Time.realtimeSinceStartup:F3}";
            logString += $" [Trace ({type}): {stackTrace}]";
            logString += $" - {currentTime} s. - \r\n";

            TrySetFileName();
            try { System.IO.File.AppendAllText(fileName, logString + "\n"); }
            catch { }

            if (type == LogType.Log) return;
            /*string json = "";
            try { json = JsonUtility.ToJson(GetDebugData(), true); }
            catch
            {
                Debug.Log("Can't save debug data");
                return;
            }

            string path = SaveDirectory + "/save data-" + startTimeFormatted + "-" + currentTime + ".json";
            File.WriteAllText(path, json);*/
        }

        private void TrySetFileName()
        {
            if (fileName == "")
            {
                System.IO.Directory.CreateDirectory(SaveDirectory);
                startTimeFormatted = DateTime.Now.ToString("MM.dd.yyyy HH.mm.ss");
                fileName = SaveDirectory + "/log-" + startTimeFormatted + ".txt";
            }
        }
        private void DrawField()
        {
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / consoleScale.x, Screen.height / consoleScale.y, 1.0f));
            GUI.TextArea(new Rect(10, 10, textArea.x, textArea.y), logInfo);
        }
        private void OnGUI()
        {
            if (!drawField) return;
#if UNITY_EDITOR
            if (ignoreEditor) return;
#endif
            DrawField();
        }

        private DebugData GetDebugData()
        {
            DebugData data = new();

            return data;
        }
        #endregion methods
        [System.Serializable]
        private class DebugData
        {

        }
#endif
    }
}