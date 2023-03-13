using System.ComponentModel;

namespace TeamGenerator;

public class Participant
{ 
    [Description("Teammate Request")]
    public string TeammateRequest { get; set; }

    [Description("Coach Request")]
    public string CoachRequest { get; set; }
    
    [Description("Players Recent Team")]
    public string PlayersRecentTeam { get; set; }
    
    [Description("Player Last Name")]
    public string PlayerLastName { get; set; }

    [Description("Little League School Name")]
    public string LittleLeagueSchoolName { get; set; }

    [Description("Postal Code")]
    public string PostalCode { get; set; }

    [Description("Player First Name")]
    public string PlayerFirstName { get; set; }
    
    [Description("Open Balance")]
    public string OpenBalance { get; set; }
    
    [Description("User First Name")]
    public string UserFirstName { get; set; }

    [Description("User Last Name")]
    public string UserLastName { get; set; }

    [Description("Gender")]
    public string Gender { get; set; }

    [Description("Division Name")]
    public string DivisionName { get; set; }

    [Description("Date of Report")]
    public string DateofReport { get; set; }

    [Description("Program Name")]
    public string ProgramName { get; set; }
    
    [Description("Street")]
    public string Street { get; set; }

    [Description("City")]
    public string City { get; set; }

    [Description("Region")]
    public string Region { get; set; }

    [Description("Phone")]
    public string Phone { get; set; }

    [Description("Email")]
    public string Email { get; set; }

    [Description("CellPhone")]
    public string CellPhone { get; set; }

    [Description("Relationship to Participant")]
    public string RelationshiptoParticipant { get; set; }

    [Description("Order Date")]
    public string OrderDate { get; set; }

    [Description("Order ID")]
    public string OrderID { get; set; }

    [Description("Payment Status")]
    public string PaymentStatus { get; set; }
    
    [Description("Player Email")]
    public string PlayerEmail { get; set; }

    [Description("Date Of Birth")]
    public string DateOfBirth { get; set; }

    [Description("Birth Certificate")]
    public string BirthCertificate { get; set; }

    [Description("Does your child play in any other youth baseball or softball programs (e.g. leagues, travel ball, tournaments, etc.)?")]
    public string Doesyourchildplayinanyotheryouthbaseballorsoftballprograms { get; set; }

    [Description("Did your child play with Spokane South Little League before?")]
    public string DidyourchildplaywithSpokaneSouthLittleLeaguebefore { get; set; }

    [Description("Proof of Residency 1")]
    public string ProofofResidency1 { get; set; }

    [Description("Proof of Residency 2")]
    public string ProofofResidency2 { get; set; }

    [Description("Proof of Residency 3")]
    public string ProofofResidency3 { get; set; }

    [Description("School Enrollment")]
    public string SchoolEnrollment { get; set; }

    [Description("Choose your method of address verification")]
    public string Chooseyourmethodofaddressverification { get; set; }

    [Description("Little League Privacy Policy")]
    public string LittleLeaguePrivacyPolicy { get; set; }

    [Description("Emergency Contact Name")]
    public string EmergencyContactName { get; set; }

    [Description("Emergency Contact Phone")]
    public string EmergencyContactPhone { get; set; }

    [Description("Jersey Size")]
    public string JerseySize { get; set; }

    [Description("Pants Size")]
    public string PantsSize { get; set; }
}
