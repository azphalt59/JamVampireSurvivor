using Common.Tools;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityCommon.Tools
{
    [CustomPropertyDrawer(typeof(InstantiableAttribute))]
    public class InstantiableDrawer : PropertyDrawer
    {
        bool _initialized;
        List<Type> _subClasses;
        List<string> _subClassesStr;
        int _selected = 0;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_initialized == false)
                Initialize(property);

            string header = "NONE";
            if (property.type != "managedReference<>")
                header = MagnifyType(property.type);

            //    DrawUILine(position, Color.white);
            DrawHeader(position, header, new Color(0.15f, 0.1f, 0.95f));
            position.y += 24;
            EditorGUI.BeginProperty(position, label, property);

            Rect popupRect = position;

            position.height = EditorGUIUtility.singleLineHeight;
            int selected = EditorGUI.Popup(position, "Type", _selected, _subClassesStr.ToArray());

            if (_selected != selected)
            {
                _selected = selected;

                if (_selected > 0)
                    SetType(property, _subClasses[_selected - 1]);
            }

            float totalHeight = EditorGUIUtility.singleLineHeight + 2;
            foreach (SerializedProperty child in GetChildren(property))
            {
                float height = EditorGUI.GetPropertyHeight(child);

                var rect = new Rect(position.x, position.y + totalHeight, position.width,
                    height);
                EditorGUI.PropertyField(rect, child);


                totalHeight += height + 2;
            }


            position.y += totalHeight;

            //      DrawUILine(position, Color.white);


            EditorGUI.EndProperty();
        }

        public static void DrawUILine(Rect position, Color color, int thickness = 2, int padding = 5)
        {
            position.height = thickness;
            position.y += padding;
            position.x -= 2;
            position.width -= 2;
            EditorGUI.DrawRect(position, color);
        }

        public static void DrawHeader(Rect position, string text, Color color, int thickness = 15, int padding = 5)
        {
            position.height = thickness;
            position.y += padding;
            position.x -= 2;
            position.width -= 2;
            EditorGUI.DrawRect(position, color);
            EditorGUI.LabelField(position, text);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight + 2;
            foreach (SerializedProperty child in GetChildren(property))
            {
                float height = EditorGUI.GetPropertyHeight(child);
                totalHeight += height + 2;
            }


            return totalHeight + 18 + 13;
        }

        private void SetType(SerializedProperty property, Type type)
        {
            property.serializedObject.Update();

            object newInstance = System.Activator.CreateInstance(type);
            property.managedReferenceValue = newInstance;
            property.serializedObject.ApplyModifiedProperties();
        }

        void Initialize(SerializedProperty property)
        {
            _initialized = true;

            if (attribute is InstantiableAttribute attr)
            {
                _subClasses = new List<Type>(GetAllSubclassOf(attr.Type));
                _subClassesStr = new List<string> { "Choose Type" };

                foreach (var c in _subClasses)
                {
                    _subClassesStr.Add(c.Name);
                }
            }

            string type = MagnifyType(property.type);
            _selected = GetTypeIndex(type);
        }

        string MagnifyType(string type)
        {
            var rg = new Regex(@"<(\w+)>");
            var match = rg.Match(type);

            return match.Success ? match.Groups[1].Value : type;
        }

        int GetTypeIndex(string type)
        {
            return _subClassesStr.IndexOf(type);
        }

        static IEnumerable<Type> GetAllSubclassOf(Type parent)
        {
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var t in a.GetTypes())
                    if (t.IsSubclassOf(parent))
                        yield return t;
        }

        static Type GetType(string typeName)
        {
            Type type = null;
            var assembly = typeof(InstantiableAttribute).Assembly;

            type = assembly.GetType(typeName);

            return type;
        }

        private IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
        {
            SerializedProperty currentProperty = property.Copy();
            SerializedProperty nextProperty = property.Copy();
            nextProperty.Next(false);

            if (currentProperty.Next(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(currentProperty, nextProperty)) break;
                    yield return currentProperty;
                } while (currentProperty.Next(false));
            }
        }
    }
}