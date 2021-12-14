using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTO;
using backend.Model;

namespace backend.Services
{
    public class ShoppingCartService
    {
        private IShoppingCartsRepository shoppingCartsRepository;
        private IUserRepository userRepository;
        private IMedicineRepository medicineRepository;
        private IOrdersRepository ordersRepository;

        public ShoppingCartService(IShoppingCartsRepository scr, IUserRepository ur, IMedicineRepository mr, IOrdersRepository or)
        {
            this.shoppingCartsRepository = scr;
            this.userRepository = ur;
            this.medicineRepository = mr;
            this.ordersRepository = or;
        }


        public bool UpdateItemQuantityInCart(UpdateShoppingCartsItemQuantityDTO updateShoppingCartsItemQuantityDTO)
        {
            bool successfullyUpdated = false;
            ShoppingCart sc = shoppingCartsRepository.GetByID(updateShoppingCartsItemQuantityDTO.ShoppingCarts_Id);
            if(updateShoppingCartsItemQuantityDTO.newQuantity > 0)
            {
                for(int i=0; i<sc.ShoppingCartItem.Count; i++)
                {
                    if(sc.ShoppingCartItem[i].OrderItemId == updateShoppingCartsItemQuantityDTO.ShoppingCartsItem_Id
                        && sc.ShoppingCartItem[i].Quantity != updateShoppingCartsItemQuantityDTO.newQuantity)
                    {
                        sc.ShoppingCartItem[i].Quantity = updateShoppingCartsItemQuantityDTO.newQuantity;
                        shoppingCartsRepository.Update(sc);
                        successfullyUpdated = true;
                        break;
                    }
                }
                
                 
                    
                }
             else if(updateShoppingCartsItemQuantityDTO.newQuantity == 0)
            {
                
                    
                        for (int j = 0; j < sc.ShoppingCartItem.Count; j++)
                        {
                            if (sc.ShoppingCartItem[j].OrderItemId == updateShoppingCartsItemQuantityDTO.ShoppingCartsItem_Id)
                            {
                                sc.ShoppingCartItem.RemoveAt(j);
                                shoppingCartsRepository.Update(sc);
                                successfullyUpdated = true;
                                break;
                            }
                        }
                    
                
            }
            
            return successfullyUpdated;
        }

        public ShoppingCart MakeAnOrder(MakeAnOrderDTO makeAnOrderDTO)
        {
            ShoppingCart sc = shoppingCartsRepository.GetByID(Int32.Parse(makeAnOrderDTO.shoppingCartId));
            makeAnOrderFromShoppingCart(sc, makeAnOrderDTO.delivery);
            sc = emptyShoppingCart(sc);
            return sc;
;
        }

        private ShoppingCart emptyShoppingCart(ShoppingCart sc)
        {
            sc.ShoppingCartItem = new List<OrderItem>();
            shoppingCartsRepository.Update(sc);
            return sc;
        }

        private void makeAnOrderFromShoppingCart(ShoppingCart sc, string delivery)
        {
            Order newOrder = new Order();
            if (delivery == "delivery")
                newOrder.deliveryReqired = true;
            else
                newOrder.deliveryReqired = false;
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderItems = sc.ShoppingCartItem;
            newOrder.Status = 0;
            newOrder.User = sc.User;
            ordersRepository.Save(newOrder);
          
            
        }

        public ShoppingCart GetById(int id)
        {
            return shoppingCartsRepository.GetByID(id);
        }

        public ShoppingCart GetByUser(Guid userId)
        {
            ShoppingCart sc = shoppingCartsRepository.GetByUserID(userId);
            if (sc == null)
            {
                
                sc = CreateNewShoppingCart(userId);
            }
            else
            {
                sc = UpdateShoppingCart(sc);
            }

                return sc;
        }

        private ShoppingCart UpdateShoppingCart(ShoppingCart sc)
        {
            Medicine m1 = new Medicine();
            m1.Description = "Warning";
            m1.Name = "Panadol";
            m1.Manufacturer = "Galenika";
            m1.WayOfConsumption = "Once per day";
            m1.Ingredients = new List<Ingredient>();
            m1.PossibleReactions = new List<string>();
            m1.PotentialDangers = "";
            m1.DosageInMilligrams =50;
            m1.SideEffects = new List<string>();
            medicineRepository.Save(m1);

            Medicine m2 = new Medicine();
            m2.Description = "Warning";
            m2.Name = "Neoangin";
            m2.Manufacturer = "ABC";
            m2.WayOfConsumption = "Twice per day";
            m2.Ingredients = new List<Ingredient>();
            m2.PossibleReactions = new List<string>();
            m2.PotentialDangers = "";
            m2.DosageInMilligrams = 50;
            m2.SideEffects = new List<string>();
            medicineRepository.Save(m2);

            OrderItem o1 = new OrderItem();
            o1.Medicine = m1;
            o1.Quantity = 4;
            o1.PriceForSingleEntity = 450;
            sc.ShoppingCartItem.Add(o1);

            OrderItem o2 = new OrderItem();
            o2.Medicine = m2;
            o2.Quantity = 4;
            o2.PriceForSingleEntity = 550;
            sc.ShoppingCartItem.Add(o2);

            shoppingCartsRepository.Update(sc);
            return sc;
        }


            public ShoppingCart CreateNewShoppingCart(Guid userId)
        {
            User u = userRepository.GetById(userId.ToString());

            ShoppingCart shoppingCart = new ShoppingCart(u, new List<OrderItem>());

            Medicine m1 = new Medicine();
            m1.Description = "Warning";
            m1.Name = "Panadol";
            m1.Manufacturer = "Galenika";
            m1.WayOfConsumption = "Once per day";
            m1.Ingredients = new List<Ingredient>();
            m1.PossibleReactions = new List<string>();
            m1.PotentialDangers = "";
            m1.DosageInMilligrams = 100;
            m1.SideEffects = new List<string>();
            medicineRepository.Save(m1);

            Medicine m2 = new Medicine();
            m2.Description = "Warning";
            m2.Name = "Probiotic";
            m2.Manufacturer = "ABC";
            m2.WayOfConsumption = "Twice per day";
            m2.Ingredients = new List<Ingredient>();
            m2.PossibleReactions = new List<string>();
            m2.PotentialDangers = "";
            m2.DosageInMilligrams = 500;
            m2.SideEffects = new List<string>();
            medicineRepository.Save(m2);

            OrderItem o1 = new OrderItem();
            o1.Medicine = m1;
            o1.Quantity = 4;
            o1.PriceForSingleEntity = 450;
            shoppingCart.ShoppingCartItem.Add(o1);

            OrderItem o2 = new OrderItem();
            o2.Medicine = m2;
            o2.Quantity = 4;
            o2.PriceForSingleEntity = 550;
            shoppingCart.ShoppingCartItem.Add(o2);

            shoppingCartsRepository.Save(shoppingCart);

            return shoppingCart;
        }

    }
}
