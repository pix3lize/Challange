## Deputy technical Challange 

This application created with C# programming language that run with .NET core 2.1 to run on Mac OS please follow the instruction below 

## To Run 
1. Please download .NET Core 2.1 SDK from this location https://www.microsoft.com/net/download/macos
2. Install .NET Core 2.1 SDK 
3. Download this github package
4. Open terminal and go to `userhierarchies` and `scheduleoverlap` folder
5. Example: `cd /Users/hendritjiptowibowo/Documents/Challange/userhierarchies` (type without "")
5. To run the application type `dotnet run` on terminal 

## To view the source code and modify input please follow the instruction to view the code 
With VisualStudioCode 
1. Please download Visual Studio Code - https://code.visualstudio.com/Download?wt.mc_id=DotNet_Home 
2. Install Visual Studio Code 
3. Please download C https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp 
4. Install C# extention  
5. VisualStudioCode is a native applicaiton on Mac OS - double click 
6. Once the application is opened select `File->Open` select project folder 
7. Modify file 
8. Save
9. Run with `dotnet run` on terminal

With Sublime
1. Please download SubLime text editor https://download.sublimetext.com/Sublime%20Text%20Build%203176.dmg 
2. Install Sublime 
3. Open program.cs from each foler folder 
4. Modify file 
8. Save
9. Run with `dotnet run` on terminal

## Developer notes 
`User Hierarchies challange`
* De-serialize json to object
* First is to find role id from user id
* Recursive iteration needed as the goal is to find all the children under that roles. 
* Stored result from Recursive and find user detail 
* Serialize object to json

`Overlapping Schedule`
* Check if the user id is the same user id 
* Create an function to check the over lapping. 2nd start time bigger than 1st start time AND 2nd start time smaller than 1st finish time <- if meet this criteria return true

## Unit testing for User Hierarchies challange 
A new project is created for user hierarchies challange 
* Open terminal and go to `/UserHierarchy/User.Tests` folder
* Example: `cd /Users/hendritjiptowibowo/Documents/Challange/UserHierarchy/User.Tests` 
* To test the application type `dotnet test`
