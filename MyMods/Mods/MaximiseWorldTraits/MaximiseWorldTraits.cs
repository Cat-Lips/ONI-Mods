using System.Collections.Generic;
using System.Linq;
using ProcGen;
using UnityEngine;

using static UnityEngine.EventSystems.PointerEventData;
using WorldTraitRules = System.Collections.Generic.Dictionary<string, (int Min, int Max, System.Collections.Generic.List<string> Forbidden)[]>;

namespace MyMods;

public static class MaximiseWorldTraits
{
    #region Private

    private const string ColonyConfigButtonBackground = "ColonyDestinationScreen2/Layout/BG";

    private static readonly List<string> ForbiddenTraits = [
        "traits/BouldersSmall",
        "traits/BouldersMedium",
        "traits/BouldersLarge",
        "traits/BouldersMixed",

        "traits/MetalPoor",
        "traits/GeoDormant",
        "traits/MagmaVents",
        "traits/MisalignedStart",
        "expansion1::traits/Volcanoes",
    ];

    private enum TraitUseType { All, None, Default }
    private static TraitUseType Next(TraitUseType x) => x switch
    {
        TraitUseType.All => TraitUseType.None,
        TraitUseType.None => TraitUseType.Default,
        TraitUseType.Default => TraitUseType.All,
        _ => throw new System.NotImplementedException(),
    };

    #endregion

    public static void Initialise()
    {
        ColonyDestinationSelectScreen_OnSpawn.OnPostfix += OnSpawn;

        void OnSpawn(ColonyDestinationSelectScreen ui)
        {
            var toggle = TraitUseType.Default;
            var defaultTraitRules = GetDefaultTraitRules();
            //var defaultTemplateRules = GetDefaultTemplateRules();
            //var defaultMixingRules = GetDefaultMixingRules();

            ui.GameObject().AddComponent<MyModMouseClick>().Click += OnClick;
            ui.GameObject().AddComponent<MyModKeyPress>().Keys(KeyCode.Home, KeyCode.End).KeyDown += OnKey;

            void OnClick(GameObject obj, InputButton btn)
            {
                if (obj.Name() is "BG" && obj.Path(3) is ColonyConfigButtonBackground)
                {
                    ToggleTraits();
                    RefreshDisplay();
                }

                void ToggleTraits()
                {
                    switch (toggle = Next(toggle))
                    {
                        case TraitUseType.All: SetWorldGenRules(true); break;
                        case TraitUseType.None: SetWorldGenRules(false); break;
                        case TraitUseType.Default: ResetWorldGenRules(); break;
                    }

                    void SetWorldGenRules(bool all)
                    {
                        var count = all ? 99 : 0;
                        OnWorldGenError.Continue = all;
                        foreach (var (key, world) in SettingsCache.worlds.worldCache)
                        {
                            world.worldTraitRules?.ForEach(rule =>
                            {
                                rule.Property("min", count);
                                rule.Property("max", count);
                                rule.Property("forbiddenTraits", rule.forbiddenTraits?.Concat(ForbiddenTraits).Distinct().ToList() ?? ForbiddenTraits);
                            });

                            //world.worldTemplateRules?.ForEach(rule =>
                            //{
                            //    rule.Property("listRule", ListRule.TryAll);
                            //});

                            //world.subworldMixingRules?.ForEach(rule =>
                            //{
                            //    rule.Property("minCount", 0);
                            //    rule.Property("maxCount", count);
                            //});
                        }
                    }

                    void ResetWorldGenRules()
                    {
                        OnWorldGenError.Continue = false;
                        foreach (var (key, world) in SettingsCache.worlds.worldCache)
                        {
                            var dflt = defaultTraitRules[key];
                            world.worldTraitRules?.ForEach((rule, idx) =>
                            {
                                var (min, max, forbidden) = dflt[idx];
                                rule.Property("min", min);
                                rule.Property("max", max);
                                rule.Property("forbiddenTraits", forbidden);
                            });

                            //var dlft2 = defaultTemplateRules[key];
                            //world.worldTemplateRules?.ForEach((rule, idx) =>
                            //{
                            //    rule.Property("listRule", dlft2[idx]);
                            //});

                            //var dlft3 = defaultMixingRules[key];
                            //world.subworldMixingRules?.ForEach((rule, idx) =>
                            //{
                            //    var (min, max) = dlft3[idx];
                            //    rule.Property("min", min);
                            //    rule.Property("max", max);
                            //});
                        }
                    }
                }

                void RefreshDisplay()
                    => ui.RefreshRowsAndDescriptions();
            }

            void OnKey(GameObject obj, KeyCode key)
            {
                switch (key)
                {
                    case KeyCode.Home: ScrollFirst(); break;
                    case KeyCode.End: ScrollLast(); break;
                }

                void ScrollFirst() => Scroll(true);
                void ScrollLast() => Scroll(false);
                void Scroll(bool first)
                {
                    var panel = ui.Field<DestinationSelectPanel>("destinationMapPanel");
                    var clusterKeys = panel.Field<List<string>>("clusterKeys");
                    var asteroidData = panel.Field<Dictionary<string, ColonyDestinationAsteroidBeltData>>("asteroidData");
                    var idx = first ? 0 : clusterKeys.Count - 1;

                    panel.Field("selectedIndex", idx);
                    panel.Raise(nameof(panel.OnAsteroidClicked), asteroidData[clusterKeys[idx]]);
                }
            }

            #region Defaults

            WorldTraitRules GetDefaultTraitRules()
            {
                return SettingsCache.worlds.worldCache.ToDictionary(kvp => kvp.Key, kvp =>
                    kvp.Value.worldTraitRules?.Select(x => (x.min, x.max, x.forbiddenTraits)).ToArray());
            }

            //WorldTemplateRules GetDefaultTemplateRules()
            //{
            //    return SettingsCache.worlds.worldCache.ToDictionary(kvp => kvp.Key, kvp =>
            //        kvp.Value.worldTemplateRules?.Select(x => x.listRule).ToArray());
            //}

            //SubworldMixingRules GetDefaultMixingRules()
            //{
            //    return SettingsCache.worlds.worldCache.ToDictionary(kvp => kvp.Key, kvp =>
            //        kvp.Value.subworldMixingRules?.Select(x => (x.minCount, x.maxCount)).ToArray());
            //}

            #endregion
        }
    }
}
