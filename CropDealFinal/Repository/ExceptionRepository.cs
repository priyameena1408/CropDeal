using CaseStudy.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CaseStudy.Repository
{
    public class ExceptionRepository
    {
        DatabaseContext _context;
        public ExceptionRepository(DatabaseContext context)
        {
            _context = context;
        }
        #region Add Exception
        /// <summary>
        /// Function to add Exception in database
        /// </summary>
        /// <param name="e"></param>
        /// <param name="ErrorAt"></param>
        /// <returns></returns>
        public async Task AddException(Exception e, string ErrorAt)
        {
            try
            {
                ExceptionError error = new ExceptionError();
                error.ExceptionAt = ErrorAt;
                error.ExceptionMessage = e.Message;
                error.ExceptionDateTime = DateTime.Now.ToString();
                error.ExceptionType = e.GetType().Name;
                _context.ExceptionErrors.Add(error);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion

    }
}
