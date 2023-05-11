using System;

namespace BatchScript
{
	//
	// code in this file is limited to the feature set of C# 5
	//
	// please see ReadMe.md for details of how to use new C# features
	//
	public class Script
	{
		public static string GetWorkingDir()
		{
			return Environment.CurrentDirectory;
		}

		public static string GetScriptHomeDir()
		{
			return Environment.GetEnvironmentVariable( "SCRIPT_HOME_DIR" );
		}

		public static int Main( string[] args )
		{
			Console.WriteLine( "script home dir: " + GetScriptHomeDir() );
			Console.WriteLine( "current dir: " + GetWorkingDir() );
			Console.WriteLine( args.Length > 0 ? string.Join( ", ", args ) : "no params" );
			return 0;
		}
	}
}