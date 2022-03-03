# EF.Core.Training

EF.Core.Training is a simple solution containing two projects to assist in the learning and understanding of the basics of using Entity Framework Core.

Both projects in the Solution utilize .NET 6.0, so be sure to [download and install it](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

The Solution uses SQLite as a simple database for interacting with, which will be saved to the EF.Core.Training root folder.

## EF.Core.Training.BlackBox

You should **not** be altering any code in this project.

You may view the objects in here if you end up needing to understand what is happening beneath the covers, but altering any of these files will result in failed Unit Tests.

## EF.Core.Training.CodeShop

This project is where you will make most of your code changes to pass off this hands-on training module.

You will primarily be working with the `ApiContext.cs` and the `EfRepository.cs`.

You should not be changing anything wrapped in a region `Do Not Alter`, as these areas contain core functionality that if changed will almost certainly cause the unit tests which rely upon the implementations to fail.

Take note of the `ApiContext.cs` code that is already implemented properly for the Genre, which is already in a fully working state as an example.

## EF.Core.Training.Tests

You should **not** be altering any code in this project.

After you rebuild the entire solution you should be able to run all of the unit tests in the Test Explorer.
If you have properly completed the necessary changes in EF.Core.Training.CodeShop, then all of the unit tests should be passing.

When you first start the `GenreCRUDTest` and `GenreWithNullNameFailsTest` cases should already be passing.

## Installation

Start by making a fork of this repository and then pulling it down to your local machine.

Do a nuget restore, followed by a build of the solution. Then you can run the unit tests to initialize the SQLite Database if you desire, or you can just call the following Windows PowerShell command (see the Migrations section for more details):
```powershell
dotnet-ef database update
```

### Migrations

As part of your work, you will be required to create at least one migration after finalizing your `ApiContext.cs` implementations.

To do this you will need to have the [dotnet-ef](https://docs.microsoft.com/en-us/ef/core/get-started/overview/install) tool installed if you do not already.
It can be installed via CLI in Windows PowerShell:
```powershell
dotnet tool install --global dotnet-ef
```

Once this is installed and your ApiContext changes are in place, navigate into the EF.Core.Training.CodeShop folder in Windows Powershell and run the command:
```powershell
dotnet-ef migrations add <MigrationName>
```
`<MigrationName>` becomes your migration's name without the `<>`.

If you built out your `ApiContext.cs` correctly then you shouldn't have any failures here, however, if you see a failure message be sure to read its contents and go back to your `ApiContext.cs` to determine what you missed in the setup. Google can also be your friend here, as there are a plethora of StackOverflow articles about issues with EF.

After you successfully add your migration, you can run it against the SQLite DB by either running the Unit Tests or by again navigating into the EF.Core.Training.CodeShop folder with Windows Powershell and running the following command:
```powershell
dotnet-ef database update
```

**Note:** You can always completely revert any broken migrations or `ApiContextModelSnapshot.cs` changes, and completely delete the `EF.Core.Training.db`, and then start over by calling:
```powershell
dotnet-ef database update
```
Just be sure you still have the `ApiContextModelSnapshot.cs` in its starting state from when you pulled the repository as well as the `InitialCreate` migration.

## Additional Information

- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
