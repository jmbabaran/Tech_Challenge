using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatientAssistance.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace PatientAssistance.Common
{
    public class Utils
    {

        public int CalculateWaitingTime ( int peopleWaiting, int aveProcessTime)
        {
            return peopleWaiting * aveProcessTime;
        }

        public int GetCalculatedWaitingTime()
        {
            return 1;
        }

        public string ComputeTimeInHours(int minutes)
        {

            string time = "";

            if (minutes < 60)
            {
                time = minutes + " mins";
            }
            else
            {
                var hrs = minutes / 60;
                // TODO: calculate decimal hours
                time = hrs + " hrs";
            }

            


            return time;

        }

        public string GetAPIResponse( string url)
        {
            var request = (HttpWebRequest.Create(url));
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public JToken ParseObjectDetails(string response)
        {
            dynamic respObj = JsonConvert.DeserializeObject(response); //  Converted
            var jObj = (JObject)respObj;

            var embedded = jObj.First;
            var item = (JObject)embedded.First;
            var itempList = item.First;
            JToken itemDetails = itempList.First;

            return itemDetails;
        }


        public List<Hospital> GetHospitalList ()
        {
            List<Hospital> hospitalList = new List<Hospital>();

            var responseString = this.GetAPIResponse("http://dmmw-api.australiaeast.cloudapp.azure.com:8080/hospitals");

            var hospDetails = ParseObjectDetails(responseString);

            foreach (JToken token in hospDetails.Children())
            {
                Hospital hospitalTemp = JsonConvert.DeserializeObject<Hospital>(token.ToString());
                hospitalList.Add(hospitalTemp);

            }

            return hospitalList;

        }

        public List<Hospital> GetHospitalsWithTotalTime( int levelOfPain)
        {
            List<Hospital> hospitalList = new List<Hospital>();
            hospitalList = GetHospitalList();
            foreach (var item in hospitalList)
            {
                foreach (var waitlist in item.waitingList)
                {
                    if (waitlist.levelOfPain == levelOfPain)
                    {
                        //Compute Waiting Time according to levelOfPain
                        item.totalWaitingTime = waitlist.patientCount * waitlist.averageProcessTime;
                        item.totalWaitingTimeInHrs = this.ComputeTimeInHours(item.totalWaitingTime);
                        break;
                    }
                }
            }

            return hospitalList;
        }

        public List<Hospital> GetSortedHospital(int levelOfPain)
        {
            List<Hospital> hospitalList = new List<Hospital>();
            List<Hospital> sortedHospital = new List<Hospital>();
            hospitalList = GetHospitalsWithTotalTime(levelOfPain);
            sortedHospital.AddRange(hospitalList.OrderBy(x => x.totalWaitingTime));

            return sortedHospital;
        }

        public List<Illness> GetIllnessList()
        {
            List<Illness> illnessList = new List<Illness>();

            var responseString = this.GetAPIResponse("http://dmmw-api.australiaeast.cloudapp.azure.com:8080/illnesses");

            var illnessDetails = ParseObjectDetails(responseString);
            var illnessInnerDetails = illnessDetails.First;

            foreach (JToken token in illnessDetails.Children())
            {
                var ill = token.First;
                var illProp = ill.First;

                Illness illnessTemp = JsonConvert.DeserializeObject<Illness>(illProp.ToString());
                illnessList.Add(illnessTemp);

            }

            return illnessList;

        }

    }
}