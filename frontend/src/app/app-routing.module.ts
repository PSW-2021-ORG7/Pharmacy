import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterMedicineComponent } from './register-medicine/register-medicine.component';
import { OrderHistoryComponent } from './orders/order-history/order-history.component';
import { OrderPharmacistComponent } from './orders/order-pharmacist/order-pharmacist.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';


const routes: Routes = [
  {path: 'home', component:HomeComponent }, 
  {path: 'register-medicine', component:RegisterMedicineComponent },
  {path: 'order-history', component:OrderHistoryComponent},
  {path: 'order-pharmacist', component:OrderPharmacistComponent},
  {path: 'shopping-cart', component:ShoppingCartComponent},
  {path: '', redirectTo: 'home', pathMatch: 'full' }, 
  {path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
