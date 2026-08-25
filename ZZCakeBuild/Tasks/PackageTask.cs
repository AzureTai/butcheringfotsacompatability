using Cake.Common.IO;
using Cake.Frosting;

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
