using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using UnityEngine;
using IntPtr = System.IntPtr;
using Object = UnityEngine.Object;

namespace TheSpaceRoles;

public class Sprites
{

    public static Dictionary<string, Sprite> CachedSprites = new Dictionary<string, Sprite>();

    public static Dictionary<string, Texture2D> CachedTexture = [];


    public static Sprite GetSprite(string path, float pixelsPerUnit = 115f)
    {
        try
        {
            if (CachedSprites.TryGetValue(path + pixelsPerUnit, out var value))
            {
                return value;
            }
            Texture2D val = LoadTextureFromResources(path);
            value = Sprite.Create(val, new Rect(0f, 0f, val.width, (float)((Texture)val).height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            Sprite obj = value;
            obj.hideFlags = obj.hideFlags;
            return CachedSprites[path + pixelsPerUnit] = value;
        }
        catch (System.Exception ex)
        {
            Logger.Message("Error can't load sprite path:" + path + "\nError : " + ex, "", "GetSprite");
        }
        return null;
    }

    public static Sprite GetSpriteFromResources(string path, float pixelsPerUnit = 115f)
    {
        return GetSprite("TheSpaceRoles.Resources." + path, pixelsPerUnit);
    }

    public static Texture2D LoadTextureFromResources(string path)
    {
        //IL_0021: Unknown result type (might be due to invalid IL or missing references)
        //IL_0027: Expected O, but got Unknown
        //IL_005e: Unknown result type (might be due to invalid IL or missing references)
        //IL_0065: Unknown result type (might be due to invalid IL or missing references)
        try
        {
            if (CachedTexture.TryGetValue(path, out var value))
            {
                return value;
            }
            value = new Texture2D(2, 2, (TextureFormat)5, true);
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(path);
            byte[] array = new byte[manifestResourceStream.Length];
            int num = manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
            LoadImage(value, array, markNonReadable: false);
            Texture2D obj = value;
            return CachedTexture[path] = value;
        }
        catch
        {
            Logger.Warning("Error loading texture from resources: " + path, "", "LoadTextureFromResources");
        }
        return null;
    }

    internal delegate bool d_LoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
    internal static d_LoadImage iCall_LoadImage;

    private static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
    {
        if (iCall_LoadImage == null)
            iCall_LoadImage = IL2CPP.ResolveICall<d_LoadImage>("UnityEngine.ImageConversion::LoadImage");
        var il2cppArray = (Il2CppStructArray<byte>)data;
        return iCall_LoadImage.Invoke(tex.Pointer, il2cppArray.Pointer, markNonReadable);
    }

    public static GameObject Render(string noResourcePath, string name, float pixelsPerUnit = 115f, int layer = 5, int sortingLayerID = 0, int sortingOrder = 5, GameObject parent = null, bool active = true)
    {
        //IL_0001: Unknown result type (might be due to invalid IL or missing references)
        //IL_0006: Unknown result type (might be due to invalid IL or missing references)
        //IL_000e: Unknown result type (might be due to invalid IL or missing references)
        //IL_0017: Unknown result type (might be due to invalid IL or missing references)
        //IL_0020: Expected O, but got Unknown
        //IL_0054: Unknown result type (might be due to invalid IL or missing references)
        GameObject val = new GameObject
        {
            layer = layer,
            active = active,
            name = name
        };
        if (parent != null)
        {
            val.transform.parent = parent.transform;
        }
        val.transform.localPosition = new Vector3(0f, 0f, -38f);
        SpriteRenderer val2 = val.AddComponent<SpriteRenderer>();
        val2.sprite = GetSprite("ChatHelperPlus.Resources." + noResourcePath, pixelsPerUnit);
        val2.sortingLayerID = sortingLayerID;
        ((Renderer)val2).sortingOrder = sortingOrder;
        return val;
    }

    public static GameObject GobjRender(GameObject @object, string noResourcePath, string name, float scale, float size = 1f, int layer = 5, int sortingLayerID = 50, int sortingOrder = 5, GameObject parent = null, bool active = true)
    {
        //IL_0093: Unknown result type (might be due to invalid IL or missing references)
        Sprite sprite = GetSprite("ChatHelperPlus.Resources." + noResourcePath, scale);
        Logger.Info("ChatHelperPlus.Resources." + noResourcePath + " : path" + (sprite.texture).ToString(), "", "GobjRender");
        GameObject val = Object.Instantiate<GameObject>(@object);
        (val).name = name;
        if (val == null)
        {
            return null;
        }
        if (parent != null)
        {
            val.transform.parent = parent.transform;
        }
        val.transform.position = new Vector3(0f, 0f, 0f);
        SpriteRenderer component = val.GetComponent<SpriteRenderer>();
        component.sprite = sprite;
        ((Renderer)component).sortingLayerID = sortingLayerID;
        ((Renderer)component).sortingOrder = sortingOrder;
        return val;
    }
}
