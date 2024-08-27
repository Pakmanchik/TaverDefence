using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace EditorTools.RuleTileGenerator
{
#if UNITY_EDITOR
    public class RuleTileGeneratorEditorWindow : EditorWindow
    {
        private UnityEngine.Object _referenceTileRuleObject;
        private UnityEngine.Object _targetTileRuleObject;
        private UnityEngine.Object _referencePngAsset;
        private UnityEngine.Object _targetPngAsset;

        private string _refPngAssetGuid;
        private string _targetPngAssetGuid;

        [MenuItem("LocalTools/RuleTileGeneratorEditorWindow")]
        public static void ShowWindow()
        {
            GetWindow<RuleTileGeneratorEditorWindow>("RuleTileGenerator");
        }

        private void OnGUI()
        {
            __ShowAssetsField();

            __ShowAssetsLoadedState();

            var loadSpriteInfoResult =
                __TryLoadAllSpriteInfos(
                    out var refTileRuleInfo,
                    out var refPngInfo,
                    out var targetPngInfo);

            __ShowSpritesInfoLoadedState(loadSpriteInfoResult);

            if (!loadSpriteInfoResult)
                return;

            __DrawProcessButton(out var isButtonPressed);

            if (isButtonPressed)
            {
                if (!__TryProcessTargetRuleTile(refTileRuleInfo, refPngInfo, targetPngInfo))
                    Debug.LogError("An error occured while asset is editing");
            }

            var tempButtonPressed = GUILayout.Button("Test button");

            if (tempButtonPressed)
            {
                var sb = new StringBuilder();

                foreach (var refInfo in refTileRuleInfo)
                {
                    var (fileId, guid) = refInfo;
                    sb.AppendLine($"FileID: {fileId}, GUID: {guid}");
                }

                Debug.Log($"Ref tileRule info array:\r\n" +
                          $"Size: {refTileRuleInfo.Count}\r\n {sb.ToString()}");
            }

            return;

            void __ShowAssetsField()
            {
                _referenceTileRuleObject =
                    EditorGUILayout.ObjectField("Reference TileRule asset", _referenceTileRuleObject, typeof(TileBase), false);
                _targetTileRuleObject =
                    EditorGUILayout.ObjectField("Target TileRule asset", _targetTileRuleObject, typeof(TileBase), false);
                _referencePngAsset =
                    EditorGUILayout.ObjectField("Reference PNG asset", _referencePngAsset, typeof(Texture2D), false);
                _targetPngAsset =
                    EditorGUILayout.ObjectField("Target PNG asset", _targetPngAsset, typeof(Texture2D), false);
            }

            void __ShowAssetsLoadedState()
            {
                var style = new GUIStyle(GUI.skin.label);
                string text;
                if (!_referenceTileRuleObject || !_targetTileRuleObject || !_referencePngAsset || !_targetPngAsset)
                {
                    style.normal.textColor = Color.red;
                    text = "LOADING ERROR";
                }
                else
                {
                    style.normal.textColor = Color.green;
                    text = "LOADED SUCCESSFULLY";
                }

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Assets loading status:");
                EditorGUILayout.LabelField(text, style);

                EditorGUILayout.EndHorizontal();
            }

            void __ShowSpritesInfoLoadedState(bool isLoadedCorrectly)
            {
                var style = new GUIStyle(GUI.skin.label);
                string text;

                if (isLoadedCorrectly)
                {
                    style.normal.textColor = Color.green;
                    text = "LOADED SUCCESSFULLY";

                }
                else
                {
                    style.normal.textColor = Color.red;
                    text = "LOADING ERROR";
                }

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Result of loading sprite information:");
                EditorGUILayout.LabelField(text, style);

                EditorGUILayout.EndHorizontal();
            }

            bool __TryLoadAllSpriteInfos(
                out List<(string, string)> refTileRuleInfo,
                out List<(string, string)> refPngInfo,
                out List<(string, string)> targetPngInfo)
            {
                refTileRuleInfo = new List<(string, string)>();
                refPngInfo = new List<(string, string)>();
                targetPngInfo = new List<(string, string)>();

                try
                {
                    bool res = true;
                    // Загружаем текст ассетов TileRule
                    res &= __TryLoadRawTextFromAsset(_referenceTileRuleObject, out var refTileRuleAssetRawText);
                    // Загружаем текст meta от PNG картинок
                    res &= __TryLoadMetaDataFromAsset(_referencePngAsset, out var refPngAssetMetaRawText);
                    res &= __TryLoadMetaDataFromAsset(_targetPngAsset, out var targetPngAssetMetaRawText);

                    // Если что-то не удалось загрузить - выходим
                    if (!res)
                        return false;

                    // Сохраняем GUID обоих PNG asset из их меты
                    res = true;
                    res &= __TryLoadAssetGuidFromMetaRawText(refPngAssetMetaRawText, out _refPngAssetGuid);
                    res &= __TryLoadAssetGuidFromMetaRawText(targetPngAssetMetaRawText, out _targetPngAssetGuid);

                    // Если что-то не удалось загрузить - выходим
                    if (!res)
                        return false;

                    // Загружаем информацию из строки в коллекцию <fileID, guid>:
                    //      m_Sprites:
                    //      - {fileID: 1714184982, guid: 920249c7b227d3f48b18112b848d9198, type: 3}
                    res = true;
                    res &= __TryLoadInfoFromTileRule(refTileRuleAssetRawText, out refTileRuleInfo);

                    // Загружаем упорядоченный список internalID из png meta обоих файлов. Порядок нам очень важен
                    res &= __TryLoadIdsFromPngMeta(refPngAssetMetaRawText, out refPngInfo);
                    res &= __TryLoadIdsFromPngMeta(targetPngAssetMetaRawText, out targetPngInfo);

                    return res;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }

            bool __TryLoadAssetGuidFromMetaRawText(string rawText, out string assetGuid)
            {
                assetGuid = string.Empty;

                var pattern = @"guid: ([0-9a-zA-Z]+)";

                try
                {
                    Match match = Regex.Match(rawText, pattern);

                    if (!match.Success)
                        return false;

                    var guid = match.Groups[1].Value;

                    if (string.IsNullOrWhiteSpace(guid))
                        return false;

                    assetGuid = guid;
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }

            bool __TryLoadRawTextFromAsset(Object asset, out string assetRawText)
            {
                assetRawText = string.Empty;

                if (!asset)
                    return false;

                try
                {
                    var tmp = AssetDatabase.GetAssetPath(asset);

                    if (string.IsNullOrEmpty(tmp))
                        return false;

                    var tmpText = File.ReadAllText(tmp);

                    if (string.IsNullOrWhiteSpace(tmpText))
                        return false;

                    assetRawText = tmpText;
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }

            bool __TryLoadMetaDataFromAsset(Object asset, out string metaRawText)
            {
                metaRawText = string.Empty;

                if (!asset)
                    return false;

                try
                {
                    var tmp = AssetDatabase.GetAssetPath(asset);

                    if (string.IsNullOrEmpty(tmp))
                        return false;

                    tmp = AssetDatabase.GetTextMetaFilePathFromAssetPath(tmp);

                    if (string.IsNullOrEmpty(tmp))
                        return false;

                    var tmpText = File.ReadAllText(tmp);

                    if (string.IsNullOrWhiteSpace(tmpText))
                        return false;

                    metaRawText = tmpText;
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }

            bool __TryLoadInfoFromTileRule(string rawTileRuleAssetData, out List<(string, string)> spritesInfo)
            {
                spritesInfo = new List<(string, string)>();

                const string pattern = @"m_Sprites:\s+- {fileID: (-?\d+), guid: ([0-9a-z]{32})";

                try
                {
                    var matches = Regex.Matches(rawTileRuleAssetData, pattern);

                    foreach (Match match in matches)
                    {
                        var fileID = match.Groups[1].Value;
                        var guid = match.Groups[2].Value;

                        spritesInfo.Add((fileID, guid));
                    }

                    return spritesInfo.Count != 0;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }

            bool __TryLoadIdsFromPngMeta(string rawPngMetaText, out List<(string, string)> spritesInfo)
            {
                spritesInfo = new List<(string, string)>();

                const string pattern = @"spriteID: ([0-9a-z]+)\s+internalID: (-?[0-9a-z]+)";

                try
                {
                    var matches = Regex.Matches(rawPngMetaText, pattern);

                    foreach (Match match in matches)
                    {
                        var spriteID = match.Groups[1].Value;
                        var internalID = match.Groups[2].Value;

                        spritesInfo.Add((spriteID, internalID));
                    }

                    return spritesInfo.Count != 0;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }

            void __DrawProcessButton(out bool isButtonPressed)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                isButtonPressed = GUILayout.Button("Process Target TileRule", GUILayout.ExpandWidth(false));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            bool __TryProcessTargetRuleTile(
                List<(string, string)> refTileRuleInfo,
                List<(string, string)> refPngInfo,
                List<(string, string)> targetPngInfo)
            {
                try
                {
                    // Загружаем всю информацию из целевого тайл сета
                    if (!__TryLoadRawTextFromAsset(_targetTileRuleObject, out var inText))
                        return false;

                    // Заменяем все guid спрайта ref на target
                    inText = inText.Replace(_refPngAssetGuid, _targetPngAssetGuid);

                    // Заменяем все fileId спрайта с ref на target
                    foreach (var tileInfo in refTileRuleInfo)
                    {
                        // Извлекаем fileId ref PNG (guid уже заменен)
                        var (fileId, guid) = tileInfo;

                        // Ищем индекс такого fileId в мете ref PNG
                        int refIndex = -1;
                        string refInternalId = String.Empty;

                        for (var i = 0; i < refPngInfo.Count; i++)
                        {
                            var (spriteId, internalId) = refPngInfo[i];

                            if (fileId == internalId)
                            {
                                refIndex = i;
                                refInternalId = internalId;
                                break;
                            }
                        }

                        // Если такого fileId не удалось найти в мете - ошибка, выходим
                        if (refIndex == -1)
                            return false;

                        // Получаем такой же InternalID из target PNG по тому же индексу
                        var (targetSpriteId, targetInternalId) = targetPngInfo[refIndex];

                        // Заменяем его в target tile rule
                        inText = inText.Replace(refInternalId, targetInternalId);
                    }

                    // Если дошли до данной строки, то все известные guid заменены. Сохраняем изменения
                    var filePath = AssetDatabase.GetAssetPath(_targetTileRuleObject);

                    if (string.IsNullOrWhiteSpace(filePath))
                        return false;

                    File.WriteAllText(filePath, inText);

                    // Реимпортим ассет
                    AssetDatabase.ImportAsset(filePath, ImportAssetOptions.ForceUpdate);

                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }
        }
    }
#endif
}