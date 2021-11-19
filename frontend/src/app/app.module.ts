import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegisterMedicineComponent } from './register-medicine/register-medicine.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MedicinesListComponent } from './medicines-list/medicines-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterMedicineComponent,
    MedicinesListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
