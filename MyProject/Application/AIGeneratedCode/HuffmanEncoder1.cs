#if AI_TAKEOVER

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGenerated.HuffmanEncoder1;

public record HuffmanCode(char Character, string Code);

public class HuffmanEncoder1
{
    public static ICollection<HuffmanCode> EncodeString(string input)
    {
        // Step 1: Calculate character frequencies
        var charFrequencies = new Dictionary<char, int>();
        foreach (char c in input)
        {
            if (charFrequencies.ContainsKey(c))
                charFrequencies[c]++;
            else
                charFrequencies[c] = 1;
        }

        // Step 2: Build a priority queue (min heap) based on character frequencies
        var priorityQueue = new SortedDictionary<int, Queue<char>>();
        foreach (var kvp in charFrequencies)
        {
            if (!priorityQueue.ContainsKey(kvp.Value))
                priorityQueue[kvp.Value] = new Queue<char>();
            priorityQueue[kvp.Value].Enqueue(kvp.Key);
        }

        // Step 3: Construct the Huffman tree
        while (priorityQueue.Count > 1)
        {
            var min1 = priorityQueue.First();
            priorityQueue.Remove(min1.Key);
            var min2 = priorityQueue.First();
            priorityQueue.Remove(min2.Key);

            foreach (var c in min1.Value)
                charFrequencies[c] = 0; // Mark as visited

            foreach (var c in min2.Value)
                charFrequencies[c] = 1; // Mark as visited

            var mergedNode = new Queue<char>(min1.Value.Concat(min2.Value));
            priorityQueue[min1.Key + min2.Key] = mergedNode;
        }

        // Step 4: Generate Huffman codes
        var huffmanCodes = new List<HuffmanCode>();
        var root = priorityQueue.First().Value;
        GenerateHuffmanCodes(root, "", huffmanCodes);

        // Step 5: Sort by character
        huffmanCodes.Sort((a, b) => a.Character.CompareTo(b.Character));

        return huffmanCodes;
    }

    private static void GenerateHuffmanCodes(Queue<char> node, string code, List<HuffmanCode> huffmanCodes)
    {
        if (node.Count == 1)
        {
            huffmanCodes.Add(new HuffmanCode(node.Peek(), code));
            return;
        }

        var left = new Queue<char>(node.Take(node.Count / 2));
        var right = new Queue<char>(node.Skip(node.Count / 2));

        GenerateHuffmanCodes(left, code + "0", huffmanCodes);
        GenerateHuffmanCodes(right, code + "1", huffmanCodes);
    }
}

// Example usage:
var inputString = "this is an example for huffman encoding";
var huffmanCodes = HuffmanEncoder.EncodeString(inputString);

foreach (var code in huffmanCodes)
{
    Console.WriteLine($"Character: {code.Character}, Huffman Code: {code.Code}");
}

#endif