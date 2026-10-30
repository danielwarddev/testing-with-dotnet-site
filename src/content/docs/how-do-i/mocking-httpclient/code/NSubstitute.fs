type MyClientTests() =
	let baseAddress = "https://www.example.com"
	let handler: HttpMessageHandler = Substitute.For<HttpMessageHandler>()
	let httpClient = new HttpClient(handler)
	let myClient =
		httpClient.BaseAddress <- System.Uri(baseAddress)
		new MyClient(httpClient)

	[<Fact>]
	member _.MyTest() = task {
		...
		handler.GetType().GetMethod("SendAsync", BindingFlags.Instance ||| BindingFlags.NonPublic)
			.Invoke(handler,[| box(
				Arg.Is<HttpRequestMessage>(fun x ->
					x.Method = HttpMethod.Get &&
					x.RequestUri <> null &&
					x.RequestUri.ToString() = "/somedata"));
				box (Arg.Any<CancellationToken>()) |])
			|> ignore
		let! result = myClient.GetSomeData()
		Assert.Equivalent(expectedValue, result)
		...
	}

