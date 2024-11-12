using UnityEditor;
using UnityEngine;
using System.IO;

public class TextureResizerEditor : Editor
{
    // 在 Texture2D 上添加右键菜单
    [MenuItem("Assets/格式化Texture", true)]
    private static bool ValidateResizeTexture()
    {
        // 仅在选中 Texture2D 时显示菜单项
        return Selection.activeObject is Texture2D;
    }

    [MenuItem("Assets/格式化Texture/扩展")]
    private static void ExpandTextureToNearestMultipleOf4()
    {
        ResizeTextureToMultipleOf4(true);
    }

    [MenuItem("Assets/格式化Texture/裁切")]
    private static void CropTextureToNearestMultipleOf4()
    {
        ResizeTextureToMultipleOf4(false);
    }

    /// <summary>
    /// 重新调整图片大小，使其扩展或裁切为4的倍数
    /// </summary>
    /// <param name="isExpand">如果为 true 则扩展，否则裁切</param>
    private static void ResizeTextureToMultipleOf4(bool isExpand)
    {
        Texture2D original = Selection.activeObject as Texture2D;
        if (original == null) return;

        // 获取原始路径并加载为 TextureImporter
        string path = AssetDatabase.GetAssetPath(original);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        if (importer != null && !importer.isReadable)
        {
            importer.isReadable = true; // 设置为可读
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate); // 强制重新导入
        }

        int newWidth = RoundToNearestMultipleOf4(original.width, isExpand);
        int newHeight = RoundToNearestMultipleOf4(original.height, isExpand);

        Texture2D resizedTexture = isExpand
            ? ExtendTexture(original, newWidth, newHeight)
            : CropTexture(original, newWidth, newHeight);

        SaveResizedTexture(resizedTexture, original);

        importer.isReadable = false;
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate); // 强制重新导入
    }

    /// <summary>
    /// 计算最接近的4的倍数
    /// </summary>
    private static int RoundToNearestMultipleOf4(int value, bool isExpand)
    {
        return isExpand ? Mathf.CeilToInt(value / 4f) * 4 : Mathf.FloorToInt(value / 4f) * 4;
    }

    /// <summary>
    /// 扩展图片
    /// </summary>
    private static Texture2D ExtendTexture(Texture2D original, int newWidth, int newHeight)
    {
        Texture2D extendedTexture = new Texture2D(newWidth, newHeight, original.format, false);

        // 填充透明像素
        Color[] clearPixels = new Color[newWidth * newHeight];
        for (int i = 0; i < clearPixels.Length; i++) clearPixels[i] = Color.clear;
        extendedTexture.SetPixels(clearPixels);

        // 计算原图的起始位置并填充
        int xOffset = (newWidth - original.width) / 2;
        int yOffset = (newHeight - original.height) / 2;
        extendedTexture.SetPixels(xOffset, yOffset, original.width, original.height, original.GetPixels());
        extendedTexture.Apply();

        return extendedTexture;
    }

    /// <summary>
    /// 裁切图片
    /// </summary>
    private static Texture2D CropTexture(Texture2D original, int newWidth, int newHeight)
    {
        Texture2D croppedTexture = new Texture2D(newWidth, newHeight, original.format, false);

        int xOffset = (original.width - newWidth) / 2;
        int yOffset = (original.height - newHeight) / 2;
        croppedTexture.SetPixels(original.GetPixels(xOffset, yOffset, newWidth, newHeight));
        croppedTexture.Apply();

        return croppedTexture;
    }

    /// <summary>
    /// 保存调整后的图片到磁盘
    /// </summary>
    private static void SaveResizedTexture(Texture2D texture, Texture2D original)
    {
        string path = AssetDatabase.GetAssetPath(original);
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);

        AssetDatabase.Refresh();
    }
}
