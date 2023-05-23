using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

namespace MyMods;

public static class CustomiseCarePackages
{
    #region Private

    private static readonly string[] CategoryOrder = [
        "Egg",
        "Seed",
        "Edible",
        "Clothes",
        "Creature",
        "Materials",
        "GasMaterials",
        "SolidMaterials",
        "LiquidMaterials"];

    #endregion

    #region Initialise

    public static void Initialise()
    {
        CarePackageContainer_OnSpawn.OnPostfix += OnSpawn;

        void OnSpawn(CarePackageContainer cpc)
        {
            if (cpc.GameObject().GetComponent<MyModMouseClick>() is null)
                cpc.GameObject().AddComponent<MyModMouseClick>().Click += OnClick;

            void OnClick(GameObject obj, InputButton btn)
            {
                // Left is select - Use right to edit
                if (btn is InputButton.Left)
                    return;

                var ctrl = Input.GetKey(KeyCode.LeftControl); // Navigate back
                var shft = Input.GetKey(KeyCode.LeftShift); // Next package by category
                var dir = ctrl ? -1 : 1;

                switch (obj.Name())
                {
                    case "PortraitContainer": NextPackage(dir, jump: shft); break;
                        //default: Log.Dev($"{obj.Name()}: {obj.Path()}"); break;
                }

                void NextPackage(int direction, bool jump)
                {
                    DeselectDeliverable();
                    SetPackage(NextPackage());
                    cpc.SelectDeliverable();

                    void DeselectDeliverable()
                        => cpc.Field<CharacterSelectionController>("controller").RemoveLast();

                    CarePackageInfo NextPackage()
                    {
                        var curPackage = cpc.Info;
                        var packageList = EligiblePackages();
                        var curIdx = Mathf.Max(0, packageList.IndexOf(curPackage));

                        if (jump)
                        {
                            var curCategory = Category(curPackage);
                            for (var idx = curIdx + direction; ; idx += direction)
                            {
                                idx = (packageList.Count + idx) % packageList.Count;
                                var p = packageList[idx];
                                if (Category(p) != curCategory) return p;
                                if (idx == curIdx) break;
                            }
                        }

                        var nextIdx = (packageList.Count + curIdx + direction) % packageList.Count;
                        return packageList[nextIdx];
                    }

                    void SetPackage(CarePackageInfo package)
                    {
                        try
                        {
                            CarePackageContainer_IsCharacterRedundant.Override = true;
                            Immigration_RandomCarePackage.Override = package;
                            cpc.Invoke("Reshuffle", false);
                        }
                        finally
                        {
                            Immigration_RandomCarePackage.Override = null;
                            CarePackageContainer_IsCharacterRedundant.Override = false;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region Utilities

    private static List<CarePackageInfo> EligiblePackages()
    {
        return [.. Immigration.Instance.Field<List<CarePackageInfo>>("carePackages")
            .Where(x => x.requirement == null || x.requirement())
            .OrderBy(x => Array.IndexOf(CategoryOrder, Category(x)))
            .ThenBy(x => x.id)];
    }

    private static Dictionary<string, string> _CategoryLookup = [];

    private static string Category(CarePackageInfo p)
    {
        if (!_CategoryLookup.TryGetValue(p.id, out var category))
            _CategoryLookup.Add(p.id, category = GetCategory(p.id));
        return category;

        string GetCategory(string id)
        {
            var element = ElementLoader.GetElement(id);
            if (element is not null)
            {
                if (element.HasTag(GameTags.Edible)) return "Edible";
                if (element.IsLiquid) return "LiquidMaterials";
                if (element.IsSolid) return "SolidMaterials";
                if (element.IsGas) return "GasMaterials";
                return "Materials";
            }

            var prefab = Assets.TryGetPrefab(p.id);
            if (prefab is not null)
            {
                if (prefab.HasTag(GameTags.Egg)) return "Egg";
                if (prefab.HasTag(GameTags.Seed)) return "Seed";
                if (prefab.HasTag(GameTags.Edible)) return "Edible";
                if (prefab.HasTag(GameTags.Clothes)) return "Clothes";
                if (prefab.HasTag(GameTags.Creature)) return "Creature";
            }

            return null;
        }
    }

    #endregion
}
