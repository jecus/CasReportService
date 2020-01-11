namespace CASReports.Models
{
	public class AtaChapter : BaseModel
	{
		public string FullName { get; set; }
		public string ShortName { get; set; }

		#region  public override string ToString()

		/// <summary>
		/// Возвращает комбинацию полей ShortName+" "+ FullName;
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ShortName + " " + FullName;
		}

		#endregion
    }
}