type MyFixture() =
    let mutable db: MyDatabaseContext option = None
    
    member _.Db = 
        match db with
        | Some d -> d
        | None -> failwith "Database not initialized"
    
    do
        // Can still have setup in the constructor
        db <- Some(new MyDatabaseContext())
    
    interface IAsyncLifetime with
        member _.InitializeAsync() =
            // Async setup code that runs once for the entire test class
            Task.CompletedTask
        
        member _.DisposeAsync() =
            // Async teardown code that runs once for the entire test class
            match db with
            | Some d -> d.Dispose()
            | None -> ()
            db <- None
            Task.CompletedTask
