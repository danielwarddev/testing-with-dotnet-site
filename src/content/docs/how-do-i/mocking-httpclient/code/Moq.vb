Public Class MyClientTests
	Private ReadOnly _myClient As MyClient
	Private ReadOnly _handler As Mock(Of HttpMessageHandler) = New Mock(Of HttpMessageHandler)()
	Private ReadOnly _baseAddress As String = "https://www.example.com"

	Public Sub New()
		Dim httpClient = New HttpClient(_handler.Object) With { .BaseAddress = New Uri(_baseAddress) }
		_myClient = New MyClient(httpClient)
	End Sub

	<Fact>
	Public Async Function MyTest() As Task
		...
		_handler.Protected() _
			.Setup(Of Task(Of HttpResponseMessage))( _
				"SendAsync", _
				ItExpr.IsAny(Of HttpRequestMessage)(), _
				ItExpr.IsAny(Of CancellationToken)()) _
			.ReturnsAsync(New HttpResponseMessage() With {
				.StatusCode = HttpStatusCode.OK,
				.Content = JsonContent.Create(expectedValue)
			})

		Dim result = Await _myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	End Function
End Class

