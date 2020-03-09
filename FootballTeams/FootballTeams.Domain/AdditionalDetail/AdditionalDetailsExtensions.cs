using Newtonsoft.Json;
using System;
using System.Linq;

namespace FootballTeams.Domain.AdditionalDetail
{
    public static class AdditionalDetailsExtensions
    {
        /// <summary>
        /// Used to store properties as normal Strings or Named URLs if both values provided
        /// </summary>
        public static void AddDetail(this AdditionalDetails vm, string detailName, string detailValue, string detailUrl = "")
        {
            vm.NamedDetails.Add(new NamedDetail
            {
                Name = detailName,
                Value = detailValue,
                Url = detailUrl
            });
        }

        /// <summary>
        /// ImportMethod is used if the entity has been imported from and external source (i.e Html Page, Txt Doc, etc)
        /// </summary>
        public static void AddImportMethod(this AdditionalDetails vm, string value)
        {
            vm.NamedDetails.Add(new NamedDetail
            {
                Name = "ImportMethod",
                Value = value
            });
        }

        /// <summary>
        /// Mostly used for technical TimeStamps, such as Created On, Imported On, etc
        ///  <para>Named Value is updated if already exists</para>
        /// </summary>
        public static void AddTimeStampNow(this AdditionalDetails vm, string timeStampName)
        {
            var existingTimeStamp = vm.GetNamedTimeStamp(timeStampName);

            if (existingTimeStamp != null)
            {
                existingTimeStamp.Value = DateTime.Now;
            }
            else
            {
                vm.NamedTimeStamps.Add(new NamedTimeStamp
                {
                    Name = timeStampName,
                    Value = DateTime.Now
                });
            }
        }

        /// <summary>
        /// Mostly used for technical TimeStamps, such as Created On, Imported On, etc
        /// <para>Named Value is updated if already exists</para>
        /// </summary>
        public static string AddTimeStampNow(this string additionalDetailsJson, string timeStampName)
        {
            var vm = additionalDetailsJson.ToAdditionalDetailsDataModel();

            vm.AddTimeStampNow(timeStampName);

            return vm.ToJson();
        }

        public static NamedTimeStamp GetNamedTimeStamp(this AdditionalDetails vm, string timeStampName)
        {
            //note: syntax like this, 
            //doesn't look too much Clean Code
            //I leave just as an example
            return vm.NamedTimeStamps.FirstOrDefault(x => x.Name.Equals(timeStampName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns value only or empty string if doesn't exist
        /// </summary>
        public static string GetDetailValueOrDefault(this AdditionalDetails vm, string detailName, string defaultValue = "")
        {
            //note: syntax like this, 
            //doesn't look too much Clean Code
            //I leave just as an example
            return vm.NamedDetails.FirstOrDefault(x => x.Name.Equals(detailName, StringComparison.OrdinalIgnoreCase))?.Value ?? defaultValue;
        }

        public static NamedDetail GetDetail(this AdditionalDetails vm, string detailName, string defaultValue = "")
        {
            //note: syntax like this, 
            //doesn't look too much Clean Code
            //I leave just as an example
            return vm.NamedDetails.FirstOrDefault(x => x.Name.Equals(detailName, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsThereAnyValue(this AdditionalDetails vm)
        {
            return vm != null && (vm.NamedDetails.Any() || vm.NamedTimeStamps.Any());
        }

        /// <summary>
        /// If there is an ImportMethod, is Imported
        /// </summary>
        public static bool IsImported(this AdditionalDetails vm)
        {
            return vm != null && vm.NamedDetails.Any(x => x.Name == "ImportMethod");
        }

        /// <summary>
        /// If Is Imported, then Is Real
        /// </summary>
        public static bool IsReal(this AdditionalDetails vm)
        {
            return vm != null && vm.IsImported();
        }

        public static string ToJson(this AdditionalDetails vm)
        {
            return JsonConvert.SerializeObject(vm);
        }

        public static AdditionalDetails ToAdditionalDetailsDataModel(this string additionalDetailsJson)
        {
            var vm = JsonConvert.DeserializeObject<AdditionalDetails>(additionalDetailsJson ?? "");

            return vm ?? new AdditionalDetails();
        }


        //Linked Details Extensions
        public static bool IsLink(this NamedDetail vm)
        {
            return !string.IsNullOrEmpty(vm?.Url);
        }
    }
}
