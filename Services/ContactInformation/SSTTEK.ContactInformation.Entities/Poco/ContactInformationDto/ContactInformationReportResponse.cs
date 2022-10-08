using EntityBase.Concrete;

namespace SSTTEK.ContactInformation.Entities.Poco.ContactInformationDto
{
    public class ContactInformationReportResponse : GetModel
    {
        public string Location { get; set; }

        public int RegisteredContact { get; set; }

        public int RegisteredContactInformationPhoneNumber { get; set; }
    }
}
