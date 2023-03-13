using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using rentACarProject.Data;
using rentACarProject.Models;
using rentACarProject.Models.Domain;

namespace rentACarProject.Controllers
{


    public class CustomersController : Controller
    {
        private readonly Database database;

        public CustomersController(Database database)
        {
            this.database = database;
        }

        
        public async Task <IActionResult> Index()
        {
            var customers=await database.Müşteriler.ToListAsync(); // listelemek için
            return View(customers);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerViewModel addCustomerRequest)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = addCustomerRequest.FirstName,
                LastName = addCustomerRequest.LastName,
                Adress = addCustomerRequest.Adress,
                contactNumber = addCustomerRequest.contactNumber
            };

            await database.Müşteriler.AddAsync(customer);//müşterinin eklenmesi
            await database.SaveChangesAsync();
            return RedirectToAction("Index"); // müşteri eklendikten sonra Index sayfasına yönlendirsin.
            
        } //müşteri ekleme isteği

        [HttpGet]
        public async Task<IActionResult> View(Guid id) //id'ye bakarak veritabanındaki müşteri olduğunu doğrulamaya çalışıyoruz.
        {
            var customer =await database.Müşteriler.FirstOrDefaultAsync(x => x.Id == id);
            // tek bir müşteriyi id'den kontrol ediyoruz.

            if (customer != null) 
            { //eğer kontrol edilebilecek bir müşteri varsa işlemi gerçekleştir.
                  var viewModel = new UpdateCustomerViewModel() // artık buradaki bilgileri kontrol edip görüntüleyeceğiz.
                  {
                       Id = customer.Id,
                       FirstName = customer.FirstName,
                       LastName = customer.LastName,
                       Adress = customer.Adress,
                       contactNumber = customer.contactNumber
                  }; // bu verileri kullanabilmek için async method kullanıyoruz


                return await Task.Run(() => View("View",viewModel)); // müşterilerde görüntüleyip , müşterinin olup olmadığını kontrol edeceğiz
            }

            return RedirectToAction("Index"); // Eğer müşteri yoksa indexe geri yönlendirecek
        }

        [HttpPost] // güncellenen bilgiyi post edeceğiz ve müşterilerin olduğu ekrana geri döneceğiz.
        public async Task <IActionResult> View(UpdateCustomerViewModel model) //formdaki bilgileri günceller.
        {
            var customer = await database.Müşteriler.FindAsync(model.Id);  //get the customer from database

            if(customer != null)
            {
                customer.FirstName = model.FirstName; // güncelennecek olan yerden(model) gelecek bilgi atanır.
                customer.LastName = model.LastName;
                customer.Adress = model.Adress;
                customer.contactNumber = model.contactNumber;

                await database.SaveChangesAsync(); // değişikliklerin kaydedilmesi , async metod olduğunda await eklendi.

                return RedirectToAction("Index"); // kayıttan sonra direkt müşteriler gözüksün
            };
            return RedirectToAction("Index"); // metod çalışmazsa direkt Index gözükür
        }

        [HttpPost]
        public async Task<IActionResult>Delete(UpdateCustomerViewModel model)
        {
            var customer = await database.Müşteriler.FindAsync(model.Id);
            if (customer != null)
            {
                database.Müşteriler.Remove(customer); // müşteriyi sil
                await database.SaveChangesAsync();//değişiklikleri kaydediyoruz.

                return RedirectToAction("Index"); //sildikten sonra müşterilerin olduğu sayfaya dönecek
            }

            return RedirectToAction("Index"); // eğer işlem yapılmazsa direkt Indexe geri dönecek
        }
    }
}
