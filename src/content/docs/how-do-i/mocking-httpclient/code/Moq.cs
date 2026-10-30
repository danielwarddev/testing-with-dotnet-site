public class MyClientTests
{
    private readonly MyClient _myClient;
    private readonly Mock<HttpMessageHandler> _handler = new();
    private readonly string _baseAddress = "https://www.example.com";

    public MyClientTests()
    {
        var httpClient = new HttpClient(_handler) { BaseAddress = new Uri(_baseAddress) };
        _myClient = new MyClient(httpClient);
    }

    [Fact]
    public async MyTest()
    {
        ...
        _handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedValue),
            });

        var result = await _myClient.GetSomeData();

        Assert.Equivalent(expectedValue, result);
        ...
    }
}