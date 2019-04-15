using System;
using Dow.SSD.Framework.Model;

namespace Dow.SSD.Framework.Repository
{
    public class UnitOfWork : IDisposable
    {
        private SSDFrameworkSampleDBEntities _context = null;

        public UnitOfWork()
        {
            _context = new SSDFrameworkSampleDBEntities();
        }


        tblSampleRepository tblSamplerepository = null;
        public tblSampleRepository tblSampleRepository
        {
            get
            {
                if (tblSamplerepository == null)
                {
                    tblSamplerepository = new tblSampleRepository(_context);
                }

                return tblSamplerepository;
            }
        }


        // Dispose the DbContext in UOW. Without UOW, We have to dispose the DbContext in each repository class
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
