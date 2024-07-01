-- Copy Contents of the solution folder onto a folder on your windows machine eg. c:\development\PersonManagement
-- Open solution in Visual Studio 2022 
-- Build Web Api Project 
-- Go into AppSettings file and set the appropriate database connectionstring
-- Execute the file InteractiveMVC_Tables_With_Data which lives inside the scripts folder. This script must be executed first against the database called "[PersonAccountTransactions]"
-- Gointo Package manager console and run the following commands against [PersonAccountTransactions] : 
   
DOTNET EF Database Update --context "SecurityContext"

DOTNET EF Database Update --context "PersonAccountTransactionDataContext"

-- Build Web Project and Run 
-- Launch the app. By default is takes you the the login page. 
-- Click on sign up and enter in your new user details. Once done a message will appear asking you to check your OTP
-- NB.. For purposes of this demo i have bypassed the otp step.
-- Use this newly created username and password to login 
