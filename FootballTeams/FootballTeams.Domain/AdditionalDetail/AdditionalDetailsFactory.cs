namespace FootballTeams.Domain.AdditionalDetail
{
    public static class AdditionalDetailsFactory
    {
        public static AdditionalDetails CreateFootbalTeamsLocalCreationDetails()
        {
            var addtionalDetails = new AdditionalDetails();

            addtionalDetails.AddTimeStampNow("Created On");

            return addtionalDetails;
        }
        public static string CreateDefaultIfNoValue(string additionalDetailsJson)
        {
            var addtionalDetails = additionalDetailsJson.ToAdditionalDetailsDataModel();

            if (!addtionalDetails.IsThereAnyValue())
            {
                addtionalDetails = CreateFootbalTeamsLocalCreationDetails();
            }

            return addtionalDetails.ToJson();
        }

        public static bool IsThereAnyValue(string additionalDetailsJson)
        {
            var addtionalDetails = additionalDetailsJson.ToAdditionalDetailsDataModel();

            return addtionalDetails.IsThereAnyValue();
        }
    }
}
