import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderPharmacistComponent } from './order-pharmacist.component';

describe('OrderPharmacistComponent', () => {
  let component: OrderPharmacistComponent;
  let fixture: ComponentFixture<OrderPharmacistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderPharmacistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderPharmacistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
