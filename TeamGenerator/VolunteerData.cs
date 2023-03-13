using System.ComponentModel;

namespace TeamGenerator;

public class VolunteerData
{
    [Description("Program Name")]
    public string ProgramName { get; set; }

    [Description("Division Name")]
    public string DivisionName { get; set; }

    [Description("Team Name")]
    public string TeamName { get; set; }

    [Description("Volunteer Role")]
    public string VolunteerRole { get; set; }

    [Description("Volunteer First Name")]
    public string VolunteerFirstName { get; set; }

    [Description("Volunteer Last Name")]
    public string VolunteerLastName { get; set; }

    [Description("Volunteer Street Address")]
    public string VolunteerStreetAddress { get; set; }

    [Description("Volunteer Address Unit")]
    public string VolunteerAddressUnit { get; set; }

    [Description("Volunteer City")]
    public string VolunteerCity { get; set; }

    [Description("Volunteer State")]
    public string VolunteerState { get; set; }

    [Description("Volunteer Postal Code")]
    public string VolunteerPostalCode { get; set; }

    [Description("Volunteer Email Address")]
    public string VolunteerEmailAddress { get; set; }

    [Description("Volunteer Telephone")]
    public string VolunteerTelephone { get; set; }

    [Description("Volunteer Cellphone")]
    public string VolunteerCellphone { get; set; }

    [Description("Volunteer Other Phone")]
    public string VolunteerOtherPhone { get; set; }
}