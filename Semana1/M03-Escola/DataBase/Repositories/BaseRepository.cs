namespace M03_Escola.DataBase.Repositories

{
    public class BaseRepository<TEntity, TKey>
        where TEntity : class // utilizamos essa sintaxe para limitar o tipo generico a apenas objetos classes 
    {

        protected readonly EscolaDBContexto _context;
        public BaseRepository(EscolaDBContexto contexto)
        {
            _context = contexto;
        }

        public virtual TEntity Atualizar(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual void Excluir(TEntity entity)
        {

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual TEntity Inserir(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual TEntity ObterPorId(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> ObterTodos()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}
