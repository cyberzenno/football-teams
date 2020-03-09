using System.Collections.Generic;

namespace FootballTeams.Domain.AdditionalDetail
{
    public class AdditionalDetails
    {
        public List<NamedDetail> NamedDetails { get; set; }
        public List<NamedTimeStamp> NamedTimeStamps { get; set; }

        public AdditionalDetails()
        {
            NamedDetails = new List<NamedDetail>();
            NamedTimeStamps = new List<NamedTimeStamp>();
        }
    }
}