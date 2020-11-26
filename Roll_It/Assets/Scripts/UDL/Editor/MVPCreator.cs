using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UDL.Editor
{
    public class MVPCreator : EditorWindow
    {
        const string PATH_TO_TEMPLATE = "Scripts/UDL/Editor/Template/";
        private string PATH_TO_ROOT;
        private string _modelSavePath;
        private string _viewSavePath;
        private string _presenterSavePath;
        private string _factorySavePath;

        private string _pathName = "Path/Path";
        private string _baseName = "PutNameHere";
        private bool _isScreen = false;
        private bool _hasContext = false;

        private bool _viewNeeded = true;
        private bool _modelNeeded = false;
        private bool _prefabNeeded = false;
        private bool _playModeTestNeeded = false;

        [MenuItem("Tools/MVP Creator")]
        private static void Init()
        {
            var window = GetWindowWithRect<MVPCreator>(new Rect(0, 0, 450, 350));
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Path Name");
            _pathName = EditorGUILayout.TextField(_pathName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Base Name");
            _baseName = EditorGUILayout.TextField(_baseName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("This is a screen");
            _isScreen = EditorGUILayout.Toggle(_isScreen);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("It has context");
            _hasContext = EditorGUILayout.Toggle(_hasContext);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("View and Factory");
            _viewNeeded = EditorGUILayout.Toggle(_viewNeeded);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Model and Presenter");
            _modelNeeded = EditorGUILayout.Toggle(_modelNeeded);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            if (GUILayout.Button("Create " + _baseName + " Classes"))
            {
                PATH_TO_ROOT = "Scripts/Project/" + _pathName + "/";
                _modelSavePath = PATH_TO_ROOT + "Model/";
                _viewSavePath = PATH_TO_ROOT + "View/";
                _presenterSavePath = PATH_TO_ROOT + "Presenter/";
                _factorySavePath = PATH_TO_ROOT + "/";


                CopyFile(PATH_TO_TEMPLATE + "ProjectDefinition.asmdef.txt", "Scripts/Project/Project.asmdef");
                
                if (_viewNeeded)
                {
                    string factoryFileName = "";

                    if (_modelNeeded)
                    {
                        factoryFileName = (_isScreen) ?
                        (_hasContext) ? "TemplateScreenFactoryWithContext.cs.txt" : "TemplateScreenFactory.cs.txt" :
                        (_hasContext) ? "TemplateFactoryWithContext.cs.txt" : "TemplateFactory.cs.txt";
                    }
                    else
                    {
                        factoryFileName = (_isScreen) ?
                        (_hasContext) ? "TemplateViewOnlyScreenFactoryWithContext.cs.txt" : "TemplateViewOnlyScreenFactory.cs.txt" :
                        (_hasContext) ? "TemplateViewOnlyFactoryWithContext.cs.txt" : "TemplateViewOnlyFactory.cs.txt";
                    }

                    CopyFile(PATH_TO_TEMPLATE + factoryFileName, _factorySavePath + _baseName + "Factory.cs");

                    if (_modelNeeded == false && _hasContext == true)
                    {
                        CopyFile(PATH_TO_TEMPLATE + "TemplateViewWithContext.cs.txt", _viewSavePath + _baseName + "View.cs");
                    }
                    else
                    {
                        CopyFile(PATH_TO_TEMPLATE + "TemplateView.cs.txt", _viewSavePath + _baseName + "View.cs");
                    }

                }

                if (_modelNeeded)
                {
                    string modelFileName = (_hasContext) ? "TemplateModelWithContext.cs.txt" : "TemplateModel.cs.txt";

                    CopyFile(PATH_TO_TEMPLATE + modelFileName, _modelSavePath + _baseName + "Model.cs");
                    CopyFile(PATH_TO_TEMPLATE + "TemplatePresenter.cs.txt", _presenterSavePath + _baseName + "Presenter.cs");
                }
                
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Prefab"))
            {
                CreatePrefab();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Play Mode Test"))
            {
                CreatePlayModeTest();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Edit Mode Test"))
            {
                CreateEditModeTest();
            }
        }

        private void CopyFile(string from, string to)
        {
            var sourcePath = Path.Combine(Application.dataPath, from);
            var targetPath = Path.Combine(Application.dataPath, to);

            var template = File.ReadAllText(sourcePath);
            template = template.Replace("#BASENAME#", _baseName);
            template = template.Replace("#PATH#", _pathName);
            template = template.Replace("#NAMESPACE#", _pathName.Replace("/", "."));

            CreateFolder(to);

            if (File.Exists(targetPath) == false)
            {
                File.WriteAllText(targetPath, template);
            }
            AssetDatabase.ImportAsset(targetPath);
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
        }

        private void CreatePrefab()
        {
            string outputPath = "Resources/Prefabs/" + _pathName + "/" + _baseName + "View.prefab";

            CreateFolder(outputPath);

            GameObject gameObject = EditorUtility.CreateGameObjectWithHideFlags(name, HideFlags.HideInHierarchy);
            if (_isScreen)
            {
                gameObject.AddComponent<RectTransform>();
                gameObject.AddComponent<CanvasRenderer>();
            }

            Assembly assembly = Assembly.Load("Project");

            string typeFullName = "Project." + _pathName.Replace('/', '.') + ".View." + _baseName + "View";
            Type t = assembly.GetType(typeFullName);

            if (t != null)
            {
                gameObject.AddComponent(t);
            }

            PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/" + outputPath);
            DestroyImmediate(gameObject);
        }

        private void CreateFolder(string to)
        {
            List<string> targetSegments = to.Split('/').ToList();
            targetSegments.RemoveAt(targetSegments.Count - 1);
            string path = Application.dataPath;
            foreach (string segment in targetSegments)
            {
                path += "/" + segment;
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private void CreatePlayModeTest()
        {
            string folder = "Scripts/Test/PlayMode/";

            var assemblyTemplatePath = PATH_TO_TEMPLATE + "PlayModeTestDefinition.asmdef.txt";
            var assemblyTargetPath = folder + "PlayModeTest.asmdef";

            CopyFile(assemblyTemplatePath, assemblyTargetPath);

            var testTemplatePath = "";

            if (_modelNeeded)
            {
                testTemplatePath = PATH_TO_TEMPLATE + ((_hasContext) ? "PlayModeTestTemplateWithContext.cs.txt" : "PlayModeTestTemplate.cs.txt");
            }
            else
            {
                testTemplatePath = PATH_TO_TEMPLATE + ((_hasContext) ? "ViewOnlyPlayModeTestTemplateWithContext.cs.txt" : "ViewOnlyPlayModeTestTemplate.cs.txt");
            }

            var testTargetPath = folder + _pathName + "/" + _baseName + "PlayModeTest.cs";

            CopyFile(testTemplatePath, testTargetPath);

            AssetDatabase.Refresh();
        }

        private void CreateEditModeTest()
        {
            string folder = "Scripts/Test/EditMode/";

            var assemblyTemplatePath = PATH_TO_TEMPLATE + "EditModeTestDefinition.asmdef.txt";
            var assemblyTargetPath = folder + "EditModeTest.asmdef";

            CopyFile(assemblyTemplatePath, assemblyTargetPath);

            var testTemplatePath = PATH_TO_TEMPLATE + ((_hasContext) ? "EditModeTestTemplateWithContext.cs.txt" : "EditModeTestTemplate.cs.txt");
            var testTargetPath = folder + _pathName + "/" + _baseName + "EditModeTest.cs";

            CopyFile(testTemplatePath, testTargetPath);

            AssetDatabase.Refresh();
        }
    }
}
