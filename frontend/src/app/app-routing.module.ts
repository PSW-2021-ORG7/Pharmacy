import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterMedicineComponent } from './register-medicine/register-medicine.component';

const routes: Routes = [
  {path: 'home', component:HomeComponent }, 
  {path: 'register-medicine', component:RegisterMedicineComponent },  
  {path: '', redirectTo: 'home', pathMatch: 'full' }, 
  {path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
