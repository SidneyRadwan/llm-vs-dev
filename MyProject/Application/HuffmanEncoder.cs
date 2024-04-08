using MyProject.Application.Models;

namespace MyProject.Application;

public static class HuffmanEncoder
{
    #if HUMAN_SURVIVE
    private record HuffmanNode(
        char? Character,
        int Weight,
        HuffmanNode? LeftNode,
        HuffmanNode? RightNode);
    
    private static ICollection<HuffmanCode> GetHuffmanCodes(HuffmanNode rootNode)
    {
        ICollection<HuffmanCode> codes = []; 
            
        void InnerLoop(HuffmanNode node, string code)
        {
            if (node.LeftNode is null && node.RightNode is null)
                codes.Add(new(node.Character!.Value, code));

            if (node.LeftNode is not null)
                InnerLoop(node.LeftNode, code + "0");
                
            if (node.RightNode is not null)
                InnerLoop(node.RightNode, code + "1");
        }

        InnerLoop(rootNode, "");

        return codes
            .OrderBy(x => x.Character)
            .ToList();
    }
    
    private static HuffmanNode? BuildBinaryTree(PriorityQueue<HuffmanNode, HuffmanNode> charWeights)
    {
        HuffmanNode? InnerLoop()
        {
            charWeights.TryDequeue(out var firstNode, out _);

            if (firstNode is null)
                return null;

            charWeights.TryDequeue(out var secondNode, out _);

            if (secondNode is null)
                return firstNode;

            var node = new HuffmanNode(
                Character: null,
                Weight: firstNode.Weight + secondNode.Weight,
                LeftNode: firstNode,
                RightNode: secondNode);

            charWeights.Enqueue(element: node, priority: node);

            return InnerLoop();
        }

        return InnerLoop();
    }

    private class HuffmanComparer : IComparer<HuffmanNode>
    {
        public int Compare(HuffmanNode? x, HuffmanNode? y)
        {
            // -1 => less means take first in priority queue
            // 0 => equal means take by default ordering (queue implementation)
            // +1 => greater means take last in priority queue
            if  (x is not null && y is null) return -1;
            if  (x is null && y is null) return 0;
            if  (x is null && y is not null) return 1;

            // Comparing by weight, take lowest weight first
            if (x.Weight < y.Weight) return -1;
            if (x.Weight > y.Weight) return 1;

            // Comparing leaf nodes to internal nodes, take leaf nodes first
            if (x.Character is not null && y.Character is null) return -1;
            if (x.Character is null && y.Character is not null) return 1;
            
            // Comparing two internal nodes, perform a recursive comparison on left nodes
            if (x.Character is null && y.Character is null) return Compare(x.LeftNode, y.LeftNode);

            // Comparing leaf nodes by character, take lowest alphanumeric character
            if (x.Character < y.Character) return -1;
            if (x.Character > y.Character) return 1;
            return 0;
        }
    }

    public static ICollection<HuffmanCode> _EncodeString(string input)
    {
        PriorityQueue<HuffmanNode, HuffmanNode> charWeights = new(comparer: new HuffmanComparer());

        charWeights.EnqueueRange(
            input
                .GroupBy(c => c)
                .Select(c => (Character: c.Key, Frequency: input.GetCharacterFrequency(c.Key)))
                .OrderBy(x => x.Frequency)
                .ThenBy(x => x.Character)
                .Select(
                    characterFrequency =>
                    {
                        var node = new HuffmanNode(
                            characterFrequency.Character,
                            Weight: characterFrequency.Frequency,
                            LeftNode: null,
                            RightNode: null);

                        return (node, node);
                    }
                ));

        var rootNode = BuildBinaryTree(charWeights);

        return rootNode is null
            ? []
            : GetHuffmanCodes(rootNode);
    }

    private static int GetCharacterFrequency(this string input, char c)
        => input.Count(x => x == c); 
    #endif

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