#if AI_TAKEOVER

import heapq
from collections import defaultdict

class HuffmanNode:
    def __init__(self, char, freq):
        self.char = char
        self.freq = freq
        self.left = None
        self.right = None

def build_huffman_tree(freq_map):
    heap = [(freq, HuffmanNode(char, freq)) for char, freq in freq_map.items()]
    heapq.heapify(heap)

    while len(heap) > 1:
        freq1, node1 = heapq.heappop(heap)
        freq2, node2 = heapq.heappop(heap)
        merged_freq = freq1 + freq2
        merged_node = HuffmanNode(None, merged_freq)
        merged_node.left, merged_node.right = node1, node2
        heapq.heappush(heap, (merged_freq, merged_node))

    return heap[0][1]

def generate_huffman_codes(root, code="", huffman_codes={}):
    if root:
        if root.char:
            huffman_codes[root.char] = code
        generate_huffman_codes(root.left, code + "0", huffman_codes)
        generate_huffman_codes(root.right, code + "1", huffman_codes)

def main():
    input_string = "abc"
    char_freq_map = defaultdict(int)

    for char in input_string:
        char_freq_map[char] += 1

    huffman_tree_root = build_huffman_tree(char_freq_map)
    huffman_codes = {}
    generate_huffman_codes(huffman_tree_root, "", huffman_codes)

    for char, code in sorted(huffman_codes.items()):
        print(f"('{char}', \"{code}\")")

if __name__ == "__main__":
    main()


#endif