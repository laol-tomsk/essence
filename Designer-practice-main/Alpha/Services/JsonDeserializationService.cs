using Alpha.Data;
using Alpha.Interfaces;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alpha.Services
{
    public class JsonDeserializationService
    {
        private JsonPaths jsonPaths = new JsonPaths();
        public List<Activity> DeserializeJsonActivities()
        {
            if (File.Exists(jsonPaths.PathToActivitiesFile))
            {
                string jsonString = File.ReadAllText(jsonPaths.PathToActivitiesFile);
                if (jsonString != null && jsonString != "")
                {
                    return JsonSerializer.Deserialize<List<Activity>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                else
                {
                    return new List<Activity>();
                }
            }
            else
            {
                using (File.Create(jsonPaths.PathToActivitiesFile)) { }
                return new List<Activity>();
            }
        }
        public List<Alpha> DeserializeJsonAlphas()
        {
            if (File.Exists(jsonPaths.PathToAlphasFile))
            {
                List<Alpha> alphas = new List<Alpha>();
                string jsonString = File.ReadAllText(jsonPaths.PathToAlphasFile);
                if (jsonString != null && jsonString != "")
                {
                    alphas = JsonSerializer.Deserialize<List<Alpha>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    foreach (Alpha alpha in alphas)
                    {
                        Guid? alphaParentId = alpha.GetAlphaParentId();
                        if (alphaParentId != null)
                        {
                            Alpha alphaParent = alphas.FirstOrDefault(a => a.Id == alphaParentId);
                            if (alphaParent == null)
                                alpha.DeleteAlphaParent();
                            else
                                alpha.SetParentAlpha(alphaParent);
                        }
                    }
                    return alphas;
                }
                else
                {
                    return new List<Alpha>();
                }
            }
            else
            {
                using (File.Create(jsonPaths.PathToAlphasFile)) { }
                return new List<Alpha>();
            }
        }
        public List<WorkProduct> DeserializeJsonWorkProducts()
        {
            if (File.Exists(jsonPaths.PathToWorkProductsFile))
            {
                string jsonString = File.ReadAllText(jsonPaths.PathToWorkProductsFile);
                if (jsonString != null && jsonString != "")
                {
                    return JsonSerializer.Deserialize<List<WorkProduct>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                else
                {
                    return new List<WorkProduct>();
                }
            }
            else
            {
                using (File.Create(jsonPaths.PathToWorkProductsFile)) { }
                return new List<WorkProduct>();
            }
        }
        public List<WorkProductCriterion> DeserializeJsonWorkProductCriterions(List<Activity> activities, List<LevelOfDetail> levelOfDetails)
        {

            if (File.Exists(jsonPaths.PathToWorkProductCriterionsFile))
            {
                List<WorkProductCriterion> workProductCriterions = new List<WorkProductCriterion>();
                string jsonString = File.ReadAllText(jsonPaths.PathToWorkProductCriterionsFile);
                if (jsonString != null && jsonString != "")
                {
                    workProductCriterions = JsonSerializer.Deserialize<List<WorkProductCriterion>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var workProductCriterion in workProductCriterions)
                {
                    Activity activity = activities.FirstOrDefault(a => a.Id == workProductCriterion.GetActivityId());
                    LevelOfDetail levelOfDetail = levelOfDetails.FirstOrDefault(l => l.GetId() == workProductCriterion.GetLevelOfDetailId());
                    if (activity != null && levelOfDetail != null)
                    {
                        workProductCriterion.SetActivity(activity);
                        workProductCriterion.SetLevelOfDetail(levelOfDetail);
                        continue;
                    }
                }
                return workProductCriterions;
            }
            else
            {
                using (File.Create(jsonPaths.PathToWorkProductCriterionsFile)) { }
                return new List<WorkProductCriterion>();
            }
        }
        public List<AlphaCriterion> DeserializeJsonAlphaCriterions(List<Activity> activities, List<State> states)
        {

            if (File.Exists(jsonPaths.PathToAlphaCriterionsFile))
            {
                List<AlphaCriterion> alphaCriterions = new List<AlphaCriterion>();
                string jsonString = File.ReadAllText(jsonPaths.PathToAlphaCriterionsFile);
                if (jsonString != null && jsonString != "")
                {
                    alphaCriterions = JsonSerializer.Deserialize<List<AlphaCriterion>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var alphaCriterion in alphaCriterions)
                {
                    Activity activity = activities.FirstOrDefault(a => a.Id == alphaCriterion.GetActivityId());
                    State state = states.FirstOrDefault(s => s.GetId() == alphaCriterion.GetStateId());
                    if (activity != null && state != null)
                    {
                        alphaCriterion.SetActivity(activity);
                        alphaCriterion.SetState(state);
                        continue;
                    }
                }
                return alphaCriterions;
            }
            else
            {
                using (File.Create(jsonPaths.PathToAlphaCriterionsFile)) { }
                return new List<AlphaCriterion>();
            }
        }

        public List<WorkProductManifest> DeserializeJsonWorkProductManifests(List<Alpha> alphas, List<WorkProduct> workProducts)
        {
            List<WorkProductManifest> workProductManifests = new List<WorkProductManifest>();
            if (File.Exists(jsonPaths.pathToWorkProductManifest))
            {
                string jsonString = File.ReadAllText(jsonPaths.pathToWorkProductManifest);
                if (jsonString != null && jsonString != "")
                {
                    workProductManifests = JsonSerializer.Deserialize<List<WorkProductManifest>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var workProductManifest in workProductManifests)
                {
                    Alpha alpha = alphas.FirstOrDefault(a => a.Id == workProductManifest.GetAlphaId());
                    WorkProduct workProduct = workProducts.FirstOrDefault(a => a.Id == workProductManifest.GetWorkProductId());
                    if (workProduct != null && alpha != null)
                    {
                        workProductManifest.SetWorkProduct(workProduct);
                        workProductManifest.SetAlpha(alpha);
                    }
                    if (alpha == null && alphas.Count() == 0)
                    {
                        workProductManifest.SetWorkProduct(workProduct);
                    }
                }
                return workProductManifests;
            }
            else
            {
                using (File.Create(jsonPaths.pathToWorkProductManifest)) { }
                return new List<WorkProductManifest>();
            }
        }
        public List<LevelOfDetail> DeserializeJsonLevelOfDetails(List<WorkProduct> workProducts)
        {
            if (File.Exists(jsonPaths.pathToLevelOfDetails))
            {
                string jsonString = File.ReadAllText(jsonPaths.pathToLevelOfDetails);
                List<LevelOfDetail> levelOfDetails = new List<LevelOfDetail>();
                if (jsonString != null && jsonString != "")
                {
                    levelOfDetails = JsonSerializer.Deserialize<List<LevelOfDetail>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var levelOfDetail in levelOfDetails)
                {
                    WorkProduct workProduct = workProducts.FirstOrDefault(wp => wp.GetWorkProductId() == levelOfDetail.GetWorkProductId());
                    if (workProduct != null)
                    {
                        levelOfDetail.SetWorkProduct(workProduct);
                        workProduct.AddLevelOfDetailToList(levelOfDetail);
                    }
                }
                SortWorkProductsLevelOfStatesByOrder(workProducts);
                return levelOfDetails;
            }
            else
            {
                using (File.Create(jsonPaths.pathToLevelOfDetails)) { }
                return new List<LevelOfDetail>();
            }
        }

        public List<AlphaContaiment> DeserializeJsonAlphaContainments(List<Alpha> alphas)
        {
            if (File.Exists(jsonPaths.pathToAlphaContainmentsFile))
            {
                List<AlphaContaiment> alphaContaiments = new List<AlphaContaiment>();
                string jsonString = File.ReadAllText(jsonPaths.pathToAlphaContainmentsFile);
                if (jsonString != null && jsonString != "")
                {
                    alphaContaiments = JsonSerializer.Deserialize<List<AlphaContaiment>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var alphaContaiment in alphaContaiments)
                {
                    Alpha supAlpha = alphas.FirstOrDefault(a => a.Id == alphaContaiment.GetSupAlphaId());
                    Alpha subAlpha = alphas.FirstOrDefault(a => a.Id == alphaContaiment.GetSubAlphaId());
                    if (supAlpha != null && subAlpha != null)
                    {
                        alphaContaiment.SetSupAlpha(supAlpha);
                        alphaContaiment.SetSubAlpha(subAlpha);
                    }
                }
                return alphaContaiments;
            }
            else
            {
                using (File.Create(jsonPaths.pathToAlphaContainmentsFile)) { }
                return new List<AlphaContaiment>();
            }
        }

        public List<State> DeserializeJsonStates(List<Alpha> alphas)
        {
            if (File.Exists(jsonPaths.pathToStatesFile))
            {
                string jsonString = File.ReadAllText(jsonPaths.pathToStatesFile);
                List<State> states = new List<State>();
                List<IDetailing> details = new List<IDetailing>();
                if (jsonString != null && jsonString != "")
                {
                    states = JsonSerializer.Deserialize<List<State>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var state in states)
                {
                    Alpha alpha = alphas.FirstOrDefault(a => a.Id == state.AlphaId);
                    if (alpha != null)
                    {
                        state.SetAlpha(alpha);
                        alpha.AddState(state);
                    }
                }
                SortAlphasStatesByOrder(alphas);
                return states;
            }
            else
            {
                using (File.Create(jsonPaths.pathToStatesFile)) { }
                return new List<State>();
            }
        }
        public List<Checkpoint> DeserializeJsonCheckpoints(List<IDetailing> details)
        {
            if (File.Exists(jsonPaths.pathToCheckpointsFile))
            {
                string jsonString = File.ReadAllText(jsonPaths.pathToCheckpointsFile);
                List<Checkpoint> checkpoints = new List<Checkpoint>();
                if (jsonString != null && jsonString != "")
                {
                    checkpoints = JsonSerializer.Deserialize<List<Checkpoint>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var checkpoint in checkpoints)
                {
                    IDetailing detail = details.FirstOrDefault(s => s.GetId() == checkpoint.GetDetailId());
                    if (detail != null)
                    {
                        detail.AddCheckpoint(checkpoint);
                    }
                }
                SortDetailsCheckpointsByOrder(details);
                return checkpoints;
            }
            else
            {
                using (File.Create(jsonPaths.pathToCheckpointsFile)) { }
                return new List<Checkpoint>();
            }
        }
        public List<DegreeOfEvidence> DeserializeJsonDegreesOfEvidence(List<Checkpoint> checkpoints, List<ICheckable> checkables)
        {
            if (File.Exists(jsonPaths.pathToDegreesOfEvidence))
            {
                string jsonString = File.ReadAllText(jsonPaths.pathToDegreesOfEvidence);
                List<DegreeOfEvidence> degreesOfEvidence = new List<DegreeOfEvidence>();
                if (jsonString != null && jsonString != "")
                {
                    degreesOfEvidence = JsonSerializer.Deserialize<List<DegreeOfEvidence>>(jsonString, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
                foreach (var degreeOfEvidence in degreesOfEvidence)
                {
                    ICheckable checkable = checkables.FirstOrDefault(c => c.GetId() == degreeOfEvidence.GetICheckableId());
                    degreeOfEvidence.SetICheckable(checkable);
                    Checkpoint checkpoint = checkpoints.FirstOrDefault(c => c.GetId() == degreeOfEvidence.GetCheckpointId());
                    if (checkpoint != null)
                        checkpoint.AddDegreeOfEvidence(degreeOfEvidence);
                }
                return degreesOfEvidence;
            }
            else
            {
                using (File.Create(jsonPaths.pathToDegreesOfEvidence)) { }
                return new List<DegreeOfEvidence>();
            }
        }
        private List<Checkpoint> GetAllCheckpointsOfStates(List<IDetailing> details)
        {
            List<Checkpoint> checkpointsTemp = new List<Checkpoint>();
            foreach (var detail in details)
            {
                checkpointsTemp.AddRange(detail.GetCheckpoints());
            }
            return checkpointsTemp;
        }
        private void SortWorkProductsLevelOfStatesByOrder(List<WorkProduct> workProducts)
        {
            foreach (var workProduct in workProducts)
            {
                workProduct.SortListOfLevelOfDetailsByOrder();
            }
        }

        private void SortDetailsCheckpointsByOrder(List<IDetailing> details)
        {
            foreach (var detail in details)
            {
                detail.SortListOfCheckpointsByOrder();
            }
        }
        private void SortAlphasStatesByOrder(List<Alpha> alphas)
        {
            foreach (var alpha in alphas)
            {
                alpha.SortListOfStatesByOrder();
            }
        }
    }
}
