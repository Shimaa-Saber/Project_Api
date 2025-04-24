using Project_Api.Interfaces;
using ProjectApi.Models;

namespace Project_Api.Reposatories
{
    public class AudioRepo:AudioSession
    {
        ApplicationDbContext Context;
        public AudioRepo(ApplicationDbContext context)
        {
            Context = context;
        }
    
      

        public void Delete(AudioSessionDetail obj)
        {
            Context.AudioSessionDetails.Remove(obj);
        }

        public List<AudioSessionDetail> GetAll()
        {
            return Context.AudioSessionDetails.ToList();
        }

        

        public AudioSessionDetail GetById(int id)
        {
            return Context.AudioSessionDetails.Where(d => d.Id == id).FirstOrDefault();
        }

      
        

        public void insert(AudioSessionDetail obj)
        {
            Context.AudioSessionDetails.Add(obj);
        }

        public void Update(AudioSessionDetail obj)
        {
            Context.AudioSessionDetails.Update(obj);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
    
    

