Public Class MyClientTests
	Private ReadOnly _myClient As MyClient
	Private ReadOnly _handler As HttpMessageHandler = A.Fake(Of HttpMessageHandler)()
	Private ReadOnly _baseAddress As String = "https://www.example.com"

	Public Sub New()
		Dim httpClient = New HttpClient(_handler) With { .BaseAddress = New Uri(_baseAddress) }
		_myClient = New MyClient(httpClient)
	End Sub

	<Fact>
	Public Async Function MyTest() As Task
		...
		A.CallTo(_handler) _
			.WithReturnType(Of Task(Of HttpResponseMessage))() _
			.Where(Function(call) _
				call.Method.Name = "SendAsync" AndAlso _
				call.Method.Name = HttpMethod.Get _ AndAlso 
				call.GetArgument(Of HttpRequestMessage)(0).RequestUri IsNot Nothing _ AndAlso 
				call.GetArgument(Of HttpRequestMessage)(0).RequestUri.ToString() = "/somedata") _
			.Returns(Task.FromResult(New HttpResponseMessage() With {
				.StatusCode = HttpStatusCode.OK,
				.Content = JsonContent.Create(expectedValue)
			}))

		Dim result = Await _myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	End Function
End Class

