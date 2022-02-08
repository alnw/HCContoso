# HCContoso

## Prepare Local Database
1. Open ApplicationDbContextFactory
2. Change the name of Database (default: CompanyDB)
3. Open command prompt
4. CD into Database.Migrations and run following in command prompt
   - dotnet ef database update

## Run Project
1. Open Solution HCContoso\GraphQL.Serverless.sln
2. Create a file called "local.settings.json"
3. Put database connection string

    ```
    {
      "IsEncrypted": false,
      "DataSource": {
        "DbConnectionString": "Server=(localdb)\\mssqllocaldb;Database=CompanyDB"
      }
    }
    ```
    - For local DB
      - Server=(localdb)\\mssqllocaldb;Database=[database-name]
    - For DB Server
      - Server=tcp:[server-name],1433;Initial Catalog=[database-name];Persist Security Info=False;User ID=[your-username];Password=[your-password];MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;


## Known Issue
### Query involves 2 resolvers have issue:
Run following against local DB is fine, but with DB server, it gets error

#### Query
```
query {
  teamById(id: 1)
  {
   id
   members {
     identities {
       identity
     }
   }
   projects {
     name
     owner {
       displayName
       identities {
         identity
       }
     } 
   }
  }
}
```
#### Error
```
  "errors": [
    {
      "message": "Unexpected Execution Error",
      "locations": [
        {
          "line": 4,
          "column": 5
        }
      ],
      "path": [
        "teamById",
        "members"
      ],
      "extensions": {
        "message": "Invalid operation. The connection is closed.",
        "stackTrace": "   at System.Data.SqlClient.SqlCommand.<>c.<ExecuteDbDataReaderAsync>b__126_0(Task`1 result)\r\n   at System.Threading.Tasks.ContinuationResultTaskFromResultTask`2.InnerInvoke()\r\n   at System.Threading.Tasks.Task.<>c.<.cctor>b__277_0(Object obj)\r\n   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)\r\n--- End of stack trace from previous location ---\r\n   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)\r\n   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.AsyncEnumerator.InitializeReaderAsync(DbContext _, Boolean result, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Storage.ExecutionStrategy.ExecuteImplementationAsync[TState,TResult](Func`4 operation, Func`4 verifySucceeded, TState state, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Storage.ExecutionStrategy.ExecuteImplementationAsync[TState,TResult](Func`4 operation, Func`4 verifySucceeded, TState state, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.AsyncEnumerator.MoveNextAsync()\r\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\r\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\r\n   at GQL.DataLoaders.ContributorsByTeamIdDataLoader.LoadGroupedBatchAsync(IReadOnlyList`1 keys, CancellationToken cancellationToken) in C:\\Users\\alwo\\source\\repos\\GraphQL\\HCContoso\\GraphQL.Serverless\\GraphQL.Serverless\\DataLoaders\\ContributorsByTeamIdDataLoader.cs:line 43\r\n   at GQL.DataLoaders.ContributorsByTeamIdDataLoader.LoadGroupedBatchAsync(IReadOnlyList`1 keys, CancellationToken cancellationToken) in C:\\Users\\alwo\\source\\repos\\GraphQL\\HCContoso\\GraphQL.Serverless\\GraphQL.Serverless\\DataLoaders\\ContributorsByTeamIdDataLoader.cs:line 46\r\n   at GreenDonut.GroupedDataLoader`2.FetchAsync(IReadOnlyList`1 keys, Memory`1 results, CancellationToken cancellationToken)\r\n   at GreenDonut.DataLoaderBase`2.<>c__DisplayClass21_0.<<DispatchBatchAsync>g__StartDispatchingAsync|0>d.MoveNext()\r\n--- End of stack trace from previous location ---\r\n   at GQL.Teams.TeamResolvers.GetMembersAsync(TeamData team, ContributorsByTeamIdDataLoader dataloader, CancellationToken cancellationToken) in C:\\Users\\alwo\\source\\repos\\GraphQL\\HCContoso\\GraphQL.Serverless\\GraphQL.Serverless\\Teams\\TeamResolvers.cs:line 20\r\n   at HotChocolate.Resolvers.Expressions.ExpressionHelper.AwaitTaskHelper[T](Task`1 task)\r\n   at HotChocolate.Types.Helpers.FieldMiddlewareCompiler.<>c__DisplayClass9_0.<<CreateResolverMiddleware>b__0>d.MoveNext()\r\n--- End of stack trace from previous location ---\r\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.ExecuteResolverPipelineAsync(CancellationToken cancellationToken)\r\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.TryExecuteAsync(CancellationToken cancellationToken)"
      }
    }
  ]
```