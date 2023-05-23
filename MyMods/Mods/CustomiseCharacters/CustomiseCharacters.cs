using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.AI;
using TUNING;
using UnityEngine;

using static UnityEngine.EventSystems.PointerEventData;

namespace MyMods;

public static class CustomiseCharacters
{
    #region Options

    public static bool UseDefaults { get; set; } = true;
    public static bool LoadMinions { get; set; } = false;
    public static bool SaveMinions { get; set; } = false;

    #endregion

    #region Defaults

    private partial class MinionConfig
    {
        private static MinionConfig Default(int idx) => idx switch
        {
            0 => new() { Name = "Pei", Interests = ["Researching", "Supplying", "Suit Wearing"], GoodTraits = ["Quick Learner", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            1 => new() { Name = "Nisbet", Interests = ["Operating", "Supplying", "Suit Wearing"], GoodTraits = ["Grease Monkey", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            2 => new() { Name = "Freyja", Interests = ["Cooking", "Supplying", "Suit Wearing"], GoodTraits = ["Gourmet", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

            3 => new() { Name = "Ellie", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            4 => new() { Name = "Marie", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            5 => new() { Name = "Catalina", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            6 => new() { Name = "Mae", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

            7 => new() { Name = "Ada", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            8 => new() { Name = "Banhi", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            9 => new() { Name = "Lindsay", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            10 => new() { Name = "Ruby", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

            11 => new() { Name = "Camille", Interests = ["Decorating", "Supplying", "Suit Wearing"], GoodTraits = ["Interior Decorator", "Innately Stylish", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            12 => new() { Name = "Ari", Interests = ["Doctoring", "Supplying", "Suit Wearing"], GoodTraits = ["Caregiver", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            13 => new() { Name = "Devon", Interests = ["Tidying", "Supplying", "Suit Wearing"], GoodTraits = ["Frost Proof", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            14 => new() { Name = "Bubbles", Interests = ["Supplying", "Tidying", "Suit Wearing"], GoodTraits = ["Frost Proof", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
            15 => new() { Name = "Gossmann", Interests = ["Rocketry", "Researching", "Suit Wearing"], GoodTraits = ["Quick Learner", "Buff", "Twinkletoes", "Skilled: <link=\"SWIMMING\">Basic Swimming</link>", "Skilled: <link=\"SWIMMING2\">Divemaster</link>"], BadTraits = [], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

            _ => new() { Name = "Meep", Interests = [], GoodTraits = [], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        };

        //    private static MinionConfig Default(int idx) => idx switch
        //    {
        //        0 => new() { Name = "Pei", Interests = ["Researching", "Supplying", "Suit Wearing"], GoodTraits = ["Quick Learner", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        1 => new() { Name = "Nisbet", Interests = ["Operating", "Supplying", "Suit Wearing"], GoodTraits = ["Grease Monkey", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        2 => new() { Name = "Freyja", Interests = ["Cooking", "Supplying", "Suit Wearing"], GoodTraits = ["Gourmet", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

        //        3 => new() { Name = "Ellie", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        4 => new() { Name = "Marie", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        5 => new() { Name = "Catalina", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        6 => new() { Name = "Mae", Interests = ["Digging", "Building", "Suit Wearing"], GoodTraits = ["Mole Hands", "Handy", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

        //        7 => new() { Name = "Ada", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        8 => new() { Name = "Banhi", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        9 => new() { Name = "Lindsay", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        10 => new() { Name = "Ruby", Interests = ["Farming", "Ranching", "Suit Wearing"], GoodTraits = ["Green Thumb", "Animal Lover", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

        //        11 => new() { Name = "Camille", Interests = ["Decorating", "Supplying", "Suit Wearing"], GoodTraits = ["Interior Decorator", "Innately Stylish", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        12 => new() { Name = "Ari", Interests = ["Doctoring", "Supplying", "Suit Wearing"], GoodTraits = ["Caregiver", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        13 => new() { Name = "Devon", Interests = ["Tidying", "Supplying", "Suit Wearing"], GoodTraits = ["Frost Proof", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        14 => new() { Name = "Bubbles", Interests = ["Supplying", "Tidying", "Suit Wearing"], GoodTraits = ["Frost Proof", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        15 => new() { Name = "Gossmann", Interests = ["Rocketry", "Researching", "Suit Wearing"], GoodTraits = ["Quick Learner", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

        //        16 => new() { Name = "Mi-Ma", Interests = ["Cooking", "Supplying", "Suit Wearing"], GoodTraits = ["Gourmet", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //        17 => new() { Name = "Leira", Interests = ["Operating", "Supplying", "Suit Wearing"], GoodTraits = ["Grease Monkey", "Buff", "Twinkletoes"], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },

        //        _ => new() { Name = "Meep", Interests = [], GoodTraits = [], BadTraits = ["Flatulent"], StressReaction = "Vomiter", JoyResponse = "Sparkle Streaker" },
        //    };
        //}
    }

    #endregion

    #region Initialise

    public static void Initialise()
    {
        CharacterContainer_OnSpawn.OnPostfix += OnSpawn;

        void OnSpawn(CharacterContainer cc)
        {
            if (cc.GameObject().GetComponent<MyModMouseClick>() is null)
                cc.GameObject().AddComponent<MyModMouseClick>().Click += OnClick;

            void OnClick(GameObject obj, InputButton btn)
            {
                // Left is select - Use right to edit
                if (btn is InputButton.Left)
                    return;

                var ctrl = Input.GetKey(KeyCode.LeftControl); // Navigate back (also: add bad trait)
                var shft = Input.GetKey(KeyCode.LeftShift); // Remove row (also: next minion by category)
                var dir = ctrl ? -1 : 1;

                switch (obj.Name())
                {
                    case "PortraitContainer": NextMinion(dir, jump: shft); break;

                    case "Title":
                        switch (obj.Parent().Name())
                        {
                            case "AptitudeContainer": AddInterest(); break;
                            case "TraitContainer": AddTrait(good: !ctrl); break;
                        }
                        break;

                    case "AptitudeLabel": if (shft) RemoveInterest(); else NextInterest(dir); break;
                    case "GoodTrait": if (shft) RemoveTrait(good: true); else NextTrait(good: true, dir); break;
                    case "BadTrait": if (shft) RemoveTrait(good: false); else NextTrait(good: false, dir); break;
                    case "ExpectationAlt": NextExpectation(dir); break;
                        //default: Log.Dev($"{obj.Name()}: {obj.Path()}"); break;
                }

                void NextMinion(int direction, bool jump)
                {
                    DeselectDeliverable();
                    SetMinion(NextMinion());
                    cc.SelectDeliverable();

                    void DeselectDeliverable()
                        => cc.Field<CharacterSelectionController>("controller").RemoveLast();

                    Personality NextMinion()
                    {
                        var name = cc.Stats.personality.Name;

                        if (jump)
                        {
                            // TODO
                        }

                        return Content.Minions.GetNext(name, direction);
                    }

                    void SetMinion(Personality minion)
                    {
                        var guaranteedAptitude = cc.Field<string>("guaranteedAptitudeID");
                        cc.SetMinion(new(minion, guaranteedAptitude));
                        PlayAnim();
                    }
                }

                void AddInterest()
                {
                    if (cc.Stats.skillAptitudes.Count < 3)
                    {
                        cc.Stats.skillAptitudes.Add(NewInterest(), DUPLICANTSTATS.APTITUDE_BONUS);
                        ResetStartingLevels();
                        RefreshDisplay();
                    }

                    SkillGroup NewInterest()
                        => Content.Interests.First(x => !cc.Stats.skillAptitudes.ContainsKey(x));
                }

                void RemoveInterest()
                {
                    var name = obj.GetComponent<LocText>().text;
                    var key = Content.Interests.Get(name);
                    cc.Stats.skillAptitudes.Remove(key);
                    ResetStartingLevels();
                    RefreshDisplay();
                }

                void NextInterest(int direction)
                {
                    var prevInterest = obj.GetComponent<LocText>().text;
                    var nextInterest = Content.Interests.GetNext(prevInterest, direction);
                    while (cc.Stats.skillAptitudes.ContainsKey(nextInterest))
                        nextInterest = Content.Interests.GetNext(nextInterest.Name, direction);
                    cc.Stats.skillAptitudes = cc.Stats.skillAptitudes.ToDictionary(
                        x => x.Key.Name == prevInterest ? nextInterest : x.Key,
                        x => x.Value);
                    ResetStartingLevels();
                    RefreshDisplay();
                }

                void AddTrait(bool good)
                {
                    if (cc.Stats.Traits.Count < DUPLICANTSTATS.MAX_TRAITS + 1) // +1 for Standard Duplicant trait
                    {
                        cc.Stats.Traits.Add(NewTrait());
                        RefreshDisplay();
                    }

                    Trait NewTrait()
                        => Traits(good).First(x => !cc.Stats.Traits.Contains(x));
                }

                void RemoveTrait(bool good)
                {
                    var name = obj.GetComponent<LocText>().text;
                    var key = Traits(good).Get(name);
                    cc.Stats.Traits.Remove(key);
                    RefreshDisplay();
                }

                void NextTrait(bool good, int direction)
                {
                    var traits = Traits(good);
                    var prevTrait = obj.GetComponent<LocText>().text;
                    var nextTrait = traits.GetNext(prevTrait, direction);
                    while (cc.Stats.Traits.Contains(nextTrait))
                        nextTrait = traits.GetNext(nextTrait.Name, direction);
                    var idx = cc.Stats.Traits.IndexOf(traits.Get(prevTrait));
                    cc.Stats.Traits[idx] = nextTrait;
                    RefreshDisplay();
                }

                void NextExpectation(int direction)
                {
                    var type = obj.GetComponent<LocText>().text.Split(':').First();

                    switch (type)
                    {
                        case "Stress Reaction":
                            cc.Stats.stressTrait = StressTraits()?.GetNext(cc.Stats.stressTrait.Name, direction) ?? cc.Stats.stressTrait;
                            break;

                        case "Overjoyed Response":
                            cc.Stats.joyTrait = JoyTraits().GetNext(cc.Stats.joyTrait.Name, direction);
                            break;
                    }

                    RefreshDisplay();
                }
            }

            #region Utilities

            Content<Trait> Traits(bool good)
                => GetTraitsFor(cc.Stats.personality, good);

            Content<Trait> StressTraits()
                => GetStressReactionsFor(cc.Stats.personality);

            Content<Trait> JoyTraits()
                => GetJoyResponsesFor(cc.Stats.personality);

            void ResetStartingLevels()
                => SetStartingLevels(cc.Stats);

            void RefreshDisplay()
            {
                cc.Invoke("SetInfoText");
                cc.StartCoroutine("SetAttributes");
            }

            void PlayAnim()
               => cc.Field<KAnimControllerBase>("fxAnim").Play("loop");

            #endregion
        }

        #region Save/Load

        ImmigrantScreen_OnProceed.OnPostfix += SaveMinions;
        ImmigrantScreen_Initialise.OnPostfix += LoadMinions;
        MinionSelectScreen_OnProceed.OnPostfix += SaveMinions;
        MinionSelectScreen_Initialise.OnPostfix += LoadMinions;

        void SaveMinions(CharacterSelectionController controller)
        {
            var stats = controller
                .Field<List<ITelepadDeliverable>>("selectedDeliverables")
                .OfType<MinionStartingStats>().ToArray();

            for (var i = 0; i < stats.Length; ++i)
            {
                var idx = Components.LiveMinionIdentities.Count + i;
                MinionConfig.SaveStartingStats(stats[i], idx);
            }
        }

        void LoadMinions(CharacterSelectionController controller)
        {
            controller.StartCoroutine(LoadMinions());

            IEnumerator LoadMinions()
            {
                yield return null;

                var containers = controller
                    .Field<List<ITelepadDeliverableContainer>>("containers")
                    .OfType<CharacterContainer>()
                    .Take(controller.Field<int>("selectableCount"))
                    .ToArray();

                for (var i = 0; i < containers.Length; ++i)
                {
                    var idx = Components.LiveMinionIdentities.Count + i;
                    var stats = MinionConfig.LoadStartingStats(idx);

                    if (stats is null) continue;
                    containers[i].SetMinion(stats);
                }

                if (containers.Length is 1)
                    containers.Single().SelectDeliverable();
            }
        }

        #endregion
    }

    #endregion

    #region Save/Load

    private partial class MinionConfig
    {
        public string Name { get; set; }
        public string[] Interests { get; set; }
        public string[] GoodTraits { get; set; }
        public string[] BadTraits { get; set; }
        public string StressReaction { get; set; }
        public string JoyResponse { get; set; }

        public static void SaveStartingStats(MinionStartingStats stats, int idx)
        {
            if (SaveMinions)
            {
                Settings.Save(new MinionConfig()
                {
                    Name = stats.personality.Name,
                    Interests = stats.skillAptitudes.Keys.Select(x => x.Name).ToArray(),
                    GoodTraits = stats.Traits.Skip(1).Where(x => x.PositiveTrait).Select(x => x.Name).ToArray(), // skip Standard Duplicant trait
                    BadTraits = stats.Traits.Skip(1).Where(x => !x.PositiveTrait).Select(x => x.Name).ToArray(), // skip Standard Duplicant trait
                    StressReaction = stats.stressTrait.Name,
                    JoyResponse = stats.joyTrait.Name,
                }, GetFilename(idx));
            }
        }

        public static MinionStartingStats LoadStartingStats(int idx)
        {
            var cfg = LoadMinions ? Settings.Load<MinionConfig>(GetFilename(idx)) : null;
            if (cfg is null && !UseDefaults) return null;
            cfg ??= Default(idx);

            var minion = Content.Minions.Get(cfg.Name);
            var interests = cfg.Interests.ToDictionary(Content.Interests.Get, x => (float)DUPLICANTSTATS.APTITUDE_BONUS);
            var baseTrait = Db.Get().traits.Get(BaseMinionConfig.GetMinionBaseTraitIDForModel(minion.model));
            var goodTraits = cfg.GoodTraits.Select(x => GetGoodTraitsFor(minion).Get(x));
            var badTraits = cfg.BadTraits.Select(x => GetBadTraitsFor(minion).Get(x));
            var stressReaction = GetStressReactionsFor(minion).Get(cfg.StressReaction);
            var joyResponse = GetJoyResponsesFor(minion).Get(cfg.JoyResponse);

            var stats = new MinionStartingStats(minion)
            {
                skillAptitudes = interests,
                Traits = goodTraits.Concat(badTraits).Prepend(baseTrait).ToList(),
                stressTrait = stressReaction,
                joyTrait = joyResponse,
            };

            SetStartingLevels(stats);
            return stats;
        }

        private static string GetFilename(int idx)
            => $"Minion{idx}.json";
    }

    #endregion

    #region Utilities

    private static Content<Trait> GetGoodTraitsFor(Personality minion) => GetTraitsFor(minion, true);
    private static Content<Trait> GetBadTraitsFor(Personality minion) => GetTraitsFor(minion, false);
    private static Content<Trait> GetTraitsFor(Personality minion, bool good)
    {
        return minion.model == GameTags.Minions.Models.Bionic
            ? (good ? Content.Bionic.UpgradeTraits : Content.Bionic.BugTraits)
            : (good ? Content.GoodTraits : Content.BadTraits);
    }

    private static Content<Trait> GetStressReactionsFor(Personality minion)
    {
        return minion.model == GameTags.Minions.Models.Bionic
            ? null
            : Content.StressTraits;
    }

    private static Content<Trait> GetJoyResponsesFor(Personality minion)
    {
        return minion.model == GameTags.Minions.Models.Bionic
            ? Content.Bionic.JoyTraits
            : Content.JoyTraits;
    }

    private static void SetStartingLevels(MinionStartingStats stats)
    {
        var skills = stats.skillAptitudes;
        var bonuses = DUPLICANTSTATS.APTITUDE_ATTRIBUTE_BONUSES;

        if (skills.Count is 0) return;
        var bonus = skills.Count > bonuses.Length ? 0 : bonuses[skills.Count - 1];

        stats.StartingLevels = skills.Keys
            .SelectMany(x => x.relevantAttributes)
            .Distinct()
            .ToDictionary(x => x.Id, x => bonus);
    }

    #endregion
}
