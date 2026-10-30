public class MyClientTests
{
    private readonly MyClient _myClient;
    private readonly HttpMessageHandler _handler = A.Fake<HttpMessageHandler>();
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
        A.CallTo(_handler)
            .WithReturnType<Task<HttpResponseMessage>>()
            .Where(call => call.Method.Name == "SendAsync" &&
                call.Method.Name == HttpMethod.Get &&
                call.GetArgument<HttpRequestMessage>(0).RequestUri != null &&
                call.GetArgument<HttpRequestMessage>(0).RequestUri.ToString() == "/somedata")
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