type MyClientTests() =
	let baseAddress = "https://www.example.com"
	let handler = new Mock<HttpMessageHandler>()
	let httpClient = new HttpClient(handler.Object)
	let myClient =
		httpClient.BaseAddress <- System.Uri(baseAddress)
		new MyClient(httpClient)

	[<Fact>]
	member _.MyTest() = task {
		...
		handler.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage(StatusCode = HttpStatusCode.OK, Content = JsonContent.Create(expectedValue)))

		let! result = myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	}

