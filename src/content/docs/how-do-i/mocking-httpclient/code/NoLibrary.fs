// Create a fake message handler class that implements HttpMessageHandler
type FakeHttpMessageHandler() =
    inherit HttpMessageHandler()
    
    let mutable _httpMethod = HttpMethod.Get
    let mutable _requestUri : Uri = null
    let mutable _response : HttpResponseMessage = null

    member this.SetResponse(
        httpMethod : HttpMethod,
        requestUri : string,
        response : HttpResponseMessage) =
        _httpMethod <- httpMethod
        _requestUri <- Uri(requestUri)
        _response <- response

    override this.SendAsync(
        request : HttpRequestMessage,
        cancellationToken : CancellationToken) =
        if request.Method = _httpMethod &&
           request.RequestUri = _requestUri then
            Task.FromResult(_response)
        else
            raise (Exception($"Unexpected request. Received {request.Method} {request.RequestUri}, " +
                            $"but expected {_httpMethod} {_requestUri}"))

// Use the test class in your tests
type MyClientTests() =
    let _handler = FakeHttpMessageHandler()
    let _baseAddress = "https://www.example.com"
    let httpClient = new HttpClient(_handler, BaseAddress = Uri(_baseAddress))
    let _myClient = MyClient(httpClient)

    [<Fact>]
    member this.MyTest() = async {
        ...
        _handler.SetResponse(
            HttpMethod.Get,
            $"{_baseAddress}/somedata",
            new HttpResponseMessage(
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedResult)
            )
        )

        let! actualResult = _myClient.GetSomeData() |> Async.AwaitTask

        Assert.Equivalent(expectedValue, actualResult)
        ...
    } |> Async.StartAsTask