﻿namespace WorkingWithDocumentation.Data
{
    public class WaterSupplyForm
    {
        public string DiameterOfCorpsePipelineAndLengthOfObject { get; set; }
        public string PerformanceBOS { get; set; }
        public string CountAndPerformanceKNS { get; set; }
        public string EstimatedCostOfWork { get; set; }
        public string TermForDevelopment { get; set; }
        public bool IsApproved { get; set; }

        public WaterSupplyForm()
        {

        }
        public WaterSupplyForm(string diameterOfCorpsePipelineAndLengthOfObject, string performanceBOS, 
            string countAndPerformanceKNS, string estimatedCostOfWork, string termForDevelopment, bool isApproved)
        {
            DiameterOfCorpsePipelineAndLengthOfObject = diameterOfCorpsePipelineAndLengthOfObject;
            PerformanceBOS = performanceBOS;
            CountAndPerformanceKNS = countAndPerformanceKNS;
            EstimatedCostOfWork = estimatedCostOfWork;
            TermForDevelopment = termForDevelopment;
            IsApproved = isApproved;
        }

        public WaterSupplyForm Clone()
        {
            return new WaterSupplyForm(DiameterOfCorpsePipelineAndLengthOfObject,
                PerformanceBOS, CountAndPerformanceKNS, EstimatedCostOfWork, TermForDevelopment, IsApproved);
        }
    }
}
