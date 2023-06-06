 using System;
using System.Diagnostics;

namespace BatchScript
{
	public static class ExtensionMethods
	{
		public static bool IsNullOrEmpty( this string toCheck ) => string.IsNullOrEmpty( toCheck );
		public static bool NotNullOrEmpty( this string toCheck ) => ( ! toCheck.IsNullOrEmpty() );
	}
	
	//////////////////////////////////////////////////////////////////////////////
	/// holds result of running an external command line
	//////////////////////////////////////////////////////////////////////////////
	public struct ExternalCommandLineResult
	{
		public enum Type
		{
			Success,
			Error,
			Exception
		}

		public Type		type;
		public string	output;
		public string	error;

		public bool Succeeded => type == Type.Success;

		public override string ToString() => $"[{type}: {(type == Type.Success ? output : error)}";
	}

	//////////////////////////////////////////////////////////////////////////////
	///
	//////////////////////////////////////////////////////////////////////////////
	public static class ExternalCommandLineRunner
	{
		//------------------------------------------------------------------------
		public static ExternalCommandLineResult RunExternalCommandLine( string osSpecificExternalCommandToRun, bool logWarningOnErrorOrException )
		{
			return PassThroughResultLoggingErrorIfRequested( osSpecificExternalCommandToRun, RunExternalCommandLine( osSpecificExternalCommandToRun ), logWarningOnErrorOrException );
		}

		//------------------------------------------------------------------------
		private static ExternalCommandLineResult PassThroughResultLoggingErrorIfRequested( string osSpecificExternalCommandToRun, ExternalCommandLineResult externalCommandLineResult, bool logWarningOnErrorOrException )
		{
			if( logWarningOnErrorOrException && ( ! externalCommandLineResult.Succeeded ) )
			{
				System.Console.WriteLine( $"Error running external command: `{osSpecificExternalCommandToRun}`: {externalCommandLineResult}" );
			}

			return externalCommandLineResult;
		}

		//------------------------------------------------------------------------
		private static ExternalCommandLineResult RunExternalCommandLine( string externalWindowsCommandToRun )
		{
			try
			{
				var startInfo = new System.Diagnostics.ProcessStartInfo( "cmd", "/c " + externalWindowsCommandToRun );

				startInfo.RedirectStandardOutput = true;
				startInfo.RedirectStandardError  = true;
				startInfo.UseShellExecute        = false;

				startInfo.CreateNoWindow = true;

				var windowsProcess = new System.Diagnostics.Process();
				windowsProcess.StartInfo = startInfo;
				windowsProcess.Start();

				windowsProcess.WaitForExit();
				var stdOut   = windowsProcess.StandardOutput.ReadToEnd();
				var stdError = windowsProcess.StandardError.ReadToEnd();

				var resultType = stdError.IsNullOrEmpty() ? ExternalCommandLineResult.Type.Success : ExternalCommandLineResult.Type.Error;

				return new ExternalCommandLineResult(){ type = resultType, output = stdOut, error = stdError };

			}
			catch( System.Exception exception )
			{
				return new ExternalCommandLineResult(){ type = ExternalCommandLineResult.Type.Exception, output = string.Empty, error = exception.Message };
			}
		}
	}

	
	//
	// code in this file can use the C# feature set corresponding to the
	// .net used by the most recent version of powershell you have installed
	//
	// please see ReadMe.md for details of how to use new C# features
	//
	public class Script
	{
		public static void Log( string logMessage ) => Console.WriteLine( logMessage );
		
		public static string GetWorkingDir() => Environment.CurrentDirectory;

		public static string GetScriptHomeDir() => Environment.GetEnvironmentVariable( "SCRIPT_HOME_DIR" );

		public static int Main( string[] args )
		{
			Log( $"script home dir: {GetScriptHomeDir()}" );
			Log( $"current dir: {GetWorkingDir()}" );
			Log( args.Length > 0 ? string.Join( ", ", args ) : "no params" );
			Log( ExternalCommandLineRunner.RunExternalCommandLine( "echo this is a test", logWarningOnErrorOrException: false ).output );
			return 0;
		}
	}
}
