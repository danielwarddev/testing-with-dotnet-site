// Create a fake message handler class that implements HttpMessageHandler
public class FakeHttpMessageHandler : HttpMessageHandler
{
    private HttpMethod _httpMethod;
    private Uri _requestUri;
    private HttpResponseMessage _response;

    public void SetResponse(
        HttpMethod httpMethod,
        string requestUri,
        HttpResponseMessage response)
    {
        _httpMethod = httpMethod;
        _requestUri = new Uri(requestUri);
        _response = response;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Method == _httpMethod &&
            request.RequestUri == _requestUri)
        {
            return Task.FromResult(_response);
        }

        throw new Exception($"Unexpected request. Received {request.Method} {request.RequestUri}, " +
                            $"but expected {_httpMethod} {_requestUri}");
    }
}

// Use the test class in your tests
public class MyClientTests
{
    private readonly MyClient _myClient;
    private readonly FakeHttpMessageHandler _handler = new();
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
        _handler.SetResponse(
            HttpMethod.Get,
            $"{_baseAddress}/somedata",
            new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedResult)
            }
        );

        var actualResult = await _myClient.GetSomeData();

        Assert.Equivalent(expectedValue, actualResult);
        ...
    }
}