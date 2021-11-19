import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-medicines-list',
  templateUrl: './medicines-list.component.html',
  styleUrls: ['./medicines-list.component.css']
})
export class MedicinesListComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  filterByDosage(dosage: number) {
  
  }

}
