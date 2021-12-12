import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegisterMedicineComponent } from './register-medicine/register-medicine.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { OrderHistoryComponent } from './orders/order-history/order-history.component';
import { OrderPharmacistComponent } from './orders/order-pharmacist/order-pharmacist.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterMedicineComponent,
    OrderHistoryComponent,
    OrderPharmacistComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
