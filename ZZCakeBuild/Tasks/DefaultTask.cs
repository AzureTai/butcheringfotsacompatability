using Cake.Frosting;

namespace CakeBuild.Tasks
{
    [TaskName("Default")]
    [IsDependentOn(typeof(PackageTask))]
    public sealed class DefaultTask : FrostingTask
    {
    }
}
