# sdk-dotnet-ecloud

This is the official .NET SDK for UKFast eCloud

You should refer to the [Getting Started](https://developers.ukfast.io/getting-started) section of the API documentation before proceeding below

## Basic usage

To get started, we'll first instantiate an instance of `IUKFastECloudClient`:

```csharp
IUKFastECloudClient client = new UKFastECloudClient(new ClientConnection("myapikey"));
```

Next, we'll obtain an instance of IVirtualMachineOperations to perform operations on virtual machines:

```csharp
var vmOps = client.VirtualMachineOperations();
```

Finally, we'll retrieve all virtual machines using the instance of `IVirtualMachineOperations`:

```csharp
IList<VirtualMachine> vms = await vmOps.GetVirtualMachinesAsync();
```

## Operations

All operations available via the SDK are exposed via the client (`IUKFastECloudClient`)