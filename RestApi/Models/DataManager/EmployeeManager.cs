using System.Collections.Generic;
using System.Linq;
using CodeFirstRestApi.Models.Repository;

namespace CodeFirstRestApi.Models.DataManager
{
    public class EmployeeManager  :IDataRepository<Employee>
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeManager(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employeeDbContext.Employees.ToList();
        }

        public Employee Get(long id)
        {
            return _employeeDbContext.Employees.FirstOrDefault(em => em.Id == id);
        }

        public void Add(Employee entity)
        {
            _employeeDbContext.Employees.Add(entity);
            _employeeDbContext.SaveChanges();
        }

        public void Update(Employee dbEntity, Employee entity)
        {
            dbEntity.FirstName = entity.FirstName;
            dbEntity.LastName = entity.LastName;
            dbEntity.Email = entity.Email;
            dbEntity.DateOfBirth = entity.DateOfBirth;
            dbEntity.PhoneNumber = entity.PhoneNumber;

            _employeeDbContext.SaveChanges();
        }

        public void Delete(Employee entity)
        {
            _employeeDbContext.Employees.Remove(entity);
            _employeeDbContext.SaveChanges();
        }
    }
}