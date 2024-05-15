using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMS.model
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public double Premium { get; set; }

        public Policy() { }

        public Policy(int policyId, string policyName, double premium)
        {
            PolicyId = policyId;
            PolicyName = policyName;
            Premium = premium;
        }

        public override string ToString()
        {
            return $"PolicyId: {PolicyId}, PolicyName: {PolicyName}, Premium: {Premium}";
        }
    }
}
