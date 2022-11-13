namespace WorkingWithDocumentation.Data
{
    public class GasSupplyForm
    {
        public string DiameterOfPipelineAndLengthOfObject1 { get; set; }
        public string DiameterOfPipelineAndLengthOfObject2 { get; set; }
        public string EstimatedCostOfWork { get; set; }
        public string TermForDevelopment { get; set; }

        public GasSupplyForm()
        {

        }
        public GasSupplyForm(string diameterOfPipelineAndLengthOfObject1,
            string diameterOfPipelineAndLengthOfObject2,
            string estimatedCostOfWork, string termForDevelopment)
        {
            DiameterOfPipelineAndLengthOfObject1 = diameterOfPipelineAndLengthOfObject1;
            DiameterOfPipelineAndLengthOfObject2 = diameterOfPipelineAndLengthOfObject2;
            EstimatedCostOfWork = estimatedCostOfWork;
            TermForDevelopment = termForDevelopment;
        }

        public GasSupplyForm Clone()
        {
            return new GasSupplyForm(DiameterOfPipelineAndLengthOfObject1, 
                DiameterOfPipelineAndLengthOfObject2, EstimatedCostOfWork, TermForDevelopment);
        }
    }
}
