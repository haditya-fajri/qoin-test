using Task1.Models;

namespace Task1.Services;

public class Test01Service : ITest01Service
{
    private readonly QoinContext _context;

    public Test01Service(QoinContext context)
    {
        _context = context;
    }

    public void Add(Test01 model)
    {
        model.Created = DateTime.Now;
        _context.Add(model);
        _context.SaveChanges();
    }

    public void Update(int id, Test01 model)
    {
        var test01 = _context.Test01s?.Find(id);
        if (test01 == null) return;

        test01.Name = model.Name;
        test01.Status = model.Status;
        test01.Updated = DateTime.Now;

        _context.SaveChanges();
    }

    public void Remove(int id)
    {
        var test01 = _context.Test01s?.Find(id);
        if (test01 == null) return;

        _context.Remove(test01);
        _context.SaveChanges();
    }

    public Test01? GetById(int id)
    {
        return _context.Test01s?.Find(id);
    }

    public PagedList<Test01> Get(Test01Parameters test01Parameters)
    {
        return PagedList<Test01>.ToPagedList(_context.Test01s,
            test01Parameters.PageNumber,
            test01Parameters.PageSize);
    }
}