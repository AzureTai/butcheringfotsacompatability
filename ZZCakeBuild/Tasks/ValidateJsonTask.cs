using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Frosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CakeBuild.Tasks
{
    [TaskName("ValidateJson")]
    public sealed class ValidateJsonTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            if (context.SkipJsonValidation)
            {
                return;
            }

            ValidateJsonFile(new FilePath("../" + BuildContext.ProjectName + "/modinfo.json"));

            FilePathCollection jsonFiles = context.GetFiles("../" + BuildContext.ProjectName + "/assets/**/*.json");
            foreach (FilePath jsonFile in jsonFiles)
            {
                ValidateJsonFile(jsonFile);
            }
        }

        private static void ValidateJsonFile(FilePath jsonFile)
        {
            try
            {
                string jsonText = File.ReadAllText(jsonFile.FullPath);
                JToken.Parse(jsonText);
            }
            catch (JsonException exception)
            {
                exception.Data["JsonFilePath"] = jsonFile.FullPath;
                throw new InvalidDataException("JSON validation failed. Inspect the inner exception and JsonFilePath data entry.", exception);
            }
        }
    }
}
