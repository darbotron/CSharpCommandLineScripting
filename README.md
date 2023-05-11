# CSharpCommandLineScripting
write scripts to be run at the command line in C#! (runs via a batch file and powershell....)

The magic is all in the batch file, it uses powershell to compile and run a CSharp file :D

The batch file also forwards (up to) the first 9 command line params passed to it to the C# code :D

There are two different CSharp files because:

- "vanilla up-to-date Windows 10" seems to default to powershell 5.1...
  - which limits the C# feature set you can use to .net 5 (i.e. C#5)
  - ScriptCSharp5.cs won't compile if it uses C# features from versions above version 5

- you can install newer versions of powershell from the Windows Store / `winget` (which is, ironically, also available via the Windows Store)
  - ScriptCSharp.cs can use whatever the latest version of C# supported by your local version of powershell is
  - (as of 2023-05-11 powershell 7 is the most recent)

The batch file assumes "vanilla" powershell 5.1 - compiling ScriptCSharp5.cs.

If you want to use newer features (& ScriptCSharp.cs) you'll need to un-comment the line which sets the environment variable `USE_LATEST_POEWERSHELL` in the batch file; this will make the batch file try to use the latest version of powershell available on your machine.

# License
I've licensed this with unlicense, TL;DR: 
1. do what you like with the content of this git repo...
2. ...other than blame me for any consequence of 1.

# Enjoy!
