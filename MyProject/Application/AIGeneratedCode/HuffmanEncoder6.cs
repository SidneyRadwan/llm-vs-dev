#if AI_TAKEOVER

using System;
using System.Collections.Generic;
using System.Linq;

public record HuffmanCode(char Character, string Code);

public class HuffmanTree
{
    public HuffmanTree(int frequency, char character, HuffmanTree left, HuffmanTree right)
    {
        Frequency = frequency;
        Character = character;
        Left = left;
        Right = right;
    }

    public int Frequency { get; }
    public char Character { get; }
    public HuffmanTree Left { get; }
    public HuffmanTree Right { get; }
}

public class Program
{
    public static List<HuffmanCode> EncodeString(string input)
    {
        var frequencies = input.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var trees = new SortedSet<HuffmanTree>(Comparer<HuffmanTree>.Create((x, y) => x.Frequency != y.Frequency ? x.Frequency.CompareTo(y.Frequency) : x.Character.CompareTo(y.Character)));
        foreach (var kv in frequencies)
        {
            trees.Add(new HuffmanTree(kv.Value, kv.Key, null, null));
        }

        while (trees.Count > 1)
        {
            var a = trees.Min;
            trees.Remove(a);
            var b = trees.Min;
            trees.Remove(b);
            trees.Add(new HuffmanTree(a.Frequency + b.Frequency, '\0', a, b));
        }

        var root = trees.Single();
        var codes = new Dictionary<char, string>();
        Traverse(root, "", codes);

        return codes.Select(kv => new HuffmanCode(kv.Key, kv.Value)).OrderBy(hc => hc.Character).ToList();
    }

    private static void Traverse(HuffmanTree tree, string prefix, Dictionary<char, string> codes)
    {
        if (tree == null)
        {
            return;
        }
        if (tree.Character != '\0')
        {
            codes[tree.Character] = prefix;
        }
        Traverse(tree.Left, prefix + "0", codes);
        Traverse(tree.Right, prefix + "1", codes);
    }
}


#endif