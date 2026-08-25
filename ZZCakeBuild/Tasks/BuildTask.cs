using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.Clean;
using Cake.Frosting;

namespace CakeBuild.Tasks
{
    [TaskName("Build")]
    [IsDependentOn(typeof(ValidateJsonTask))]
    public sealed class BuildTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            string projectPath = "../" + BuildContext.ProjectName + "/" + BuildContext.ProjectName + ".csproj";

            DotNetCleanSettings cleanSettings = new DotNetCleanSettings
            {
                Configuration = context.BuildConfiguration
            };
            context.DotNetClean(projectPath, cleanSettings);

            DotNetBuildSettings buildSettings = new DotNetBuildSettings
            {
                Configuration = context.BuildConfiguration
            };
            context.DotNetBuild(projectPath, buildSettings);
        }
    }
}
