using System.Linq;
using OfficeOpenXml;

namespace TeamGenerator
{
    internal class TeamServices
    {
        public async Task CreateExcelFile(string saveAsLocation, List<Tuple<string, List<Participant>, List<VolunteerData>>> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            foreach (var tuple in data)
            {
                var divisionName = tuple.Item1;
                var file = new FileInfo(saveAsLocation + $"\\{DateTime.Now.ToString("MM_dd_HH_mmss") + "_" + divisionName}.xlsx");

                DeleteIfExists(file);
                using var package = new ExcelPackage(file);
                await SaveExcelFile(package, tuple.Item2, tuple.Item3);
            }
            
        }

        private async Task SaveExcelFile(ExcelPackage package, List<Participant> participants, List<VolunteerData> volunteers)
        {
            await SaveAllPartcipants(package, participants);

            await SaveAllVolunteers(package, volunteers);

            var teams = await AttemptCreateAndSaveTeamsFromCoaches(package, participants, volunteers);
            List<Participant> unassigned = participants.Where(x => teams.All(y => !y.Players.Contains(x))).ToList();
            
            await SaveUnAssigned(package, unassigned);
        }

        private async Task SaveUnAssigned(ExcelPackage package, List<Participant> unassignedParticipants)
        {
            var coachUnassigned = unassignedParticipants.Where(x => !string.IsNullOrWhiteSpace(x.CoachRequest));
            var wsCoachUnassigned = package.Workbook.Worksheets.Add("wCoach Unassigned");
            var rangeCoachUnassigned = wsCoachUnassigned.Cells["A1"].LoadFromCollection(coachUnassigned, true);
            rangeCoachUnassigned.AutoFitColumns();
            
            var kidUnassigned = unassignedParticipants.Where(x => !coachUnassigned.Contains(x) && !string.IsNullOrWhiteSpace(x.TeammateRequest));
            var wsKidUnassigned = package.Workbook.Worksheets.Add("wKid Unassigned");

            var rangeKidUnassigned = wsKidUnassigned.Cells["A1"].LoadFromCollection(kidUnassigned, true);
            rangeKidUnassigned.AutoFitColumns();

            
            var unassigned = unassignedParticipants.Where(x => !coachUnassigned.Contains(x) && !kidUnassigned.Contains(x));
            var wsUnassigned = package.Workbook.Worksheets.Add("Unassigned");

            var rangeUnassigned = wsUnassigned.Cells["A1"].LoadFromCollection(unassigned, true);
            rangeUnassigned.AutoFitColumns();

            await package.SaveAsync();
        }

        private async Task<List<Team>> AttemptCreateAndSaveTeamsFromCoaches(ExcelPackage package, List<Participant> participants, List<VolunteerData> volunteers)
        {
            List<Team> teams = new List<Team>();
            foreach (var volunteer in volunteers.Where(x => x.VolunteerRole.Equals("Head Coach")))
            {
                List<Participant> participantsAssigned = teams.SelectMany(x => x.Players).ToList();
                List<Participant> playersRequestingCoach = participants
                    .Where(x => !participantsAssigned.Contains(x) 
                                && x.CoachRequest.LastNameNormalize().Contains(volunteer.VolunteerLastName.LastNameNormalize(), StringComparison.InvariantCultureIgnoreCase)
                                || x.PlayerLastName.Equals(volunteer.VolunteerLastName)).ToList();

                List<Participant> playersRequestingPlayers = participants
                    .Where(x => !participantsAssigned.Contains(x) 
                                && !playersRequestingCoach.Contains(x) 
                                && playersRequestingCoach.Any(y => x.TeammateRequest.LastNameNormalize().Contains(y.PlayerLastName.LastNameNormalize(), StringComparison.InvariantCultureIgnoreCase))
                                ).ToList();
                List<Participant> players = playersRequestingCoach.Concat(playersRequestingPlayers).ToList();
                participantsAssigned.AddRange(players);

                // save team and create ws
                Team team = new Team
                {
                    TeamName = $"Team {volunteer.VolunteerLastName}",
                    Coach = volunteer,
                    Players = players
                };
                teams.Add(team);

                var coachWorkSheet = package.Workbook.Worksheets.Add(team.TeamName);

                var rangeVolunteers = coachWorkSheet.Cells["A1"].LoadFromCollection(players, true);
                rangeVolunteers.AutoFitColumns();
            }

            await package.SaveAsync();

            return new List<Team>(teams);
        }

        private static async Task SaveAllVolunteers(ExcelPackage package, List<VolunteerData> volunteers)
        {
            // volunteers
            var wsVolunteers = package.Workbook.Worksheets.Add("Volunteers");

            var rangeVolunteers = wsVolunteers.Cells["A1"].LoadFromCollection(volunteers, true);
            rangeVolunteers.AutoFitColumns();

            await package.SaveAsync();
        }

        private static async Task SaveAllPartcipants(ExcelPackage package, List<Participant> participants)
        {
            // participants
            var wsParticipants = package.Workbook.Worksheets.Add("Participants");

            var rangeParticipants = wsParticipants.Cells["A1"].LoadFromCollection(participants, true);
            rangeParticipants.AutoFitColumns();

            await package.SaveAsync();
        }

        private void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
                file.Delete();
        }
    }
}
