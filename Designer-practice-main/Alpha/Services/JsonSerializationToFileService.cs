using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Alpha.Data;
using System.IO;

namespace Alpha.Services
{
    // TODO: Generic
    public class JsonSerializationToFileService
    {
        private JsonPaths jsonPaths = new JsonPaths();
        // TODO: PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        public void ExportWorkProductCriterionsToFile(List<WorkProductCriterion> workProductCriterions)
        {
            var jsonWorkProductCriterions = JsonSerializer.Serialize(workProductCriterions, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase

            });
            File.WriteAllText(jsonPaths.PathToWorkProductCriterionsFile, jsonWorkProductCriterions);
        }
        public void ExportAlphaCriterionsToFile(List<AlphaCriterion> alphaCriterions)
        {
            var jsonAlphaCriterions = JsonSerializer.Serialize(alphaCriterions, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase

            });
            File.WriteAllText(jsonPaths.PathToAlphaCriterionsFile, jsonAlphaCriterions);
        }
        public void ExportActivitiesToJsonFile(List<Activity> activities)
        {
            var jsonActivities = JsonSerializer.Serialize(activities, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.PathToActivitiesFile, jsonActivities);
        }
        public void ExportAlphasToFile(List<Alpha> alphas)
        {
            var jsonAlphas = JsonSerializer.Serialize(alphas, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.PathToAlphasFile, jsonAlphas);
        }
        public void ExportWorkProductsToJsonFile(List<WorkProduct> workProducts)
        {
            var jsonWorkProducts = JsonSerializer.Serialize(workProducts, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.PathToWorkProductsFile, jsonWorkProducts);
        }
        public void ExportWorkProductManifestsToJsonFile(List<WorkProductManifest> workProductManifests)
        {
            var jsonWorkProductManifests = JsonSerializer.Serialize(workProductManifests, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.pathToWorkProductManifest, jsonWorkProductManifests);
        }
        public void ExportLevelOfDetailsToJsonFile(List<LevelOfDetail> levelOfDetails)
        {

            var jsonLevelOfDetails = JsonSerializer.Serialize(levelOfDetails, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.pathToLevelOfDetails, jsonLevelOfDetails);
        }
        public void ExportStatesToJsonFile(List<State> states)
        {
            var jsonStates = JsonSerializer.Serialize(states, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.pathToStatesFile, jsonStates);
        }
        public void ExportCheckpointsToJsonFile(List<Checkpoint> checkpoints)
        {
            var jsonCheckpoints = JsonSerializer.Serialize(checkpoints, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.pathToCheckpointsFile, jsonCheckpoints);
        }
        public void ExportAlphaContainmentsToJsonFile(List<AlphaContaiment> alphaContaiments)
        {
            var jsonAlphaContainments = JsonSerializer.Serialize(alphaContaiments, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.pathToAlphaContainmentsFile, jsonAlphaContainments);
        }
        public void ExportDegreesOfEvidenceToJsonFile(List<DegreeOfEvidence> degreeOfEvidences)
        {
            var jsonDegreesOfEvidence = JsonSerializer.Serialize(degreeOfEvidences, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(jsonPaths.pathToDegreesOfEvidence, jsonDegreesOfEvidence);
        }
    }
}
