using MyProject.Application;

namespace MyProject.Tests.Application;

public class FileReaderTest
{
    private const string _testLongString = "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Orci a scelerisque purus semper Elit ut aliquam purus sit amet luctus venenatis lectus magna Risus commodo viverra maecenas accumsan lacus vel facilisis volutpat Feugiat sed lectus vestibulum mattis ullamcorper velit Diam ut venenatis tellus in metus vulputate eu scelerisque felis Aenean sed adipiscing diam donec Arcu dui vivamus arcu felis Varius sit amet mattis vulputate Fames ac turpis egestas sed tempus Proin fermentum leo vel orci porta non Facilisis volutpat est velit egestas dui id ornare arcu Ac auctor augue mauris augue neque gravida in fermentum et Aliquam sem fringilla ut morbi tincidunt augue interdum velit euismod Tincidunt augue interdum velit euismod Arcu dictum varius duis at consectetur lorem donec massa sapien Tincidunt eget nullam non nisi est sit Tincidunt tortor aliquam nulla facilisi cras fermentum odio eu Viverra nam libero justo laoreet sit Massa enim nec dui nunc mattis enim Amet mattis vulputate enim nulla Pulvinar neque laoreet suspendisse interdum consectetur Velit sed ullamcorper morbi tincidunt ornare massa eget egestas purus Faucibus in ornare quam viverra orci sagittis eu volutpat odio Dui accumsan sit amet nulla facilisi morbi tempus Molestie a iaculis at erat pellentesque Tortor pretium viverra suspendisse potenti nullam ac Augue interdum velit euismod in Vitae congue mauris rhoncus aenean vel elit scelerisque Porttitor lacus luctus accumsan tortor Massa eget egestas purus viverra accumsan in nisl nisi scelerisque Massa tempor nec feugiat nisl pretium Mi proin sed libero enim sed faucibus turpis Pellentesque dignissim enim sit amet venenatis urna cursus eget Auctor neque vitae tempus quam pellentesque nec Consequat interdum varius sit amet mattis vulputate enim nulla Parturient montes nascetur ridiculus mus mauris vitae Suspendisse potenti nullam ac tortor Eu turpis egestas pretium aenean pharetra magna ac Enim sed faucibus turpis in eu mi bibendum neque egestas Nulla porttitor massa id neque aliquam vestibulum morbi blandit Cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo Eu scelerisque felis imperdiet proin fermentum leo Etiam tempor orci eu lobortis elementum nibh tellus molestie Mauris rhoncus aenean vel elit scelerisque mauris pellentesque Nullam eget felis eget nunc lobortis mattis aliquam Sit amet commodo nulla facilisi nullam vehicula Elit ullamcorper dignissim cras tincidunt lobortis feugiat Imperdiet dui accumsan sit amet Non arcu risus quis varius Consequat semper viverra nam libero justo laoreet sit amet cursus Sit amet porttitor eget dolor morbi non arcu risus Id diam vel quam elementum pulvinar etiam non quam Egestas diam in arcu cursus euismod quis viverra";
    
    [Fact]
    public void FileReader_ReadValidFile_MatchesExpected()
    {
        // arrange
        string filePath = "Resources/Application/validTextFile.txt";

        // act
        var actual = FileReader.ReadAndValidateFile(filePath);

        // assert
        Assert.Equal(_testLongString, actual);
    }

    [Theory]
    [InlineData("invalidPath")]
    [InlineData("Resources.Application.invalidTextFile.txt")]
    public void FileReader_ReadInvalidFile_ThrowsException(string filePath)
    {
        // act & assert
        Assert.Throws<InvalidOperationException>(() => FileReader.ReadAndValidateFile(filePath));
    }
}