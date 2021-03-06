namespace R7.University.Models
{
    public interface IContingent
    {
        int ContingentId { get; }

        decimal? AvgAdmScore { get; }

        int? AdmittedFB { get; }

        int? AdmittedRB { get; }

        int? AdmittedMB { get; }

        int? AdmittedBC { get; }

        int? ActualFB { get; }

        int? ActualRB { get; }

        int? ActualMB { get; }

        int? ActualBC { get; }

        int? ActualForeignFB { get; }

        int? ActualForeignRB { get; }

        int? ActualForeignMB { get; }

        int? ActualForeignBC { get; }

        int? VacantFB { get; }

        int? VacantRB { get; }

        int? VacantMB { get; }

        int? VacantBC { get; }

        int? MovedIn { get; }

        int? MovedOut { get; }

        int? Restored { get; }

        int? Expelled { get; }

        IEduProgramProfileFormYear EduProgramProfileFormYear { get; }
    }

    public interface IContingentWritable: IContingent
    {
        new int ContingentId { get; set; }

        new decimal? AvgAdmScore { get; set; }

        new int? AdmittedFB { get; set; }

        new int? AdmittedRB { get; set; }

        new int? AdmittedMB { get; set; }

        new int? AdmittedBC { get; set; }

        new int? ActualFB { get; set; }

        new int? ActualRB { get; set; }

        new int? ActualMB { get; set; }

        new int? ActualBC { get; set; }

        new int? ActualForeignFB { get; set; }

        new int? ActualForeignRB { get; set; }

        new int? ActualForeignMB { get; set; }

        new int? ActualForeignBC { get; set; }

        new int? VacantFB { get; set;}

        new int? VacantRB { get; set; }

        new int? VacantMB { get; set; }

        new int? VacantBC { get; set; }

        new int? MovedIn { get; set; }

        new int? MovedOut { get; set; }

        new int? Restored { get; set; }

        new int? Expelled { get; set; }

        new IEduProgramProfileFormYear EduProgramProfileFormYear { get; set; }
    }

    public class ContingentInfo: IContingentWritable
    {
        public int ContingentId { get; set; }

        public decimal? AvgAdmScore { get; set; }

        public int? AdmittedFB { get; set; }

        public int? AdmittedRB { get; set; }

        public int? AdmittedMB { get; set; }

        public int? AdmittedBC { get; set; }

        public int? ActualFB { get; set; }

        public int? ActualRB { get; set; }

        public int? ActualMB { get; set; }

        public int? ActualBC { get; set; }

        public int? ActualForeignFB { get; set; }

        public int? ActualForeignRB { get; set; }

        public int? ActualForeignMB { get; set; }

        public int? ActualForeignBC { get; set; }

        public int? VacantFB { get; set;}

        public int? VacantRB { get; set; }

        public int? VacantMB { get; set; }

        public int? VacantBC { get; set; }

        public int? MovedIn { get; set; }

        public int? MovedOut { get; set; }

        public int? Restored { get; set; }

        public int? Expelled { get; set;}

        public virtual EduProgramProfileFormYearInfo EduProgramProfileFormYear { get; set; }

        IEduProgramProfileFormYear IContingent.EduProgramProfileFormYear => EduProgramProfileFormYear;

        IEduProgramProfileFormYear IContingentWritable.EduProgramProfileFormYear {
            get { return EduProgramProfileFormYear; }
            set { EduProgramProfileFormYear = (EduProgramProfileFormYearInfo) value; }
        }
    }
}
