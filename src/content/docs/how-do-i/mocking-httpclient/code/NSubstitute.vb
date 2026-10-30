Public Class MyClientTests
	Private ReadOnly _myClient As MyClient
	Private ReadOnly _handler As HttpMessageHandler = Substitute.For(Of HttpMessageHandler)()
	Private ReadOnly _baseAddress As String = "https://www.example.com"

	Public Sub New()
		Dim httpClient = New HttpClient(_handler) With { .BaseAddress = New Uri(_baseAddress) }
		_myClient = New MyClient(httpClient)
	End Sub

	<Fact>
	Public Async Function MyTest() As Task
		...
		_handler.GetType().GetMethod("SendAsync", BindingFlags.Instance Or BindingFlags.NonPublic) _
			.Invoke(_handler, New Object() {
				Arg.Is(Of HttpRequestMessage)(Function(x)
					x.Method = HttpMethod.Get AndAlso
					x.RequestUri Is Not Nothing AndAlso
					x.RequestUri.ToString() = "/somedata"),
				Arg.Any(Of CancellationToken)()
			})
		Dim result = Await _myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	End Function
End Class

