@setlocal
:: "vanilla up-to-date Windows 10" seems to default to powershell 5.1
:: this limits the C# feature set you can use to .net 5
::
:: you can install newer versions of powershell from the Windows Store
:: (as of 2023-05-11 powershell 7 is the most recent)
:: * powershell 5 is run with command line "powershell"
:: * powershell 6+ are run with command line "pwsh -Command"
::
:: this batch file assumes the lowest common denominator of powershell 5
:: to use whatever version "pwsh" is set to on your system un-comment 
:: the next line which sets USE_LATEST_POWERSHELL
::@set USE_LATEST_POWERSHELL=1

:: set vars for assumed base legacy powershell version of 5.1
@if defined USE_LATEST_POWERSHELL goto LABEL_USE_LATEST

	@set POWERSHELL_COMMAND=powershell
	@set CSHARP_FILE=ScriptCSharp5.cs
	
	@echo using powershell 5 - script is "%CSHARP_FILE%"
	
	@goto LABEL_DO_COMMAND 

:: set vars for latest powershell
:LABEL_USE_LATEST	

	@set POWERSHELL_COMMAND=pwsh -Command
	@set CSHARP_FILE=ScriptCSharp.cs
	
	@echo using latest powershell - script is "%CSHARP_FILE%"
	
:LABEL_DO_COMMAND
:: store directory of this batch file into environment variable for access in C# land later
@set SCRIPT_HOME_DIR=%~dp0

@echo:

:: this compiles the C# code and passes batch file command line params 1-9 to it as 
:: a string array
@%POWERSHELL_COMMAND% $source=(Get-Content -Path ".\%CSHARP_FILE%" -Raw); Add-Type -TypeDefinition $source -Language CSharp;[System.String[]]$params=@();if('%1'){$params+='%1'};if('%2'){$params+='%2'};if('%3'){$params+='%3'};if('%4'){$params+='%4'};if('%5'){$params+='%5'};if('%6'){$params+='%6'};if('%7'){$params+='%7'};if('%8'){$params+='%8'};if('%9'){$params+='%9'}; [BatchScript.Script]::Main( $params )
