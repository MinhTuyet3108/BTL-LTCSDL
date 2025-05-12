using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using DataLayer;

namespace BusinessLayer
{
    public class PetBL
    {
        private PetDL petDL;
        private Pet pet;

        public PetBL()
        {
            petDL = new PetDL();
        }
        public List<Pet> GetPets()
        {
            try
            {
                return petDL.GetPets();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public void AddPet(Pet pet)
        {
            try
            {
                petDL.Add(pet);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }
        public bool DeletePet(int id)
        {
            try
            {
                return petDL.Delete(id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public bool UpdatePet(Pet pet)
        {
            try
            {
                return petDL.Update(pet);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public List<Pet> SearchPets(string keyword)
        {
            try
            {
                return petDL.Search(keyword);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
