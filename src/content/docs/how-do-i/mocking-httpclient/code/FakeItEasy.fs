type MyClientTests() =
	let baseAddress = "https://www.example.com"
	let handler: HttpMessageHandler = A.Fake<HttpMessageHandler>()
	let httpClient = new HttpClient(handler)
	let myClient =
		httpClient.BaseAddress <- System.Uri(baseAddress)
		new MyClient(httpClient)

	[<Fact>]
	member _.MyTest() = task {
		...
		A.CallTo(handler)
			.WithReturnType<Task<HttpResponseMessage>>()
			.Where(fun call ->
				call.Method.Name = "SendAsync" &&
				call.Method.Name = HttpMethod.Get &&
				call.GetArgument<HttpRequestMessage>(0).RequestUri <> null &&
				call.GetArgument<HttpRequestMessage>(0).RequestUri.ToString() = "/somedata")
			.Returns(Task.FromResult(new HttpResponseMessage(
				StatusCode = HttpStatusCode.OK,
				Content = JsonContent.Create(expectedValue)
			)))

		let! result = myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	}

