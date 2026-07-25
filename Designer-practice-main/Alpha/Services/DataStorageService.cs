using Alpha.Interfaces;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Services
{
    public class DataStorageService
    {
        private DataStorageService() { }
        private static DataStorageService instance = null;
        private JsonDeserializationService jsonDeserializationService = new JsonDeserializationService();
        private JsonSerializationToFileService jsonSerializationToFileService = new JsonSerializationToFileService();
        private List<Alpha> Alphas { get; set; } = new List<Alpha>();
        private List<AlphaContaiment> AlphaContaiments { get; set; } = new List<AlphaContaiment>();
        private List<State> States { get; set; } = new List<State>();
        private List<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
        private List<WorkProduct> WorkProducts { get; set; } = new List<WorkProduct>();
        private List<LevelOfDetail> LevelOfDetails { get; set; } = new List<LevelOfDetail>();
        private List<Activity> Activities { get; set; } = new List<Activity>();
        private List<WorkProductCriterion> WorkProductCriterions { get; set; } = new List<WorkProductCriterion>();
        private List<AlphaCriterion> AlphaCriterions { get; set; } = new List<AlphaCriterion>();
        private List<WorkProductManifest> WorkProductManifests { get; set; } = new List<WorkProductManifest>();
        private List<IDetailing> Details { get; set; } = new List<IDetailing>();
        private List<ICheckable> Checkables { get; set; } = new List<ICheckable>();
        private List<ICriterion> Criterions { get; set; } = new List<ICriterion>();
        private List<DegreeOfEvidence> DegreesOfEvidence { get; set; } = new List<DegreeOfEvidence>();

        public List<Alpha> GetAlphas() => Alphas;
        public List<Activity> GetActivities() => Activities;
        public List<WorkProductCriterion> GetWorkProductCriterions() => WorkProductCriterions;
        public List<AlphaCriterion> GetAlphaCriterions() => AlphaCriterions;
        public List<WorkProduct> GetWorkProducts() => WorkProducts;
        public List<WorkProductManifest> GetWorkProductManifests() => WorkProductManifests;
        public List<AlphaContaiment> GetAlphaContaiments() => AlphaContaiments;
        public List<DegreeOfEvidence> GetDegreesOfEvidence() => DegreesOfEvidence;
        public List<ICheckable> GetICheckables()
        {
            UpdateICheckablesList();
            return Checkables;
        }
        public List<IDetailing> GetDetails()
        {
            UpdateIDetailsList();
            return Details;
        }
        public List<ICriterion> GetCriterions()
        {
            UpdateICriterionList();
            return Criterions;
        }
        public static DataStorageService GetInstance()
        {
            if (instance == null)
            {
                instance = new DataStorageService();
                instance.DeserializeAllJsons();
            }
            return instance;
        }
        private void DeserializeAllJsons()
        {
            Alphas = jsonDeserializationService.DeserializeJsonAlphas();
            AlphaContaiments = jsonDeserializationService.DeserializeJsonAlphaContainments(Alphas);
            States = jsonDeserializationService.DeserializeJsonStates(Alphas);
            WorkProducts = jsonDeserializationService.DeserializeJsonWorkProducts();
            LevelOfDetails = jsonDeserializationService.DeserializeJsonLevelOfDetails(WorkProducts);
            Activities = jsonDeserializationService.DeserializeJsonActivities();
            WorkProductManifests = jsonDeserializationService.DeserializeJsonWorkProductManifests(Alphas, WorkProducts);
            WorkProductCriterions = jsonDeserializationService.DeserializeJsonWorkProductCriterions(Activities, LevelOfDetails);
            AlphaCriterions = jsonDeserializationService.DeserializeJsonAlphaCriterions(Activities, States);
            UpdateIDetailsList();
            Checkpoints = jsonDeserializationService.DeserializeJsonCheckpoints(Details);
            UpdateICheckablesList();
            DegreesOfEvidence = jsonDeserializationService.DeserializeJsonDegreesOfEvidence(Checkpoints, Checkables);
        }
        private void UpdateIDetailsList()
        {
            Details.Clear();
            foreach (var level in LevelOfDetails)
            {
                Details.Add(level);
            }
            foreach (var state in States)
            {
                Details.Add(state);
            }
        }
        private void UpdateICheckablesList()
        {
            Checkables.Clear();
            foreach (var level in LevelOfDetails)
            {
                Checkables.Add(level);
            }
            foreach (var state in States)
            {
                Checkables.Add(state);
            }
            foreach (var checkpoint in Checkpoints)
            {
                Checkables.Add(checkpoint);
            }
        }
        private void UpdateICriterionList()
        {
            Criterions.Clear();
            foreach (var criterion in WorkProductCriterions)
            {
                Criterions.Add(criterion);
            }
            foreach (var criterion in AlphaCriterions)
            {
                Criterions.Add(criterion);
            }
        }
        public void AddAlpha(Alpha alpha)
        {
            Alphas.Add(alpha);
            jsonSerializationToFileService.ExportAlphasToFile(Alphas);
        }
        public void UpdateAlphas()
        {
            jsonSerializationToFileService.ExportAlphasToFile(Alphas);
        }
        public void RemoveAlpha(Alpha alpha)
        {
            RemoveStatesByAlpha(alpha);
            Alphas.Remove(alpha);
            RemoveWorkProductManifestByAlpha(alpha);
            jsonSerializationToFileService.ExportAlphasToFile(Alphas);
            jsonSerializationToFileService.ExportStatesToJsonFile(States);
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
            jsonSerializationToFileService.ExportWorkProductManifestsToJsonFile(WorkProductManifests);
        }
        private void RemoveWorkProductManifestByAlpha(Alpha alpha)
        {
            WorkProductManifests.Remove(alpha.GetWorkProductManifest());
            alpha.DeleteWorkProductManifest();
        }
        private void RemoveStatesByAlpha(Alpha alpha)
        {
            List<State> statesForRemove = alpha.GetStates();
            foreach (var state in statesForRemove)
            {
                RemoveCheckpointsAndCriteriaByDetail(state);
                RemoveDegreesOfEvidenceByICheckable(state);
                States.Remove(state);
                Details.Remove(state);
                Checkables.Remove(state);
            }
        }

        public void AddWorkProduct(WorkProduct workProduct)
        {
            WorkProducts.Add(workProduct);
            jsonSerializationToFileService.ExportWorkProductsToJsonFile(WorkProducts);
        }
        public void RemoveWorkProduct(WorkProduct workProduct)
        {
            List<LevelOfDetail> lodForRemove = workProduct.GetLevelOfDetails();
            foreach (var levelOfDetail in lodForRemove)
            {
                RemoveCheckpointsAndCriteriaByDetail(levelOfDetail);
                RemoveDegreesOfEvidenceByICheckable(levelOfDetail);
                RemoveWorkProductManifestByWorkProduct(workProduct);
                LevelOfDetails.Remove(levelOfDetail);
                Details.Remove(levelOfDetail);
                Checkables.Remove(levelOfDetail);
            }
            WorkProducts.Remove(workProduct);
            jsonSerializationToFileService.ExportWorkProductsToJsonFile(WorkProducts);
            jsonSerializationToFileService.ExportLevelOfDetailsToJsonFile(LevelOfDetails);
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
            jsonSerializationToFileService.ExportWorkProductManifestsToJsonFile(WorkProductManifests);
        }
        private void RemoveWorkProductManifestByWorkProduct(WorkProduct workProduct)
        {
            foreach (var workProductManifest in workProduct.GetWorkProductManifests())
            {
                Alpha alpha = Alphas.FirstOrDefault(a=>a.GetAlphaId() == workProductManifest.GetAlphaId());
                alpha.DeleteWorkProductManifest();
                workProduct.RemoveWorkProductManifest(workProductManifest);
                WorkProductManifests.Remove(workProductManifest);
            }
        }
        public void UpdateWorkProducts()
        {
            jsonSerializationToFileService.ExportWorkProductsToJsonFile(WorkProducts);
        }
        public void AddAlphaContaiment(AlphaContaiment alphaContaiment)
        {
            AlphaContaiments.Add(alphaContaiment);
            jsonSerializationToFileService.ExportAlphaContainmentsToJsonFile(AlphaContaiments);
        }
        public void RemoveAlphaContaiment(AlphaContaiment alphaContaiment)
        {
            AlphaContaiments.Remove(alphaContaiment);
            jsonSerializationToFileService.ExportAlphaContainmentsToJsonFile(AlphaContaiments);
        }
        public void UpdateAlphaContaiments()
        {
            jsonSerializationToFileService.ExportAlphaContainmentsToJsonFile(AlphaContaiments);
        }
        public void AddAlphaCriterion(AlphaCriterion alphaCriterion)
        {
            AlphaCriterions.Add(alphaCriterion);
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
        }
        public void RemoveAlphaCriterion(AlphaCriterion alphaCriterion)
        {
            AlphaCriterions.Remove(alphaCriterion);
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
        }
        public void UpdateAlphaCriterions()
        {
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
        }
        public void AddWorkProductManifest(WorkProductManifest workProductManifest)
        {
            WorkProductManifests.Add(workProductManifest);
            jsonSerializationToFileService.ExportWorkProductManifestsToJsonFile(WorkProductManifests);
        }
        public void RemoveWorkProductManifest(WorkProductManifest workProductManifest)
        {
            WorkProductManifests.Remove(workProductManifest);
            jsonSerializationToFileService.ExportWorkProductManifestsToJsonFile(WorkProductManifests);
        }
        public void UpdateWorkProductManifests()
        {
            jsonSerializationToFileService.ExportWorkProductManifestsToJsonFile(WorkProductManifests);
        }
        public void AddState(State state)
        {

            States.Add(state);
            Details.Add(state);
            Checkables.Add(state);
            jsonSerializationToFileService.ExportStatesToJsonFile(States);
        }
        public void RemoveState(State state)
        {
            RemoveCheckpointsAndCriteriaByDetail(state);
            RemoveDegreesOfEvidenceByICheckable(state);
            States.Remove(state);
            Details.Remove(state);
            Checkables.Remove(state);            
            jsonSerializationToFileService.ExportStatesToJsonFile(States);
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
        }
        public void UpdateStates()
        {
            jsonSerializationToFileService.ExportStatesToJsonFile(States);
        }
        public void AddCheckpoint(Checkpoint checkpoint)
        {
            Checkpoints.Add(checkpoint);
            Checkables.Add(checkpoint);
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
        }
        public void RemoveCheckpoint(Checkpoint checkpoint)
        {
            RemoveDegreesOfEvidenceByCheckpoint(checkpoint);
            Checkpoints.Remove(checkpoint);
            Checkables.Remove(checkpoint);
            RemoveDegreesOfEvidenceByICheckable(checkpoint);
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
        }
        public void UpdateCheckpoints()
        {
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
        }

        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
            jsonSerializationToFileService.ExportActivitiesToJsonFile(Activities);
        }
        public void UpdateActivities()
        {
            jsonSerializationToFileService.ExportActivitiesToJsonFile(Activities);
        }
        public void RemoveActivity(Activity activity)
        {
            Activities.Remove(activity);
            jsonSerializationToFileService.ExportActivitiesToJsonFile(Activities);
        }

        public void AddDegreeOfEvidence(DegreeOfEvidence degreeOfEvidence)
        {
            DegreesOfEvidence.Add(degreeOfEvidence);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
        }
        public void UpdateDegreesOfEvidence()
        {
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
        }
        public void RemoveDegreeOfEvidence(DegreeOfEvidence degreeOfEvidence)
        {
            DegreesOfEvidence.Remove(degreeOfEvidence);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
        }

        public void AddWorkProductCriterion(WorkProductCriterion workProductCriterion)
        {
            WorkProductCriterions.Add(workProductCriterion);
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
        }
        public void RemoveWorkProductCriterion(WorkProductCriterion workProductCriterion)
        {
            WorkProductCriterions.Remove(workProductCriterion);
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
        }
        public void UpdateWorkProductCriterions()
        {
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
        }
        public void AddLevelOfDetail(LevelOfDetail levelOfDetail)
        {
            LevelOfDetails.Add(levelOfDetail);
            Checkables.Add(levelOfDetail);
            Details.Add(levelOfDetail);
            jsonSerializationToFileService.ExportLevelOfDetailsToJsonFile(LevelOfDetails);
        }
        public void RemoveLevelOfDetail(LevelOfDetail levelOfDetail)
        {
            RemoveCheckpointsAndCriteriaByDetail(levelOfDetail);
            LevelOfDetails.Remove(levelOfDetail);
            Checkables.Remove(levelOfDetail);
            Details.Remove(levelOfDetail);
            RemoveDegreesOfEvidenceByICheckable(levelOfDetail);
            jsonSerializationToFileService.ExportLevelOfDetailsToJsonFile(LevelOfDetails);
            jsonSerializationToFileService.ExportCheckpointsToJsonFile(Checkpoints);
            jsonSerializationToFileService.ExportDegreesOfEvidenceToJsonFile(DegreesOfEvidence);
            jsonSerializationToFileService.ExportAlphaCriterionsToFile(AlphaCriterions);
            jsonSerializationToFileService.ExportWorkProductCriterionsToFile(WorkProductCriterions);
        }
        public void UpdateLevelOfDetails()
        {
            jsonSerializationToFileService.ExportLevelOfDetailsToJsonFile(LevelOfDetails);
        }
        private void RemoveCheckpointsAndCriteriaByDetail(IDetailing detail)
        {
            List<Checkpoint> checkpointsForRemove = detail.GetCheckpoints();
            List<ICriterion> criteria = GetCriterions().Where(c => c.GetDetailId() == detail.GetId()).ToList();
            foreach (var criterion in criteria)
            {
                if (criterion.GetType() == typeof(AlphaCriterion))
                {
                    AlphaCriterions.Remove((AlphaCriterion)criterion);
                }
                else if (criterion.GetType() == typeof(WorkProductCriterion))
                {
                    WorkProductCriterions.Remove((WorkProductCriterion)criterion);
                }
            }
            foreach (var checkpoint in checkpointsForRemove)
            {
                RemoveDegreesOfEvidenceByCheckpoint(checkpoint);
                Checkables.Remove(checkpoint);
                Checkpoints.Remove(checkpoint);
            }
        }
        private void RemoveDegreesOfEvidenceByCheckpoint(Checkpoint checkpoint)
        {
            List<DegreeOfEvidence> degreesOfEvidence = checkpoint.GetDegreeOfEvidences();
            RemoveDegreesOfEvidenceByICheckable(checkpoint);
            foreach (var degreeOfEvidence in degreesOfEvidence)
            {
                DegreesOfEvidence.Remove(degreeOfEvidence);
            }
        }
        private void RemoveDegreesOfEvidenceByICheckable(ICheckable checkable)
        {
            var degreesOfEvidences = DegreesOfEvidence.Where(e => e.ICheckableId == checkable.GetId()).ToList();
            foreach (var degree in degreesOfEvidences)
            {
                DegreesOfEvidence.Remove(degree);
            }
        }
    }
}
