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
        private IShoppingCardsRepository shoppingCartsRepository;

        public ShoppingCartService(IShoppingCardsRepository shoppingCardsRepository)
        {
            this.shoppingCartsRepository = shoppingCartsRepository;
        }

        public bool UpdateItemQuantityInCart(UpdateShoppingCartsItemQuantityDTO updateShoppingCartsItemQuantityDTO)
        {
            bool successfullyUpdated = false;
            List<ShoppingCart> ShoppingCarts = shoppingCartsRepository.GetAll();
            if(updateShoppingCartsItemQuantityDTO.newQuantity > 0)
            {
                for (int i = 0; i < ShoppingCarts.Count; i++)
                {
                    if (ShoppingCarts[i].ShoppingCart_Id == updateShoppingCartsItemQuantityDTO.ShoppingCarts_Id)
                    {
                        for (int j = 0; j < ShoppingCarts[i].ShoppingCartItem.Count; j++)
                        {
                            if (ShoppingCarts[i].ShoppingCartItem[j].OrderItemId == updateShoppingCartsItemQuantityDTO.ShoppingCartsItem_Id
                                && ShoppingCarts[i].ShoppingCartItem[j].Quantity != updateShoppingCartsItemQuantityDTO.newQuantity)
                            {
                                ShoppingCarts[i].ShoppingCartItem[j].Quantity = updateShoppingCartsItemQuantityDTO.newQuantity;
                                shoppingCartsRepository.Update(ShoppingCarts[i]);
                                successfullyUpdated = true;
                                break;
                            }
                        }
                        break;
                    }
                }
            } else if(updateShoppingCartsItemQuantityDTO.newQuantity == 0)
            {
                for (int i = 0; i < ShoppingCarts.Count; i++)
                {
                    if (ShoppingCarts[i].ShoppingCart_Id == updateShoppingCartsItemQuantityDTO.ShoppingCarts_Id)
                    {
                        for (int j = 0; j < ShoppingCarts[i].ShoppingCartItem.Count; j++)
                        {
                            if (ShoppingCarts[i].ShoppingCartItem[j].OrderItemId == updateShoppingCartsItemQuantityDTO.ShoppingCartsItem_Id)
                            {
                                ShoppingCarts[i].ShoppingCartItem.RemoveAt(j);
                                shoppingCartsRepository.Update(ShoppingCarts[i]);
                                successfullyUpdated = true;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            
            return successfullyUpdated;
        }
    }
}
