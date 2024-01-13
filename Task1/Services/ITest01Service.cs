using Task1.Models;

namespace Task1.Services;

public interface ITest01Service
{
    void Add(Test01 model);
    void Update(int id, Test01 model);
    void Remove(int id);
    Test01? GetById(int id);

    PagedList<Test01> Get(Test01Parameters test01Parameters);
}