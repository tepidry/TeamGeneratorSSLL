using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;
using TeamGenerator;


List<Tuple<string, List<Participant>, List<VolunteerData>>> GetParticipantsByDivision(string currentDirectory, List<VolunteerData> allVolunteers)
{
    var participantsByDivision = new List<Tuple<string, List<Participant>, List<VolunteerData>>>();
    foreach (string file in Directory.EnumerateFiles(currentDirectory, "*.csv").Where(x => x.Contains("Participants")))
    {
        var participants = new List<Participant>();
        using (TextFieldParser parser = new TextFieldParser(file))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            int rowIndex = 0;
            string[]? headers = null;
            while (!parser.EndOfData)
            {
                // process header row
                if (rowIndex == 0)
                {
                    headers = parser.ReadFields().ToArray();
                }
                else
                {
                    //Processing participant row
                    var participantFields = parser.ReadFields();

                    var dictionary = new Dictionary<string, string>();
                    for (int i = 0; i < participantFields.Length; i++)
                    {
                        if (!dictionary.ContainsKey(headers[i]))
                        {
                            dictionary.Add(headers[i], participantFields[i]);
                        }
                        else
                        {
                            dictionary.Add(Guid.NewGuid().ToString(), participantFields[i]);
                        }
                    }

                    var participant = new Participant();
                    foreach (var item in dictionary)
                    {
                        var propertyInfos = participant.GetType().GetProperties();

                        var propertyInfo = propertyInfos.SingleOrDefault(p =>
                        {
                            var argName = Attribute.IsDefined(p, typeof(DescriptionAttribute))
                                ? (Attribute.GetCustomAttribute(element: p, typeof(DescriptionAttribute)) as
                                    DescriptionAttribute)?.Description
                                : p.Name;
                            return argName.Equals(item.Key);
                        });

                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            propertyInfo.SetValue(participant, item.Value, null);
                        }
                    }


                    participants.Add(participant);
                }

                rowIndex++;
            }
        }

        var divisionName = participants.FirstOrDefault().DivisionName;
        participantsByDivision.Add(new Tuple<string, List<Participant>, List<VolunteerData>>(divisionName, participants, allVolunteers.Where(x => x.DivisionName == divisionName).ToList()));
    }

    return participantsByDivision;
}

List<VolunteerData> GetVolunteers(string s)
{
    var volunteers = new List<VolunteerData>();

    using (TextFieldParser parser = new TextFieldParser(s + @"\Volunteer_Details.csv"))
    {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        int rowIndex = 0;
        string[]? headers = null;
        while (!parser.EndOfData)
        {
            // process header row
            if (rowIndex == 0)
            {
                headers = parser.ReadFields().ToArray();
            }
            else
            {
                //Processing participant row
                var volunteerFields = parser.ReadFields();

                var dictionary = new Dictionary<string, string>();
                for (int i = 0; i < volunteerFields.Length; i++)
                {
                    if (!dictionary.ContainsKey(headers[i]))
                    {
                        dictionary.Add(headers[i], volunteerFields[i]);
                    }
                    else
                    {
                        dictionary.Add(Guid.NewGuid().ToString(), volunteerFields[i]);
                    }
                }

                var volunteer = new VolunteerData();
                foreach (var item in dictionary)
                {
                    var propertyInfos = volunteer.GetType().GetProperties();

                    var propertyInfo = propertyInfos.SingleOrDefault(p =>
                    {
                        var argName = Attribute.IsDefined(p, typeof(DescriptionAttribute))
                            ? (Attribute.GetCustomAttribute(element: p, typeof(DescriptionAttribute)) as
                                DescriptionAttribute)?.Description
                            : p.Name;
                        return argName.Equals(item.Key);
                    });

                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(volunteer, item.Value, null);
                    }
                }


                volunteers.Add(volunteer);
            }

            rowIndex++;
        }
    }

    return volunteers;
}

var currentDirectory = System.IO.Directory.GetCurrentDirectory();

var services = new TeamServices();

var volunteers = GetVolunteers(currentDirectory);

var participantsByDivision = GetParticipantsByDivision(currentDirectory ,volunteers);
await services.CreateExcelFile(currentDirectory, participantsByDivision);

