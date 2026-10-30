type MyClientTests() =
	let baseAddress = "https://www.example.com"
	let handler = new MockHttpMessageHandler()
	let httpClient = new HttpClient(handler)
	let myClient =
		httpClient.BaseAddress <- System.Uri(baseAddress)
		new MyClient(httpClient)

	[<Fact>]
	member _.MyTest() = task {
		...
		handler.Expect(HttpMethod.Get, $"{baseAddress}/someData")
			   .Respond(HttpStatusCode.OK, JsonContent.Create(expectedValue))

		let! result = myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	}