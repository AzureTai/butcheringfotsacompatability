using Cake.Common;
using Cake.Core;
using Cake.Frosting;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CakeBuild
{
    public sealed class BuildContext : FrostingContext
    {
        public const string ProjectName = "ButcheringFotSACompatability";

        public string BuildConfiguration { get; }

        public string ModIdentifier { get; }

        public bool SkipJsonValidation { get; }

        public string Version { get; }

        public BuildContext(ICakeContext context)
            : base(context)
        {
            BuildConfiguration = context.Argument<string>("configuration", "Release");
            SkipJsonValidation = context.Argument<bool>("skipJsonValidation", false);

            string modInformationPath = Path.Combine("..", ProjectName, "modinfo.json");
            string modInformationText = File.ReadAllText(modInformationPath);
            JObject modInformation = JObject.Parse(modInformationText);

            ModIdentifier = ReadRequiredString(modInformation, "modid");
            Version = ReadRequiredString(modInformation, "version");
        }

        private static string ReadRequiredString(JObject source, string propertyName)
        {
            JToken? propertyToken = source[propertyName];
            if (propertyToken == null)
            {
                throw new InvalidDataException("A required mod information property is missing.");
            }

            string? propertyValue = propertyToken.Value<string>();
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                throw new InvalidDataException("A required mod information property is empty.");
            }

            return propertyValue;
        }
    }
}
