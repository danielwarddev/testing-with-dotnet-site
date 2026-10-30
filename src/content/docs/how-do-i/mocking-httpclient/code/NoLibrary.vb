' Create a fake message handler class that implements HttpMessageHandler
Public Class FakeHttpMessageHandler
    Inherits HttpMessageHandler

    Private _httpMethod As HttpMethod
    Private _requestUri As Uri
    Private _response As HttpResponseMessage

    Public Sub SetResponse(
        httpMethod As HttpMethod,
        requestUri As String,
        response As HttpResponseMessage)
        _httpMethod = httpMethod
        _requestUri = New Uri(requestUri)
        _response = response
    End Sub

    Protected Overrides Function SendAsync(
        request As HttpRequestMessage,
        cancellationToken As CancellationToken) As Task(Of HttpResponseMessage)
        If request.Method = _httpMethod AndAlso
           request.RequestUri = _requestUri Then
            Return Task.FromResult(_response)
        End If

        Throw New Exception($"Unexpected request. Received {request.Method} {request.RequestUri}, " +
                           $"but expected {_httpMethod} {_requestUri}")
    End Function
End Class

' Use the test class in your tests
Public Class MyClientTests
    Private ReadOnly _myClient As MyClient
    Private ReadOnly _handler As New FakeHttpMessageHandler()
    Private ReadOnly _baseAddress As String = "https://www.example.com"

    Public Sub New()
        Dim httpClient = New HttpClient(_handler) With {.BaseAddress = New Uri(_baseAddress)}
        _myClient = New MyClient(httpClient)
    End Sub

    <Fact>
    Public Async Function MyTest() As Task
        ...
        _handler.SetResponse(
            HttpMethod.Get,
            $"{_baseAddress}/somedata",
            New HttpResponseMessage With {
                .StatusCode = HttpStatusCode.OK,
                .Content = JsonContent.Create(expectedResult)
            }
        )

        Dim actualResult = Await _myClient.GetSomeData()

        Assert.Equivalent(expectedValue, actualResult)
        ...
    End Function
End Class