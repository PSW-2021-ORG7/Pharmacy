export class Item{
    item_Id : string;
    medicineName : string;
    quantity : string;
    price: string;
    priceAll : string;

    constructor(id : string, medicineName: string, quantity: string, price: string, priceAll: string){
        this.item_Id = id;
        this.medicineName= medicineName;
        this.quantity = quantity;
        this.price = price;
        this.priceAll = priceAll;
    }
}