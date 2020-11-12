using System.IO;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CustomizationSet : ScriptableObject
{
    [SerializeField] private string _characterPrefabName = default;
    
    public string CharacterPrefabName => _characterPrefabName;

    public List<Texture2D> LoadLegTextures()
    {
        string path = Path.Combine(Application.streamingAssetsPath, _characterPrefabName, "Legs");
        List<Texture2D> legTextures = new List<Texture2D>();
        var directoryInfo = new DirectoryInfo(path);
        var allFiles = directoryInfo.GetFiles("*.*");
        foreach (var fileInfo in allFiles)
        {
            if (fileInfo.Name.Contains("meta"))
            {
                continue;
            }
            var bytes = File.ReadAllBytes(fileInfo.FullName);
            
            // TGA format is not supported => I have to use wrapper
            var texture2D = TGALoader.LoadTGA(fileInfo.FullName);
            legTextures.Add(texture2D);
        }

        return legTextures;
    }
    
    public List<Texture2D> LoadTorsoTextures()
    {
        string path = Path.Combine(Application.streamingAssetsPath, _characterPrefabName, "Torso");
        List<Texture2D> torsoTextures = new List<Texture2D>();
        var directoryInfo = new DirectoryInfo(path);
        var allFiles = directoryInfo.GetFiles("*.*");
        foreach (var fileInfo in allFiles)
        {
            if (fileInfo.Name.Contains("meta"))
            {
                continue;
            }
            var bytes = File.ReadAllBytes(fileInfo.FullName);
            
            // TGA format is not supported => I have to use wrapper
            var texture2D = TGALoader.LoadTGA(fileInfo.FullName);
            torsoTextures.Add(texture2D);
        }

        return torsoTextures;
    }
}
