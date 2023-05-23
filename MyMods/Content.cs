using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.AI;
using TUNING;

namespace MyMods;

public static class Content
{
    public static Content<Personality> Minions => field ??=
        new(Db.Get().Personalities.resources
            .Where(x => !x.Disabled)
            .OrderBy(x => x.model)
            .ThenBy(x => x.genderStringKey)
            .ThenBy(x => x.Name));

    public static Content<SkillGroup> Interests => field ??=
        new(Db.Get().SkillGroups.resources
            .Where(x => x.allowAsAptitude));

    public static Content<Trait> GoodTraits => field ??=
        new(DUPLICANTSTATS.GOODTRAITS
            .Select(x => Db.Get().traits.Get(x.id))
            .OrderBy(x => x.Name));

    public static Content<Trait> BadTraits => field ??=
        new(DUPLICANTSTATS.BADTRAITS
            .Select(x => Db.Get().traits.Get(x.id))
            .OrderBy(x => x.Name));

    public static Content<Trait> StressTraits => field ??=
        new(DUPLICANTSTATS.STRESSTRAITS
            .Select(x => Db.Get().traits.Get(x.id))
            .OrderBy(x => x.Name));

    public static Content<Trait> JoyTraits => field ??=
        new(DUPLICANTSTATS.JOYTRAITS
            .Where(x => !x.IsDlc3())
            .Select(x => Db.Get().traits.Get(x.id))
            .OrderBy(x => x.Name));

    public static class Bionic
    {
        public static Content<Trait> UpgradeTraits => field ??=
            new(DUPLICANTSTATS.BIONICUPGRADETRAITS
                .Select(x => Db.Get().traits.Get(x.id))
                .OrderBy(x => x.Name));

        public static Content<Trait> BugTraits => field ??=
            new(DUPLICANTSTATS.BIONICBUGTRAITS
                .Select(x => Db.Get().traits.Get(x.id))
                .OrderBy(x => x.Name));

        public static Content<Trait> JoyTraits => field ??=
            new(DUPLICANTSTATS.JOYTRAITS
                .Where(x => x.IsDlc3())
                .Select(x => Db.Get().traits.Get(x.id))
                .OrderBy(x => x.Name));
    }
}

public class Content<T> : IEnumerable<T> where T : Resource
{
    private readonly IList<T> content;
    private readonly IDictionary<string, int> indexLookup;

    public T Get(int index) => content[index];
    public T Get(string name) => Get(GetIndex(name));

    public T GetNext(int index, int direction) => Get(GetNextIndex(index, direction));
    public T GetNext(string name, int direction) => GetNext(GetIndex(name), direction);

    public int GetIndex(string name) => indexLookup[name];
    private int GetNextIndex(int currentIndex, int direction)
        => (content.Count + currentIndex + direction) % content.Count;

    public IEnumerator<T> GetEnumerator() => content.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)content).GetEnumerator();

    public Content(IEnumerable<T> source)
    {
        var index = -1;
        content = source.ToArray();
        indexLookup = content.ToDictionary(x => x.Name, x => ++index);
    }
}
