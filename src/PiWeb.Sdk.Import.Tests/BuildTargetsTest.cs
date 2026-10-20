#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Tests;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using NUnit.Framework;

[TestFixture]
public class BuildTargetsTest
{
    #region methods

    [Test]
    [Description( "Verifies that a manifest/assembly version mismatch logs a warning during the build." )]
    public async Task BuildingPluginWithDifferentManifestVersion_ThenWarningIsLogged()
    {
        const string assemblyVersion = "1.2.3.0";
        const string pluginVersion = "2.0.0";

        var buildTargetsPath = FindBuildTargetsPath();
        var projectDirectory = Path.Combine( Path.GetTempPath(), $"PiWeb.Sdk.Import.Tests.{Guid.NewGuid():N}" );
        Directory.CreateDirectory( projectDirectory );

        try
        {
            var projectFilePath = Path.Combine( projectDirectory, "VersionMismatchPlugin.csproj" );
            var projectFileContents = $"""
                <Project Sdk="Microsoft.NET.Sdk">
                  <PropertyGroup>
                    <TargetFramework>net10.0</TargetFramework>
                    <AssemblyName>VersionMismatchPlugin</AssemblyName>
                    <Version>{assemblyVersion}</Version>
                    <GeneratePluginPackageOnBuild>true</GeneratePluginPackageOnBuild>
                  </PropertyGroup>
                  <Import Project="{SecurityElement.Escape( buildTargetsPath )}" />
                </Project>
                """;
            var manifestFilePath = Path.Combine( projectDirectory, "manifest.json" );
            const string manifestFileContents = $$"""
                {
                  "version": "{{pluginVersion}}"
                }
                """;
            var sourceFilePath = Path.Combine( projectDirectory, "Plugin.cs" );

            await File.WriteAllTextAsync( projectFilePath, projectFileContents );
            await File.WriteAllTextAsync( manifestFilePath, manifestFileContents );
            await File.WriteAllTextAsync( sourceFilePath, "public class Plugin { }" );

            var startInfo = new ProcessStartInfo( "dotnet" )
            {
                WorkingDirectory = Path.GetDirectoryName( buildTargetsPath )!,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            startInfo.ArgumentList.Add( "build" );
            startInfo.ArgumentList.Add( projectFilePath );
            startInfo.ArgumentList.Add( "--nologo" );
            startInfo.ArgumentList.Add( "--verbosity:minimal" );

            using var process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            var standardOutputTask = process.StandardOutput.ReadToEndAsync();
            var standardErrorTask = process.StandardError.ReadToEndAsync();
            using var timeout = new CancellationTokenSource( TimeSpan.FromMinutes( 2 ) );

            try
            {
                await process.WaitForExitAsync( timeout.Token );
            }
            catch (OperationCanceledException)
            {
                if (!process.HasExited)
                    process.Kill( entireProcessTree: true );

                await process.WaitForExitAsync( CancellationToken.None );
                throw;
            }

            var output = await standardOutputTask;
            var error = await standardErrorTask;
            process.ExitCode.Should().Be( 0, $"dotnet build failed:{Environment.NewLine}{output}{Environment.NewLine}{error}" );

            output.Should().Contain(
                $"The plugin version in the manifest.json ({pluginVersion}) differs from the assembly version ({assemblyVersion})." );
        }
        finally
        {
            if (Directory.Exists( projectDirectory ))
                Directory.Delete( projectDirectory, recursive: true );
        }
    }

    private static string FindBuildTargetsPath()
    {
        var directory = new DirectoryInfo( TestContext.CurrentContext.TestDirectory );

        while (directory is not null)
        {
            var buildTargetsPath = new[]
            {
                Path.Join( directory.FullName, "PiWeb.Sdk.Import", "build.targets" ),
                Path.Join( directory.FullName, "src", "PiWeb.Sdk.Import", "build.targets" )
            }.FirstOrDefault( File.Exists );

            if (buildTargetsPath is not null)
                return buildTargetsPath;

            directory = directory.Parent;
        }

        throw new FileNotFoundException( "Could not find PiWeb.Sdk.Import\\build.targets above the test assembly directory." );
    }

    #endregion
}