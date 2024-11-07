namespace ContactsApp.Database.Interface
{
    public abstract class RepositoryBase<T> where T : class
    {
        public IDatabase Database { get; }
        protected RepositoryBase(IDatabase database)
        {
            Database = database;
        }
        public virtual void Save()
        {
            Database.Save();
        }
    }
}
