namespace VMS1.Repository.IRepository
{
    public interface IUnitWork
    {
       
        public IEventRepository Events { get; }
        public IVolunteerRegistrationsRepository VolunteerRegistrations { get; }
        public IFeedbackRepository Feedback { get; }
        void Save();
    }
}
