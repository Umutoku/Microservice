namespace FreeEducation.Shared.Messages
{
    public class EducationNameChangedEvent
    {
        public string EducationId { get; set; }
        public string UpdatedName { get; set; }
    }
}