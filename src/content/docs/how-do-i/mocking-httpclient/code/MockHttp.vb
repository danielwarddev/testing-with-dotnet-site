Public Class MyClientTests
	Private ReadOnly _myClient As MyClient
	Private ReadOnly _handler As New MockHttpMessageHandler()
	Private ReadOnly _baseAddress As String = "https://www.example.com"

	Public Sub New()
		Dim httpClient = New HttpClient(_handler) With { .BaseAddress = New Uri(_baseAddress) }
		_myClient = New MyClient(httpClient)
	End Sub

	<Fact>
	Public Async Function MyTest() As Task
		...
		_handler.Expect(HttpMethod.Get, $"{_baseAddress}/someData") _
				.Respond(HttpStatusCode.OK, JsonContent.Create(expectedValue))

		Dim result = Await _myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	End Function
End Class
