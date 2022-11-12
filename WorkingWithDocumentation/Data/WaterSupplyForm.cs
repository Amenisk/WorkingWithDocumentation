namespace WorkingWithDocumentation.Data
{
    public class WaterSupplyForm
    {
        public string DiameterOfCorpsePipelineAndLengthOfObject { get; set; }
        public string PerformanceBOS { get; set; }
        public string CountAndPerformanceKNS { get; set; }
        public string EstimatedCostOfWork { get; set; }
        public string TermForDevelopment { get; set; }

        public WaterSupplyForm(string diameterOfCorpsePipelineAndLengthOfObject, string performanceBOS, 
            string countAndPerformanceKNS, string estimatedCostOfWork, string termForDevelopment)
        {
            DiameterOfCorpsePipelineAndLengthOfObject = diameterOfCorpsePipelineAndLengthOfObject;
            PerformanceBOS = performanceBOS;
            CountAndPerformanceKNS = countAndPerformanceKNS;
            EstimatedCostOfWork = estimatedCostOfWork;
            TermForDevelopment = termForDevelopment;
        }
    }
}
