using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace MyMods;

public static class TranspilerExtensions
{
    public static IEnumerable<CodeInstruction> Dump(this IEnumerable<CodeInstruction> instructions, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null)
    {
        foreach (var instruction in instructions)
        {
            Log.Dev(instruction, filePath, memberName);
            yield return instruction;
        }
    }

    public static IEnumerable<CodeInstruction> Transpile(this IEnumerable<CodeInstruction> instructions, string searchItem, (OpCode OpCode, object Operand)? replaceItem, OpCode? findTrigger = null, int findCount = 1, bool log = false, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null)
    {
        return instructions.Transpile(new[] { searchItem }, new[] { replaceItem }, findTrigger ?? DefaultOpCode(), findCount, log, filePath, memberName);

        OpCode DefaultOpCode()
        {
            return (OpCode)typeof(OpCodes)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Single(x => x.Name.ToLower() == OpCodeStr().ToLower())
                .GetValue(null);

            string OpCodeStr()
                => searchItem.Split().First().Replace(".", "_");
        }
    }

    public static IEnumerable<CodeInstruction> Transpile(this IEnumerable<CodeInstruction> instructions, IList<string> searchItems, IList<(OpCode OpCode, object Operand)?> replaceItems, OpCode findTrigger, int findCount = 1, bool log = false, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null)
    {
        Debug.Assert(replaceItems == null || searchItems.Count == replaceItems.Count);

        var found = 0;
        var searchBuffer = new CodeInstruction[searchItems.Count];
        using (var iterator = instructions.GetEnumerator())
        {
            while (iterator.MoveNext())
            {
                if (found < findCount && iterator.Current.opcode == findTrigger)
                {
                    if (!LookAheadMatch(iterator, searchItems, ref searchBuffer, out var bufferCount))
                    {
                        for (var bufferIndex = 0; bufferIndex < bufferCount; ++bufferIndex)
                        {
                            yield return searchBuffer[bufferIndex];
                        }
                    }
                    else
                    {
                        ++found;
                        Debug.Assert(searchItems.Count == bufferCount);
                        for (var searchIndex = 0; searchIndex < searchItems.Count; ++searchIndex)
                        {
                            var instruction = searchBuffer[searchIndex];

                            if (replaceItems == null)
                            {
                                if (log) Log.Dev($"*** Replacing {instruction} ***", filePath, memberName);

                                instruction.opcode = OpCodes.Nop;
                                instruction.operand = null;
                            }
                            else
                            {
                                var replaceItem = replaceItems[searchIndex];
                                if (replaceItem != null)
                                {
                                    if (log) Log.Dev($"*** Replacing {instruction} ***", filePath, memberName);

                                    instruction.opcode = replaceItem.Value.OpCode;
                                    instruction.operand = replaceItem.Value.Operand;
                                }
                            }

                            yield return instruction;
                        }
                    }
                }

                yield return iterator.Current;
            }
        }

        static bool LookAheadMatch(IEnumerator<CodeInstruction> iterator, IList<string> searchItems, ref CodeInstruction[] searchBuffer, out int bufferCount)
        {
            bufferCount = 0;
            Debug.Assert(searchItems.Count == searchBuffer.Length);
            foreach (var searchItem in searchItems)
            {
                if ($"{iterator.Current}".StartsWith(searchItem))
                {
                    searchBuffer[bufferCount++] = iterator.Current;
                    if (!iterator.MoveNext())
                    {
                        return false;
                    }

                    continue;
                }

                return false;
            }

            return true;
        }
    }
}
