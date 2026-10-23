using Cake.Common.IO;
using Cake.Frosting;
using System.IO;

namespace CakeBuild.Tasks
{
    [TaskName("Package")]
    [IsDependentOn(typeof(BuildTask))]
    public sealed class PackageTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            string releasesDirectory = "../Releases";
            string stagingDirectory = releasesDirectory + "/" + context.ModIdentifier;
            string archivePath = releasesDirectory + "/" + context.ModIdentifier + "_" + context.Version + ".zip";
            string sourceDirectory = "../" + BuildContext.ProjectName;
            string assemblyPath = sourceDirectory + "/bin/" + context.BuildConfiguration + "/Build/" + BuildContext.ProjectName + ".dll";

            if (!context.FileExists(assemblyPath))
            {
                throw new FileNotFoundException("The compiled mod DLL is missing. Packaging cannot continue.", assemblyPath);
            }

            context.EnsureDirectoryExists(releasesDirectory);

            if (context.DirectoryExists(stagingDirectory))
            {
                context.CleanDirectory(stagingDirectory);
            }
            else
            {
                context.EnsureDirectoryExists(stagingDirectory);
            }

            context.CopyDirectory(sourceDirectory + "/assets", stagingDirectory + "/assets");
            context.CopyFile(sourceDirectory + "/modinfo.json", stagingDirectory + "/modinfo.json");
            context.CopyFile(assemblyPath, stagingDirectory + "/" + BuildContext.ProjectName + ".dll");

            if (context.FileExists(sourceDirectory + "/modicon.png"))
            {
                context.CopyFile(sourceDirectory + "/modicon.png", stagingDirectory + "/modicon.png");
            }

            if (context.FileExists(archivePath))
            {
                context.DeleteFile(archivePath);
            }

            context.Zip(stagingDirectory, archivePath);
        }
    }
}
