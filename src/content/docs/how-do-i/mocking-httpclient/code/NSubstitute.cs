public class MyClientTests
{
    private readonly MyClient _myClient;
    private readonly HttpMessageHandler _handler = Substitute.For<HttpMessageHandler>();
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
        _handler.GetType().GetMethod("SendAsync", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(_handler, new object?[]
            {
                Arg.Is<HttpRequestMessage>(x =>
                    x.Method == HttpMethod.Get &&
                    x.RequestUri != null &&
                    x.RequestUri.ToString() == "/somedata"),
                Arg.Any<CancellationToken>()
            })
            .Returns(Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedValue)
            }));

        var result = await _myClient.GetSomeData();

        Assert.Equivalent(expectedValue, result);
        ...
    }
}