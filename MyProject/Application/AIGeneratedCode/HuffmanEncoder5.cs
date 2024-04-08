#if AI_TAKEOVER

using System;
using System.Collections.Generic;
using System.Linq;

public record HuffmanCode(char Character, string Code);

public class HuffmanEncoder
{
    public static List<HuffmanCode> EncodeString(string input)
    {
        // Calculate character frequencies
        Dictionary<char, int> charFrequencies = new Dictionary<char, int>();
        foreach (char c in input)
        {
            if (charFrequencies.ContainsKey(c))
                charFrequencies[c]++;
            else
                charFrequencies[c] = 1;
        }

        // Build the Huffman tree
        var priorityQueue = new SortedSet<(int freq, HuffmanNode node)>(Comparer<(int, HuffmanNode)>.Create((a, b) => a.freq - b.freq));
        foreach (var kvp in charFrequencies)
            priorityQueue.Add((kvp.Value, new HuffmanNode(kvp.Key)));

        while (priorityQueue.Count > 1)
        {
            var (freq1, node1) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);
            var (freq2, node2) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            var mergedNode = new HuffmanNode(null, freq1 + freq2);
            mergedNode.Left = node1;
            mergedNode.Right = node2;
            priorityQueue.Add((mergedNode.Frequency, mergedNode));
        }

        // Generate Huffman codes
        var huffmanCodes = new List<HuffmanCode>();
        GenerateCodes(priorityQueue.Min.node, "", huffmanCodes);

        return huffmanCodes.OrderBy(code => code.Character).ToList();
    }

    private static void GenerateCodes(HuffmanNode node, string code, List<HuffmanCode> huffmanCodes)
    {
        if (node == null)
            return;

        if (node.Character != null)
            huffmanCodes.Add(new HuffmanCode(node.Character.Value, code));

        GenerateCodes(node.Left, code + "0", huffmanCodes);
        GenerateCodes(node.Right, code + "1", huffmanCodes);
    }

    private class HuffmanNode
    {
        public char? Character { get; }
        public int Frequency { get; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public HuffmanNode(char? character, int frequency)
        {
            Character = character;
            Frequency = frequency;
        }
    }
}

class Program
{
    static void Main()
    {
        string input = "abc";
        var huffmanCodes = HuffmanEncoder.EncodeString(input);

        Console.WriteLine("Computed Huffman codes:");
        foreach (var code in huffmanCodes)
        {
            Console.WriteLine($"HuffmanCode('{code.Character}', \"{code.Code}\")");
        }
    }
}


#endif