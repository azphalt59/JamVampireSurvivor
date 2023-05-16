//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityCommon.Graphics;
//using UnityEditor;
//using UnityEngine;



//namespace UnityCommon
//{

//    [CustomEditor(typeof(SpritesAnimationDatas))]
//    public class SpritesAnimationDatasEditor : Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            SpritesAnimationDatas animations = (SpritesAnimationDatas)target;
//            string nameBefore = animations.name;

//            EditorGUI.BeginChangeCheck();

//            base.DrawDefaultInspector();

//            GUILayout.Space(20);
//            bool clickButton = GUILayout.Button("Generate Animations Consts" + animations.name + ".cs");

//            if ((EditorGUI.EndChangeCheck() == true && nameBefore == animations.name ) || clickButton)
//            {
//                GenerateClass(animations);

//                AssetDatabase.Refresh();
//            }

//            clickButton = GUILayout.Button("Generate Timer Consts");

//            if (clickButton)
//            {
//                GenerateTimes(animations);

//                AssetDatabase.Refresh();
//            }
//        }

//        public static List<T> GetAtPath<T>(string path) where T : UnityEngine.Object
//        {

//            path = Path.GetDirectoryName(path);

//            List<T> al = new List<T>();
//            string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path.Replace("Asset/",""));
//            foreach (string fileName in fileEntries)
//            {
//                Debug.Log(fileName);

//                int index = fileName.LastIndexOf("/");
//                string localPath =  path;

//                if (index > 0)
//                    localPath += fileName.Substring(index);



//                Debug.Log(localPath);


//                T t = AssetDatabase.LoadAssetAtPath<T>(localPath);

//                Debug.Log("loaded" );


//                if (t != null)
//                    al.Add(t);
//            }

//            return al;
//        }

//        private void GenerateTimes(SpritesAnimationDatas animations)
//        {
//            string path =  AssetDatabase.GetAssetPath(animations);
//            Debug.Log(path);
//            List<SpritesAnimationDatas> datas = GetAtPath<SpritesAnimationDatas>(path);
//            Debug.Log(datas.Count);

//            string directory = Directory.GetCurrentDirectory() + "/Assets/Scripts/Generated/";
//            string filename = "SpritesAnimationTimer.cs";
//            path = directory + filename;

//            if (Directory.Exists(directory) == false)
//                Directory.CreateDirectory(directory);

//            if (File.Exists(path) == false)
//            {
//                using (File.Create(path))
//                {
//                }
//            }

//            //public static class EntityFactory {
//            //public static int GetActionDuration(EntityType type, int actionID) {
//            //    if (type == EntityType.Soldier) {
//            //        if (actionID == EntitiesAnimations.Soldier_Hit){
//            //            return 800;
//            //        }
//            //    }

//            //    return -1;
//            //}
//            string fileText = "public static class EntityFactory { \n{\n";
//            fileText += "public static int GetActionDuration(int type, int actionID) { \n{\n";

//            foreach (var item in datas)
//            {
//                fileText += "if (type == " + item.EntityIndex +") {  \n{\n";

//                int index =0;
//                foreach (var animation in item.Animations)
//                {
//                    int millisecondes = (int)((animation.Sprites.Count * (1.0f / (float)animation.FPS)) * 1000.0f);

//                    fileText += "if (actionID == " + (index++) + ") {  \n{\n";
//                    fileText += "return " + millisecondes + "\n}\n";

//                }
//                fileText += "}\n";
//            }

//            fileText += "return -1;}\n";
//            fileText += "}\n";

//            File.WriteAllText(path, fileText);

//            Debug.Log("[SpritesAnimationDatas] Update time file :" + filename);

//        }

//        void GenerateClass(SpritesAnimationDatas datas)
//        {
//            if (string.IsNullOrEmpty(datas.name))
//            {
//                Debug.LogError("SpritesAnimationDatas name is not set !");
//                return;
//            }

//            string directory = Directory.GetCurrentDirectory() + "/Assets/Scripts/Generated/";
//            string filename = "SpritesAnimationConsts" + datas.name + ".cs";
//            string path = directory + filename;

//            if (Directory.Exists(directory) == false)
//                Directory.CreateDirectory(directory);

//            if (File.Exists(path) == false)
//            {
//                using (File.Create(path))
//                {
//                }
//            }

//            string fileText = "public partial class SpritesAnimationDatas" + datas.name + " \n{\n";

//            int index = 0;
//            foreach (var item in datas.Animations)
//            {
//                string name = item.Name;
//                int millisecondes = (int)((item.Sprites.Count * (1.0f / (float)item.FPS)) * 1000.0f);

//                fileText += "public const int " + datas.name + "_" + name + " = " + (index++) + ";\n";
//            }

//            fileText += "}\n";

//            File.WriteAllText(path, fileText);

//            Debug.Log("[SpritesAnimationDatas] Update enum file :" + filename);
//        }

//    }


//}