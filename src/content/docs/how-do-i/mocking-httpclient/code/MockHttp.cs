public class MyClientTests
{
    private readonly MyClient _myClient;
    private readonly MockHttpMessageHandler _handler = new();
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
        _handler.Expect(HttpMethod.Get, $"{_baseAddress}/someData")
                .Respond(HttpStatusCode.OK, JsonContent.Create(expectedValue));

        var result = await _myClient.GetSomeData();

        Assert.Equivalent(expectedValue, result);
        ...
    }
}