using System;

namespace BatchScript
{
	//
	// code in this file can use the C# feature set corresponding to the
	// .net used by the most recent version of powershell you have installed
	//
	// please see ReadMe.md for details of how to use new C# features
	//
	public class Script
	{
		public static string GetWorkingDir() => Environment.CurrentDirectory;

		public static string GetScriptHomeDir() => Environment.GetEnvironmentVariable( "SCRIPT_HOME_DIR" );

		public static int Main( string[] args )
		{
			Console.WriteLine( $"script home dir: {GetScriptHomeDir()}" );
			Console.WriteLine( $"current dir: {GetWorkingDir()}" );
			Console.WriteLine( args.Length > 0 ? string.Join( ", ", args ) : "no params" );
			return 0;
		}
	}
}