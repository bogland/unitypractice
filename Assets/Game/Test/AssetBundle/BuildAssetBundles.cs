using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class BuildAsssetBundles : EditorWindow
{
    [MenuItem("Custom/Build AssetBundles Direct")]
    public static void BuildAllAssetBundlesDirect()
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
    [MenuItem("Custom/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        var resourcePath = Application.dataPath + "/Texture/Ani";
        string mask = "*.png,*.spriteatlasv2,*.prefab";
        string[] filePaths = Directory.GetFiles(resourcePath, "*.*", SearchOption.AllDirectories).Where(s => mask.Contains(Path.GetExtension(s).ToLower())).ToArray();
        List<AssetInfo> fileNameList = new List<AssetInfo>();
        foreach (var path in filePaths)
        {
            var path2 = path.Replace("/", "\\");
            Debug.Log(path2);
            var pathProject = Path.GetFullPath(Path.Combine(Application.dataPath, "../"));
            Debug.Log(pathProject);
            var pathFilter = path2.Replace(pathProject, "");
            Debug.Log(pathFilter);
            var obj = AssetImporter.GetAtPath(pathFilter);
            var fileName = Path.GetFileNameWithoutExtension(path);
            obj.assetBundleName = fileName;
            fileNameList.Add(new AssetInfo() { name = fileName, path = pathFilter });
        }
        BuildNeedAssetBundle(fileNameList);
    }
    public static void BuildNeedAssetBundle(List<AssetInfo> fileNameList)
    {
        if (!Directory.Exists(Application.dataPath + "/AssetBundles"))
        {
            Directory.CreateDirectory(Application.dataPath + "/AssetBundles");
        }
        //test 라벨을 가진 어셋번들로 뽑을때
        AssetBundleBuild[] buildBundles =
        {
            new AssetBundleBuild(){
            assetBundleName = "test",
            assetNames = fileNameList.Select(v=>v.path).ToArray(),
        }};
        //개별 라벨을 가진 어셋번들로 뽑을때
        //AssetBundleBuild[] buildBundles = fileNameList.Select(v => new AssetBundleBuild()
        //{
        //    assetBundleName = v.name,
        //    assetNames = new string[1] { v.path },
        //}).ToArray();
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/AssetBundles", buildBundles, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }
}
public class AssetInfo
{
    public AssetInfo()
    {
    }
    public string name { get; set; }
    public string path { get; set; }
}