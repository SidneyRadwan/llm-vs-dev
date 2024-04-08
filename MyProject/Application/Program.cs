using MyProject.Application;

public static class Program
{
    public static void Main()
    {
        var filePath = "";
        var input = FileReader.ReadAndValidateFile(filePath);

        var encoding = HuffmanEncoder.EncodeString(input);

        Console.WriteLine("Huffman encoding for input string: \"{input}\" is : {encodings}",
            input,
            string.Join(", ", encoding.Select(x =>$"({x.Character}, {x.Code})")));
    }
}
