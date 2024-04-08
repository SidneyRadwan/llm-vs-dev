using MyProject.Application;
using MyProject.Application.Models;
using Xunit.Sdk;

namespace MyProject.Tests.Application;

public class HuffmanEncoderTest
{
    private const string _testModeratelyLongInput =
        "Lorem ipsum dotor sit amet consectetur adipiscing Enit sed Orci a sceterisue purus semper";//""Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Orci a scelerisque purus semper Elit ut aliquam purus sit amet luctus venenatis lectus magna Risus commodo viverra maecenas accumsan lacus vel facilisis volutpat";

    private const string _testLongInput =
        "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Orci a scelerisque purus semper Elit ut aliquam purus sit amet luctus venenatis lectus magna Risus commodo viverra maecenas accumsan lacus vel facilisis volutpat Feugiat sed lectus vestibulum mattis ullamcorper velit Diam ut venenatis tellus in metus vulputate eu scelerisque felis Aenean sed adipiscing diam donec Arcu dui vivamus arcu felis Varius sit amet mattis vulputate Fames ac turpis egestas sed tempus Proin fermentum leo vel orci porta non Facilisis volutpat est velit egestas dui id ornare arcu Ac auctor augue mauris augue neque gravida in fermentum et Aliquam sem fringilla ut morbi tincidunt augue interdum velit euismod Tincidunt augue interdum velit euismod Arcu dictum varius duis at consectetur lorem donec massa sapien Tincidunt eget nullam non nisi est sit Tincidunt tortor aliquam nulla facilisi cras fermentum odio eu Viverra nam libero justo laoreet sit Massa enim nec dui nunc mattis enim Amet mattis vulputate enim nulla Pulvinar neque laoreet suspendisse interdum consectetur Velit sed ullamcorper morbi tincidunt ornare massa eget egestas purus Faucibus in ornare quam viverra orci sagittis eu volutpat odio Dui accumsan sit amet nulla facilisi morbi tempus Molestie a iaculis at erat pellentesque Tortor pretium viverra suspendisse potenti nullam ac Augue interdum velit euismod in Vitae congue mauris rhoncus aenean vel elit scelerisque Porttitor lacus luctus accumsan tortor Massa eget egestas purus viverra accumsan in nisl nisi scelerisque Massa tempor nec feugiat nisl pretium Mi proin sed libero enim sed faucibus turpis Pellentesque dignissim enim sit amet venenatis urna cursus eget Auctor neque vitae tempus quam pellentesque nec Consequat interdum varius sit amet mattis vulputate enim nulla Parturient montes nascetur ridiculus mus mauris vitae Suspendisse potenti nullam ac tortor Eu turpis egestas pretium aenean pharetra magna ac Enim sed faucibus turpis in eu mi bibendum neque egestas Nulla porttitor massa id neque aliquam vestibulum morbi blandit Cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo Eu scelerisque felis imperdiet proin fermentum leo Etiam tempor orci eu lobortis elementum nibh tellus molestie Mauris rhoncus aenean vel elit scelerisque mauris pellentesque Nullam eget felis eget nunc lobortis mattis aliquam Sit amet commodo nulla facilisi nullam vehicula Elit ullamcorper dignissim cras tincidunt lobortis feugiat Imperdiet dui accumsan sit amet Non arcu risus quis varius Consequat semper viverra nam libero justo laoreet sit amet cursus Sit amet porttitor eget dolor morbi non arcu risus Id diam vel quam elementum pulvinar etiam non quam Egestas diam in arcu cursus euismod quis viverra";

    [Fact]
    public void HuffmanEncoder_EncodeEmptyString_ReturnsEmpty()
    {
        // arrange
        string input = "";

        // act
        var actual = HuffmanEncoder.EncodeString(input);

        // assert
        Assert.Empty(actual);
    }
    
    [Fact]
    public void HuffmanEncoder_EncodeValidShortString_MatchesExpected()
    {
        // arrange
        string input = "abc";
        ICollection<HuffmanCode> expected =
        [ 
            new('a', "10"), 
            new('b', "11"),
            new('c', "0")
        ];

        // act
        var actual = HuffmanEncoder.EncodeString(input);

        // assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void HuffmanEncoder_EncodeValidMediumString_MatchesExpected()
    {
        // arrange
        string input = "abbcccddddefg";
        ICollection<HuffmanCode> expected =
        [ 
            new('a', "1110"), 
            new('b', "110"),
            new('c', "01"),
            new('d', "10"), 
            new('e', "1111"),
            new('f', "000"),
            new('g', "001")
        ];

        // act
        var actual = HuffmanEncoder.EncodeString(input);

        // assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void HuffmanEncoder_EncodeValidModeratelyLongString_MatchesExpected()
    {
        // arrange
        ICollection<HuffmanCode> expected =
        [ 
            new(' ', "101"),
            new('E', "010000"),
            new('L', "010001"),
            new('O', "010010"), 
            new('a', "10010"),
            new('c', "0101"),
            new('d', "10011"),
            new('e', "011"),
            new('g', "010011"),
            new('i', "1111"),
            new('m', "11101"),
            new('n', "11100"),
            new('o', "0000"),
            new('p', "0001"),
            new('r', "1100"),
            new('s', "001"),
            new('t', "1101"),
            new('u', "1000"),
        ];

        // act
        var actual = HuffmanEncoder.EncodeString(_testModeratelyLongInput);

        // assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void HuffmanEncoder_EncodeValidLongString_MatchesExpected()
    {
        // arrange
        ICollection<HuffmanCode> expected =
        [ 
            new(' ', "110"),
            new('A', "00101000"),
            new('C', "0111000111"),
            new('D', "11101101101"),
            new('E', "111011010"),
            new('F', "1110110111"),
            new('I', "0111000000"),
            new('L', "01110001100"),
            new('M', "111011000"),
            new('N', "1110110010"),
            new('O', "01110001101"),
            new('P', "011100001"),
            new('R', "11101101100"),
            new('S', "1110110011"),
            new('T', "001010010"),
            new('V', "001010011"),
            new('a', "1000"),
            new('b', "0111001"),
            new('c', "01111"),
            new('d', "00100"),
            new('e', "010"),
            new('f', "0010101"),
            new('g', "001011"),
            new('h', "011100010"),
            new('i', "1111"),
            new('j', "0111000001"),
            new('l', "0000"),
            new('m', "0001"),
            new('n', "0011"),
            new('o', "11100"),
            new('p', "111010"),
            new('q', "1110111"),
            new('r', "0110"),
            new('s', "1001"),
            new('t', "1010"),
            new('u', "1011"),
            new('v', "011101")
        ];

        // act
        var actual = HuffmanEncoder.EncodeString(_testLongInput);

        // assert
        Assert.Equivalent(expected, actual, strict: true);
    }
    
    [Fact]
    public void AssertEquivalentStrict_EquivalentHuffmanCodeCollections_Passes()
    {
        // arrange
        ICollection<HuffmanCode> sorted =
        [ 
            new(' ', "110"),
            new('A', "1001"),
            new('B', "10110011"),
            new('C', "01111"),
            new('D', "01110"),
            new('E', "010"),
            new('F', "1011000"),
            new('G', "000011"),
            new('H', "101100101"),
            new('I', "1111"),
            new('J', "101100100"),
            new('L', "0001"),
            new('M', "0010"),
            new('N', "0011"),
            new('O', "10111"),
            new('P', "00000"),
            new('Q', "000010"),
            new('R', "0110"),
            new('S', "1000"),
            new('T', "1010"),
            new('U', "1110"),
            new('V', "101101")
        ];
        
        ICollection<HuffmanCode> unsorted =
        [ 
            new(' ', "110"),
            new('A', "1001"),
            new('B', "10110011"),
            new('C', "01111"),
            new('D', "01110"),
            new('M', "0010"),
            new('N', "0011"),
            new('O', "10111"),
            new('P', "00000"),
            new('Q', "000010"),
            new('R', "0110"),
            new('S', "1000"),
            new('E', "010"),
            new('F', "1011000"),
            new('G', "000011"),
            new('H', "101100101"),
            new('I', "1111"),
            new('J', "101100100"),
            new('L', "0001"),
            new('T', "1010"),
            new('U', "1110"),
            new('V', "101101")
        ];
        
        ICollection<HuffmanCode> partialEmpty =
        [ 
            new(' ', "110"),
            new('A', "1001"),
            new('B', "10110011"),
            new('C', "01111"),
            new('H', "101100101"),
            new('I', "1111"),
            new('J', "101100100"),
            new('L', "0001"),
            new('T', "1010"),
            new('U', "1110"),
            new('V', "101101")
        ];

        // act & assert
        Assert.Equivalent(sorted, unsorted, strict: true);
        Assert.Throws<EquivalentException>(() => Assert.Equivalent(partialEmpty, unsorted, strict: true));
        Assert.Throws<EquivalentException>(() => Assert.Equivalent(partialEmpty, sorted, strict: true));
    }
}