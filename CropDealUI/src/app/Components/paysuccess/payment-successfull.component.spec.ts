import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentSuccessfullComponent } from './payment-successfull.component';

describe('PaymentSuccessfullComponent', () => {
  let component: PaymentSuccessfullComponent;
  let fixture: ComponentFixture<PaymentSuccessfullComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentSuccessfullComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentSuccessfullComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
