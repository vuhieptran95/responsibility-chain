# Development
## Environment
- ASP.NET Core 3.1
- C# 8
- Visual Studio 2019 16.4
- [Seq](https://datalust.co/seq)

## Windows authentication
To enable Windows authentication when hosting with IIS/IIS Express on development machine with SSL enabled, you would probably get ERR_CONNECTION_RESET error.

To fix this issue, run in `PowerShell`

```
dir cert:\localmachine\my
```

And got something like this

```
Thumbprint                                Subject
----------                                -------
A450FE80A4B3DE1E5C8876C580C3B7FB12376248  CN=localhost
```

Then run the following commands

```
$guid = [guid]::NewGuid()
$certHash = "A450FE80A4B3DE1E5C8876C580C3B7FB12376248"
$ip = "0.0.0.0" # This means all IP addresses
$port = "5001" # the default HTTPS port
"http add sslcert ipport=$($ip):$port certhash=$certHash appid={$guid}" | netsh
```

Reference:
https://forums.asp.net/t/2143976.aspx

## Add migration scripts
To add a new migration, run this command from repository's root

```
dotnet ef migrations add <migration_name> --startup-project ProjectHealthReport.Web/ --project ProjectHealthReport.Domains
```

```
dotnet ef migrations add <migration_name> --startup-project IdentityServer/ --project IdentityServer.Features
```

# Add a new feature checklist
## Request
- A request, either command or query, should implement `IAsyncRequest<TResponse>` interface.
- Requests should have either `Command` or `Query`-suffix conventionally.

Example:

```c#
public class AddProjectCommand : IAsyncRequest<int>
{
    [Required]
    public string Name { get; set; }
}
```

## Business
- Business handler should implement `IBusinessHandler<TRequest, TResponse>` interface and have `Handler`-suffix conventionally.

Example:

```c#
public class AddProjectCommandHandler : IBusinessHandler<AddProjectCommand, int>
{
    public Task<int> HandleAsync(AddProjectCommand command, Func<AddProjectCommand, Task<int>> next)
    {
        // handle business
    }
}
```

## Authentication
This application is configured to use Windows authentication and integrate with Niteco's Active Directory to retrieve Nitecan's information including
- Username, e.g. `son.nguyen2`
- Role, e.g. `Developer`
- Email, e.g. `son.nguyen2@niteco.se`
- Division, e.g. `Tyr`

Inject `IRequestContext` into constructor, where you need this information.
Take a look at `SetupRequestContext` class to see how `IRequestContext` object is constructed.

## Authorization
- Authorization configurations on all commands/queries are required.
- Add a _single_ class which is inherited from `AuthorizationContext<TRequest>` and have `AuthorizationContext`-suffix conventionally.

Example:

```c#
public class AddProjectCommandAuthorizationContext : AuthorizationContext<AddProjectCommand>
{
    public AddProjectCommandAuthorizationContext()
    {
        Accesses = new[] {new Access(new[] {"create", "update", "read"}, new[] {"project", "backlog"})};
    }
}
```

## Preprocessing
- A _single_ preprocessor should implement `IPreprocessor<TRequest, TResponse>` interface and have `Preprocessor`-suffix conventionally.
- If you want more than one preprocessor for a given `TRequest`, then
  - have a composite preprocessor which extends `Preprocessor<TRequest, TResponse>` abstract class
  - have many preprocessor elements which implement `IAsyncHandler<TRequest, TResponse>`
  - hook up these elements into composite preprocessor via `AddHandler(element)` within its constructor

Example:

```c#
public class AddProjectCommandPreprocessor : IPreprocessor<AddProjectCommand, int>
{
    public Task<int> HandleAsync(AddProjectCommand command, Func<AddProjectCommand, Task<int>> next)
    {
        // preprocess the request

        // pass the request object to the next handler
        return next(command);
    }
}
```

## Validation
- A _single_ validator should implement `RuleBasedValidator<TRequest, TResponse>` interface and have `Validator`-suffix conventionally.
- Validation rule(s) should implement `IAsyncHandler<TRequest, TResponse>` and have `Rule`-suffix conventionally.
- Validation rule(s) should be injected into validator's constructor and added to the pipeline.

Example:

```c#
public class AddProjectCommandValidator : RuleBasedValidator<AddProjectCommand, int>
{
    public AddProjectCommandValidator(ProjectNameMustBeUniqueRule projectNameMustBeUniqueRule)
    {
        AddHandler(projectNameMustBeUniqueRule);
        // more rules come here
    }
}

public class ProjectNameMustBeUniqueRule : IAsyncHandler<AddProjectCommand, int>
{
    public Task<int> HandleAsync(AddProjectCommand command, Func<AddProjectCommand, Task<int>> next)
    {
        if (projectNameIsNotUnique)
        {
            // throw exception
        }

        // proceed otherwise
        return next(command);
    }
}
```

### Notes:
- Data annotation attributes are also supported.

## Postprocessing
- A _single_ postprocessor should implement `IPostprocessor<TRequest, TResponse>` interface and have `Postprocessor`-suffix conventionally.

```c#
public class CalculateRemainingBacklogItemsPostprocessor : IPostprocessor<GetWeeklyReportQuery, GetWeeklyReportDto>
{
    public Task<GetWeeklyReportDto> HandleAsync(GetWeeklyReportDto response, Func<GetWeeklyReportDto, Task<GetWeeklyReportDto>> next)
    {
        // calculate remaining backlog items here
    }
}

public interface IPostprocessor<TRequest, TResponse>
    : IAsyncHandler<TResponse, TResponse>
    where TRequest : IAsyncRequest<TResponse>
{
}
```

### Notes:
- If there are multiple postprocessors, which implement `IPostprocessor<TRequest, TResponse>` of the same `TRequest` type, were provided, an `ArgumentException` will be thrown upon building up the dependency graph of the controller.
- If you want more than one postprocessor for a given `TRequest`, then
  - have a composite postprocessor which extends `Postprocessor<TRequest, TResponse>` abstract class
  - have many postprocessor elements which implement `IAsyncHandler<TResponse, TResponse>`
  - hook up these elements into composite postprocessor via `AddHandler(element)` within its constructor

```c#
public class GetWeeklyReportQueryPostprocessor : Postprocessor<GetWeeklyReportQuery, GetWeeklyReportDto>
{
    public GetWeeklyReportQueryPostprocessor(
        CalculateRemainingBacklogItems calculateRemainingBacklogItems,
        CalculateNewAndRemainingBugs calculateNewAndRemainingBugs)
    {
        AddHandler(calculateRemainingBacklogItems);
        AddHandler(calculateNewAndRemainingBugs);
    }
}

public class CalculateRemainingBacklogItems : IAsyncHandler<GetWeeklyReportDto, GetWeeklyReportDto>
{
    public Task<GetWeeklyReportDto> HandleAsync(GetWeeklyReportDto response, Func<GetWeeklyReportDto, Task<GetWeeklyReportDto>> next)
    {
        // modify the response

        // then pass it to the next handler
        return next(input);
    }
}
```

- Known limitation: look at the signature of `IPostprocessor<TRequest, TResponse>` interface, you will see there is no information of the request object except its `TRequest` type. This can be solved by capturing the request object within `PostprocessingHandler<TRequest, TResponse>` and injecting `IRequestObjectAccessor<TRequest>` where needed.

## Caching
- To cache the response of a query, add a _single_ class which is inherited from `CacheConfiguration<TRequest>` and have `CacheConfiguration`-suffix conventionally.
- To provide a custom cache key generation, override the `CacheConfiguration<TRequest>.GetCacheKey` method.
- Cache items are personalized by default.

Example:

```c#
public class GetActiveNitecansQueryCacheConfiguration : CacheConfiguration<GetActiveNitecansQuery>
{
    public GetActiveNitecansQueryCacheConfiguration()
    {
        AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
    }

    public override string GetCacheKey(CacheableQuery request)
    {
        // custom cache key generation
    }
}
```

## Audit
- To enable auditing a project, add a _single_ class which extends `AuditConfiguration<TRequest, TResponse>` and have `AuditConfiguration`-suffix conventionally.
- Provide implementation for `abstract string SpecifyEntityId(TRequest request, TResponse response)` method, which specifies the entity ID of the entity being audited.
- The entity ID, e.g. `Projects/1`, comprises of two parts
  - The entity name in pluralized, Pascal-case form, e.g. `Projects`
  - The entity ID in the database
- All commands with either `create` or `update` verb must provide `AuditConfiguration<TRequest, TResponse>` implementation.

```c#
public class AddEditProjectCommandAuditConfiguration : AuditConfiguration<AddEditProjectCommand, int>
{
    protected override string SpecifyEntityId(AddEditProjectCommand request, int response)
    {
        return $"Projects/{response}";
    }
}
```

## Invocation
- Inject `AsyncRequestHandler<TRequest, TResponse>` into controller's constructor

Example:

```c#
public class ProjectsController : Controller
{
    private readonly AsyncRequestHandler<AddProjectCommand, int> _addProject;

    public ProjectsController(AsyncRequestHandler<AddProjectCommand, int> addProject)
    {
        _addProject = addProject;
    }

    public async Task AddProject()
    {
        var command = new AddProjectCommand {Name = "Project Health Report"};

        await _addProject.HandleAsync(command, null);
    }
}
```

# Deployment
## Build and publish
- Run from repository's root directory
- Delete PHR_publish/DMR_publish folder first

```
dotnet publish --output PHR_publish --configuration Release src/ProjectHealthReport.Web/ProjectHealthReport.Web.csproj
```

```
dotnet publish --output DMR_publish --configuration Release src/DeliveryManagerReport.Web/DeliveryManagerReport.Web.csproj
```

```
dotnet publish --output Hangfire_publish --configuration Release src/Report.Notification.Web/Report.Notification.Web.csproj
```

- Copy output directory to web server at `C:\inetpub\production\project_health_report`
- Create a tag to mark pointing to the current commit as a release, e.g. `release/1.5.0`, `staging/1.5.1`, and push tag to git repository as well.
  - `release`-prefix for production deployment
  - `staging`-prefix for staging deployment 

### Note
- Log file is output to this directory `C:\inetpub\production\project_health_report_logs`.
- For staging deployment, use `C:\inetpub\staging` directory.

## Configure for different environments
- For non-production environment, i.e. staging, the same source code is used, only `ASPNETCORE_ENVIRONMENT` variable varies.
- `ASPNETCORE_ENVIRONMENT` variable should be set per application pool, e.g.

```
cd %systemroot%\System32\inetsrv\
appcmd.exe set config -section:system.applicationHost/applicationPools /+"[name='phr-staging'].environmentVariables.[name='ASPNETCORE_ENVIRONMENT',value='Staging']" /commit:apphost
```

### References
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1
- https://docs.microsoft.com/en-us/iis/configuration/system.applicationHost/applicationPools/add/environmentVariables/#appcmdexe

## Logging
Log entries are written to
- local [Seq](https://datalust.co/seq) server at http://localhost:5341 or http://phr.niteco.se:5341, and
- file located in `Logs` folder

### Setup Seq
- Download Seq from https://datalust.co/seq
- Install

#### For local development
- Go to http://localhost:5341 to see the log

#### For server
- Add an inbound rule to Windows Firewall to allow TCP:5341
- From developer's machine, go to http://phr.niteco.se:5341 to see the log
