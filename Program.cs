using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;

namespace finalproblem2
{
    class Program
    {
        public static Thrift_shopDataContext db = new Thrift_shopDataContext();
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine("choose from this menu  ");
                Console.WriteLine(" 0 - to end this program ");
                Console.WriteLine(" 1 - show all products with its brand ");
                Console.WriteLine(" 2 - to add new products to the system ");
                Console.WriteLine(" 3 - select products with price less than specific price ");
                Console.WriteLine(" 4 - Show brands ");
                Console.WriteLine(" 5 - Sort products ");
                Console.WriteLine(" 6- Add Some Data ");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose == 0)
                {
                    break;
                }else if (choose == 1)
                {
                    showallproducts(); // end 
                }else if (choose == 2)
                {
                     
                    addnewproducts(); //end 
                }else if (choose == 3)
                {
                    selectproductswithprice();//end

                }else if (choose == 4)
                {
                    Showbrands();
                }else if (choose == 5)
                {
                    Sortproducts();
                }else if(choose == 6)
                {
                    Add_someData();
                }


            }

           

        }
        public static void showallproducts() {
            var G = from t1 in db.Brands
                    join t2 in db.Products on t1.Id equals t2.Brand_id
                    select new { t2.ID, t2.name, t2.price, t2.Category , t1.BrandName, t2.Brand_id };
            Console.WriteLine("product Table with Brand table  ");
            Console.WriteLine("ID\tname\tprice\tCategory\tBrandName\tBrand_id ");
            foreach (var  s in G)
            {
                Console.WriteLine(s.ID+"\t"+s.name+"\t"+s.price+"\t"+s.Category+"\t"+s.BrandName+"\t"+s.Brand_id);
            }


        }
       
        public static void addnewproducts() {
            //the product name, price, category and brand name.
            //Make sure that the brand is existing in the Brand table
            Console.WriteLine("product nam");
            string productname= Console.ReadLine();
            Console.WriteLine("Id product");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("price");
            string str= Console.ReadLine();
            float price =  float.Parse(str);
            Console.WriteLine("category");
            string  category = Console.ReadLine();
            Console.WriteLine("brand name");
            string brand = Console.ReadLine();
            bool flag = db.Brands.Any(p => p.BrandName == brand );
            if (flag == true)
            {
                 int brandId=3;
               var h = from I in db.Brands where I.BrandName == brand select I;
                foreach (var s in h) { brandId = s.Id; }
                Product ord = new Product
                {
                    ID = id ,
                    price = price,
                    Category = category , 
                    Brand_id= brandId, 
                    name = productname 

                 };

                // Add the new object to the Orders collection.
                db.Products.InsertOnSubmit(ord);
                db.SubmitChanges();
                var results = from P in db.Brands where P.BrandName == brand
                               select P ;  

                foreach ( var Z in results)
                {
                    Z.numofproducts = Z.numofproducts + 1;
                }

                db.SubmitChanges();
                Console.WriteLine("end process secufully ");


            }
            else
            {
                Console.WriteLine("error this not exitst in databse");
            }
        }
        public static void selectproductswithprice() {
            Console.WriteLine(" enter pricess  ");
            string  pr =  Console.ReadLine();
            float p= float.Parse(pr);
            var G = from q in db.Products where q.price <= p 
                                   select new {q.ID , q.name , q.Category};
            int co = 0;
            foreach (var s in G)
            {
                co++;
            }
            if (co > 0)
            {
                Console.WriteLine("product Table with price less than or equal   " + p  );
                Console.WriteLine("ID\tname\tCategory");
                foreach (var s in G)
                {
                    Console.WriteLine(s.ID + "\t" + s.name + "\t" + s.Category );
                }
            }
            else
            {
                Console.WriteLine("no product price less than or equal " + p );
            }

        }
        public static void Showbrands() {
           var G = from r in db.Brands orderby r.numofproducts select r ;
            Console.WriteLine( "brand name  " + " ID "+ " " + "# of products " );
            foreach (var s in G)
            {
                Console.WriteLine(s.BrandName + " " + s.Id + " " + s.numofproducts);
            }
        }
        public static void Sortproducts() {
            //by name or sorted by price
            //Ascending or descending
            Console.WriteLine(" enter sord methed 6- name or 7-prices  ");
            int method = Convert.ToInt32(Console.ReadLine());
            if(method == 6)
            {
                Console.WriteLine(" enter sord 69-Ascending or 88-descending ");
                int e = Convert.ToInt32(Console.ReadLine());
                if (e == 88)
                {
                    var G = from P in db.Products
                            orderby P.name descending
                            select new { P.ID, P.name, P.price, P.Category, P.Brand_id };
                    Console.WriteLine("ID\tname\tprice\tCategory\tBrand_id ");
                    foreach (var s in G)
                    {
                        Console.WriteLine(s.ID + "\t" + s.name + "\t" + s.price + "\t" + s.Category + "\t" + s.Brand_id);
                    }
                }
                else if(e==69)
                {
                    var G = from P in db.Products
                            orderby P.name 
                            select new { P.ID, P.name, P.price, P.Category, P.Brand_id };
                    Console.WriteLine("ID\tname\tprice\tCategory\tBrand_id ");
                    foreach (var s in G)
                    {
                        Console.WriteLine(s.ID + "\t" + s.name + "\t" + s.price + "\t" + s.Category + "\t" + s.Brand_id);
                    }
                }
                else
                {
                    Console.WriteLine("error not found ");
                }
                //////////////
            }else if (method == 7)
            {
                Console.WriteLine(" enter sord 69-Ascending or 88-descending ");
                int e = Convert.ToInt32(Console.ReadLine());
                if (e == 88)
                {
                    var G = from P in db.Products
                            orderby P.price descending
                            select new { P.ID, P.name, P.price, P.Category, P.Brand_id };
                    Console.WriteLine("ID\tname\tprice\tCategory\tBrand_id ");
                    foreach (var s in G)
                    {
                        Console.WriteLine(s.ID + "\t" + s.name + "\t" + s.price + "\t" + s.Category + "\t" + s.Brand_id);
                    }
                }
                else
                {
                    var G = from P in db.Products
                            orderby P.price
                            select new { P.ID, P.name, P.price, P.Category, P.Brand_id };
                    Console.WriteLine("ID\tname\tprice\tCategory\tBrand_id ");
                    foreach (var s in G)
                    {
                        Console.WriteLine(s.ID + "\t" + s.name + "\t" + s.price + "\t" + s.Category + "\t" + s.Brand_id);
                    }
                }
            }
        }
        public static void Add_someData()
        {
            ///////////////////////////////////////////
            Console.WriteLine("brand name");
            string brand = Console.ReadLine();
            Console.WriteLine("brand Id ");
            int brandId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("product nam");
            string productname = Console.ReadLine();
            Console.WriteLine("Id product");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("price");
            string str = Console.ReadLine();
            float price = float.Parse(str);
            Console.WriteLine("category");
            string category = Console.ReadLine();
            bool flag = db.Brands.Any(p => p.Id == brandId);
            bool flag2 = db.Products.Any(p => p.ID == id);
            if (flag == true )
            {
                Console.WriteLine("you are enter data already in database  , change brand ID ");
                return; 
            }
            if(flag2 == true)
            {
                Console.WriteLine("you are enter data already in database  , change product ID  ");
                return;
            }
            Brand Br = new Brand
            {
                Id = brandId,
                BrandName = brand,
                numofproducts = 1

           };
            Product ord = new Product
            {
                ID = id,
                price = price,
                Category = category,
                Brand_id = brandId,
                name = productname

            };
            db.Brands.InsertOnSubmit(Br);
            db.Products.InsertOnSubmit(ord);
            db.SubmitChanges();
            //////////////////////////////////////////
            Console.WriteLine("process is end succefuly ");
           

        }

        }
}
